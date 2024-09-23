

IF NOT EXISTS (select column_name from INFORMATION_SCHEMA.columns where table_name = 'VesselETAChange' and column_name = 'PlanDateTimeOfBerth')
begin
Alter table VesselETAChange
ADD PlanDateTimeOfBerth DateTime NULL;
end

IF NOT EXISTS (select column_name from INFORMATION_SCHEMA.columns where table_name = 'VesselETAChange' and column_name = 'PlanDateTimeToStartCargo')
begin
Alter table VesselETAChange
ADD PlanDateTimeToStartCargo DateTime NULL;
end

IF NOT EXISTS (select column_name from INFORMATION_SCHEMA.columns where table_name = 'VesselETAChange' and column_name = 'PlanDateTimeToCompleteCargo')
begin
Alter table VesselETAChange
ADD PlanDateTimeToCompleteCargo DateTime NULL;
end

IF NOT EXISTS (select column_name from INFORMATION_SCHEMA.columns where table_name = 'VesselETAChange' and column_name = 'PlanDateTimeToVacateBerth')
begin
Alter table VesselETAChange
ADD PlanDateTimeToVacateBerth DateTime NULL;
end

IF NOT EXISTS (select column_name from INFORMATION_SCHEMA.columns where table_name = 'VesselETAChange' and column_name = 'OldPlanDateTimeOfBerth')
begin
Alter table VesselETAChange
ADD OldPlanDateTimeOfBerth DateTime NULL;
end

IF NOT EXISTS (select column_name from INFORMATION_SCHEMA.columns where table_name = 'VesselETAChange' and column_name = 'OldPlanDateTimeToStartCargo')
begin
Alter table VesselETAChange
ADD OldPlanDateTimeToStartCargo DateTime NULL;
end

IF NOT EXISTS (select column_name from INFORMATION_SCHEMA.columns where table_name = 'VesselETAChange' and column_name = 'OldPlanDateTimeToCompleteCargo')
begin
Alter table VesselETAChange
ADD OldPlanDateTimeToCompleteCargo DateTime NULL;
end

IF NOT EXISTS (select column_name from INFORMATION_SCHEMA.columns where table_name = 'VesselETAChange' and column_name = 'OldPlanDateTimeToVacateBerth')
begin
Alter table VesselETAChange
ADD OldPlanDateTimeToVacateBerth DateTime NULL;
end


