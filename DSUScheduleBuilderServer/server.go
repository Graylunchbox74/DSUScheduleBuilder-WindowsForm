package main

import (
	"database/sql"
	"net/http"
	"strconv"

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

func doesUserExist(name string) bool {
	rows, err := db.Query("SELECT name FROM user")
	checkErr(err)

	var tmpName string
	//check each name in the database to see if there is someone with the same name
	for rows.Next() {
		rows.Scan(&tmpName)
		if tmpName == name {
			//user with that name already exists
			return true
		}
	}
	//user does not exist
	return false
}

//delete a user given their name from the database
func deleteUser(name string) {
	if doesUserExist(name) {
		//get the user id of the person with this name
		var uid int
		rows, err := db.Query("SELECT id FROM user WHERE name=" + name)
		rows.Scan(&uid)

		//delete the user from the user table
		stmt, err := db.Prepare("DELETE FROM user WHERE name=" + name)
		checkErr(err)
		_, err = stmt.Exec()
		checkErr(err)

		//delete the table that stores the users enrolled courses
		stmt, err = db.Prepare("DROP TABLE enrolledCourses" + strconv.Itoa(uid))
		checkErr(err)
		_, err = stmt.Exec()
		checkErr(err)

		//delete the table that stores the users enrolled courses
		stmt, err = db.Prepare("DROP TABLE previousCourses" + strconv.Itoa(uid))
		checkErr(err)
		_, err = stmt.Exec()
		checkErr(err)
	}
}

//create new user given name, password, string, by inputing into database
func newUser(name, password, major string) {

	if !doesUserExist(name) {

		stmt, err := db.Prepare("INSERT INTO user (name, password, major) values(?,?,?)")
		checkErr(err)
		//hash the password to store it
		password, err = hashPassword(password)
		checkErr(err)

		_, err = stmt.Exec(name, password, major)
		checkErr(err)

		rows, err := db.Query("SELECT id FROM user")
		checkErr(err)
		var uid int
		for rows.Next() {
			rows.Scan(&uid)
		}
		stmt, err = db.Prepare("CREATE TABLE PreviousClasses" + strconv.Itoa(uid) + " (classID VARCHAR, credits INT)")
		checkErr(err)
		_, err = stmt.Exec()
		checkErr(err)

		stmt, err = db.Prepare("CREATE TABLE EnrolledClasses" + strconv.Itoa(uid) + " (classID VARCHAR, className VARCHAR, teacher VARCHAR, location VARCHAR, startTime TIME, endTime TIME, startDate DATE, endDate DATE, credits INT)")
		checkErr(err)
		_, err = stmt.Exec()
		checkErr(err)
	}
}

//initialize
func init() {
	var err error
	db, err = sql.Open("sqlite3", "./userDatabase.db")
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
	newUser("Thomas", "Password", "Applied Computer Science")
	r.Run(":4200")
}
