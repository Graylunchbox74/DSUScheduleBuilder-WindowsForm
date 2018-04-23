package main

import (
	"database/sql"
	"errors"
	"fmt"
	"math/rand"
	"net/http"
	"os"
	"strconv"
	"strings"
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

	ClassID    string   `json:"classID"`
	ClassName  string   `json:"className"`
	Location   string   `json:"location"`
	DaysOfWeek string   `json:"daysOfWeek"`
	Teacher    []string `json:"teacher"`
	StartDate  string   `json:"startDate"`
	EndDate    string   `json:"endDate"`
}

//(sectionID ,open, academicLevel , courseID , description , courseName , startDate, endDate , location , meetingInformation, supplies , credits , slotsAvailable , slotsCapacity,
// slotsWaitlist, timeStart, timeEnd , professorEmails , prereqNonCourse , recConcurrentCourses, reqConcurrentCourses, prereqCoursesAnd, prereqCoursesOR,instructionalMethods,term);
type availableCourse struct {
	SectionID            string   `json:"sectionID"`
	Open                 bool     `json:"open"`
	AcademicLevel        string   `json:"academicLevel"`
	CourseID             string   `json:"courseID"`
	Description          string   `json:"description"`
	CourseName           string   `json:"courseName"`
	StartDate            string   `json:"startDate"`
	EndDate              string   `json:"endDate"`
	Location             string   `json:"location"`
	MeetingInformation   string   `json:"meetingInformation"`
	Supplies             string   `json:"supplies"`
	Credits              int      `json:"credits"`
	SlotsAvailable       int      `json:"slotsAvailable"`
	SlotsCapacity        int      `json:"slotsCapacity"`
	SlotsWaitlist        int      `json:"slotsWaitlist"`
	TimeStart            int      `json:"timeStart"`
	TimeEnd              int      `json:"timeEnd"`
	ProfessorEmails      string   `json:"professorEmails"`
	Teacher              []string `json:"teacher"`
	PrereqNonCourse      string   `json:"prereqNonCourse"`
	RecConcurrentCourses string   `json:"recConcurrentCourses"`
	ReqConcurrentCourses string   `json:"reqConcurrentCourses"`
	PrereqCoursesAnd     string   `json:"prereqCoursesAnd"`
	PrereqCoursesOr      string   `json:"prereqCoursesOr"`
	InstructionalMethods string   `json:"instructionalMethods"`
	Term                 string   `json:"term"`
	Key                  int      `json:"key"`
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
	f, err := os.OpenFile("sdpass.log", os.O_APPEND|os.O_CREATE|os.O_WRONLY, 0600)

	if err != nil {
		panic(err)
	}

	defer f.Close()

	for {
		select {
		case lErr = <-errorChannel:
			fmt.Println(lErr.Location, lErr.Sublocation, lErr)
			f.WriteString(fmt.Sprintf("%s: %s, %s, %s\n", time.Now().Format("2006-01-02 15:04:05"), lErr.Location, lErr.Sublocation, lErr))
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

func getTeacher(email string) ([]string, int, error) {
	var name string
	var err error
	var teachers []string
	email = strings.Replace(email, "|", "", 1)
	emails := strings.SplitAfter(email, "|")

	for currentEmail := range emails {
		emails[currentEmail] = strings.Replace(emails[currentEmail], "|", "", -1)
		if emails[currentEmail] != "" {
			err = db.QueryRow("select name from teachers where email=$1", emails[currentEmail]).Scan(&name)

			if err == sql.ErrNoRows {
				err = errors.New("No teacher matches that email " + emails[currentEmail])
				return teachers, 5, err
			}

			if err != nil {
				return teachers, 5, err
			}

			teachers = append(teachers, name)
		}
	}

	return teachers, 200, err
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

		if err != nil {
			return allCourses, errCode, err
		}

		class.Teacher = teacher
		allCourses = append(allCourses, class)
	}

	return allCourses, 200, err
}

//enrolled class database functions
func addEnrolledClass(userID, key int) (int, error) {
	//check if user is already enrolled for this course
	var currentEnrolled string
	var uidDB string
	uidDB = "%|" + strconv.Itoa(userID) + "|%"
	err := db.QueryRow("SELECT userID from EnrolledClasses where key=$1 and userID like $2", key, uidDB).Scan(&currentEnrolled)
	if err == sql.ErrNoRows {
		//check if the class is already in the db
		err = db.QueryRow("SELECT userID from EnrolledClasses where key=$1", key).Scan(&currentEnrolled)
		if err == sql.ErrNoRows {
			//insert the new class
			var class course
			var emails string
			err = db.QueryRow("SELECT courseID,courseName,professorEmails,location,daysOfWeek,timeStart,timeEnd,startDate,endDate,credits,key from availableCourses where key=$1", key).Scan(
				&class.ClassID,
				&class.ClassName,
				&emails,
				&class.Location,
				&class.DaysOfWeek,
				&class.DaysOfWeek,
				&class.StartTime,
				&class.EndTime,
				&class.StartDate,
				&class.EndDate,
				&class.Credits,
				&class.Key,
			)
			if err == sql.ErrNoRows {
				return 5, errors.New("class with this key does not exist in availableClasses")
			} else if err != nil {
				return 5, err
			}
			class.UserID = "|" + strconv.Itoa(userID) + "|"
			teachers, errCode, err := getTeacher(emails)
			if err != nil {
				return errCode, err
			}
			teacher := "|" + strings.Join(teachers, "||") + "|"
			_, err = db.Exec("insert into enrolledClasses values($1,$2,$3,$4,$5,$6,$7,$8,$9,$10,$11,$12)",
				class.UserID,
				class.ClassID,
				class.ClassName,
				teacher,
				class.Location,
				class.DaysOfWeek,
				class.StartTime,
				class.EndTime,
				class.StartDate,
				class.EndDate,
				class.Credits,
				class.Key,
			)
			if err != nil {
				return 16, err
			}
			return 200, err
		} else {
			uidDB = "|" + strconv.Itoa(userID) + "|"
			currentEnrolled = currentEnrolled + uidDB
			_, errCode, err := updateEnrolledClass(strconv.Itoa(key), "userID", currentEnrolled)
			if err != nil {
				return errCode, err
			} else {
				return 200, err
			}
		}
	} else if err != nil {
		return 5, err
	} else {
		return 5, errors.New("user has already enrolled in this course")
	}
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

	err = db.QueryRow("SELECT * from EnrolledClasses where key=$1", parameter).Scan(
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

	var teacherStr string

	defer rows.Close()
	var tmp string
	for rows.Next() {
		class = course{}

		err = rows.Scan(
			&tmp, &class.ClassID, &class.ClassName, &teacherStr, &class.Location, &class.DaysOfWeek,
			&class.StartTime, &class.EndTime, &class.StartDate, &class.EndDate, &class.Credits, &class.Key,
		)

		if err != nil {
			go logError(location, "1", err)
			return classes, 5, err
		}

		var teachers []string
		teacherStr = strings.Replace(teacherStr, "|", "", 1)
		teachers = strings.SplitAfter(teacherStr, "|")

		for currentTeacher := range teachers {
			teachers[currentTeacher] = strings.Replace(teachers[currentTeacher], "|", "", -1)
			if teachers[currentTeacher] != "" {

				class.Teacher = append(class.Teacher, teachers[currentTeacher])
			}
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
	var teacherStr string
	for rows.Next() {
		class = course{}

		err = rows.Scan(
			&tmp, &class.ClassID, &class.ClassName, &teacherStr,
			&class.StartTime, &class.EndTime, &class.StartDate, &class.EndDate, &class.Credits, &class.Key,
		)

		if err != nil {
			go logError(location, "1", err)
			return classes, 5, err
		}

		var teachers []string
		teacherStr = strings.Replace(teacherStr, "|", "", 1)
		teachers = strings.SplitAfter(teacherStr, "|")

		for currentTeacher := range teachers {
			teachers[currentTeacher] = strings.Replace(teachers[currentTeacher], "|", "", -1)
			if teachers[currentTeacher] != "" {

				class.Teacher = append(class.Teacher, teachers[currentTeacher])
			}
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

			users.POST("/enroll/", func(c *gin.Context) {
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

				//key := c.PostForm("key")
				key, _ := strconv.Atoi(c.PostForm("key"))
				errCode, err := addEnrolledClass(userID, key)

				if err != nil {
					currentError := createErrorStruct(errCode, c.Request.URL.String(), "4", err)
					c.JSON(500, currentError)
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

				c.JSON(200, gin.H{"classes": allCourses})
				//select all courses that are available to register for

			})
			//term, prefix (CSC), number (150), instructor last nam
			courses.GET("/search/:uuid", func(c *gin.Context) {
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

				q := c.Request.URL.Query()
				var commands []string

				if len(q["term"]) > 0 {
					param := "%" + q["term"][0] + "%"
					param = " term like \"" + param + "\""
					commands = append(commands, param)
				}

				if len(q["prefix"]) > 0 {
					param := "%" + q["prefix"][0] + "%"
					param = " courseID like \"" + param + "\""
					commands = append(commands, param)
				}
				if len(q["instructor"]) > 0 {
					var name string
					param := "%" + q["instructor"][0] + "%"
					rows, err := db.Query("select email from teachers where name like $1", param)

					if err != nil {
						currentError := createErrorStruct(5, c.Request.URL.String(), "1", err)
						c.JSON(500, currentError)
						return
					}
					teacher := "( "
					x := 0
					for rows.Next() {
						err = rows.Scan(&name)
						if err != nil {
							currentError := createErrorStruct(5, c.Request.URL.String(), "1", err)
							c.JSON(500, currentError)
							return
						}
						if x != 0 {
							param = " OR professorEmails like \"" + "%" + name + "%" + "\" "
						} else {
							param = "professorEmails like \"%" + name + "%\""
						}
						teacher = teacher + param
						x = x + 1
					}
					teacher = teacher + " )"
					commands = append(commands, teacher)

				}
				if len(q["number"]) > 0 {
					param := "%" + q["number"][0] + "%"
					param = " courseID like \"" + param + "\""
					commands = append(commands, param)
				}

				if len(q["startTime"]) > 0 {
					paramNum := q["startTime"][0]
					param := " timeStart >= " + paramNum
					commands = append(commands, param)
				}

				if len(q["endTime"]) > 0 {
					paramNum := q["endTime"][0]
					param := " timeEnd <= " + paramNum
					commands = append(commands, param)
				}

				if len(q["slotsAvailable"]) > 0 {
					paramNum := q["slotsAvailable"][0]
					param := " slotsAvailable >= " + paramNum
					commands = append(commands, param)
				}

				if len(q["key"]) > 0 {
					paramNum := q["key"][0]
					param := " key = " + paramNum
					commands = append(commands, param)
				}

				if len(q["open"]) > 0 {
					paramNum := q["open"][0]
					param := " open = " + paramNum
					commands = append(commands, param)
				}

				parameter := strings.Join(commands, " AND ")

				execute := "select * from availablecourses where " + parameter
				var allCourses []availableCourse
				rows, err := db.Query(execute)

				if err != nil {
					currentError := createErrorStruct(16, c.Request.URL.String(), "2", errors.New(execute))
					c.JSON(500, currentError)
					return
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
						currentError := createErrorStruct(5, c.Request.URL.String(), "2", err)
						c.JSON(500, currentError)
						return
					}

					teacher, errCode, err := getTeacher(class.ProfessorEmails)

					if err != nil {
						currentError := createErrorStruct(errCode, c.Request.URL.String(), "2", err)
						c.JSON(500, currentError)
						return
					}

					class.Teacher = teacher
					allCourses = append(allCourses, class)
				}

				c.JSON(200, gin.H{"classes": allCourses})

			})

		}
	}
	r.Run(":4200")
}
