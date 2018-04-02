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
	var uid int
	err := db.QueryRow("SELECT name FROM user WHERE name=$1", name).Scan(&uid)

	if err == nil {
		return false
	}
	return true

}

//delete a user given their name from the database
func deleteUser(name string) {
	if doesUserExist(name) {
		//get the user id of the person with this name
		var uid int
		stmt, err := db.Query("SELECT id FROM user WHERE name=$1", name)
		checkErr(err)
		stmt.Next()
		err = stmt.Scan(&uid)
		stmt.Close()
		checkErr(err)
		println(strconv.Itoa(uid))

		//delete the user from the user table
		_, err = db.Exec("DELETE FROM user WHERE id=$1", uid)
		checkErr(err)
		/*
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
		*/
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
		stmt.Close()

		_, err = stmt.Exec(name, password, major)
		checkErr(err)

		rows, err := db.Query("SELECT id FROM user WHERE name=$1", name)
		checkErr(err)
		var uid int
		rows.Next()
		rows.Scan(&uid)
		rows.Close()

		stmt, err = db.Prepare("CREATE TABLE PreviousClasses" + strconv.Itoa(uid) + " (classID VARCHAR, credits INT)")
		checkErr(err)
		_, err = stmt.Exec()
		checkErr(err)
		stmt.Close()

		stmt, err = db.Prepare("CREATE TABLE EnrolledClasses" + strconv.Itoa(uid) + " (classID VARCHAR, className VARCHAR, teacher VARCHAR, location VARCHAR, startTime TIME, endTime TIME, startDate DATE, endDate DATE, credits INT)")
		checkErr(err)
		_, err = stmt.Exec()
		checkErr(err)
		stmt.Close()
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
	newUser("Thomas", "Password", "Applied Computer Science")
	deleteUser("Thomas")
	r.Run(":4200")
}
