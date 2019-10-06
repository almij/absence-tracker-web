CREATE OR REPLACE FUNCTION public.sp_update_absence (
    _person_id INT,
    _absence_type_id INT,
    _effective_from timestamp without time zone,
    _days_total timestamp without time zone,
    _hours_total INT,
    _days_worked_on_holidays INT,
    INOUT _absence_id INT
    )
AS
$$
BEGIN
	UPDATE public.absence
    SET person_id = _person_id,
        absence_type_id = _absence_type_id,
        effective_from = _effective_from,
        days_total = _days_total,
        hours_total = _hours_total,
        days_worked_on_holidays = _days_worked_on_holidays
    WHERE absence_id = _absence_id;
END
$$
LANGUAGE PLPGSQL SECURITY DEFINER;