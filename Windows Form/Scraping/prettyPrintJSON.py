import json, sys

def main():
    try:
        with open(sys.argv[1], "r") as fi:
            obj = json.load(fi)
            print(json.dumps(obj, indent=4))
    except Exception as e:
        print(e)

if __name__ == "__main__":
    main()