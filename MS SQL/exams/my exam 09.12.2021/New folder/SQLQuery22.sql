SELECT*
FROM Aircraft 
WHERE Condition LIKE '%C%' AND Condition LIKE '%B%' AND FlightHours IS NULL OR LEN(FlightHours)>=100 
AND YEAR([YEAR])>=2013