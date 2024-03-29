CREATE OR REPLACE FUNCTION public.sp_update_absence (
    _person_id INT,
    _absence_type_id INT,
    _effective_from timestamp without time zone,
    _work_hours_total INT,
    INOUT _absence_id INT
    )
AS
$$
BEGIN
	UPDATE public.absence
    SET person_id = _person_id,
        absence_type_id = _absence_type_id,
        effective_from = _effective_from,
        work_hours_total = _work_hours_total
    WHERE absence_id = _absence_id;
END
$$
LANGUAGE PLPGSQL SECURITY DEFINER;