import sqlite3
import json
import sys

db = sqlite3.connect("userDatabase.db")

data = None
with open("../Scraping/2018FA.json", "r") as f:
    data = json.load(f)

for teacherEmail, teacher in data["Teachers"].items():
    print (teacherEmail)
    cmd = '''INSERT INTO
        teachers
        ( name, email, phone )
        VALUES (?, ?, ?)
        '''

    param = (
        teacher["Name"],
        teacher["Email"],
        teacher["Phone"]
    )
    db.execute(cmd, param)

db.commit()