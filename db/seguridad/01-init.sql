--
-- PostgreSQL database dump
--

\restrict 3Ky57w2gzRtkbWBuYNgW8UDZRtU32OEw2L7cpALdi6no7oQNocJr84PavKofvzY

-- Dumped from database version 18.6
-- Dumped by pg_dump version 18.6

-- Started on 2026-09-30 13:15:25

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: pg_database_owner
--

CREATE SCHEMA IF NOT EXISTS public;


ALTER SCHEMA public OWNER TO pg_database_owner;

--
-- TOC entry 5026 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: pg_database_owner
--

COMMENT ON SCHEMA public IS 'standard public schema';


--
-- TOC entry 222 (class 1255 OID 16773)
-- Name: fn_agregar_usuario(uuid, character varying, character varying, character varying, character varying, boolean, character varying, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.fn_agregar_usuario(p_id uuid, p_nombre character varying, p_apellido1 character varying, p_apellido2 character varying, p_passwordhash character varying, p_estado boolean, p_correo character varying, p_idrol integer) RETURNS uuid
    LANGUAGE plpgsql
    AS $$
BEGIN
    INSERT INTO usuarios (
        id,
        nombre,
        apellido1,
        apellido2,
        passwordhash,
        estado,
        correo,
        idrol
    )
    VALUES (
        p_id,
        p_nombre,
        p_apellido1,
        p_apellido2,
        p_passwordhash,
        p_estado,
        p_correo,
        p_idrol
    );
 
    RETURN p_id;
END;
$$;



--
-- TOC entry 223 (class 1255 OID 16774)
-- Name: obtenerusuariocorreo(text); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.obtenerusuariocorreo(p_correo text) RETURNS TABLE(id uuid, nombre text, apellido1 text, apellido2 text, passwordhash text, estado boolean, correo text, idrol integer, rol text)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT u.id,
           u.nombre::text,
           u.apellido1::text,
           u.apellido2::text,
           u.passwordhash::text,
           u.estado,
           u.correo::text,
           u.idrol,
           r.nombre::text AS rol
    FROM usuarios u
    INNER JOIN roles r ON r.id = u.idrol
    WHERE u.correo = p_correo;
END;
$$;



--
-- TOC entry 224 (class 1255 OID 16779)
-- Name: obtenerusuarioporid(uuid); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.obtenerusuarioporid(p_id uuid) RETURNS TABLE(id uuid, nombre text, apellido1 text, apellido2 text, passwordhash text, estado boolean, correo text, idrol integer, rol text)
    LANGUAGE sql STABLE
    AS $$
	SELECT u.id, u.nombre :: text, u.apellido1 :: text, u.apellido2 :: text, 
	u.passwordhash :: text, u.estado, u.correo :: text, u.idrol, r.nombre :: text as rol
	FROM usuarios u 
	INNER JOIN roles r ON u.idrol = r.id
	WHERE u.id = p_id;
$$;






SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 220 (class 1259 OID 16743)
-- Name: roles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.roles (
    id integer NOT NULL,
    nombre character varying(50) NOT NULL
);



--
-- TOC entry 219 (class 1259 OID 16742)
-- Name: roles_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.roles ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.roles_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);

CREATE OR REPLACE FUNCTION public.obtener_roles()
RETURNS SETOF public.roles
LANGUAGE sql
STABLE
AS $$

	SELECT * FROM roles;

$$;
--
-- TOC entry 221 (class 1259 OID 16750)
-- Name: usuarios; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.usuarios (
    id uuid NOT NULL,
    nombre character varying(20) NOT NULL,
    apellido1 character varying(40) NOT NULL,
    apellido2 character varying(40),
    passwordhash character varying NOT NULL,
    estado boolean DEFAULT true NOT NULL,
    correo character varying NOT NULL,
    idrol integer NOT NULL
);


--
-- TOC entry 5019 (class 0 OID 16743)
-- Dependencies: 220
-- Data for Name: roles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.roles (id, nombre) FROM stdin;
1	Soporte
2	Cliente
\.





--
-- TOC entry 5027 (class 0 OID 0)
-- Dependencies: 219
-- Name: roles_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.roles_id_seq', 2, true);


--
-- TOC entry 4865 (class 2606 OID 16749)
-- Name: roles roles_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.roles
    ADD CONSTRAINT roles_pkey PRIMARY KEY (id);


--
-- TOC entry 4867 (class 2606 OID 16766)
-- Name: usuarios usuarios_correo_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuarios
    ADD CONSTRAINT usuarios_correo_key UNIQUE (correo);


--
-- TOC entry 4869 (class 2606 OID 16764)
-- Name: usuarios usuarios_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuarios
    ADD CONSTRAINT usuarios_pkey PRIMARY KEY (id);


--
-- TOC entry 4870 (class 2606 OID 16767)
-- Name: usuarios fk_usuarios_idrol; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuarios
    ADD CONSTRAINT fk_usuarios_idrol FOREIGN KEY (idrol) REFERENCES public.roles(id) ON UPDATE CASCADE ON DELETE RESTRICT;


-- Completed on 2026-09-30 13:15:26

--
-- PostgreSQL database dump complete
--

\unrestrict 3Ky57w2gzRtkbWBuYNgW8UDZRtU32OEw2L7cpALdi6no7oQNocJr84PavKofvzY

