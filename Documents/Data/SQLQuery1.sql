-- 1 Pallet มี 1 TraceNo และ 1 LotNo
SELECT PalletNo 
  FROM G4Yarn 
 GROUP BY PalletNo
HAVING COUNT(PalletNo) > 1

-- 1 TraceNo มี Lot No มากกว่า 1
SELECT TraceNo, PalletNo, LotNo
  FROM G4Yarn 
 WHERE TraceNo IN 
 (
	SELECT TraceNo
	  FROM G4Yarn 
	 GROUP BY TraceNo
	HAVING COUNT(TraceNo) > 1
 )
 ORDER BY TraceNo, PalletNo, LotNo



-- LotNo ซ้ำได้
SELECT LotNo, COUNT(LotNo) AS Cnt 
  FROM G4Yarn 
 GROUP BY LotNo
HAVING COUNT(LotNo) > 1



SELECT * FROM G4Yarn WHERE TraceNo = N'2170330024'


SELECT LotNo
     , TraceNo 
     , PalletNo 
     , ItemYarn 
  FROM G4Yarn 
-- WHERE LotNo = N'#M3/227'
 WHERE TraceNo = N'2170330024'
 ORDER BY LotNo, TraceNo, PalletNo, ItemYarn


