-- FROM SP GetTwist1Opts
    SELECT DISTINCT 
	         A.* 
		   , B.MCCode, B.ProductLotNo --, B.CustomerName, B.ProductName 
		   --, B.ItemYarn, B.CordStructure, B.TreatRoute, B.TwistSpec, B.PCDate, B.IssueDate, B.IssueBy 
		   , C.ShiftName, C.UserName 
      FROM PCTwist1Operation A
	     , PCTwist1View B
		 , Twist1LoadRecord C
     WHERE B.PCTwist1Id = A.PCTwist1Id
	   AND C.PCTwist1Id = A.PCTwist1Id
	   AND (C.ShiftName IS NOT NULL AND LTRIM(RTRIM(C.ShiftName)) <> '')
	   AND (C.UserName IS NOT NULL AND LTRIM(RTRIM(C.UserName)) <> '')
	   AND A.PCTwist1Id = 246
   ORDER BY A.ProductionDate

--SELECT * FROM PCTwist1View WHERE PCTwist1Id = 246 -- OK

SELECT * FROM PCTwist1Operation WHERE PCTwist1Id = 246

SELECT * FROM Twist1LoadRecord WHERE PCTwist1Id = 246 ORDER BY ProductionDate, DoffNo