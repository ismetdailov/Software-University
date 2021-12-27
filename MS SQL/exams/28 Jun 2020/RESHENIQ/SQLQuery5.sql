SELECT J.Id, FORMAT(J.JourneyStart, 'dd/MM/yyyy') AS JourneySrart ,
		FORMAT(J.JourneyEnd, 'dd/MM/yyyy') AS JourneyEnd
FROM Journeys J 
WHERE Purpose = 'Military'
ORDER BY J.JourneyStart 
