CREATE OR REPLACE FUNCTION public.sp_insert_person (
    _aspnetuser_id text,
    _first_name text,
    _last_name text,
    INOUT _person_id INT = 0
    )
AS
$$
BEGIN
	INSERT INTO public.person (
        aspnetuser_id, 
        first_name, 
        last_name)
    VALUES (
        _aspnetuser_id, 
        _first_name, 
        _last_name
        )
	RETURNING person_id INTO _person_id;
END
$$
LANGUAGE PLPGSQL SECURITY DEFINER;