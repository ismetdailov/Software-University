SELECT *
FROM Pilots P
 JOIN Aircraft A ON P.Id = A.TypeId
WHERE A.FlightHours<304