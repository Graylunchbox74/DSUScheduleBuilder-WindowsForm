package main

import (
	"database/sql"
	"errors"
	"fmt"
	"math/rand"
	"net/http"
	"strconv"
	"time"

	"github.com/gin-contrib/static"
	"github.com/gin-gonic/gin"
	_ "github.com/mattn/go-sqlite3"
	"golang.org/x/crypto/bcrypt"
)

//make the database global, db = pointer to a database
var db *sql.DB
var errorChannel chan locationalError

//holds the information for a single course being/has been offered
type course struct {
	UserID    string `json:"uid"`
	Key       int    `json:"key"`
	StartTime int    `json:"startTime"`
	EndTime   int    `json:"endTime"`
	Credits   int    `json:"credits"`

	ClassID    string `json:"classID"`
	ClassName  string `json:"className"`
	Location   string `json:"location"`
	DaysOfWeek string `json:"daysOfWeek"`
	Teacher    string `json:"teacher"`
	StartDate  string `json:"startDate"`
	EndDate    string `json:"endDate"`
}

//(sectionID ,open, academicLevel , courseID , description , courseName , startDate, endDate , location , meetingInformation, supplies , credits , slotsAvailable , slotsCapacity,
// slotsWaitlist, timeStart, timeEnd , professorEmails , prereqNonCourse , recConcurrentCourses, reqConcurrentCourses, prereqCoursesAnd, prereqCoursesOR,instructionalMethods,term);
type availableCourse struct {
	SectionID            string `json:"sectionID"`
	Open                 bool   `json:"open"`
	AcademicLevel        string `json:"academicLevel"`
	CourseID             string `json:"courseID"`
	Description          string `json:"description"`
	CourseName           string `json:"courseName"`
	StartDate            string `json:"startDate"`
	EndDate              string `json:"endDate"`
	Location             string `json:"location"`
	MeetingInformation   string `json:"meetingInformation"`
	Supplies             string `json:"supplies"`
	Credits              int    `json:"credits"`
	SlotsAvailable       int    `json:"slotsAvailable"`
	SlotsCapacity        int    `json:"slotsCapacity"`
	SlotsWaitlist        int    `json:"slotsWaitlist"`
	TimeStart            int    `json:"timeStart"`
	TimeEnd              int    `json:"timeEnd"`
	ProfessorEmails      string `json:"professorEmails"`
	Teacher              string `json:"teacher"`
	PrereqNonCourse      string `json:"prereqNonCourse"`
	RecConcurrentCourses string `json:"recConcurrentCourses"`
	ReqConcurrentCourses string `json:"reqConcurrentCourses"`
	PrereqCoursesAnd     string `json:"prereqCoursesAnd"`
	PrereqCoursesOr      string `json:"prereqCoursesOr"`
	InstructionalMethods string `json:"instructionalMethods"`
	Term                 string `json:"term"`
	Key                  int    `json:"key"`
}

type teacher struct {
	name  string
	email string
	phone string
	id    int
}

type locationalError struct {
	Error                 error
	Location, Sublocation string
}

//User holds the information for a single user
type User struct {
	Email  string `json:"email"`
	Fname  string `json:"fname"`
	Lname  string `json:"lname"`
	Majors string `json:"majors"`
	Minors string `json:"minors"`
	UID    int    `json:"uid"`
}

//errorStruct holds an error
type errorStruct struct {
	ErrorStatusCode int    `json:"errorCode"`
	ErrorLogMessage string `json:"errorMessage"`
}

func checkLogError(location, sublocation string, err error) {
	if err != nil {
		logError(location, sublocation, err)
	}
}

func createUUID(size int) string {
	const letterBytes = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"

	b := make([]byte, size)
	for i := range b {
		b[i] = letterBytes[rand.Intn(len(letterBytes))]
	}
	return string(b)
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

func doesUserExistWithField(field, value string) bool {
	err := db.QueryRow("SELECT %s FROM users WHERE %s=$1", field, field, value).Scan(&value)
	return err != nil
}

func createErrorStruct(code int, location, sublocation string, err error) errorStruct {
	go logError(location, sublocation, err)
	return errorStruct{code, fmt.Sprintf("%s, %s: %s", location, sublocation, err.Error())}
}

func getUserID(name string) int {
	var uid int
	err := db.QueryRow("SELECT id FROM user WHERE name=$1", name).Scan(&uid)
	checkLogError("getUserID", "1", err)
	return uid
}

func getTeacher(email string) (string, int, error) {
	var name string
	err := db.QueryRow("select name from teachers where email=$1", email).Scan(&name)

	if err == sql.ErrNoRows {
		return name, 5, err
	}

	if err != nil {
		return name, 5, err
	}

	return name, 200, err
}

//user database functions
//create new user given name, password, string, by inputing into database
func newUser(fname, lname, email, password string) (int, error) {
	location := "newUser"

	var tmp string
	err := db.QueryRow("SELECT email from users where email=$1", email).Scan(&tmp)
	if err != sql.ErrNoRows {
		return 13, errors.New("the email already exists")
	}

	//hash the password to store it
	// maxAttempts, numAttempts := 10, 0
	// var err error
	// for err = nil; err != nil && numAttempts <= maxAttempts; {
	// password, err = hashPassword(password)
	// numAttempts++
	// }

	// if err != nil {
	// 	go logError(location, "2", err)
	// 	return 1, err
	// }

	_, err = db.Exec(`
		INSERT INTO users 
		(fname,lname,email,password,majors,minors)
		values($1,$2,$3,$4,$5,$6)`, fname, lname, email, password, "", "",
	)

	if err != nil {
		go logError(location, "1", err)
		return 1, errors.New("Error inserting new user into database")
	}

	return 200, nil
}

func addMajor(userID int, major string) (int, error) {
	majors := "%|" + major + "|%"
	var currentMajors string
	//check if major already exits
	err := db.QueryRow("select majors from users where majors like $1", majors).Scan(&currentMajors)

	if err != sql.ErrNoRows {
		return 14, errors.New("major alread exists for that user")
	}
	//get the current list of majors
	err = db.QueryRow("select majors from users where uid=$1", userID).Scan(&currentMajors)

	if err != nil {
		return 5, err
	}
	//add new major
	currentMajors = currentMajors + "|" + major + "|"
	_, err = db.Exec("UPDATE users set majors=$1 where uid=$2", currentMajors, userID)
	if err != nil {
		return 15, err
	}

	return 200, err
}

//delete a user given a user struct from the database
//NEEDS TO BE REFACTORED!
func deleteUser(user User) (int, error) {
	location := "deleteUser"
	var err error
	// if doesUserExistWithField("id", user.UID) {
	// 	var uid int
	// 	err := db.QueryRow("SELECT id FROM user WHERE id=$1", user.UID).Scan(&uid)
	// 	checkLogError(location, "Check if user exists before deleting", err)
	// 	return 500, err
	// }
	//delete the user from the user table
	_, err = db.Exec("DELETE FROM user WHERE id=$1", user.UID)
	checkLogError(location, "Delete user information from user table", err)
	if err == nil {
		_, err = db.Exec("DELETE FROM PreviousClasses WHERE userID=$1", user.UID)
		checkLogError(location, "Delete user information from PreviousClasses", err)
		if err == nil {
			_, err = db.Exec("DELETE FROM EnrolledClasses WHERE userID=$1", user.UID)
			checkLogError(location, "Delete user information from EnrolledClasses", err)
			if err == nil {
				return 200, err
			}
		}
	}
	return 500, err
}

//update information in the user table for a user KEYWORD = the column you want to change and NEWVALUE = the value to change to
func updateUser(user User, keyword, newValue string) (int, error) {
	location := "updateUser"
	var err error
	if newValue != "password" {
		_, err = db.Exec("UPDATE user SET $1=$2 WHERE id=$3", keyword, newValue, user.UID)
		checkLogError(location, "Update user information that is not password", err)
	} else {
		newValue, _ := hashPassword(newValue)
		_, err := db.Exec("UPDATE user SET $1=$2 WHERE id=$3", keyword, newValue, user.UID)
		checkLogError(location, "Update user password", err)
	}
	if err == nil {
		return 200, err
	}
	return 500, err
}

//given the name of a user return a structure with the user information
func getUser(id int) (User, int, error) {
	location := "getUser"
	var user User
	err := db.QueryRow("SELECT uid,fname,lname,email,minors,majors FROM USERS WHERE uid=$1", id).Scan(&user.UID, &user.Fname, &user.Lname, &user.Email, &user.Minors, &user.Majors)
	checkLogError(location, "Selecting the user by name", err)
	if err == nil {
		return user, 200, err
	}
	return user, 4, err
}

func getAllAvailableCourses() ([]availableCourse, int, error) {
	var allCourses []availableCourse
	rows, err := db.Query("SELECT * FROM availableCourses")

	if err != nil {
		return allCourses, 5, err
	}

	for rows.Next() {
		var class availableCourse
		err = rows.Scan(
			&class.SectionID,
			&class.Open,
			&class.AcademicLevel,
			&class.CourseID,
			&class.Description,
			&class.CourseName,
			&class.StartDate,
			&class.EndDate,
			&class.Location,
			&class.MeetingInformation,
			&class.Supplies,
			&class.Credits,
			&class.SlotsAvailable,
			&class.SlotsCapacity,
			&class.SlotsWaitlist,
			&class.TimeStart,
			&class.TimeEnd,
			&class.ProfessorEmails,
			&class.PrereqNonCourse,
			&class.RecConcurrentCourses,
			&class.ReqConcurrentCourses,
			&class.PrereqCoursesAnd,
			&class.PrereqCoursesOr,
			&class.InstructionalMethods,
			&class.Term,
			&class.Key,
		)

		if err != nil {
			return allCourses, 5, err
		}

		teacher, errCode, err := getTeacher(class.ProfessorEmails)

		allCourses = append(allCourses, class)
	}

	return allCourses, 200, err
}

//enrolled class database functions
func addEnrolledClass(class course) (int, error) {
	//make sure this class does not exist for the user with this id already, else skip
	location := "addEnrolledClass"
	var tmp int
	var err error
	tmp = -1
	err = db.QueryRow("SELECT userID FROM EnrolledClasses WHERE userID=$1 AND classID=$2", class.UserID, class.ClassID).Scan(&tmp)
	checkLogError(location, "Selecting from database to see if it already exists", err)
	if tmp == -1 && err == nil {
		_, err = db.Exec("INSERT INTO EnrolledClasses (userID, classID, className, teacher, location, startTime, endTime, startDate, endDate, credits) values($1,$2,$3,$4,$5,$6,$7,$8,$9,$10)", class.UserID, class.ClassID, class.ClassName, class.Teacher, class.Location, class.StartTime, class.EndTime, class.StartDate, class.EndDate, class.Credits)
		checkLogError(location, "Insert the enrolled class in the database", err)
		if err == nil {
			return 200, err
		}
	} else if tmp != -1 {
		return 501, err
	}
	return 500, err
}

func deleteEnrolledClass(class course) (int, error) {
	location := "deleteEnrolledClass"
	_, err := db.Exec("DELETE FROM EnrolledClasses WHERE userID=$1 AND classID=$2", class.UserID, class.ClassID)
	checkLogError(location, "Delete enrolled class from database", err)
	if err == nil {
		return 200, err
	}
	return 500, err
}

//updates an entry in the enrolledclasses table, although we cannot allow to change the userID
func updateEnrolledClass(classKey, keyword, newValue string) (course, int, error) {
	var err error
	var newClass course
	parameter := classKey

	_, err = db.Exec("UPDATE EnrolledClasses SET $1=$2 WHERE key=$3 AND classID=$4", keyword, newValue, parameter)

	if err != nil {
		return newClass, 12, err
	}

	err = db.QueryRow("SELECT * from EnrolledClasses where keyy=$1", parameter).Scan(
		&newClass.UserID, &newClass.ClassID, &newClass.ClassName, &newClass.Teacher, &newClass.Location, &newClass.DaysOfWeek,
		&newClass.StartTime, &newClass.EndTime, &newClass.StartDate, &newClass.EndDate, &newClass.Credits, &newClass.Key,
	)

	if err != nil {
		return newClass, 5, err
	}

	return newClass, 200, err
}

func getEnrolledClasses(uid int) ([]course, int, error) {
	location := "getEnrolledClasses"
	var classes []course
	var class course
	parameter := "|%" + strconv.Itoa(uid) + "%|"
	rows, err := db.Query(`
		SELECT *
		FROM EnrolledClasses 
		WHERE userID LIKE $1`, parameter,
	)

	defer rows.Close()
	var tmp string
	for rows.Next() {
		class = course{}

		err = rows.Scan(
			&tmp, &class.ClassID, &class.ClassName, &class.Teacher, &class.Location, &class.DaysOfWeek,
			&class.StartTime, &class.EndTime, &class.StartDate, &class.EndDate, &class.Credits, &class.Key,
		)

		if err != nil {
			go logError(location, "1", err)
			return classes, 5, err
		}

		classes = append(classes, class)
	}

	err = rows.Err()

	if err != nil {
		go logError(location, "2", err)
		return classes, 5, err
	}

	return classes, 200, nil
}

//previous class database functions
func addPreviousClass(class course) (int, error) {
	//make sure this class does not exist for the user with this id already, else skip
	var tmp int
	location := "addPreviousClass"
	var err error
	tmp = -1
	err = db.QueryRow("SELECT userID FROM PreviousClasses WHERE userID=$1 AND classID=$2", class.UserID, class.ClassID).Scan(&tmp)
	checkLogError(location, "Checking if class already exists", err)
	if tmp == -1 && err == nil {
		_, err = db.Exec("INSERT INTO PreviousClasses (userID, classID, className, teacher, startTime, endTime, startDate, endDate, credits) values($1,$2,$3,$4,$5,$6,$7,$8,$9)", class.UserID, class.ClassID, class.ClassName, class.Teacher, class.StartTime, class.EndTime, class.StartDate, class.EndDate, class.Credits)
		checkLogError(location, "Inserting the new class into database", err)
		if err == nil {
			return 200, err
		}
	} else if tmp != -1 {
		return 501, err
	}
	return 500, err
}

func deletePreviousClass(class course) (int, error) {
	location := "deletePreviousClass"
	var err error
	_, err = db.Exec("DELETE FROM PreviousClasses WHERE userID=$1 AND classID=$2", class.UserID, class.ClassID)
	checkLogError(location, "Deleted class from database", err)
	if err == nil {
		return 200, err
	}
	return 500, err
}

//updates an entry in the enrolledclasses table, although we cannot allow to change the userID
func updatePreviousClass(classKey, keyword, newValue string) (course, int, error) {
	var err error
	var newClass course
	parameter := classKey

	_, err = db.Exec("UPDATE PreviousClasses SET $1=$2 WHERE key=$3 AND classID=$4", keyword, newValue, parameter)

	if err != nil {
		return newClass, 12, err
	}

	err = db.QueryRow("SELECT * from PreviousClasses where keyy=$1", parameter).Scan(
		&newClass.UserID, &newClass.ClassID, &newClass.ClassName, &newClass.Teacher,
		&newClass.StartTime, &newClass.EndTime, &newClass.StartDate, &newClass.EndDate, &newClass.Credits, &newClass.Key,
	)

	if err != nil {
		return newClass, 5, err
	}

	return newClass, 200, err
}

func getPreviousClasses(uid int) ([]course, int, error) {
	location := "getPreviousClasses"
	var classes []course
	var class course
	parameter := "|%" + strconv.Itoa(uid) + "%|"
	rows, err := db.Query(`
		SELECT *
		FROM PreviousClasses 
		WHERE userID LIKE $1`, parameter,
	)

	defer rows.Close()
	var tmp string
	for rows.Next() {
		class = course{}

		err = rows.Scan(
			&tmp, &class.ClassID, &class.ClassName, &class.Teacher,
			&class.StartTime, &class.EndTime, &class.StartDate, &class.EndDate, &class.Credits, &class.Key,
		)

		if err != nil {
			go logError(location, "1", err)
			return classes, 5, err
		}

		classes = append(classes, class)
	}

	err = rows.Err()

	if err != nil {
		go logError(location, "2", err)
		return classes, 5, err
	}

	return classes, 200, nil
}

func init() {
	errorChannel = make(chan locationalError)

	var err error
	rand.Seed(time.Now().UnixNano())

	db, err = sql.Open("sqlite3", "./userDatabase.db?_busy_timeout=5000")
	if err != nil {
		panic(err)
	}
}

func main() {
	errorChannel = make(chan locationalError)
	go errorDrain()
	r := gin.Default()

	r.GET("/", func(c *gin.Context) { http.ServeFile(c.Writer, c.Request, "./index.html") })
	r.GET("/static/css/:fi", static.Serve("/static/css", static.LocalFile("static/css/", true)))
	r.GET("/static/img/:fi", static.Serve("/static/img", static.LocalFile("static/img/", true)))
	r.GET("/static/js/:fi", static.Serve("/static/js", static.LocalFile("static/js/", true)))
	r.GET("/static/custom/:fi", static.Serve("/static/custom", static.LocalFile("static/custom/", true)))

	api := r.Group("/api")
	{
		api.GET("/clear", func(c *gin.Context) {
			db.Exec("delete from user_sessions where 1=1")
		})

		api.GET("/something", func(c *gin.Context) {
			c.JSON(200, gin.H{"msg": ""})
		})

		users := api.Group("user")
		{

			users.POST("/newUser/", func(c *gin.Context) {
				fname := c.PostForm("firstName")
				lname := c.PostForm("lastName")
				email := c.PostForm("email")
				password := c.PostForm("password")

				errCode, err := newUser(fname, lname, email, password)

				if err != nil {
					currentError := createErrorStruct(errCode, c.Request.URL.String(), "3", err)
					c.JSON(500, currentError)
					return
				}

				c.JSON(200, gin.H{"success": 1})

			})

			users.POST("/addMajor/", func(c *gin.Context) {
				uuid := c.PostForm("uuid")
				var userID int
				err := db.QueryRow("SELECT uid FROM USER_SESSIONS WHERE uuid=$1", uuid).Scan(&userID)

				if err == sql.ErrNoRows {
					currentError := createErrorStruct(2, c.Request.URL.String(), "3", err)
					c.JSON(500, currentError)
					return
				}

				if err != nil {
					currentError := createErrorStruct(3, c.Request.URL.String(), "1", err)
					c.JSON(500, currentError)
					return
				}

				major := c.PostForm("major")
				errCode, err := addMajor(userID, major)

				if err != nil {
					currentError := createErrorStruct(errCode, c.Request.URL.String(), "4", err)
					c.JSON(500, currentError)
					return
				}

				c.JSON(200, gin.H{"success": 1})
			})

			users.GET("/getData/:uuid", func(c *gin.Context) {
				uuid := c.Param("uuid")
				var userID int
				err := db.QueryRow("SELECT uid FROM USER_SESSIONS WHERE uuid=$1", uuid).Scan(&userID)

				if err == sql.ErrNoRows {
					currentError := createErrorStruct(2, c.Request.URL.String(), "3", err)
					c.JSON(500, currentError)
					return
				}

				if err != nil {
					currentError := createErrorStruct(3, c.Request.URL.String(), "1", err)
					c.JSON(500, currentError)
					return
				}

				var user User
				user, errno, err := getUser(userID)

				if err != nil {
					currentError := createErrorStruct(errno, c.Request.URL.String(), "2", err)
					c.JSON(500, currentError)
					return
				}
				user.UID = 0
				c.JSON(200, user)
			})

			users.POST("/logout/", func(c *gin.Context) {
				uuid := c.PostForm("uuid")
				var uid int

				err := db.QueryRow("SELECT uid FROM USER_SESSIONS WHERE uuid=$1", uuid).Scan(&uid)

				if err == sql.ErrNoRows {
					currentError := createErrorStruct(2, c.Request.URL.String(), "3", err)
					c.JSON(500, currentError)
					return
				}

				if err != nil {
					currentError := createErrorStruct(3, c.Request.URL.String(), "1", err)
					c.JSON(500, currentError)
					return
				}

				_, err = db.Exec("DELETE FROM USER_SESSIONS WHERE uuid=$1", uuid)

				if err != nil {
					currentError := createErrorStruct(10, c.Request.URL.String(), "2", err)
					c.JSON(500, currentError)
					return
				}

				c.JSON(200, gin.H{"success": 1})

			})

			users.POST("/login/", func(c *gin.Context) {
				email := c.PostForm("email")
				password := c.PostForm("password")
				var tmp int
				var user User
				//hash password
				//password, _ = hashPassword(password)

				var userPassword string
				err := db.QueryRow("SELECT * FROM USERS WHERE email=$1", email).Scan(&user.Fname, &user.Lname, &user.Majors, &user.Minors, &user.Email, &userPassword, &user.UID)

				if err == sql.ErrNoRows {
					currentError := createErrorStruct(7, c.Request.URL.String(), "3", err)
					c.JSON(500, currentError)
					return
				}

				if err != nil {
					currentError := createErrorStruct(8, c.Request.URL.String(), "1", err)
					c.JSON(500, currentError)
					return
				}

				err = db.QueryRow("SELECT uid from USER_SESSIONS where uid=$1", user.UID).Scan(&tmp)

				if err != sql.ErrNoRows {
					err = errors.New("User already has a uuid")
					currentError := createErrorStruct(9, c.Request.URL.String(), "4", err)
					c.JSON(500, currentError)
					return
				}

				if password != userPassword {
					err = errors.New("Password does not match")
					currentError := createErrorStruct(6, c.Request.URL.String(), "2", err)
					c.JSON(500, currentError)
					return
				}

				uuid := createUUID(50)
				err = db.QueryRow("SELECT * FROM USER_SESSIONS WHERE uuid=$1", uuid).Scan(&tmp)

				for err != sql.ErrNoRows {
					uuid := createUUID(50)
					err = db.QueryRow("SELECT * FROM USER_SESSIONS WHERE uuid=$1", uuid).Scan(&tmp)
				}

				_, err = db.Exec("INSERT INTO USER_SESSIONS (uid,uuid) values($1,$2)", user.UID, uuid)

				if err != nil {
					currentError := createErrorStruct(10, c.Request.URL.String(), "5", err)
					c.JSON(500, currentError)
					return
				}

				c.JSON(200, gin.H{"uuid": uuid, "user": user})
			})
		}
		courses := api.Group("courses")
		{

			courses.GET("/previous/:uuid", func(c *gin.Context) {
				uuid := c.Param("uuid")
				var userID int
				err := db.QueryRow("SELECT uid FROM USER_SESSIONS WHERE uuid=$1", uuid).Scan(&userID)

				if err == sql.ErrNoRows {
					currentError := createErrorStruct(2, c.Request.URL.String(), "3", err)
					c.JSON(500, currentError)
					return
				}

				if err != nil {
					currentError := createErrorStruct(3, c.Request.URL.String(), "1", err)
					c.JSON(500, currentError)
					return
				}

				var classes []course
				classes, errno, err := getPreviousClasses(userID)

				if err != nil {
					currentError := createErrorStruct(errno, c.Request.URL.String(), "2", err)
					c.JSON(500, currentError)
					return
				}
				c.JSON(200, gin.H{"classes": classes})
			})

			courses.GET("/enrolled/:uuid", func(c *gin.Context) {
				uuid := c.Param("uuid")
				var userID int
				err := db.QueryRow("SELECT uid FROM USER_SESSIONS WHERE uuid=$1", uuid).Scan(&userID)

				if err == sql.ErrNoRows {
					currentError := createErrorStruct(2, c.Request.URL.String(), "3", err)
					c.JSON(500, currentError)
					return
				}

				if err != nil {
					currentError := createErrorStruct(3, c.Request.URL.String(), "1", err)
					c.JSON(500, currentError)
					return
				}
				var classes []course
				classes, errno, err := getEnrolledClasses(userID)

				if err != nil {
					currentError := createErrorStruct(errno, c.Request.URL.String(), "2", err)
					c.JSON(500, currentError)
					return
				}

				c.JSON(200, gin.H{"classes": classes})
			})

			courses.POST("/updateEnrolled/", func(c *gin.Context) {
				uuid := c.PostForm("uuid")
				var userID int
				err := db.QueryRow("SELECT uid FROM USER_SESSIONS WHERE uuid=$1", uuid).Scan(&userID)

				if err == sql.ErrNoRows {
					currentError := createErrorStruct(2, c.Request.URL.String(), "3", err)
					c.JSON(500, currentError)
					return
				}

				if err != nil {
					currentError := createErrorStruct(3, c.Request.URL.String(), "1", err)
					c.JSON(500, currentError)
					return
				}

				if userID > 3 {
					currentError := createErrorStruct(11, c.Request.URL.String(), "4", err)
					c.JSON(500, currentError)
					return
				}

				classKey := c.PostForm("classKey")
				keyword := c.PostForm("keyword")
				newValue := c.PostForm("newValue")

				var class course
				class, errno, err := updateEnrolledClass(classKey, keyword, newValue)

				if err != nil {
					currentError := createErrorStruct(errno, c.Request.URL.String(), "2", err)
					c.JSON(500, currentError)
					return
				}

				c.JSON(200, gin.H{"class": class})
			})

			courses.POST("/updatePrevious/", func(c *gin.Context) {
				uuid := c.PostForm("uuid")
				var userID int
				err := db.QueryRow("SELECT uid FROM USER_SESSIONS WHERE uuid=$1", uuid).Scan(&userID)

				if err == sql.ErrNoRows {
					currentError := createErrorStruct(2, c.Request.URL.String(), "3", err)
					c.JSON(500, currentError)
					return
				}

				if err != nil {
					currentError := createErrorStruct(3, c.Request.URL.String(), "1", err)
					c.JSON(500, currentError)
					return
				}

				if userID > 3 {
					currentError := createErrorStruct(11, c.Request.URL.String(), "4", err)
					c.JSON(500, currentError)
					return
				}

				classKey := c.PostForm("classKey")
				keyword := c.PostForm("keyword")
				newValue := c.PostForm("newValue")

				var class course
				class, errno, err := updatePreviousClass(classKey, keyword, newValue)

				if err != nil {
					currentError := createErrorStruct(errno, c.Request.URL.String(), "2", err)
					c.JSON(500, currentError)
					return
				}

				c.JSON(200, gin.H{"class": class})
			})

			courses.GET("/available/:uuid", func(c *gin.Context) {
				uuid := c.Param("uuid")
				var userID int
				err := db.QueryRow("SELECT uid FROM USER_SESSIONS WHERE uuid=$1", uuid).Scan(&userID)

				if err == sql.ErrNoRows {
					currentError := createErrorStruct(2, c.Request.URL.String(), "3", err)
					c.JSON(500, currentError)
					return
				}

				if err != nil {
					currentError := createErrorStruct(3, c.Request.URL.String(), "1", err)
					c.JSON(500, currentError)
					return
				}

				allCourses, errCode, err := getAllAvailableCourses()

				if err != nil {
					currentError := createErrorStruct(errCode, c.Request.URL.String(), "1", err)
					c.JSON(500, currentError)
					return
				}

				c.JSON(200, allCourses)
				//select all courses that are available to register for

			})

		}
	}
	r.Run(":4200")
}
