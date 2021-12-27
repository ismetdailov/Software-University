SELECT U.Username, C.Name AS CategoryName
FROM Reports R
JOIN Users U ON R.UserId = U.Id
JOIN Categories C ON R.CategoryId = C.Id	
WHERE MONTH(U.Birthdate) = MONTH(R.OpenDate) AND DAY(U.Birthdate) = DAY(R.OpenDate)
ORDER BY U.Username,CategoryName