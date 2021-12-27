SELECT 
	 FirstName + ' ' + LastName AS FullName, 
	 COUNT(DISTINCT R.UserId) AS UsersCount
FROM Reports   AS R
RIGHT JOIN Employees AS E ON E.Id = R.EmployeeId
GROUP BY EmployeeId, FirstName, LastName
ORDER BY UsersCount DESC, FullName ASC