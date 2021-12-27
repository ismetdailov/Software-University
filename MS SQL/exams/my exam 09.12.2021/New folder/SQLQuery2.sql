SELECT A.Manufacturer AS Manufacturer,
A.Model AS Model, 
A.FlightHours AS FlightHours ,
A.Condition AS Condition
FROM Aircraft A
ORDER BY A.FlightHours