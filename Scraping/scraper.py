from splinter import Browser
import glob, json

def main():
    with open(glob.glob("*_config.json")) as fi:
        config = json.load(fi)
    executable_path = {'executable_path': config['browser']}
    b = Browser(config['type'], headless=config['headless'], **executable_path)
    
    

if __name__ == "__main__":
    main()