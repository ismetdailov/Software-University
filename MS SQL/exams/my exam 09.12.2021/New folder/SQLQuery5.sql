SELECT P.FirstName,P.LastName,A.Manufacturer,A.Model,A.FlightHours
FROM PilotsAircraft PA
JOIN Aircraft A ON PA.AircraftId = A.Id
JOIN Pilots P ON PA.PilotId = P.Id
WHERE A.FlightHours<304
ORDER BY A.FlightHours DESC, P.FirstName