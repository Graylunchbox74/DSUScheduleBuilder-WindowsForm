from splinter import Browser
from time import sleep
import glob, json

with open(glob.glob("*_config.json")[0]) as fi:
    config = json.load(fi)
executable_path = {'executable_path': config['browser']}
b = Browser(config['type'], headless=config['headless'], **executable_path)

def main():
    getToQuery()
    b.quit()
    
def getToQuery():
    b.visit("https://wa-dsu.prod.sdbor.edu/WebAdvisor/webadvisor")
    b.find_by_text(" Prospective Students").click()
    b.find_by_text("Search for Class Sections").click()
    sleep(5)


if __name__ == "__main__":
    main()