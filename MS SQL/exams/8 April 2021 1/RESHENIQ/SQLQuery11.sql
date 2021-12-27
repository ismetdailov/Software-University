SELECT E.FirstName+' '+E.LastName AS FullName,
			COUNT(*) AS UsersCount
FROM Reports R
JOIN Employees E ON R.EmployeeId = E.Id
JOIN Users U ON R.UserId = U.Id

GROUP BY R.EmployeeId,E.FirstName,E.LastName 
ORDER BY UsersCount DESC , FullName