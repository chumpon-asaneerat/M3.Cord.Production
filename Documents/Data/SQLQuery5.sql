/*
SELECT DIPPCId FROM S9AppearanceCheckSheet WHERE AppearId = 5

SELECT * FROM DIPPCCardView WHERE DIPPCId = 7

SELECT * FROM S9AppearanceCheckSheetItem WHERE CheckGood =1 
*/
/*
SELECT A.DIPPCId
     , A.CustomerName
	 , A.ProductCode
	 , A.ItemYarn
	 , A.MCCode 
	 , B.AppearId
	 , Count(C.CheckGood) AS TotalGood
  FROM DIPPCCardView A
     , S9AppearanceCheckSheet B
	 , S9AppearanceCheckSheetItem C  
 WHERE B.DIPPCId = A.DIPPCId
   AND C.AppearId = B.AppearId
   AND C.CheckGood = 1
   AND A.DIPLotNo = '23K30A'
 GROUP BY A.DIPPCId
     , A.CustomerName
	 , A.ProductCode
	 , A.ItemYarn
	 , A.MCCode 
	 , B.AppearId
*/

/*
EXEC GetLabelCHS9Summary '23K30A'
EXEC GetLabelCHS9Items '23K30A', 3, 11
*/

/*
DELETE FROM CordSamplingDetails
*/