SELECT ISNULL( E.FirstName+' '+E.LastName, 'None') AS Employee,
			ISNULL(D.Name,'None') AS Department,
			ISNULL(C.Name,'None') AS Category,
			R.Description AS Description,
			FORMAT(R.OpenDate, 'dd.MM.yyyy')AS OpenDate,
			S.Label AS [Status],
			U.Name AS [User]
FROM  Reports R
LEFT JOIN Employees E ON R.EmployeeId = E.Id
LEFT JOIN Departments D ON E.DepartmentId = D.Id
LEFT JOIN Categories C ON R.CategoryId = C.Id
LEFT JOIN Status S ON R.StatusId = S.Id
LEFT JOIN Users U ON R.UserId = U.Id
ORDER BY E.FirstName DESC, E.LastName DESC, Department, Category, Description,OpenDate,Status,[User]