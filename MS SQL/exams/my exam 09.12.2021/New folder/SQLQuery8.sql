SELECT TOP(20) FD.Id, FD.Start, P.FullName, A.AirportName,FD.TicketPrice
FROM FlightDestinations FD
JOIN Passengers P ON FD.PassengerId = P.Id
JOIN Airports A ON FD.AirportId = A.Id
WHERE DAY(Start)%2 =0
ORDER BY FD.TicketPrice DESC, A.AirportName