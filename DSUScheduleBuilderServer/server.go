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

//check if there was an error
func checkErr(err error) {
	if err != nil {
		panic(err)
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

//delete a user given their name from the database
func deleteUser(uid int) {
	if doesUserExist(uid) {
		//delete the user from the user table
		_, err := db.Exec("DELETE FROM user WHERE id=$1", uid)
		checkErr(err)

		_, err = db.Exec("DELETE FROM PreviousClasses WHERE userID=$1", uid)
		checkErr(err)

		_, err = db.Exec("DELETE FROM EnrolledClasses WHERE userID=$1", uid)
		checkErr(err)
	}
}

//create new user given name, password, string, by inputing into database
func newUser(name, password, major string) {

	var uid int
	err := db.QueryRow("SELECT id FROM user WHERE name=$1", name).Scan(&uid)
	//the user does not currently exist in the database with the same name
	if err != nil {

		//hash the password to store it
		password, err := hashPassword(password)
		checkErr(err)

		_, err = db.Exec("INSERT INTO user (name, password, major) values($1,$2,$3)", name, password, major)
		checkErr(err)
	}
}

func addEnrolledClass(uid, startTime, endTime, credits int, classID, className, teacher, location, startDate, endDate string) {
	//make sure this class does not exist for the user with this id already, else skip
	rows, _ := db.QueryRow("SELECT userID FROM EnrolledClasses WHERE uid=$1 AND classID=$2", uid, classID)
	if {
		_, err := db.Exec("INSERT INTO EnrolledClasses (userID, classID, className, teacher, location, startTime, endTime, startDate, endDate, credits) values($1,$2,$3,$4,$5,$6,$7,$8,$9,$10)", uid, classID, className, teacher, location, startTime, endTime, startDate, endDate, credits)
		checkErr(err)
	}
}

func addPreviousClass(uid, startTime, endTime, credits int, classID, className, teacher, startDate, endDate string) {

	//make sure this class does not exist for the user with this id already, else skip
	err := db.QueryRow("SELECT userID FROM PreviousClasses WHERE uid=$1 AND classID=$2", uid, classID)
	if err != nil {
		_, err := db.Exec("INSERT INTO PreviousClasses (userID, classID, className, teacher, startTime, endTime, startDate, endDate, credits) values($1,$2,$3,$4,$5,$6,$7,$8,$9)", uid, classID, className, teacher, startTime, endTime, startDate, endDate, credits)
		checkErr(err)
	}
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
	//	newUser("Thomas", "Password", "Applied Computer Science")
	addEnrolledClass(0, 0, 1, 2, "e", "test", "me", "here", "", "then")
	//addPreviousClass(0, 0, 0, 0, "d", "d", "d", "d", "d")
	r.Run(":4200")
}
