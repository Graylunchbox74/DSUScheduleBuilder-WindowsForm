import json
import sqlite3


def main():
    db = sqlite3.connect("userDatabase.db")
    d = db.cursor()
    with open("totalData.json",) as json_data:
        data = json.load(json_data)