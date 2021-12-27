SELECT E.FirstName +' '+ E.LastName AS FullName,
		 COUNT(E.Id =U.Id) AS UserCount
FROM Employees E 
JOIN Reports R ON E.Id =R.EmployeeId
JOIN Users U ON R.EmployeeId = U.Id

