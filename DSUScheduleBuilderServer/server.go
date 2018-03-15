package main

import (
	"database/sql"
	"net/http"

	"github.com/gin-contrib/static"
	"github.com/gin-gonic/gin"
	_ "github.com/mattn/go-sqlite3"
)

func checkErr(err error) {
	if err != nil {
		panic(err)
	}
}

func newUser(name, password, major string) {
	db, err := sql.Open("sqlite3", "./userDatabase.db")
	checkErr(err)
	stmt, err := db.Prepare("INSERT INTO user (name, password, major) values(?,?,?)")
	checkErr(err)
	_, err = stmt.Exec(name, password, major)
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
