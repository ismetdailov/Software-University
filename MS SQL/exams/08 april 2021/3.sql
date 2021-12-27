--Select the top 5 most reported categories and order them by the number of reports per category
--in descending order and then alphabetically by name.
SELECT *
FROM Categories C
JOIN Reports R ON C.Id = R.CategoryId
GROUP BY C.Name
