CREATE OR REPLACE FUNCTION public.sp_delete_absence (
    INOUT _absence_id INT
    )
AS
$$
BEGIN
	DELETE FROM public.absence
    WHERE absence_id = _absence_id;
END
$$
LANGUAGE PLPGSQL SECURITY DEFINER;