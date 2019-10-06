CREATE OR REPLACE FUNCTION public.sp_insert_absence (
    _person_id INT,
    _absence_type_id INT,
    _effective_from timestamp without time zone,
    _days_total timestamp without time zone,
    _hours_total INT,
    _days_worked_on_holidays INT,
    INOUT _absence_id INT = 0
    )
AS
$$
BEGIN
	INSERT INTO public.absence(
        person_id,
        absence_type_id,
        effective_from,
        days_total,
        hours_total,
        days_worked_on_holidays
        )
    VALUES (
        _person_id,
        _absence_type_id,
        _effective_from,
        _days_total,
        _hours_total,
        _days_worked_on_holidays
        )
    RETURNING absence_id INTO _absence_id;
END
$$
LANGUAGE PLPGSQL SECURITY DEFINER;