UPDATE Aircraft 
SET Condition = 'A'
WHERE (Condition LIKE '%C%' OR Condition LIKE '%B%')