SELECT ISNULL((E.FirstName+' '+E.LastName),'None') AS Employee,
		ISNULL((D.Name),'None') AS Department,
		ISNULL((C.Name),'None') AS Category,
		ISNULL((R.Description),'None') AS Description,
		ISNULL((FORMAT(R.OpenDate, 'dd.MM.yyyy')),'None') AS OpenDate,
		ISNULL((S.Label),'None') AS Status,
		ISNULL((U.Name),'None') AS [User]
FROM Reports R
LEFT JOIN Employees E ON R.EmployeeId = E.Id
LEFT JOIN Categories C ON R.CategoryId = C.Id
LEFT JOIN Departments D ON E.DepartmentId = D.Id
LEFT JOIN Users U ON R.UserId = U.Id
LEFT JOIN Status S ON R.StatusId = S.Id 
ORDER BY E.FirstName DESC, E.LastName DESC, Department , Category,Description,R.CloseDate,Status,User