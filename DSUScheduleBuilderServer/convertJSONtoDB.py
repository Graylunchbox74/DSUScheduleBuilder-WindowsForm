import sqlite3
import json
import sys

db = sqlite3.connect("userDatabase.db")

data = None
with open("../Scraping/2018FA.json", "r") as f:
    data = json.load(f)

if len(sys.argv) < 2:
    print("Please provide a term to use")
    sys.exit(1)

term = sys.argv[1]
if data[term] == None:
    print("Invalid term")
    sys.exit(1)

for collegeKey, college in data[term].items():
    print (collegeKey)
    for couresName, course in college.items():
        for section in course:
            for sectionID, cd in section.items():
                cmd = '''INSERT INTO
                availableCourses
                ( sectionID
                , open
                , academicLevel
                , courseID
                , description
                , courseName
                , startDate
                , endDate
                , location
                , meetingInformation
                , supplies
                , credits
                , slotsAvailable
                , slotsCapacity
                , slotsWaitlist
                , timeStart
                , timeEnd
                , professorEmails
                , prereqNonCourse
                , recConcurrentCourses
                , reqConcurrentCourses
                , prereqCoursesAnd
                , prereqCoursesOR
                , instructionalMethods
                , term)
                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
                '''

                param = (sectionID,
                    cd["Open"],
                    cd["AcademicLevel"],
                    cd["CourseCode"],
                    cd["CourseDescription"],
                    cd["CourseName"],
                    cd["DateStart"],
                    cd["DateEnd"],
                    cd["Location"],
                    cd["MeetingInformation"], 
                    cd["Supplies"],
                    cd["Credits"],
                    cd["SlotsAvailable"],
                    cd["SlotsCapacity"],
                    cd["SlotsWaitlist"],
                    cd["TimeStart"],
                    cd["TimeEnd"],
                    "|" + ("|".join(cd["ProfessorEmails"])) + "|",
                    cd["PrereqNonCourse"],
                    "|" + "|".join(cd["RecConcurrentCourses"]) + "|",
                    "|" + "|".join(cd["ReqConcurrentCourses"]) + "|",
                    "|" + "|".join(cd["PrereqCourses"]["and"]) + "|",
                    "|" + "|".join(cd["PrereqCourses"]["or"]) + "|",
                    "",
                    term)
                db.execute(cmd, param)

db.commit()