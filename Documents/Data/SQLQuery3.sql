/*
DELETE FROM DIPCondition
Update DIPCondition SET DIPPcId = 28
*/
/*
SELECT * FROM DIPCondition
SELECT * FROM DIPTimeTable
DELETE FROM DIPTimeTable
*/
--EXEC GetDIPTimeTables N'2024-01-14'

EXEC SaveDIPTimeTableStdVRow 'P1100ZFT', '2024-01-14', '234'
EXEC SaveDIPTimeTableStdBRow 'P1100ZFT', '2024-01-14', '234'
EXEC SaveDIPTimeTableStdCFRow 'P1100ZFT', '2024-01-14', '234'

/*
INSERT INTO DIPTimeTable 
(
       [ProductCode]
      ,[RowType]
      ,[PeriodTime]
      ,[LotNo]
      ,[S7BobbinSC]
      ,[S7Bobbin]
      ,[S8CoolingWaterSystemBath1SC]
      ,[S8CoolingWaterSystemBath1Min]
      ,[S8CoolingWaterSystemBath1Max]
      ,[S8CoolingWaterSystemBath1Value]
      ,[S8CoolingWaterSystemBath2SC]
      ,[S8CoolingWaterSystemBath2]
      ,[S8CoolingWaterSystemBath2Min]
      ,[S8CoolingWaterSystemBath2Max]
      ,[S8CoolingWaterSystemBath2Value]
      ,[S8ChemicalWorkSC]
      ,[S8ChemicalWork]
      ,[S8ChemicalFilterSC]
      ,[S8ChemicalFilter]
      ,[S8SpeedSC]
      ,[S8Speed]
      ,[S8SpeedErr]
      ,[S8SpeedValue]
      ,[S8StretchDSC]
      ,[S8StretchD]
      ,[S8StretchDErr]
      ,[S8StretchDValue]
      ,[S8StretchHSC]
      ,[S8StretchH]
      ,[S8StretchHErr]
      ,[S8StretchHValue]
      ,[S8StretchNSC]
      ,[S8StretchN]
      ,[S8StretchNErr]
      ,[S8StretchNValue]
      ,[S8TempDSC]
      ,[S8TempD]
      ,[S8TempDErr]
      ,[S8TempDValue]
      ,[S8TempHNSC]
      ,[S8TempHN]
      ,[S8TempHNErr]
      ,[S8TempHNValue]
      ,[S9GlideStatusSC]
      ,[S9GlideStatus]
      ,[Remark]
      ,[CheckBy]
      ,[CheckDate]
) 
*/

/*
INSERT INTO DIPTimeTable
(
       [RowType]
      ,[PeriodTime]
      ,[LotNo]
      ,[ProductCode]
      ,[S7BobbinSC]
      ,[S8CoolingWaterSystemBath1SC]
      ,[S8CoolingWaterSystemBath1Min]
      ,[S8CoolingWaterSystemBath1Max]
      ,[S8CoolingWaterSystemBath2SC]
      ,[S8CoolingWaterSystemBath2Min]
      ,[S8CoolingWaterSystemBath2Max]
      ,[S8ChemicalWorkSC]
      ,[S8ChemicalFilterSC]
      ,[S8SpeedSC]
      ,[S8Speed]
      ,[S8SpeedErr]
      ,[S8StretchDSC]
      ,[S8StretchD]
      ,[S8StretchDErr]
      ,[S8StretchHSC]
      ,[S8StretchH]
      ,[S8StretchHErr]
      ,[S8StretchNSC]
      ,[S8StretchN]
      ,[S8StretchNErr]
      ,[S8TempDSC]
      ,[S8TempDErr]
      ,[S8TempHNSC]
      ,[S8TempHNErr]
      ,[S9GlideStatusSC]
)
SELECT RowType = -1
     , PeriodTime = '2024-01-14 08:00:00'
     , LotNo = '123'
     ,[ProductCode]
     ,[S7BobbinSC]
     ,[S8CoolingWaterSystemBath1SC]
     ,[S8CoolingWaterSystemBath1Min]
     ,[S8CoolingWaterSystemBath1Max]
     ,[S8CoolingWaterSystemBath2SC]
     ,[S8CoolingWaterSystemBath2Min]
     ,[S8CoolingWaterSystemBath2Max]
     ,[S8ChemicalWorkSC]
     ,[S8ChemicalFilterSC]
     ,[S8SpeedSC]
     ,[S8Speed]
     ,[S8SpeedErr]
     ,[S8StretchDSC]
     ,[S8StretchD]
     ,[S8StretchDErr]
     ,[S8StretchHSC]
     ,[S8StretchH]
     ,[S8StretchHErr]
     ,[S8StretchNSC]
     ,[S8StretchN]
     ,[S8StretchNErr]
     ,[S8TempDSC]
     ,[S8TempDErr]
     ,[S8TempHNSC]
     ,[S8TempHNErr]
     ,[S9GlideStatusSC]
  FROM DIPTimeTableStd 
 WHERE ProductCode = 'P1100ZFT';
*/