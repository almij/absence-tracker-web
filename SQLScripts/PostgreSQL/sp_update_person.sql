CREATE OR REPLACE FUNCTION public.sp_update_person (
    _first_name text,
    _last_name text,
    INOUT _person_id INT
    )
AS
$$
BEGIN
	UPDATE public.person
    SET first_name = _first_name, 
        last_name = _last_name
    WHERE person_id = _person_id;
END
$$
LANGUAGE PLPGSQL SECURITY DEFINER;