--
-- PostgreSQL database dump
--

\restrict NjIhwz4TuSqoiGacZr7xPbolqd3pmR2YbTcbafxT8gaXssbQFe6rHjqdswGyTap

-- Dumped from database version 18.6
-- Dumped by pg_dump version 18.6

-- Started on 2026-09-30 13:13:22

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
-- TOC entry 5019 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: pg_database_owner
--

COMMENT ON SCHEMA public IS 'standard public schema';


--
-- TOC entry 221 (class 1255 OID 16740)
-- Name: fn_agregar_ticket(uuid, text, text, text, uuid, uuid); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.fn_agregar_ticket(p_id uuid, p_asunto text, p_estado text, p_descripcion text, p_id_emisor uuid, p_id_soporte uuid) RETURNS uuid
    LANGUAGE plpgsql
    AS $$
DECLARE
    cr_fecha_creacion timestamp := (now() AT TIME ZONE 'America/Costa_Rica');
BEGIN
    INSERT INTO tickets (id, asunto, estado, descripcion, fechacreacion, idemisor, idsoporte)
    VALUES (p_id, p_asunto, p_estado, p_descripcion, cr_fecha_creacion, p_id_emisor, p_id_soporte);
 
    RETURN p_id;
END;
$$;



--
-- TOC entry 220 (class 1255 OID 16734)
-- Name: fn_agregar_ticket(uuid, text, text, date, text, uuid, uuid); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.fn_agregar_ticket(p_id uuid, p_asunto text, p_estado text, p_fecha_creacion date, p_descripcion text, p_id_emisor uuid, p_id_soporte uuid) RETURNS uuid
    LANGUAGE plpgsql
    AS $$
BEGIN
    INSERT INTO tickets (id, asunto, estado, descripcion, fecha_creacion, id_emisor, id_soporte)
    VALUES (p_id, p_asunto, p_estado, p_descripcion, p_fecha_creacion, p_id_emisor, p_id_soporte);
 
    RETURN p_id;
END;
$$;



--
-- TOC entry 222 (class 1255 OID 16735)
-- Name: fn_editar_ticket(uuid, text, text, text, uuid, uuid); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.fn_editar_ticket(p_id uuid, p_asunto text, p_estado text, p_descripcion text, p_id_emisor uuid, p_id_soporte uuid) RETURNS uuid
    LANGUAGE plpgsql
    AS $$
BEGIN
    UPDATE tickets
    SET asunto      = p_asunto,
        estado      = p_estado,
        descripcion = p_descripcion,
        idemisor   = p_id_emisor,
        idsoporte  = p_id_soporte
    WHERE id = p_id;
 
    IF NOT FOUND THEN
        RAISE EXCEPTION 'No se encontró un ticket con id %', p_id;
    END IF;
 
    RETURN p_id;
END;
$$;



--
-- TOC entry 223 (class 1255 OID 16736)
-- Name: fn_eliminar_ticket(uuid); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.fn_eliminar_ticket(p_id uuid) RETURNS uuid
    LANGUAGE plpgsql
    AS $$
BEGIN
    DELETE FROM tickets
    WHERE id = p_id;
 
    IF NOT FOUND THEN
        RAISE EXCEPTION 'No se encontró un ticket con id %', p_id;
    END IF;
 
    RETURN p_id;
END;
$$;



SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 219 (class 1259 OID 16721)
-- Name: tickets; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tickets (
    id uuid NOT NULL,
    asunto character varying(50) NOT NULL,
    estado character varying(30) NOT NULL,
    descripcion character varying NOT NULL,
    fechacreacion date NOT NULL,
    idemisor uuid NOT NULL,
    idsoporte uuid
);



--
-- TOC entry 224 (class 1255 OID 16780)
-- Name: obtener_ticket(uuid); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.obtener_ticket(p_id uuid) RETURNS SETOF public.tickets
    LANGUAGE sql STABLE
    AS $$

	SELECT * FROM tickets where id = p_id;

$$;



--
-- TOC entry 225 (class 1255 OID 16781)
-- Name: obtener_tickets(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.obtener_tickets() RETURNS SETOF public.tickets
    LANGUAGE sql STABLE
    AS $$

	SELECT * FROM tickets;

$$;



--
-- TOC entry 226 (class 1255 OID 16782)
-- Name: obtener_tickets_asignados(uuid); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.obtener_tickets_asignados(p_id uuid) RETURNS SETOF public.tickets
    LANGUAGE sql STABLE
    AS $$

	SELECT * FROM tickets WHERE idsoporte = p_id;

$$;



--
-- TOC entry 227 (class 1255 OID 16783)
-- Name: obtener_tickets_creados(uuid); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.obtener_tickets_creados(p_id uuid) RETURNS SETOF public.tickets
    LANGUAGE sql STABLE
    AS $$

	SELECT * FROM tickets WHERE idemisor = p_id;

$$;



--
-- TOC entry 228 (class 1255 OID 16784)
-- Name: obtener_tickets_pendientes(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.obtener_tickets_pendientes() RETURNS SETOF public.tickets
    LANGUAGE sql STABLE
    AS $$

	SELECT * FROM tickets WHERE estado = 'Pendiente';

$$;



--
-- TOC entry 5013 (class 0 OID 16721)
-- Dependencies: 219
-- Data for Name: tickets; Type: TABLE DATA; Schema: public; Owner: postgres
--


--
-- TOC entry 4865 (class 2606 OID 16733)
-- Name: tickets tickets_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tickets
    ADD CONSTRAINT tickets_pkey PRIMARY KEY (id);


-- Completed on 2026-09-30 13:13:22

--
-- PostgreSQL database dump complete
--

\unrestrict NjIhwz4TuSqoiGacZr7xPbolqd3pmR2YbTcbafxT8gaXssbQFe6rHjqdswGyTap

