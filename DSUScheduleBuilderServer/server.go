package main

import (
	"database/sql"
	"errors"
	"fmt"
	"net/http"

	"github.com/gin-contrib/static"
	"github.com/gin-gonic/gin"
	//_ "github.com/mattn/go-sqlite3"
	"golang.org/x/crypto/bcrypt"
)

//make the database global, db = pointer to a database
var db *sql.DB
var errorChannel chan locationalError

//holds the information for a single course being/has been offered
type course struct {
	userID, startTime, endTime, credits                       int
	classID, className, teacher, location, startDate, endDate string
}

//holds the information for a single user
type user struct {
	uid                   int
	name, password, major string
}
type locationalError struct {
	Error                 error
	Location, Sublocation string
}

//check if there was an error
func checkErr(err error) {
	if err != nil {
		println(err)
	}
}

func checkLogError(location, sublocation string, err error) {
	if err != nil {
		logError(location, sublocation, err)
	}
}

func logError(location, sublocation string, err error) {
	errorChannel <- locationalError{err, location, sublocation}
}

func errorDrain() {
	var lErr locationalError
	for {
		select {
		case lErr = <-errorChannel:
			fmt.Println(lErr.Location, lErr.Sublocation, lErr.Error)
			//Handle Error Logging Here
		}
	}
}

func hashPassword(password string) (string, error) {
	bytes, err := bcrypt.GenerateFromPassword([]byte(password), 14)
	return string(bytes), err
}

func checkPasswordHash(password, hash string) bool {
	err := bcrypt.CompareHashAndPassword([]byte(hash), []byte(password))
	return err == nil
}

func doesUserExistWithField(field, value interface{}) bool {
	err := db.QueryRow(fmt.Sprintf("SELECT %s FROM user WHERE %s=$1", field, field), value).Scan(&value)
	return err == nil
}

func getUserID(name string) int {
	var uid int
	err := db.QueryRow("SELECT id FROM user WHERE name=$1", name).Scan(&uid)
	checkErr(err)
	return uid
}

//user database functions
//create new user given name, password, string, by inputing into database
func newUser(User user) (uint16, error) {
	funcName := "newUser"
	//the user does not currently exist in the database with the same name
	if doesUserExistWithField("name", User.name) {
		//hash the password to store it
		maxAttempts, numAttempts := 10, 0
		var err error
		for err = nil; err != nil && numAttempts <= maxAttempts; {
			User.password, err = hashPassword(User.password)
			numAttempts++
		}
		if err == nil {
			_, err = db.Exec("INSERT INTO user (name, password, major) values($1,$2,$3)", User.name, User.password, User.major)
			if err != nil {
				go logError(funcName, "1", err)
				return 500, errors.New("Error inserting new user into database")
			}
			return 200, nil
		}
		go logError(funcName, "2", err)
		return 500, err
	}
	return 200, nil
}

//delete a user given a user struct from the database
func deleteUser(User user) (uint16, error) {
	var location = "deleteUser"
	var err error
	if doesUserExistWithField("id", User.uid) {
		//delete the user from the user table
		_, err = db.Exec("DELETE FROM user WHERE id=$1", User.uid)
		checkLogError(location, "Delete user information from user table", err)
		if err == nil {
			_, err = db.Exec("DELETE FROM PreviousClasses WHERE userID=$1", User.uid)
			checkLogError(location, "Delete user information from PreviousClasses", err)
			if err == nil {
				_, err = db.Exec("DELETE FROM EnrolledClasses WHERE userID=$1", User.uid)
				checkLogError(location, "Delete user information from EnrolledClasses", err)
				if err == nil {
					return 200, err
				}
			}
		}
	} else {
		var uid int
		err := db.QueryRow("SELECT id FROM user WHERE name=$1", User.name).Scan(&uid)
		checkLogError(location, "Check if user exists before deleting", err)
		return 500, err
	}
	return 500, err
}

//update information in the user table for a user KEYWORD = the column you want to change and NEWVALUE = the value to change to
func updateUser(User user, keyword, newValue string) (uint16, error) {
	var location = "updateUser"
	var err error
	if newValue != "password" {
		_, err = db.Exec("UPDATE user SET $1=$2 WHERE id=$3", keyword, newValue, User.uid)
		checkLogError(location, "Update user information that is not password", err)
	} else {
		newValue, _ := hashPassword(newValue)
		_, err := db.Exec("UPDATE user SET $1=$2 WHERE id=$3", keyword, newValue, User.uid)
		checkLogError(location, "Update user password", err)
	}
	if err == nil {
		return 200, err
	}
	return 500, err
}

//given the name of a user return a structure with the user information
func getUser(name string) (user, uint16, error) {
	var location = "getUser"
	var User user
	err := db.QueryRow("SELECT FROM user WHERE name=$1", name).Scan(&User.uid, &User.name, &User.password, &User.major)
	checkLogError(location, "Selecting the user by name", err)
	if err == nil {
		return User, 200, err
	}
	return User, 500, err
}

//enrolled class database functions
func addEnrolledClass(class course) (uint16, error) {
	//make sure this class does not exist for the user with this id already, else skip
	var location = "addEnrolledClass"
	var tmp int
	var err error
	tmp = -1
	err = db.QueryRow("SELECT userID FROM EnrolledClasses WHERE userID=$1 AND classID=$2", class.userID, class.classID).Scan(&tmp)
	checkLogError(location, "Selecting from database to see if it already exists", err)
	if tmp == -1 && err == nil {
		_, err = db.Exec("INSERT INTO EnrolledClasses (userID, classID, className, teacher, location, startTime, endTime, startDate, endDate, credits) values($1,$2,$3,$4,$5,$6,$7,$8,$9,$10)", class.userID, class.classID, class.className, class.teacher, class.location, class.startTime, class.endTime, class.startDate, class.endDate, class.credits)
		checkLogError(location, "Insert the enrolled class in the database", err)
		if err == nil {
			return 200, err
		}
	} else if tmp != -1 {
		return 501, err
	}
	return 500, err
}

func deleteEnrolledClass(class course) (uint16, error) {
	var location = "deleteEnrolledClass"
	_, err := db.Exec("DELETE FROM EnrolledClasses WHERE userID=$1 AND classID=$2", class.userID, class.classID)
	checkLogError(location, "Delete enrolled class from database", err)
	if err == nil {
		return 200, err
	}
	return 500, err
}

//updates an entry in the enrolledclasses table, although we cannot allow to change the userID
func updateEnrolledClass(class course, keyword, newValue string) (uint16, error) {
	var location = "updateEnrolledClass"
	var err error
	if keyword != "classID" {
		_, err = db.Exec("UPDATE EnrolledClasses SET $1=$2 WHERE userID=$3 AND classID=$4", keyword, newValue, class.userID, class.classID)
		checkLogError(location, "Updating in EnrolledClass something that is not classID", err)
	} else {
		_, err = db.Exec("UPDATE EnrolledClasses SET $1=$2 WHERE userID=$3 AND className=$4", keyword, newValue, class.userID, class.className)
		checkLogError(location, "Updating in EnrolledClass the classID", err)
	}
	if err == nil {
		return 200, err
	}
	return 500, err
}

func getEnrolledClass(uid int, classID string) (course, uint16, error) {
	var class course
	var location = "getEnrolledClass"
	err := db.QueryRow("SELECT FROM EnrolledClasses WHERE userID=$1 AND classID=$2", uid, classID).Scan(&class.userID, &class.classID, &class.className, &class.teacher, &class.location, &class.startTime, &class.endTime, &class.startDate, &class.endDate, &class.credits)
	checkLogError(location, "Selecting from the enrolledClasses table", err)
	if err == nil {
		return class, 200, err
	}
	return class, 500, err
}

//previous class database functions
func addPreviousClass(class course) (uint16, error) {
	//make sure this class does not exist for the user with this id already, else skip
	var tmp int
	var location = "addPreviousClass"
	var err error
	tmp = -1
	err = db.QueryRow("SELECT userID FROM PreviousClasses WHERE userID=$1 AND classID=$2", class.userID, class.classID).Scan(&tmp)
	checkLogError(location, "Checking if class already exists", err)
	if tmp == -1 && err == nil {
		_, err = db.Exec("INSERT INTO PreviousClasses (userID, classID, className, teacher, startTime, endTime, startDate, endDate, credits) values($1,$2,$3,$4,$5,$6,$7,$8,$9)", class.userID, class.classID, class.className, class.teacher, class.startTime, class.endTime, class.startDate, class.endDate, class.credits)
		checkLogError(location, "Inserting the new class into database", err)
		if err == nil {
			return 200, err
		}
	} else if tmp != -1 {
		return 501, err
	}
	return 500, err
}

func deletePreviousClass(class course) (uint16, error) {
	var location = "deletePreviousClass"
	var err error
	_, err = db.Exec("DELETE FROM PreviousClasses WHERE userID=$1 AND classID=$2", class.userID, class.classID)
	checkLogError(location, "Deleted class from database", err)
	if err == nil {
		return 200, err
	}
	return 500, err
}

//updates an entry in the enrolledclasses table, although we cannot allow to change the userID
func updatePreviousClass(class course, keyword, newValue string) (uint16, error) {
	var location = "updatePreviousClass"
	var err error
	if keyword != "classID" {
		_, err := db.Exec("UPDATE PreviousClasses SET $1=$2 WHERE userID=$3 AND classID=$4", keyword, newValue, class.userID, class.classID)
		checkLogError(location, "Updated something that is not classID in PreviousClasses", err)
	} else {
		_, err := db.Exec("UPDATE PreviousClasses SET $1=$2 WHERE userID=$3 AND className=$4", keyword, newValue, class.userID, class.className)
		checkLogError(location, "Updated classID in PreviousClasses", err)
	}
	if err == nil {
		return 200, err
	}
	return 500, err
}

func getPreviousClass(uid int, classID string) (course, uint16, error) {
	var class course
	var location = "getPreviousClass"
	var err error
	err = db.QueryRow("SELECT FROM PreviousClasses WHERE userID=$1 AND classID=$2", uid, classID).Scan(&class.userID, &class.classID, &class.className, &class.teacher, &class.startTime, &class.endTime, &class.startDate, &class.endDate, &class.credits)
	checkLogError(location, "Selected class from PreviousClasses", err)
	if err == nil {
		return class, 200, err
	}
	return class, 500, err
}

//initialize
func init() {
	var err error
	db, err = sql.Open("sqlite3", "./userDatabase.db?_busy_timeout=5000")
	checkErr(err)
}

func main() {
	go errorDrain()
	r := gin.Default()

	r.GET("/", func(c *gin.Context) { http.ServeFile(c.Writer, c.Request, "./index.html") })
	r.GET("/static/css/:fi", static.Serve("/static/css", static.LocalFile("static/css/", true)))
	r.GET("/static/img/:fi", static.Serve("/static/img", static.LocalFile("static/img/", true)))
	r.GET("/static/js/:fi", static.Serve("/static/js", static.LocalFile("static/js/", true)))
	r.GET("/static/custom/:fi", static.Serve("/static/custom", static.LocalFile("static/custom/", true)))

	api := r.Group("/api")
	{
		api.GET("/something", func(c *gin.Context) {
			c.JSON(200, gin.H{"msg": ""})
		})
	}
	r.Run(":4200")
}
