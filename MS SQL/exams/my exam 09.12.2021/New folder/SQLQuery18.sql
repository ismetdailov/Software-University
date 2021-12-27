
UPDATE  FlightDestinations 
SET TicketPrice= 'Low'
WHERE TicketPrice<=400
UPDATE FlightDestinations 
SET TicketPrice= 'Medium'
WHERE TicketPrice>400 AND TicketPrice <=1500
UPDATE FlightDestinations 
SET TicketPrice= 'LOW'
WHERE TicketPrice>1501

SELECT A.AirportName, P.FullName,AC.Manufacturer,
convert(varchar,convert(decimal(8,2),FD.TicketPrice)) AS LevelOfTickerPrice,
AC.Condition,ATY.TypeName
FROM FlightDestinations FD
JOIN Airports  A ON FD.AirportId = A.Id
LEFT JOIN Passengers P ON FD.PassengerId = P.Id
LEFT JOIN Aircraft AC ON FD.AircraftId = AC.Id
JOIN AircraftTypes ATY ON AC.TypeId = ATY.Id
WHERE  A.AirportName LIKE '%Sir Seretse Khama International Airport%'
GROUP BY FD.TicketPrice,A.AirportName,P.FullName,AC.Manufacturer,AC.Condition,ATY.TypeName


UPDATE FlightDestinations 
SET TicketPrice= 'Low'
WHERE TicketPrice<=400
UPDATE FlightDestinations 
SET TicketPrice= 'Medium'
WHERE TicketPrice>400 AND TicketPrice <=1500
UPDATE FlightDestinations 
SET TicketPrice= 'LOW'
WHERE TicketPrice>1501