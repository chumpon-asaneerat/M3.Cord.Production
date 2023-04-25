/*
-- G3_YARN
SELECT * FROM G3_YARN WHERE rownum <= 100

SELECT EntryDate AS "EntryDate"
     , ITM_YARN AS "ItemYarn"
     , PalletNo AS "PalletNo"
     , YarnType AS "YarnType"
     , WeightQty AS "WeightQty"
     , CONECH AS "ConeCH"
     , PalletType AS "PalletType"
     , ITM400 AS "Item400"
     , UM AS "Unit"
     , LotNo AS "LotNo"
     , TraceNo AS "TraceNo"
     , KgPerCH AS "KgPerCH"
  FROM G3_YARN 
 WHERE rownum <= 100

*/


/*
-- G3_ISSUEYARN
SELECT * FROM G3_ISSUEYARN WHERE rownum <= 100

SELECT PalletNo AS "PalletNo"
     , TraceNo AS "TraceNo"
     , Weight AS "Weight"
     , CH AS "CH"
     , PalletType AS "PalletType"
  FROM G3_ISSUEYARN 
 WHERE rownum <= 100
*/




/*
-- ITM_CODE
SELECT * FROM ITEMCODE WHERE rownum <= 100

SELECT ITM_CODE AS "ItemCode"
     , ITM_WEAVING AS "ItemWeaving"
     , ITM_YARN AS "ItemYarn"
     , ITM_WIDTH AS "ItemWidth"
     , ITM_Prepare AS "ItemPrepare"
     , COREWEIGHT AS "CoreWeight"
     , FULLWEIGHT AS "FullWeight"
     , ITM_GROUP AS "ItemGroup"
     , YARNCODE AS "YarnCode"
     , WIDTHCODE AS "WidthCode"
     , WIDTHWEAVING AS "WidthWeaving"     
     , WEAVE_TYPE AS "WeaveType"     
  FROM ITEMCODE 
 WHERE rownum <= 100
*/  

