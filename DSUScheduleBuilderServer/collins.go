func getEnrolledClasses(uid int) ([]course, int, error) {
	location := "getEnrolledClasses"
	var classes []course
	var class course
	parameter := "|" + strconv.Itoa(uid) + "|"
	rows, err := db.Query(`
		SELECT *
		FROM EnrolledClasses 
		WHERE userID LIKE $1`, parameter,
	)

	defer rows.Close()

	for rows.Next() {
		class = course{}

		err = rows.Scan(
			&class.UserID, &class.ClassID, &class.ClassName, &class.Teacher,
			&class.StartTime, &class.EndTime, &class.StartDate, &class.EndDate, &class.Credits,
		)

		if err != nil {
			go logError(location, "1", err)
			return classes, 5, err
		}

		classes = append(classes, class)
	}

	err = rows.Err()

	if err != nil {
		go logError(location, "2", err)
		return classes, 5, err
	}

	return classes, 200, nil
}




courses.GET("/enrolled/:uuid", func(c *gin.Context) {
				uuid := c.Param("uuid")
				var userID int
				err := db.QueryRow("SELECT uid FROM USER_SESSIONS WHERE uuid=$1", uuid).Scan(&userID)

				if err == sql.ErrNoRows {
					currentError := createErrorStruct(2, c.Request.URL.String(), "3", err)
					c.JSON(500, currentError)
					return
				}

				if err != nil {
					currentError := createErrorStruct(3, c.Request.URL.String(), "1", err)
					c.JSON(500, currentError)
					return
				}

				var classes []course
				classes, errno, err := getEnrolledClasses(userID)

				if err != nil {
					currentError := createErrorStruct(errno, c.Request.URL.String(), "2", err)
					c.JSON(500, currentError)
					return
				}

				c.JSON(200, classes)
			})