from re import match
from splinter import Browser
from time import sleep
import glob, json

class Course:
    Open              = False

    AcademicLevel      = ""
    CourseCode         = ""
    CourseDescription  = ""
    CourseName         = ""
    DateStart          = ""
    DateEnd            = ""
    Location           = ""
    MeetingInformation = ""
    Professor          = ""
    
    Credits           = 0
    TimeEnd           = 0
    TimeStart         = 0

    Lab               = None
    Prereqcourse      = None
    Prereqnoncourse   = None
    Prerequisites     = None

class Teacher:
    Email = ""
    Name  = ""
    Phone = ""

""" JSON Model for Data
    {
        CourseCodes: [{List of course codes as strings}],
        Courses:     ["{Course Code}": {Course Object}]
    }
"""

with open(glob.glob("*_config.json")[0]) as fi:
    config = json.load(fi)
executable_path = {'executable_path': config['browser']}
b = Browser(config['type'], headless=config['headless'], **executable_path)

def main():
    getToQuery()
    courses = []
    semesters = getSemesters()
    subjects  = getSubjects()
    for semester in semesters:
        for subject in subjects:
            getToQuery()
            selectDropdown("VAR1", semester)
            selectDropdown("LIST_VAR1_1", subject)
            selectDropdown("VAR6", "DSU")
            b.find_by_name("SUBMIT2").click()
            if b.text_is_present("No classes meeting the search criteria have been found."):
                continue
            m = match("Page (\d+) of (\d+)")
            if m:
                while m.group(1) != m.group(2):    
                    m = match("Page (\d+) of (\d+)")
                    courses.extend(scrapeTable(b.find_by_xpath('//*[@id="GROUP_Grp_WSS_COURSE_SECTIONS_GWT"]/table/tbody/tr[2]/td/table')))
            else:
                courses.extend(scrapeTable(b.find_by_xpath('//*[@id="GROUP_Grp_WSS_COURSE_SECTIONS_GWT"]/table/tbody/tr[2]/td/table')))
    #print(semesters)
    print(b.url)
    b.quit()

def getSemesters(): #assumes that you're already on the Prospective students search page
    select = b.find_by_id("VAR1")
    options = select.first.find_by_tag("option")
    return [x['value'] for x in options if x.text]

def getSubjects(): #assumes that you're already on the Prospective students search page
    select = b.find_by_id("LIST_VAR1_1")
    options = select.first.find_by_tag("option")
    return [x['value'] for x in options if x.text]

def getToQuery():
    b.visit("https://wa-dsu.prod.sdbor.edu/WebAdvisor/webadvisor")
    b.find_by_text(" Prospective Students").click()
    b.find_by_text("Search for Class Sections").click()

def scrapeTable(tab):
    courses = []
    for tr in tab.find_by_tag("tr")[1:]:
        course = Course()
        link = ""
        for e, td in enumerate(tr.find_by_tag("td")):
            if e == 2:
                course.Open = "Open" in td.text
            elif e == 3:
                link = td.find_by_tag("a").first.click()
                #Start
                course.CourseName = b.find_by_id("VAR1").first.text
                course.CourseCode = b.find_by_id("VAR2").first.text
                course.CourseDescription = b.find_by_id("VAR3").first.text
                course.Location = b.find_by_id("VAR40").first.text
                course.Credits = float(b.find_by_id("VAR4").first.text)
                course.DateStart = b.find_by_id("VAR6").first.text
                course.DateEnd = b.find_by_id("VAR7").first.text
                course.MeetingInformation = b.find_by_id("LIST_VAR12_1")first.text
                course.Teacher = Teacher()
                course.Teacher.Email = b.find_by_id("LIST_VAR10_1").first.text
                course.Teacher.Name = b.find_by_id("LIST_VAR7_1").first.text
                course.Teacher.Phone = b.find_by_id("LIST_VAR8_1").first.text
        courses.append(course)
    return courses

def selectDropdown(ddt, t): #assumes that you're already on the Prospective students search page
    selects = b.find_by_id(ddt).first
    selects.select(t)

def writeCoursesToJSON(l):
    pass

if __name__ == "__main__":
    main()