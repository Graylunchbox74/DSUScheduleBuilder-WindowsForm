from re import match
from splinter import Browser, exceptions
from time import sleep
import glob, json, os, requests

class Course:
    Open               = False

    AcademicLevel      = ""
    CourseCode         = ""
    CourseDescription  = ""
    CourseName         = ""
    DateStart          = ""
    DateEnd            = ""
    Location           = ""
    MeetingInformation = ""
    Professor          = ""
    
    Credits            = 0
    SlotsAvailable     = 0
    SlotsCapacity      = 0
    SlotsWaitlist      = 0
    TimeEnd            = 0
    TimeStart          = 0

    Lab                = None
    Prereqcourse       = None
    Prereqnoncourse    = None
    Prerequisites      = None

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
    courses = []
    initToQuery()
    semesters = getSemesters()
    subjects  = getSubjects()
    for semester in semesters:
        for subject in subjects:
            unsuccessfulRun = True
            run = 0
            while unsuccessfulRun:
                run += 1
                print("Run", run)
                #try:
                for x in getExtraExits():
                    x.click()
                if b.is_element_present_by_text("Section Selection Results"):
                    unsuccessfulBackOut = True
                    while unsuccessfulBackOut:
                        try:
                            b.find_by_text("Go back").first.click()
                            unsuccessfulBackOut = False
                        except Exception as e:
                            print("Failed to go back: ", e)
                    while b.is_element_not_present_by_id("VAR1", 1):
                        pass
                elif not b.is_element_present_by_text("Search for Class Sections"):
                    b.execute_script("window.location.reload()")
                    sleep(1)
                    continue
                selectDropdown("VAR1", semester)
                selectDropdown("LIST_VAR1_1", subject)
                selectDropdown("VAR6", "DSU")
                unsuccessfulClick = True
                while unsuccessfulClick:
                    try:
                        b.find_by_id("WASubmit").first.click()
                        unsuccessfulClick = False
                    except Exception as e:
                        print(e)
                        print("Click failed for {0}, trying again".format(subject))
                        sleep(2.5)
                badResult = False
                while not badResult and b.is_element_not_present_by_text("Section Selection Results", 1):
                    badResult = b.is_text_present("No classes meeting the search criteria have been found.")
                if badResult:
                    unsuccessfulRun = False
                    continue
                print(b.find_by_css('table[summary="Paged Table Navigation Area"]').first.find_by_css('td[align="right"]').first.text)
                currentPage, totalPages = "0", "1"
                while currentPage != totalPages:
                    while b.is_element_not_present_by_css('table[summary="Paged Table Navigation Area"]'):
                        pass
                    m = match("Page (\d+) of (\d+)", b.find_by_css('table[summary="Paged Table Navigation Area"]').first.find_by_css('td[align="right"]').first.text)
                    currentPage, totalPages = m.groups(1)
                    scrapeTable(b.find_by_css('table[summary="Sections"]'))
                    for x in getExtraExits():
                        x.click()
                    b.find_by_css('input[value="NEXT"]').first.click()
                    if currentPage != totalPages:
                        while not b.is_text_present("Page {0} of {1}", int(currentPage) + 1, totalPages):
                            sleep(1)
                unsuccessfulRun = False
                # m = match("Page (\d+) of (\d+)", b.find_by_xpath('//*[@id="GROUP_Grp_WSS_COURSE_SECTIONS_GWT"]/table/tbody/tr[1]/td/table/tbody/tr/td[2]/div').text)
                # if m:
                #     while m.group(1) != m.group(2):    
                #         m = match("Page (\d+) of (\d+)", b.find_by_xpath('//*[@id="GROUP_Grp_WSS_COURSE_SECTIONS_GWT"]/table/tbody/tr[1]/td/table/tbody/tr/td[2]/div').text)
                #         courses.extend(scrapeTable(b.find_by_xpath('//*[@id="GROUP_Grp_WSS_COURSE_SECTIONS_GWT"]/table/tbody/tr[2]/td/table')))
                #         b.find_by_xpath('//*[@id="GROUP_Grp_WSS_COURSE_SECTIONS_GWT"]/table/tbody/tr[1]/td/table/tbody/tr/td[1]/table/tbody/tr/td[1]/table/tbody/tr/td[3]/button').click()
                # else:
                #     courses.extend(scrapeTable(b.find_by_xpath('//*[@id="GROUP_Grp_WSS_COURSE_SECTIONS_GWT"]/table/tbody/tr[2]/td/table')))
                # except exceptions.ElementDoesNotExist as e:
                #     b.execute_script("window.location.reload()")
                # except Exception as e:
                #     print("Trying again after error: \n{0}".format(e))
    b.quit()

def initToQuery():
    url = "https://portal.sdbor.edu"
    b.visit(url)
    b.find_by_id("username").fill(config["wa_username"])
    b.find_by_id("password").fill(config["wa_password"] + "\n")
    b.visit(url + "/dsu-student/Pages/default.aspx")
    while b.is_element_not_present_by_text("WebAdvisor for Prospective Students"):
        b.visit(url + "/dsu-student/Pages/default.aspx")
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

def getSubjects(): #assumes that you're already on the Prospective students search page
    select = b.find_by_id("LIST_VAR1_1")
    options = select.first.find_by_tag("option")
    return [x['value'] for x in options if x.text]

def getExtraExits():
    l = list(b.find_by_css('button[type="button"][class="btn btn-sm btn-danger"]'))
    if len(l) > 1:
        return l[1:]
    return []
    



def getToQuery():
    b.visit("https://wa-dsu.prod.sdbor.edu/WebAdvisor/webadvisor")
    b.find_by_text(" Prospective Students").click()
    b.find_by_text("Search for Class Sections").click()

def scrapeTable(tab):
    courses = []
    for n in range(len(tab.find_by_tag("tr"))):
        if n == 0:
            continue
        course = Course()
        link = ""
        for e in range(len(tab.find_by_tag("tr")[n].find_by_tag("td"))):
            try:
                print(e, tab.find_by_tag("tr")[n].find_by_tag("td")[e].text)
            except:
                pass
            if e == 2:
                course.Open = "Open" in tab.find_by_tag("tr")[n].find_by_tag("td")[e].text
            elif e == 3:
                #b.visit("https://wa-dsu.prod.sdbor.edu/WebAdvisor/webadvisor" + match(r"javascript:window\.open\('(.*)', .*", td.find_by_tag("a")["onclick"]).group(1))
                tab.find_by_tag("tr")[n].find_by_tag("td")[e].find_by_tag("a").first.click()
                while b.is_element_not_present_by_text("Section Information", 1):
                    print("Waiting on Section Information")
                #Start scraping for the bulk of the course data
                course.CourseName = b.find_by_id("VAR1").first.text
                course.CourseCode = b.find_by_id("VAR2").first.text
                course.CourseDescription = b.find_by_id("VAR3").first.text
                course.Location = b.find_by_id("VAR40").first.text
                course.Credits = float(b.find_by_id("VAR4").first.text)
                course.DateStart = b.find_by_id("VAR6").first.text
                course.DateEnd = b.find_by_id("VAR7").first.text
                course.MeetingInformation = b.find_by_id("LIST_VAR12_1").first.text
                course.Teacher = Teacher()
                course.Teacher.Email = b.find_by_id("LIST_VAR10_1").first.text
                course.Teacher.Name = b.find_by_id("LIST_VAR7_1").first.text
                course.Teacher.Phone = b.find_by_id("LIST_VAR8_1").first.text
                for x in getExtraExits():
                    x.click()
            elif e == 7:
                nm = match(r"(-?\d+) +\/ +(-?\d+) +\/ +(-?\d+)", tab.find_by_tag("tr")[n].find_by_tag("td")[e].text)
                course.SlotsAvailable = int(nm.group(1))
                course.SlotsCapacity = int(nm.group(2))
                course.SlotsWaitlist = int(nm.group(3))
            elif e == 9:
                course.AcademicLevel = tab.find_by_tag("tr")[n].find_by_tag("td")[e].text
        print(course)
        courses.append(course)
    return courses

def selectDropdown(ddt, t): #assumes that you're already on the Prospective students search page
    selects = b.find_by_id(ddt).first
    selects.select(t)

def writeCoursesToJSON(l):
    pass

if __name__ == "__main__":
    main()