from splinter import Browser
import glob, json, os, time, subprocess

class Course:
    Open                 = False

    AcademicLevel        = ""
    CourseCode           = ""
    CourseDescription    = ""
    CourseName           = ""
    DateStart            = ""
    DateEnd              = ""
    Location             = ""
    MeetingInformation   = ""
    Supplies             = ""
    
    Credits              = 0
    SlotsAvailable       = 0
    SlotsCapacity        = 0
    SlotsWaitlist        = 0
    TimeEnd              = 0
    TimeStart            = 0

    ProfessorEmails      = []
    PrereqNonCourse      = []
    RecConcurrentCourses = []
    ReqConcurrentCourses = []

    PrereqCourses        = {}
    InstructionalMethods = {}

    def __init__(self):
        self.Open                 = False

        self.AcademicLevel        = ""
        self.CourseCode           = ""
        self.CourseDescription    = ""
        self.CourseName           = ""
        self.DateStart            = ""
        self.DateEnd              = ""
        self.Location             = ""
        self.MeetingInformation   = ""
        self.Supplies             = ""
        
        self.Credits              = 0
        self.SlotsAvailable       = 0
        self.SlotsCapacity        = 0
        self.SlotsWaitlist        = 0
        self.TimeEnd              = 0
        self.TimeStart            = 0

        self.ProfessorEmails      = []
        self.PrereqNonCourse      = []
        self.RecConcurrentCourses = []
        self.ReqConcurrentCourses = []

        self.PrereqCourses        = {}
        self.InstructionalMethods = {}


class Teacher:
    Email = ""
    Name  = ""
    Phone = ""
    def __init__(self, e, n, p):
        self.Email = e
        self.Name = n
        self.Phone = p


""" JSON Model for Data
    {
        "Teachers": {"{Teacher Email}": {Teacher Object}},
        "TotalTime": "{Total time it took for the data for all semesters to be collected}",
        "{Semester}": {
            "Time": "{Time it took for the data to be collected for the given semester}",
            "Courses": {
                "{Subject}": {"Course Code": [{"{Section Codes}": {Course Object}}]}
            }
        }        
    }
"""

with open(glob.glob("*_config.json")[0]) as fi:
    config = json.load(fi)
executable_path = {'executable_path': config['browser']}
b = Browser(config['type'], headless=config['headless'], **executable_path)
totalData = {"Teachers": {}}

def main():    
    initToQuery()
    semesters = getSemesters()
    b.quit()
    semestersDict = {}
    for semester in semesters:
        p = subprocess.Popen(["python", "scraper.py", semester], shell=True)
        semestersDict[semester] = p
    for semester in semestersDict:
        semestersDict[semester].wait()
        if os.path.exists(semester + ".json"):
            with open(semester + ".json") as fi:
                semesterJSON = json.load(fi)
            for teacher in semesterJSON["Teachers"]:
                if teacher in totalData["Teachers"]:
                    if not totalData["Teachers"][teacher]["Email"] and semesterJSON["Teachers"][teacher]["Email"]:
                        totalData["Teachers"][teacher]["Email"] = semesterJSON["Teachers"][teacher]["Email"]
                    if not totalData["Teachers"][teacher]["Name"] and semesterJSON["Teachers"][teacher]["Name"]:
                        totalData["Teachers"][teacher]["Name"] = semesterJSON["Teachers"][teacher]["Name"]
                    if not totalData["Teachers"][teacher]["Phone"] and semesterJSON["Teachers"][teacher]["Phone"]:
                        totalData["Teachers"][teacher]["Phone"] = semesterJSON["Teachers"][teacher]["Phone"]
                else:
                    totalData["Teachers"][teacher] = semesterJSON["Teachers"][teacher]
            totalData[semester] = semesterJSON[semester]
            os.remove(semester + ".json")
        else:
            print("Error with JSON file for semester: " + semester)
    with open("totalData.json", "w") as fi:
        json.dump(totalData, fi)

def initToQuery():
    url = "https://portal.sdbor.edu"
    b.visit(url)
    b.find_by_id("username").first.fill(config["wa_username"])
    b.find_by_id("password").first.fill(config["wa_password"] + "\n")
    b.visit(url + "/dsu-student/Pages/default.aspx")
    while b.is_element_not_present_by_text("WebAdvisor for Prospective Students"):
        pass
    b.find_by_text("WebAdvisor for Prospective Students").click()
    while b.is_element_not_present_by_text("Admission Information", 1):
        pass
    b.find_by_text("Admission Information").first.click()
    while b.is_element_not_present_by_text("Search for Class Sections", 1):
        pass
    b.find_by_text("Search for Class Sections").first.click()
    while b.is_element_not_present_by_id("VAR1", 1):
        pass

def getSemesters(): #assumes that you're already on the Prospective students search page
    select = b.find_by_id("VAR1")
    options = select.first.find_by_tag("option")
    return [x['value'] for x in options if x.text]
    
def objectToDict(obj):
    return {attr: getattr(obj, attr) for attr in type(obj).__dict__ if not attr.startswith("__")}

if __name__ == "__main__":
    ts = time.time()
    main()
    te = time.time()
    timing = ""

    hours = (te - ts) // 3600
    minutes = (te - ts) % 3600 // 60
    seconds = (te - ts) % 60 // 1

    if hours:
        timing += "{0} hour".format(hours) + ("s" if hours - 1 else "")

    if minutes:
        if hours:
            if not seconds:
                timing += ", and "
            else:
                timing += ", "
        timing += "{0} minute".format(minutes) + ("s" if minutes - 1 else "")

    if seconds:
        if hours or minutes:
            timing += ", and "
        timing += "{0} second".format(seconds) + ("s" if seconds - 1 else "")

    print("Scraping took " + timing + ".")