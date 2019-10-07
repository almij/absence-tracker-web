DROP TABLE public.team_personnel;
DROP TABLE public.team;
DROP TABLE public.absence;
DROP TABLE public.person;
DROP TABLE public.absence_type;

CREATE TABLE public.person
(
    person_id INT GENERATED ALWAYS AS IDENTITY,
    aspnetuser_id text NOT NULL,
    first_name VARCHAR(50),
    last_name VARCHAR(100),
		
    CONSTRAINT pk_person_id PRIMARY KEY (person_id)
);

CREATE TABLE public.absence
(
    absence_id INT GENERATED ALWAYS AS IDENTITY,
    absence_type_id INT NOT NULL,
    person_id INT NOT NULL,
    effective_from DATE NOT NULL,
    work_hours_total INT,
	
    CONSTRAINT pk_absence_id PRIMARY KEY (absence_id)
);

CREATE TABLE public.absence_type
(
    absence_type_id INT GENERATED ALWAYS AS IDENTITY,
    absence_type_name VARCHAR(100) NOT NULL CONSTRAINT unique_absence_type_name UNIQUE,
    is_day_off BIT NOT NULL,
    is_overtime BIT NOT NULL,
	
    CONSTRAINT pk_absence_type_id PRIMARY KEY (absence_type_id)
);

CREATE TABLE public.team
(
    team_id int GENERATED ALWAYS AS IDENTITY,
    team_name VARCHAR(100) NOT NULL,
    team_head_id INT,
	
    CONSTRAINT pk_team_id PRIMARY KEY (team_id)
);

CREATE TABLE public.team_personnel
(
    team_id INT NOT NULL,
    person_id INT NOT NULL,

    CONSTRAINT pk_team_combo PRIMARY KEY (team_id, person_id)
);


ALTER TABLE public.person ADD
    CONSTRAINT fk_person_aspnetuser FOREIGN KEY (aspnetuser_id) REFERENCES public."AspNetUsers"("Id");

ALTER TABLE public.absence ADD
    CONSTRAINT fk_absense_absence_type FOREIGN KEY (absence_type_id) REFERENCES public.absence_type(absence_type_id);
ALTER TABLE public.absence ADD
    CONSTRAINT fk_absence_person FOREIGN KEY (person_id) REFERENCES public.person(person_id);

ALTER TABLE public.team ADD
    CONSTRAINT fk_team_person FOREIGN KEY (team_head_id) REFERENCES public.Person(person_id);

ALTER TABLE public.team_personnel ADD
    CONSTRAINT fk_team_personnel_team FOREIGN KEY (team_id) REFERENCES public.team(team_id);
ALTER TABLE public.team_personnel ADD
    CONSTRAINT fk_team_personnel_person FOREIGN KEY (person_id) REFERENCES public.person(person_id);

