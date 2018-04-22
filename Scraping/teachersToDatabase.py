import sqlite3
import json
import sys

db = sqlite3.connect("userDatabase.db")

data = None
with open("../Scraping/newTotalData.json", "r") as f:
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
                teachers
                ( name, email, phone)
                VALUES (?, ?, ?)
                '''

                param = ( cd["name"],
                    cd["email"],
                    cd["phone"] )
                db.execute(cmd, param)

db.commit()