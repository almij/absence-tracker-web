CREATE OR REPLACE FUNCTION public.sp_insert_absence (
    _person_id INT,
    _absence_type_id INT,
    _effective_from timestamp without time zone,
    _work_hours_total INT,
    INOUT _absence_id INT = 0
    )
AS
$$
BEGIN
	INSERT INTO public.absence(
        person_id,
        absence_type_id,
        effective_from,
        work_hours_total
        )
    VALUES (
        _person_id,
        _absence_type_id,
        _effective_from,
        _work_hours_total
        )
    RETURNING absence_id INTO _absence_id;
END
$$
LANGUAGE PLPGSQL SECURITY DEFINER;