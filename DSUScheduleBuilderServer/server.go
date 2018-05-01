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
	UserID string `json:"uid"`
	Key    int    `json:"key"`

	StartTime int `json:"startTime"`
	EndTime   int `json:"endTime"`
	Credits   int `json:"credits"`

	ClassID    string   `json:"classID"`
	ClassName  string   `json:"className"`
	Location   string   `json:"location"`
	DaysOfWeek string   `json:"daysOfWeek"`
	Teacher    []string `json:"teacher"`
	StartDate  string   `json:"startDate"`
	EndDate    string   `json:"endDate"`
}

type majorID struct {
	ID        int    `json:"majorID"`
	Name      string `json:"majorName"`
	MajorType string `json:"majorType"`
}

type enrolledCourse struct {
	RecommendedCourses []string `json:"recommended"`
	RequiredCourses    []string `json:"required"`
}

type previousCourse struct {
	CourseID   string `json:"courseID"`
	CourseName string `json:"courseName"`
	Credits    int    `json:"credits"`
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
	DaysOfWeek           string   `json:"daysOfWeek"`
	Key                  int      `json:"key"`
}

type club struct {
	ClubID      string `json:"clubID"`
	ClubName    string `json:"name"`
	StartTime   int    `json:"startTime"`
	EndTime     int    `json:"endTime"`
	DaysOfWeek  string `json:"daysOfWeek"`
	Location    string `json:"location"`
	Description string `json:"description"`
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

func newMajorID(size int) string {
	const letterBytes = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"

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
			errString := fmt.Sprintf("%s - %s, %s: %s\n", time.Now().Format("2006-01-02 15:04:05"), lErr.Location, lErr.Sublocation, lErr.Error)
			fmt.Println(errString)
			f.WriteString(errString)
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

func isAdmin(uid int) (bool, int, error) {
	var grouping string
	err := db.QueryRow("SELECT grouping from users where uid=$1", uid).Scan(&grouping)
	if err == sql.ErrNoRows {
		return false, 5, errors.New("Error user with this id does not exist")
	}
	if err != nil {
		return false, 5, err
	}
	if grouping != "admin" {
		return false, 27, errors.New("Error user with this id does not have access to this function")
	}
	return true, 200, err
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
		(fname,lname,email,password,majors,minors, grouping)
		values($1,$2,$3,$4,$5,$6,$7)`, fname, lname, email, password, "", "", "user",
	)

	if err != nil {
		go logError(location, "1", err)
		return 1, errors.New("Error inserting new user into database")
	}

	return 200, nil
}

func addMajor(userID int, major string, catalogYear string) (int, error) {
	majors := "%|" + major + "|%"
	var currentMajors string
	//check to make sure major exists (not made up)
	err := db.QueryRow("select id from MajorCodeMap where name=$1", major).Scan(&currentMajors)
	if err != sql.ErrNoRows {
		return 31, errors.New("major does not currently exist in the database: " + major)
	}

	//check if major already exits for this user
	err = db.QueryRow("select majors from users where majors like $1 and uid=$2", majors, userID).Scan(&currentMajors)

	if err != sql.ErrNoRows {
		return 14, errors.New("major already exists for that user")
	}
	//get the current list of majors
	err = db.QueryRow("select majors from users where uid=$1", userID).Scan(&currentMajors)

	var majorID string
	//store the id of that major
	err = db.QueryRow("SELECT id from MajorCodeMap where name=$1", major).Scan(&majorID)
	if err != nil {
		return 5, err
	}
	//get the currentUsers in that catalogYear
	var currentUsers string
	err = db.QueryRow("SELECT userID from CatalogYears where majorID=$1 and catalogYear=$2", majorID, catalogYear).Scan(&currentUsers)
	if err == sql.ErrNoRows {
		//find the biggest year and store the user in that one
		rows, err := db.Query("SELECT catalogYear from CatalogYears where majorID=$1", majorID)
		if err != nil {
			return 5, err
		}
		maxYear := -1
		var currentYear int
		var currentYearString string
		for rows.Next() {
			rows.Scan(&currentYearString)
			currentYear, err = strconv.Atoi(currentYearString)
			if err != nil {
				return 33, err
			}
			if maxYear < currentYear {
				maxYear = currentYear
			}
		}
		maxYearString := strconv.Itoa(maxYear)
		err = db.QueryRow("SELECT userID from CatalogYears where majorID=$1 and catalogYear=$2", majorID, maxYearString).Scan(&currentUsers)
		if err != nil {
			return 5, err
		}
		currentUsers = currentUsers + "|" + strconv.Itoa(userID) + "|"
		_, err = db.Exec("UPDATE CatalogYears set userID=$1 where majorID=$2 and catalogYear=$3", currentUsers, majorID, catalogYear)

	} else if err != nil {
		return 500, err
	} else {
		//store user in the year of their choosing
		currentUsers = currentUsers + "|" + strconv.Itoa(userID) + "|"
		_, err = db.Exec("UPDATE CatalogYears set userID=$1 where majorID=$2 and catalogYear=$3", currentUsers, majorID, catalogYear)
	}

	if err != nil {
		return 5, err
	}
	//add new major
	_, err = db.Exec("UPDATE users set majors=$1 where uid=$2", "|"+majorID+"|", userID)
	if err != nil {
		return 15, err
	}

	return 200, err
}

func deleteMajor(userID int, major string) (int, error) {
	majors := "%|" + major + "|%"
	var usersMajors string
	err := db.QueryRow("SELECT majors from users where uid=$1 AND majors like $2", userID, majors).Scan(&usersMajors)
	if err == sql.ErrNoRows {
		return 25, errors.New("User does not have this major to delete")
	} else if err != nil {
		return 5, err
	}
	usersMajors = strings.Replace(usersMajors, "|"+major+"|", "", 1)
	_, err = db.Exec("UPDATE users SET majors=$1 where uid=$2", usersMajors, userID)
	if err != nil {
		return 15, err
	}
	return 200, err
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
			&class.DaysOfWeek,
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
func addEnrolledClass(userID, key int) (enrolledCourse, int, error) {
	//check if user is already enrolled for this course
	var currentEnrolled string
	var uidDB string
	var reqrec enrolledCourse
	uidDB = "%|" + strconv.Itoa(userID) + "|%"
	err := db.QueryRow("SELECT userID from EnrolledClasses where key=$1 and userID like $2", key, uidDB).Scan(&currentEnrolled)
	if err == sql.ErrNoRows {
		//make sure the user has the required prereqs for the classes
		var prereqAnd string
		var prereqOr string
		err = db.QueryRow("SELECT prereqCoursesAnd,prereqCoursesOr from availableCourses where key=$1", key).Scan(&prereqAnd, &prereqOr)
		if err != nil {
			return reqrec, 29, err
		}
		andCourses := strings.Split(prereqAnd, "|")
		var tmp string
		orCourses := strings.Split(prereqOr, "|")
		for x := range andCourses {
			andCourses[x] = strings.Replace(andCourses[x], "|", "", -1)
			if andCourses[x] != "" {
				err = db.QueryRow("SELECT userID from PreviousClasses where courseID=$1 and userID like $2", andCourses[x], "%|"+strconv.Itoa(userID)+"|%").Scan(&tmp)
				if err == sql.ErrNoRows {
					return reqrec, 5, errors.New("User has not taken the required course: " + andCourses[x])
				}
				if err != nil {
					return reqrec, 5, err
				}
			}
		}
		isOr := false
		prereqOr = strings.Replace(prereqOr, "|", "", -1)
		if prereqOr == "" {
			isOr = true
		}
		var prereqCoursesOr string
		for x := range orCourses {
			orCourses[x] = strings.Replace(orCourses[x], "|", "", -1)
			if orCourses[x] != "" {
				if prereqCoursesOr == "" {
					prereqCoursesOr = prereqCoursesOr + " " + orCourses[x]
				} else {
					prereqCoursesOr = prereqCoursesOr + ", " + orCourses[x]

				}
				err = db.QueryRow("SELECT userID from PreviousClasses where courseID=$1 and userID like $2", andCourses[x], "%|"+strconv.Itoa(userID)+"|%").Scan(&tmp)
				if err == nil {
					isOr = true
				}
				if err != sql.ErrNoRows && err != nil {
					return reqrec, 5, err
				}
			}
		}
		if isOr == false {
			return reqrec, 29, errors.New("User has not taken any of these required courses:" + prereqCoursesOr)
		}
		var RequiredCourse, RecommendedCourse string
		err = db.QueryRow("SELECT reqConcurrentCourses,recConcurrentCourses from availableCourses where key=$1", key).Scan(&RequiredCourse, &RecommendedCourse)
		if err != nil {
			return reqrec, 5, err
		}
		reqC := strings.Split(RequiredCourse, "|")
		recC := strings.Split(RecommendedCourse, "|")
		for x := range reqC {
			reqC[x] = strings.Replace(reqC[x], "|", "", -1)
			if reqC[x] != "" {
				reqrec.RequiredCourses = append(reqrec.RequiredCourses, reqC[x])
			}
		}
		for x := range recC {
			recC[x] = strings.Replace(recC[x], "|", "", -1)
			if recC[x] != "" {
				reqrec.RecommendedCourses = append(reqrec.RecommendedCourses, recC[x])
			}
		}
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
				&class.StartTime,
				&class.EndTime,
				&class.StartDate,
				&class.EndDate,
				&class.Credits,
				&class.Key,
			)
			if err == sql.ErrNoRows {
				return reqrec, 5, errors.New("Class with this key does not exist in availableClasses")
			} else if err != nil {
				return reqrec, 5, err
			}
			var timeStart int
			var timeEnd int
			err = db.QueryRow("SELECT timeStart,timeEnd from availableCourses where key=$1", key).Scan(&timeStart, &timeEnd)
			if err != nil {
				return reqrec, 5, err
			}
			//skip if online class
			if timeStart != 0 && timeEnd != 0 {
				//find what days the new class is
				days := strings.Split(class.DaysOfWeek, "|")
				var daysOfWeek []string
				for x := range days {
					days[x] = strings.Replace(days[x], "|", "", -1)
					if days[x] != "" {
						daysOfWeek = append(daysOfWeek, "%"+days[x]+"%")
					}
				}
				for x := 0; x < 6; x = x + 1 {
					if x >= len(daysOfWeek) {
						daysOfWeek = append(daysOfWeek, "ZZZZ")
					}
				}
				var classID string
				err = db.QueryRow("SELECT classID from enrolledClasses where ((startTime >= $1 AND endTime <= $2) OR (startTime >= $1 AND startTime <= $2) OR (endTime >= $1 AND endTime <= $2)) and ((daysOfWeek like $3) or (daysOfWeek like $4) or (daysOfWeek like $5) or (daysOfWeek like $6) or (daysOfWeek like $7)) and userID like $8",
					timeStart, timeEnd, daysOfWeek[0], daysOfWeek[1], daysOfWeek[2], daysOfWeek[3], daysOfWeek[4], uidDB).Scan(&classID)
				if err != sql.ErrNoRows {
					if err != nil {
						return reqrec, 5, err
					}
					return reqrec, 19, errors.New("Class conflicts with " + classID)
				}
			}
			class.UserID = "|" + strconv.Itoa(userID) + "|"
			teachers, errCode, err := getTeacher(emails)
			if err != nil {
				return reqrec, errCode, err
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
				return reqrec, 16, err
			}
			return reqrec, 200, err
		}
		var timeStart int
		var timeEnd int
		var days string
		err = db.QueryRow("SELECT startTime,endTime,daysOfWeek from enrolledClasses where key=$1 AND userID like $2", key, uidDB).Scan(&timeStart, &timeEnd, &days)
		if timeStart != 0 && timeEnd != 0 {
			//find what days the new class is
			days := strings.Split(days, "|")
			var daysOfWeek []string
			for x := range days {
				days[x] = strings.Replace(days[x], "|", "", -1)
				if days[x] != "" {
					daysOfWeek = append(daysOfWeek, "%"+days[x]+"%")
				}
			}
			for x := 0; x < 6; x = x + 1 {
				if x >= len(daysOfWeek) {
					daysOfWeek = append(daysOfWeek, "ZZZZ")
				}
			}
			var classID string
			err = db.QueryRow("SELECT classID from enrolledClasses where ((startTime >= $1 AND endTime <= $2) OR (startTime >= $1 AND startTime <= $2) OR (endTime >= $1 AND endTime <= $2)) AND ((daysOfWeek like $3) or (daysOfWeek like $4) or (daysOfWeek like $5) or (daysOfWeek like $6) or (daysOfWeek like $7)) and userID like $8",
				timeStart, timeEnd, daysOfWeek[0], daysOfWeek[1], daysOfWeek[2], daysOfWeek[3], daysOfWeek[4], uidDB).Scan(&classID)
			if err != sql.ErrNoRows {
				if err != nil {
					return reqrec, 5, err
				}
				return reqrec, 19, errors.New("Class conflicts with " + classID)
			}
		}
		uidDB = "|" + strconv.Itoa(userID) + "|"
		currentEnrolled = currentEnrolled + uidDB
		errCode, err := updateEnrolledClass(strconv.Itoa(key), "userID", currentEnrolled)
		if err != nil {
			return reqrec, errCode, err
		}
		return reqrec, 200, err
	} else if err != nil {
		return reqrec, 5, err
	} else {
		return reqrec, 5, errors.New("user has already enrolled in this course")
	}
}

func deleteEnrolledClass(key int) (int, error) {
	var err error
	_, err = db.Exec("DELETE FROM EnrolledClasses WHERE key=$1", key)
	if err == nil {
		return 200, err
	}
	return 22, err
}

//updates an entry in the enrolledclasses table, although we cannot allow to change the userID
func updateEnrolledClass(classKey, keyword, newValue string) (int, error) {
	var err error
	var parameter string
	parameter = "UPDATE EnrolledClasses SET " + keyword + "=\"" + newValue + "\" WHERE key=" + classKey
	_, err = db.Exec(parameter)
	if err != nil {
		return 12, err
	}

	return 200, err
}

func getEnrolledClasses(uid int) ([]course, int, error) {
	location := "getEnrolledClasses"
	var classes []course
	var class course
	parameter := "%|" + strconv.Itoa(uid) + "|%"
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

func getPreviousClasses(uid int) ([]previousCourse, int, error) {
	rows, err := db.Query("SELECT courseID,courseName,credits from PreviousClasses where userID like $1", "%|"+strconv.Itoa(uid)+"|%")
	if err == sql.ErrNoRows {
		return nil, 5, errors.New("User does not have any previous classes")
	}
	if err != nil {
		return nil, 5, err
	}

	var previousClasses []previousCourse
	var previousTMP previousCourse
	for rows.Next() {
		rows.Scan(&previousTMP.CourseID, &previousTMP.CourseName, &previousTMP.Credits)
		previousClasses = append(previousClasses, previousTMP)
	}

	return previousClasses, 200, err
}

func init() {
	errorChannel = make(chan locationalError)

	var err error
	rand.Seed(time.Now().UnixNano())

	db, err = sql.Open("sqlite3", "./userDatabase.db?_busy_timeout=5000")
	if err != nil {
		panic(err)
	}

	//	make sure that no user sessions exist when starting the server

	//_, err = db.Exec("DELETE FROM USER_SESSIONS WHERE 1=1")
	//if err != nil {
	//	panic(err)
	//}
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

		api.GET("/majors/:uuid", func(c *gin.Context) {
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
			rows, err := db.Query("SELECT * from Majors")
			var majorsList []majorID

			if err != nil {
				currentError := createErrorStruct(5, c.Request.URL.String(), "1", err)
				c.JSON(500, currentError)
				return
			}

			for rows.Next() {
				var major majorID
				rows.Scan(&major.ID, &major.Name, &major.MajorType)
				majorsList = append(majorsList, major)
			}

			c.JSON(200, gin.H{"majors": majorsList})
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
			users.POST("/changePassword/", func(c *gin.Context) {
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
				currentPassword := c.PostForm("currentPassword")
				newPassword := c.PostForm("newPassword")

				//select current password and make sure they match
				var userPassword string
				err = db.QueryRow("select password from users where uid=$1", userID).Scan(&userPassword)
				if err != nil {
					currentError := createErrorStruct(20, c.Request.URL.String(), "1", err)
					c.JSON(500, currentError)
					return
				}
				if userPassword != currentPassword {
					currentError := createErrorStruct(20, c.Request.URL.String(), "1", errors.New("Password does match users current password"))
					c.JSON(500, currentError)
					return
				}

				_, err = db.Exec("UPDATE users SET password=$1 where uid=$2", newPassword, userID)
				if err != nil {
					currentError := createErrorStruct(20, c.Request.URL.String(), "1", err)
					c.JSON(500, currentError)
					return
				}
				c.JSON(200, gin.H{"success": 1})
			})

			users.POST("/delete/", func(c *gin.Context) {
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
				//log the user out
				_, err = db.Exec("DELETE FROM USER_SESSIONS WHERE uuid=$1", uuid)

				if err != nil {
					currentError := createErrorStruct(10, c.Request.URL.String(), "2", err)
					c.JSON(500, currentError)
					return
				}
				param := "%|" + strconv.Itoa(userID) + "|%"

				//delete from CatalogYears where user exists
				rows, err := db.Query("SELECT userID,majorID,catalogYear from CatalogYears where userID like $1", param)
				var currentMajorID string
				var currentCatalogYear string
				var currentUserID string
				var MajorID []string
				var CatalogYear []string
				var UsersID []string
				for rows.Next() {
					rows.Scan(&currentUserID, &currentMajorID, &currentCatalogYear)
					currentUserID = strings.Replace(currentUserID, "|"+strconv.Itoa(userID)+"|", "", -1)
					MajorID = append(MajorID, currentMajorID)
					CatalogYear = append(CatalogYear, currentCatalogYear)
					UsersID = append(UsersID, currentUserID)
				}
				for x := range UsersID {
					execute := "UPDATE CatalogYears SET userID=\"" + UsersID[x] + "\" WHERE majorID=\"" + MajorID[x] + "\"" + " and catalogYear=\"" + CatalogYear[x] + "\""
					_, err = db.Exec(execute)
					if err != nil {
						currentError := createErrorStruct(21, c.Request.URL.String(), "7", err)
						c.JSON(500, currentError)
						return
					}
				}

				//delete from  previousClasses
				_, err = db.Exec("DELETE from PreviousClasses where userID=$1", "|"+strconv.Itoa(userID)+"|")
				if err != nil {
					currentError := createErrorStruct(24, c.Request.URL.String(), "5", err)
					c.JSON(500, currentError)
					return
				}
				rows, err = db.Query("SELECT userID,courseID from PreviousClasses where userID like $1", param)
				if err != sql.ErrNoRows && err != nil {
					currentError := createErrorStruct(5, c.Request.URL.String(), "5", err)
					c.JSON(500, currentError)
					return
				}
				var PreviousUpdateCourse []string
				var PreviousUpdateID []string
				for rows.Next() {
					var newUserID string
					var courseID string
					rows.Scan(&newUserID, &courseID)
					newUserID = strings.Replace(newUserID, "|"+strconv.Itoa(userID)+"|", "", -1)
					PreviousUpdateCourse = append(PreviousUpdateCourse, courseID)
					PreviousUpdateID = append(PreviousUpdateID, newUserID)
				}
				for x := range PreviousUpdateID {
					execute := "UPDATE PreviousClasses SET userID=\"" + PreviousUpdateID[x] + "\" WHERE courseID=\"" + PreviousUpdateCourse[x] + "\""
					_, err = db.Exec(execute)
					if err != nil {
						currentError := createErrorStruct(21, c.Request.URL.String(), "7", err)
						c.JSON(500, currentError)
						return
					}
				}

				//delete from enrolledClasses
				_, err = db.Exec("DELETE from EnrolledClasses where userID=$1", "|"+strconv.Itoa(userID)+"|")
				if err != nil {
					currentError := createErrorStruct(24, c.Request.URL.String(), "5", err)
					c.JSON(500, currentError)
					return
				}
				rows, err = db.Query("SELECT userID,key from EnrolledClasses where userID like $1", param)
				if err != sql.ErrNoRows && err != nil {
					currentError := createErrorStruct(5, c.Request.URL.String(), "5", err)
					c.JSON(500, currentError)
					return
				}
				var enrolledUpdateKey []int
				var enrolledUpdateID []string
				for rows.Next() {
					var newUserID string
					var key int
					rows.Scan(&newUserID, &key)
					newUserID = strings.Replace(newUserID, "|"+strconv.Itoa(userID)+"|", "", -1)
					enrolledUpdateKey = append(enrolledUpdateKey, key)
					enrolledUpdateID = append(enrolledUpdateID, newUserID)
				}
				for x := range enrolledUpdateID {
					execute := "UPDATE EnrolledClasses SET userID=\"" + enrolledUpdateID[x] + "\" WHERE key=" + strconv.Itoa(enrolledUpdateKey[x])
					_, err = db.Exec(execute)
					if err != nil {
						currentError := createErrorStruct(21, c.Request.URL.String(), "7", err)
						c.JSON(500, currentError)
						return
					}
				}
				//delete from users
				_, err = db.Exec("DELETE from users where uid=$1", userID)
				if err != nil {
					currentError := createErrorStruct(24, c.Request.URL.String(), "7", err)
					c.JSON(500, currentError)
					return
				}
				c.JSON(200, gin.H{"success": 1})
			})
			users.POST("/dropEnrolledCourse", func(c *gin.Context) {
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
				courseID := c.PostForm("courseID")
				param := "%|" + strconv.Itoa(userID) + "|%"
				var currentCourseID string
				//check to make sure user is enrolled in the course
				err = db.QueryRow("SELECT userID from EnrolledClasses where userID like $1 and key=$2", param, courseID).Scan(&currentCourseID)
				if err == sql.ErrNoRows {
					currentError := createErrorStruct(26, c.Request.URL.String(), "6", errors.New("User in not enrolled in this class"))
					c.JSON(500, currentError)
					return
				} else if err != nil {
					currentError := createErrorStruct(5, c.Request.URL.String(), "20", err)
					c.JSON(500, currentError)
					return
				}
				newUserID := strings.Replace(currentCourseID, "|"+strconv.Itoa(userID)+"|", "", -1)
				if newUserID == "" {
					_, err = db.Exec("DELETE from EnrolledClasses where key=$1", courseID)
					if err != nil {
						currentError := createErrorStruct(22, c.Request.URL.String(), "8", err)
						c.JSON(500, currentError)
						return
					}
				} else {
					_, err = db.Exec("UPDATE EnrolledClasses SET userID=$1 where key=$2", newUserID, courseID)
					if err != nil {
						currentError := createErrorStruct(23, c.Request.URL.String(), "8", err)
						c.JSON(500, currentError)
						return
					}
				}
				c.JSON(200, gin.H{"success": 1})
			})
			users.GET("/totalCredits/:uuid", func(c *gin.Context) {
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

				rows, err := db.Query("SELECT credits from PreviousClasses where userID like $1", "%|"+strconv.Itoa(userID)+"|%")
				if err != nil {
					currentError := createErrorStruct(3, c.Request.URL.String(), "10", err)
					c.JSON(500, currentError)
					return
				}
				var totalCredits, tmp int
				for rows.Next() {
					rows.Scan(&tmp)
					totalCredits = totalCredits + tmp
				}

				c.JSON(200, gin.H{"totalCredits": totalCredits})

			})

			users.GET("/userClubs/:uuid", func(c *gin.Context) {
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

				rows, err := db.Query("SELECT clubID,clubName,startTime,endTime,location,description,daysOfWeek from Clubs where userID like $1", "%|"+strconv.Itoa(userID)+"|%")
				if err != nil && err != sql.ErrNoRows {
					currentError := createErrorStruct(3, c.Request.URL.String(), "11", err)
					c.JSON(500, currentError)
					return
				}
				var userClubs []club
				var currentClub club
				for rows.Next() {
					rows.Scan(&currentClub.ClubID, &currentClub.ClubName, &currentClub.StartTime, &currentClub.EndTime, &currentClub.Location, &currentClub.Description, &currentClub.DaysOfWeek)
					userClubs = append(userClubs, currentClub)
				}

				c.JSON(200, gin.H{"clubs": userClubs})

			})

			users.POST("/addClub/", func(c *gin.Context) {
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

				clubID := c.PostForm("clubID")
				//make sure the club exists
				var currentUserID string
				err = db.QueryRow("SELECT userID from Clubs where clubID=$1", clubID).Scan(&currentUserID)
				if err == sql.ErrNoRows {
					currentError := createErrorStruct(3, c.Request.URL.String(), "1", errors.New("Club with that ID does not exist"))
					c.JSON(500, currentError)
					return
				}
				if err != nil {
					currentError := createErrorStruct(3, c.Request.URL.String(), "1", err)
					c.JSON(500, currentError)
					return
				}

				//make sure the user is not already in that club
				err = db.QueryRow("SELECT userID from Clubs where clubID=$1 and userID like $2", clubID, "%|"+strconv.Itoa(userID)+"|%").Scan(&currentUserID)
				if err != sql.ErrNoRows {
					if err != nil {
						currentError := createErrorStruct(3, c.Request.URL.String(), "1", err)
						c.JSON(500, currentError)
						return
					}
					currentError := createErrorStruct(3, c.Request.URL.String(), "1", errors.New("User is already enlisted in this club"))
					c.JSON(500, currentError)
					return
				}
				//check to make sure the club does not conflict with any classes the user is enrolled in or other clubs
				//check classes first
				var clubStartTime, clubEndTime int
				var clubDaysOfWeek string
				err = db.QueryRow("SELECT startTime,endTime,daysOfWeek from Clubs where clubID=$1", clubID).Scan(&clubStartTime, &clubEndTime, &clubDaysOfWeek)
				if err != nil {
					currentError := createErrorStruct(3, c.Request.URL.String(), "3", err)
					c.JSON(500, currentError)
					return
				}

				//make the days a slice of actual days
				daysSlice := strings.Split(clubDaysOfWeek, "|")
				var daysOfWeek []string
				for x := range daysSlice {
					daysSlice[x] = strings.Replace(daysSlice[x], "|", "", -1)
					if daysSlice[x] != "" {
						daysOfWeek = append(daysOfWeek, daysSlice[x])
					}
				}
				for x := 0; x < 6; x = x + 1 {
					if x >= len(daysOfWeek) {
						daysOfWeek = append(daysOfWeek, "ZZZZ")
					}
				}

				//need a startTime, endTime, slice of size 5 (each for a day of the week)
				err = db.QueryRow("SELECT classID from enrolledClasses where ((startTime >= $1 AND endTime <= $2) OR (startTime >= $1 AND startTime <= $2) OR (endTime >= $1 AND endTime <= $2)) and ((daysOfWeek like $3) or (daysOfWeek like $4) or (daysOfWeek like $5) or (daysOfWeek like $6) or (daysOfWeek like $7)) and userID like $8",
					clubStartTime, clubEndTime, daysOfWeek[0], daysOfWeek[1], daysOfWeek[2], daysOfWeek[3], daysOfWeek[4], "%|"+strconv.Itoa(userID)+"%|").Scan(&currentUserID)
				if err != sql.ErrNoRows {
					if err != nil {
						currentError := createErrorStruct(3, c.Request.URL.String(), "3", err)
						c.JSON(500, currentError)
						return
					}
					currentError := createErrorStruct(6, c.Request.URL.String(), "13", errors.New("Error this club conflicts with "+currentUserID))
					c.JSON(500, currentError)
					return
				}

				//now check for clubs
				err = db.QueryRow("SELECT clubName from Clubs where ((startTime >= $1 AND endTime <= $2) OR (startTime >= $1 AND startTime <= $2) OR (endTime >= $1 AND endTime <= $2)) and ((daysOfWeek like $3) or (daysOfWeek like $4) or (daysOfWeek like $5) or (daysOfWeek like $6) or (daysOfWeek like $7)) and userID like $8",
					clubStartTime, clubEndTime, daysOfWeek[0], daysOfWeek[1], daysOfWeek[2], daysOfWeek[3], daysOfWeek[4], "%|"+strconv.Itoa(userID)+"%|").Scan(&currentUserID)
				if err != sql.ErrNoRows {
					if err != nil {
						currentError := createErrorStruct(3, c.Request.URL.String(), "3", err)
						c.JSON(500, currentError)
						return
					}
					currentError := createErrorStruct(6, c.Request.URL.String(), "13", errors.New("Error this club conflicts with "+currentUserID))
					c.JSON(500, currentError)
					return
				}

				//Now we can actually allow the user to sign up for the club
				err = db.QueryRow("SELECT userID from Clubs where clubID=$1", clubID).Scan(&currentUserID)
				if err != nil {
					currentError := createErrorStruct(3, c.Request.URL.String(), "3", err)
					c.JSON(500, currentError)
					return
				}
				currentUserID = currentUserID + "|" + strconv.Itoa(userID) + "|"
				_, err = db.Exec("UPDATE Clubs set userID=\"" + currentUserID + "\" where clubID=\"" + clubID + "\"")
				if err != nil {
					currentError := createErrorStruct(3, c.Request.URL.String(), "3", err)
					c.JSON(500, currentError)
					return
				}

				c.JSON(200, gin.H{"success": 1})

			})

			users.POST("/dropClub/", func(c *gin.Context) {
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
				clubID := c.PostForm("clubID")
				var currentUserID string
				//make sure the club exists
				err = db.QueryRow("SELECT userID from Clubs where clubID=$1", clubID).Scan(&currentUserID)
				if err != nil {
					if err == sql.ErrNoRows {
						currentError := createErrorStruct(3, c.Request.URL.String(), "4", errors.New("Club with this ID does not exist"))
						c.JSON(500, currentError)
						return
					}
					currentError := createErrorStruct(3, c.Request.URL.String(), "1", err)
					c.JSON(500, currentError)
					return
				}
				//check to make sure that the user is in the club
				err = db.QueryRow("SELECT userID from Clubs where clubID=$1 and userID like $2", clubID, "%|"+strconv.Itoa(userID)+"|%").Scan(&currentUserID)
				if err != nil {
					if err == sql.ErrNoRows {
						currentError := createErrorStruct(3, c.Request.URL.String(), "4", errors.New("User is not currently listed for this club"))
						c.JSON(500, currentError)
						return
					}
					currentError := createErrorStruct(3, c.Request.URL.String(), "1", err)
					c.JSON(500, currentError)
					return
				}
				//now remove the user from the list of users in the club
				currentUserID = strings.Replace(currentUserID, "|"+strconv.Itoa(userID)+"|", "", -1)
				//update the db
				_, err = db.Exec("UPDATE Clubs set userID=\"" + currentUserID + "\" where clubID=\"" + clubID + "\"")
				if err != nil {
					currentError := createErrorStruct(3, c.Request.URL.String(), "1", err)
					c.JSON(500, currentError)
					return
				}

				c.JSON(200, gin.H{"success": 1})
			})

			users.POST("/addPrevious/", func(c *gin.Context) {
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
				courseID := c.PostForm("courseID")

				var previousCourses string
				//check to make sure user has not already have this course listed
				err = db.QueryRow("SELECT courseName from PreviousClasses where userID like $1 and courseID=$2", "%|"+strconv.Itoa(userID)+"|%", courseID).Scan(&previousCourses)
				if err != sql.ErrNoRows {
					if err != nil {
						currentError := createErrorStruct(5, c.Request.URL.String(), "3", err)
						c.JSON(500, currentError)
						return
					}
					currentError := createErrorStruct(28, c.Request.URL.String(), "2", errors.New("User has already listed this course"))
					c.JSON(500, currentError)
					return
				}
				//see if the course with this ID already exists
				err = db.QueryRow("SELECT userID from PreviousClasses where courseID=$1", courseID).Scan(&previousCourses)
				if err != sql.ErrNoRows && err != nil {
					currentError := createErrorStruct(5, c.Request.URL.String(), "8", err)
					c.JSON(500, currentError)
					return
				}
				if err == sql.ErrNoRows {
					//insert the new course
					courseName := c.PostForm("courseName")
					credits := c.PostForm("credits")
					_, err = db.Exec("INSERT into PreviousClasses (userID,courseID,courseName,credits) values($1,$2,$3,$4)", "|"+strconv.Itoa(userID)+"|", courseID, courseName, credits)
					if err != nil {
						currentError := createErrorStruct(30, c.Request.URL.String(), "5", err)
						c.JSON(500, currentError)
						return
					}
				} else {
					//update the userID of the course
					previousCourses = previousCourses + "|" + strconv.Itoa(userID) + "|"
					_, err = db.Exec("UPDATE PreviousClasses set userID=$1 where courseID=$2", previousCourses, courseID)
					if err != nil {
						currentError := createErrorStruct(31, c.Request.URL.String(), "6", err)
						c.JSON(500, currentError)
						return
					}
				}

				c.JSON(200, gin.H{"success": 1})
			})

			users.POST("/deletePrevious/", func(c *gin.Context) {
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

				courseID := c.PostForm("courseID")

				//make sure that the course exists in the users previousCourses column
				var previousCourses string
				err = db.QueryRow("SELECT userID from PreviousClasses where userID like $1 and courseID=$2", "%|"+strconv.Itoa(userID)+"|%", courseID).Scan(&previousCourses)

				if err == sql.ErrNoRows {
					currentError := createErrorStruct(3, c.Request.URL.String(), "6", errors.New("This class does exist in the users previously taken courses"))
					c.JSON(500, currentError)
					return
				}
				if err != nil {
					currentError := createErrorStruct(3, c.Request.URL.String(), "7", err)
					c.JSON(500, currentError)
					return
				}

				previousCourses = strings.Replace(previousCourses, "|"+strconv.Itoa(userID)+"|", "", -1)

				_, err = db.Exec("UPDATE PreviousClasses SET userID=$1 where courseID=$2", previousCourses, courseID)
				if err != nil {
					currentError := createErrorStruct(15, c.Request.URL.String(), "4", err)
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
				reqrec, errCode, err := addEnrolledClass(userID, key)

				if err != nil {
					currentError := createErrorStruct(errCode, c.Request.URL.String(), "4", err)
					c.JSON(500, currentError)
					return
				}
				c.JSON(200, gin.H{"classes": reqrec})
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
				catalogYear := c.PostForm("catalogYear")
				errCode, err := addMajor(userID, major, catalogYear)

				if err != nil {
					currentError := createErrorStruct(errCode, c.Request.URL.String(), "4", err)
					c.JSON(500, currentError)
					return
				}

				c.JSON(200, gin.H{"success": 1})
			})

			users.POST("/deleteMajor/", func(c *gin.Context) {
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
				errCode, err := deleteMajor(userID, major)

				if err != nil {
					currentError := createErrorStruct(errCode, c.Request.URL.String(), "1", err)
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
				err := db.QueryRow("SELECT fname,lname,majors,minors,email,password,uid FROM USERS WHERE email=$1", email).Scan(&user.Fname, &user.Lname, &user.Majors, &user.Minors, &user.Email, &userPassword, &user.UID)

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
		clubs := api.Group("clubs")
		{

			clubs.GET("/allClubs/:uuid", func(c *gin.Context) {
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

				rows, err := db.Query("SELECT clubID,clubName,startTime,endTime,location,description,daysOfWeek from Clubs")
				if err != nil {
					currentError := createErrorStruct(3, c.Request.URL.String(), "1", err)
					c.JSON(500, currentError)
					return
				}

				var allClubs []club
				for rows.Next() {
					var currentClub club
					rows.Scan(&currentClub.ClubID, &currentClub.ClubName, &currentClub.StartTime, &currentClub.EndTime, &currentClub.Location, &currentClub.Description)
					allClubs = append(allClubs, currentClub)
				}
				c.JSON(200, gin.H{"clubs": allClubs})
			})

			clubs.GET("/search/:uuid", func(c *gin.Context) {

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

				classesReturn, errCode, err := getPreviousClasses(userID)
				if err != nil {
					currentError := createErrorStruct(errCode, c.Request.URL.String(), "5", err)
					c.JSON(500, currentError)
					return
				}
				c.JSON(200, gin.H{"classes": classesReturn})
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
						&class.DaysOfWeek,
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
		admin := api.Group("/admin")
		{
			admin.POST("/updateEnrolled/", func(c *gin.Context) {
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

				admin, errCode, err := isAdmin(userID)
				if admin == false {
					currentError := createErrorStruct(errCode, c.Request.URL.String(), "4", errors.New("User attempted to log in as admin USERID: "+strconv.Itoa(userID)))
					c.JSON(500, currentError)
					return
				}

				classKey := c.PostForm("classKey")
				keyword := c.PostForm("keyword")
				newValue := c.PostForm("newValue")

				errno, err := updateEnrolledClass(classKey, keyword, newValue)

				if err != nil {
					currentError := createErrorStruct(errno, c.Request.URL.String(), "2", err)
					c.JSON(500, currentError)
					return
				}

				c.JSON(200, gin.H{"success": 1})
			})
			// CSC150 # CSC-2017|CSC-2018|CO-2017
			// CIS3XX|CIS4XX # CSC-2017|CSC-2018|CO-2017
			//names of majors should always be sent all lowercase

			admin.POST("/newMajor/", func(c *gin.Context) {
				uuid := c.PostForm("uuid")
				majorName := c.PostForm("majorName")
				//catalogYear := c.PostForm("catalogYear")

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

				admin, errCode, err := isAdmin(userID)
				if admin == false {
					currentError := createErrorStruct(errCode, c.Request.URL.String(), "4", errors.New("User attempted to log in as admin USERID: "+strconv.Itoa(userID)))
					c.JSON(500, currentError)
					return
				}
				//make sure major does not already exist
				var existingMajor string
				err = db.QueryRow("select id from MajorCodeMap where name=$1", majorName).Scan(&existingMajor)
				if err != sql.ErrNoRows {
					if err != nil {
						currentError := createErrorStruct(3, c.Request.URL.String(), "9", err)
						c.JSON(500, currentError)
						return
					}
					currentError := createErrorStruct(3, c.Request.URL.String(), "1", errors.New("Major already exists with this name"))
					c.JSON(500, currentError)
					return
				}

				//generate new and unique 3 letter id for the new major
				newMajorCode := newMajorID(3)
				err = db.QueryRow("SELECT name from MajorCodeMap where id=$1", newMajorCode).Scan(&existingMajor)
				if err != nil && err != sql.ErrNoRows {
					currentError := createErrorStruct(3, c.Request.URL.String(), "1", err)
					c.JSON(500, currentError)
					return
				}
				for err != sql.ErrNoRows {
					newMajorCode = newMajorID(3)
					err = db.QueryRow("SELECT name from MajorCodeMap where id=$1", newMajorCode).Scan(&existingMajor)
					if err != nil && err != sql.ErrNoRows {
						currentError := createErrorStruct(3, c.Request.URL.String(), "1", err)
						c.JSON(500, currentError)
						return
					}
				}

				_, err = db.Exec("Insert into MajorCodeMap (id,name) values($1,$2)", newMajorCode, majorName)
				if err != nil {
					currentError := createErrorStruct(3, c.Request.URL.String(), "1", err)
					c.JSON(500, currentError)
					return
				}

				c.JSON(200, gin.H{"success": 1})

			})

			admin.POST("/addCatalogYear/", func(c *gin.Context) {
				uuid := c.PostForm("uuid")
				majorName := c.PostForm("majorName")
				catalogYear := c.PostForm("catalogYear")
				//classes := c.PostForm("classes")
				//list of classes

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

				admin, errCode, err := isAdmin(userID)
				if admin == false {
					currentError := createErrorStruct(errCode, c.Request.URL.String(), "4", errors.New("User attempted to log in as admin USERID: "+strconv.Itoa(userID)))
					c.JSON(500, currentError)
					return
				}
				var majorsID string
				//make sure the major exists
				err = db.QueryRow("Select id from MajorCodeMap where name=$1", majorName).Scan(&majorsID)
				if err == sql.ErrNoRows {
					currentError := createErrorStruct(3, c.Request.URL.String(), "9", errors.New("Major does not exist"))
					c.JSON(500, currentError)
					return
				}
				if err != nil {
					currentError := createErrorStruct(3, c.Request.URL.String(), "19", err)
					c.JSON(500, currentError)
					return
				}

				//make sure the catalogYear does not already exist
				var existingMajor string
				err = db.QueryRow("select id from MajorCodeMap where catalogYears like $1 and name=$2", "%|"+catalogYear+"|%", majorName).Scan(&existingMajor)
				if err != sql.ErrNoRows {
					if err != nil {
						currentError := createErrorStruct(3, c.Request.URL.String(), "10", err)
						c.JSON(500, currentError)
						return
					}
					currentError := createErrorStruct(3, c.Request.URL.String(), "11", errors.New("This catalog year aready exists for this year"))
					c.JSON(500, currentError)
					return
				}

				err = db.QueryRow("select courseID from CatalogYears where majorID=$1 and catalogYear=$2", majorsID, catalogYear).Scan(&existingMajor)
				if err != sql.ErrNoRows {
					if err != nil {
						currentError := createErrorStruct(3, c.Request.URL.String(), "9", err)
						c.JSON(500, currentError)
						return
					}
					currentError := createErrorStruct(3, c.Request.URL.String(), "1", errors.New("This catalog year aready exists for this year"))
					c.JSON(500, currentError)
					return
				}

				//add the catalogYear to the majorCodeMap table
				var currentCatalogYears string
				err = db.QueryRow("SELECT catalogYears from MajorCodeMap where name=$1", majorName).Scan(&currentCatalogYears)
				if err != nil {
					currentError := createErrorStruct(3, c.Request.URL.String(), "12", err)
					c.JSON(500, currentError)
					return
				}
				currentCatalogYears = currentCatalogYears + "|" + catalogYear + "|"
				_, err = db.Exec("UPDATE MajorCodeMap set catalogYears=$1 where name=$2", currentCatalogYears, majorName)
				if err != nil {
					currentError := createErrorStruct(3, c.Request.URL.String(), "13", err)
					c.JSON(500, currentError)
					return
				}

				//add catalog year to the CatalogYear table
				_, err = db.Exec("INSERT into CatalogYears (majorID,catalogYear,userID) values($1,$2,$3)", majorsID, catalogYear, "")
				if err != nil {
					currentError := createErrorStruct(3, c.Request.URL.String(), "14", err)
					c.JSON(500, currentError)
					return
				}

				//add classes to db
				//for x := range classes {
				//	var tmp string
				//	err = db.QueryRow("SELECT ")
				//}

			})

			admin.POST("/updateCatalogYear/", func(c *gin.Context) {

			})

		}
	}
	r.Run(":4200")
}
