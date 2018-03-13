package main

import(
	"net/http"
	"github.com/gin-gonic/gin"
	"github.com/gin-contrib/static"
)


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
            c.JSON(200, gin.H{"msg": "Riley sucks dong"})
        })
    }

    r.Run(":4200")
}