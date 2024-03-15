DECLARE @Date datetime = '2024-01-15'
DECLARE @StartTime datetime
DECLARE @EndTime datetime
DECLARE @Today int
DECLARE @Nextday int

SET @Today = DATEDIFF(dd, 0, @Date);
SET @Nextday = DATEDIFF(dd, -1, @Date);

SET @StartTime = DATEADD(HH, 8, @Today);
SET @EndTime = DATEADD(HH, 7, @Nextday);

SELECT @StartTime, @EndTime
