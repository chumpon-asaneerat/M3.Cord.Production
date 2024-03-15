SELECT * FROM PalletSetting
SELECT * FROM PalletSettingItem

SELECT * FROM G4YarnView 
 WHERE ItemYarn = '1100-360-704M-OKA' 
   AND ReceiveDate IS NOT NULL
   AND FinishFlag = 0

-- Find Trace for Load Yarn
SELECT A.*, B.ItemYarn FROM G4IssueYarn A, G4YarnView B
 WHERE B.ItemYarn = '1100-360-704M-OKA' 
   AND A.TraceNo = B.TraceNo
   AND A.WHReceiveDate IS NOT NULL

EXEC SearchG4AgeingPallet
EXEC SearchPCTwist1ByLotNo

SELECT * FROM DIPPalletSlip
SELECT * FROM DIPPalletSlipView