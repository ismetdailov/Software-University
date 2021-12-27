SELECT R.Description, FORMAT(R.OpenDate, 'dd-MM-yyyy')  AS OpenDate
FROM Reports R
LEFT JOIN Employees E ON R.EmployeeId = E.Id
WHERE R.EmployeeId IS NULL
ORDER BY R.OpenDate ,R.Description