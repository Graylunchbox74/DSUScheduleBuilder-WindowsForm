package main

import (
	"database/sql"
	"net/http"

	"github.com/gin-contrib/static"
	"github.com/gin-gonic/gin"
	_ "github.com/mattn/go-sqlite3"
	"golang.org/x/crypto/bcrypt"
)

//make the database global, db = pointer to a database
var db *sql.DB

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

//check if there was an error
func checkErr(err error) {
	if err != nil {
		println(err)
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

func doesUserExist(uid int) bool {
	err := db.QueryRow("SELECT id FROM user WHERE id=$1", uid).Scan(&uid)

	if err == nil {
		return true
	}
	return false
}

func getUserID(name string) int {
	var uid int
	err := db.QueryRow("SELECT id FROM user WHERE name=$1", name).Scan(&uid)
	checkErr(err)
	return uid
}

//create new user given name, password, string, by inputing into database
func newUser(User user) {
	var uid int
	err := db.QueryRow("SELECT id FROM user WHERE name=$1", User.name).Scan(&uid)
	//the user does not currently exist in the database with the same name
	if err != nil {

		//hash the password to store it
		User.password, err = hashPassword(User.password)
		checkErr(err)
		if err == nil {
			_, err = db.Exec("INSERT INTO user (name, password, major) values($1,$2,$3)", User.name, User.password, User.major)
			checkErr(err)
		}
	}
}

//delete a user given a user struct from the database
func deleteUser(User user) {
	if doesUserExist(User.uid) {
		//delete the user from the user table
		_, err := db.Exec("DELETE FROM user WHERE id=$1", User.uid)
		checkErr(err)
		if err == nil {
			_, err = db.Exec("DELETE FROM PreviousClasses WHERE userID=$1", User.uid)
			checkErr(err)
			if err == nil {
				_, err = db.Exec("DELETE FROM EnrolledClasses WHERE userID=$1", User.uid)
				checkErr(err)
			}
		}
	}
}

//update information in the user table for a user KEYWORD = the column you want to change and NEWVALUE = the value to change to
func updateUser(User user, keyword, newValue string) {
	if newValue != "password" {
		_, err := db.Exec("UPDATE user SET $1=$2 WHERE id=$3", keyword, newValue, User.uid)
		checkErr(err)
	} else {
		newValue, _ := hashPassword(newValue)
		_, err := db.Exec("UPDATE user SET $1=$2 WHERE id=$3", keyword, newValue, User.uid)
		checkErr(err)
	}
}

//given the name of a user return a structure with the user information
func getUser(name string) user {
	var User user
	err := db.QueryRow("SELECT FROM user WHERE name=$1", name).Scan(&User.uid, &User.name, &User.password, &User.major)
	checkErr(err)
	return User
}

func addEnrolledClass(class course) {
	//make sure this class does not exist for the user with this id already, else skip
	var tmp int
	tmp = -1
	err := db.QueryRow("SELECT userID FROM EnrolledClasses WHERE userID=$1 AND classID=$2", class.userID, class.classID).Scan(&tmp)
	checkErr(err)
	if tmp == -1 && err == nil {
		_, err = db.Exec("INSERT INTO EnrolledClasses (userID, classID, className, teacher, location, startTime, endTime, startDate, endDate, credits) values($1,$2,$3,$4,$5,$6,$7,$8,$9,$10)", class.userID, class.classID, class.className, class.teacher, class.location, class.startTime, class.endTime, class.startDate, class.endDate, class.credits)
		checkErr(err)
	}
}

func deleteEnrolledClass(uid int, classID string) {
	_, err := db.Exec("DELETE FROM EnrolledClasses WHERE userID=$1 AND classID=$2", uid, classID)
	checkErr(err)
}

//updates an entry in the enrolledclasses table, although we cannot allow to change the userID
func updateEnrolledClass(class course, keyword, newValue string) {
	if keyword != "classID" {
		_, err := db.Exec("UPDATE EnrolledClasses SET $1=$2 WHERE userID=$3 AND classID=$4", keyword, newValue, class.userID, class.classID)
		checkErr(err)
	} else {
		_, err := db.Exec("UPDATE EnrolledClasses SET $1=$2 WHERE userID=$3 AND className=$4", keyword, newValue, class.userID, class.className)
		checkErr(err)
	}
}

func getEnrolledClass(uid int, classID string) (course, error) {
	var class course
	err := db.QueryRow("SELECT FROM EnrolledClasses WHERE userID=$1 AND classID=$2", uid, classID).Scan(&class.userID, &class.classID, &class.className, &class.teacher, &class.location, &class.startTime, &class.endTime, &class.startDate, &class.endDate, &class.credits)
	return class, err
}

func addPreviousClass(class course) {
	//make sure this class does not exist for the user with this id already, else skip
	var tmp int
	tmp = -1
	err := db.QueryRow("SELECT userID FROM PreviousClasses WHERE userID=$1 AND classID=$2", class.userID, class.classID).Scan(&tmp)
	checkErr(err)
	if tmp == -1 && err == nil {
		_, err = db.Exec("INSERT INTO PreviousClasses (userID, classID, className, teacher, startTime, endTime, startDate, endDate, credits) values($1,$2,$3,$4,$5,$6,$7,$8,$9)", class.userID, class.classID, class.className, class.teacher, class.startTime, class.endTime, class.startDate, class.endDate, class.credits)
		checkErr(err)
	}
}

func deletePreviousClass(uid int, classID string) {
	_, err := db.Exec("DELETE FROM PreviousClasses WHERE userID=$1 AND classID=$2", uid, classID)
	checkErr(err)
}

//updates an entry in the enrolledclasses table, although we cannot allow to change the userID
func updatePreviousClass(class course, keyword, newValue string) {
	if keyword != "classID" {
		_, err := db.Exec("UPDATE PreviousClasses SET $1=$2 WHERE userID=$3 AND classID=$4", keyword, newValue, class.userID, class.classID)
		checkErr(err)
	} else {
		_, err := db.Exec("UPDATE PreviousClasses SET $1=$2 WHERE userID=$3 AND className=$4", keyword, newValue, class.userID, class.className)
		checkErr(err)
	}
}

func getPreviousClass(uid int, classID string) (course, error) {
	var class course
	err := db.QueryRow("SELECT FROM PreviousClasses WHERE userID=$1 AND classID=$2", uid, classID).Scan(&class.userID, &class.classID, &class.className, &class.teacher, &class.startTime, &class.endTime, &class.startDate, &class.endDate, &class.credits)
	return class, err
}

//initialize
func init() {
	var err error
	db, err = sql.Open("sqlite3", "./userDatabase.db?_busy_timeout=5000")
	checkErr(err)
}

func main() {
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
