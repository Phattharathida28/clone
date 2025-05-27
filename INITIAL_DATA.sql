--
-- PostgreSQL database dump
--

-- Dumped from database version 15.2
-- Dumped by pg_dump version 15.3

-- Started on 2024-01-08 04:25:23

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE "x-dev";
--
-- TOC entry 3536 (class 1262 OID 50161)
-- Name: x-dev; Type: DATABASE; Schema: -; Owner: -
--

CREATE DATABASE "x-dev" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.utf8';


\connect -reuse-previous=on "dbname='x-dev'"

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 7 (class 2615 OID 107194)
-- Name: db; Type: SCHEMA; Schema: -; Owner: -
--

CREATE SCHEMA db;


--
-- TOC entry 4 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: -
--

CREATE SCHEMA public;


--
-- TOC entry 3537 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: -
--

COMMENT ON SCHEMA public IS 'standard public schema';


--
-- TOC entry 6 (class 2615 OID 50166)
-- Name: su; Type: SCHEMA; Schema: -; Owner: -
--

CREATE SCHEMA su;


--
-- TOC entry 242 (class 1255 OID 50167)
-- Name: get_age_number(date); Type: FUNCTION; Schema: public; Owner: -
--

CREATE FUNCTION public.get_age_number(birthday date) RETURNS numeric
    LANGUAGE plpgsql
    AS $$
begin
	return extract(year FROM age(current_date, birthday))::numeric + (extract(month FROM age(current_date, birthday))::numeric * 0.01)::numeric(18,2);
end;
$$;


--
-- TOC entry 243 (class 1255 OID 50168)
-- Name: uuid_generate_v4(); Type: FUNCTION; Schema: public; Owner: -
--

CREATE FUNCTION public.uuid_generate_v4() RETURNS uuid
    LANGUAGE c STRICT PARALLEL SAFE
    AS '$libdir/uuid-ossp', 'uuid_generate_v4';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 238 (class 1259 OID 107195)
-- Name: language; Type: TABLE; Schema: db; Owner: -
--

CREATE TABLE db.language (
    language_code character varying(20) NOT NULL,
    description character varying(300),
    active boolean,
    created_by character varying(100),
    created_date timestamp(3) without time zone,
    created_program character varying(100),
    updated_by character varying(100),
    updated_date timestamp(3) without time zone,
    updated_program character varying(100)
);


--
-- TOC entry 239 (class 1259 OID 107202)
-- Name: language_lang; Type: TABLE; Schema: db; Owner: -
--

CREATE TABLE db.language_lang (
    language_code character varying(20) NOT NULL,
    language_code_forname character varying(20) NOT NULL,
    language_name character varying(200),
    created_by character varying(100),
    created_date timestamp(3) without time zone,
    created_program character varying(100),
    updated_by character varying(100),
    updated_date timestamp(3) without time zone,
    updated_program character varying(100)
);


--
-- TOC entry 241 (class 1259 OID 107219)
-- Name: list_item; Type: TABLE; Schema: db; Owner: -
--

CREATE TABLE db.list_item (
    list_item_group_code character varying(20) NOT NULL,
    list_item_code character varying(200) NOT NULL,
    list_item_name_tha character varying(500) NOT NULL COLLATE pg_catalog."C",
    list_item_name_eng character varying(500),
    sequence integer,
    remark character varying(200) COLLATE pg_catalog."C",
    attibute1 character varying(200) COLLATE pg_catalog."C",
    attibute2 character varying(200) COLLATE pg_catalog."C",
    attibute3 character varying(200) COLLATE pg_catalog."C",
    active boolean DEFAULT true NOT NULL,
    created_by character varying(50),
    created_date timestamp with time zone,
    created_program character varying(50),
    updated_by character varying(50),
    updated_date timestamp with time zone,
    updated_program character varying(50),
    attibute4 character varying(200),
    attibute5 character varying(200),
    list_item_short_name_tha character varying(500),
    list_item_short_name_eng character varying(500),
    list_item_code_mua character varying(20),
    list_item_desc_tha character varying(500),
    list_item_desc_eng character varying(500)
);


--
-- TOC entry 240 (class 1259 OID 107214)
-- Name: list_item_group; Type: TABLE; Schema: db; Owner: -
--

CREATE TABLE db.list_item_group (
    list_item_group_code character varying(20) NOT NULL,
    list_item_group_name character varying(200) NOT NULL COLLATE pg_catalog."C",
    system_code character varying(5) COLLATE pg_catalog."C",
    created_by character varying(50) NOT NULL COLLATE pg_catalog."C",
    created_date timestamp with time zone NOT NULL,
    created_program character varying(50) NOT NULL COLLATE pg_catalog."C",
    updated_by character varying(50) NOT NULL COLLATE pg_catalog."C",
    updated_date timestamp with time zone NOT NULL,
    updated_program character varying(50) NOT NULL COLLATE pg_catalog."C"
);


--
-- TOC entry 216 (class 1259 OID 50398)
-- Name: activity_log; Type: TABLE; Schema: su; Owner: -
--

CREATE TABLE su.activity_log (
    id integer NOT NULL,
    activity_type_code character varying(20) NOT NULL,
    log_message text,
    logged_by character varying(100),
    logged_date timestamp without time zone,
    active boolean DEFAULT true NOT NULL,
    created_by character varying(100),
    created_date timestamp without time zone,
    created_program character varying(100),
    updated_by character varying(100),
    updated_date timestamp without time zone,
    updated_program character varying(100)
);


--
-- TOC entry 217 (class 1259 OID 50404)
-- Name: activity_log_id_seq; Type: SEQUENCE; Schema: su; Owner: -
--

ALTER TABLE su.activity_log ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME su.activity_log_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 218 (class 1259 OID 50405)
-- Name: activity_type; Type: TABLE; Schema: su; Owner: -
--

CREATE TABLE su.activity_type (
    activity_type_code character varying(20) NOT NULL,
    activity_type_name character varying(200),
    activity_group_code character varying(100) NOT NULL,
    log_template text,
    active boolean DEFAULT true NOT NULL,
    created_by character varying(100),
    created_date timestamp(3) without time zone,
    created_program character varying(100),
    updated_by character varying(100),
    updated_date timestamp(3) without time zone,
    updated_program character varying(100)
);


--
-- TOC entry 219 (class 1259 OID 50411)
-- Name: content; Type: TABLE; Schema: su; Owner: -
--

CREATE TABLE su.content (
    id bigint NOT NULL,
    name character varying(500),
    path character varying(500),
    reference boolean,
    validate_path boolean DEFAULT false NOT NULL,
    created_by character varying(100),
    created_date timestamp(3) without time zone,
    created_program character varying(100),
    updated_by character varying(100),
    updated_date timestamp(3) without time zone,
    updated_program character varying(100),
    container character varying(500),
    size integer
);


--
-- TOC entry 220 (class 1259 OID 50417)
-- Name: content_id_seq; Type: SEQUENCE; Schema: su; Owner: -
--

ALTER TABLE su.content ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME su.content_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 221 (class 1259 OID 50418)
-- Name: email_template; Type: TABLE; Schema: su; Owner: -
--

CREATE TABLE su.email_template (
    email_template_code character varying(10) NOT NULL,
    subject character varying(100),
    content character varying,
    created_by character varying(50),
    created_date timestamp without time zone,
    created_program character varying(50),
    updated_by character varying(50),
    updated_date timestamp without time zone,
    updated_program character varying(50)
);


--
-- TOC entry 222 (class 1259 OID 50423)
-- Name: menu; Type: TABLE; Schema: su; Owner: -
--

CREATE TABLE su.menu (
    menu_code character varying(20) NOT NULL,
    program_code character varying(50),
    main_menu character varying(20),
    system_code character varying(5),
    icon character varying(50),
    active boolean,
    created_by character varying(100),
    created_date timestamp with time zone,
    created_program character varying(100),
    updated_by character varying(100),
    updated_date timestamp with time zone,
    updated_program character varying(100)
);


--
-- TOC entry 223 (class 1259 OID 50428)
-- Name: menu_label; Type: TABLE; Schema: su; Owner: -
--

CREATE TABLE su.menu_label (
    menu_code character varying(20) NOT NULL,
    language_code character varying(20) NOT NULL,
    menu_name character varying(200),
    system_code character varying(5),
    created_by character varying(100),
    created_date timestamp with time zone,
    created_program character varying(100),
    updated_by character varying(100),
    updated_date timestamp with time zone,
    updated_program character varying(100)
);


--
-- TOC entry 224 (class 1259 OID 50433)
-- Name: message; Type: TABLE; Schema: su; Owner: -
--

CREATE TABLE su.message (
    message_code character varying(20) NOT NULL,
    language_code character varying(20) NOT NULL,
    message_desc character varying(200),
    remark character varying(200),
    created_by character varying(100),
    created_date timestamp(3) without time zone,
    created_program character varying(100),
    updated_by character varying(100),
    updated_date timestamp(3) without time zone,
    updated_program character varying(100)
);


--
-- TOC entry 225 (class 1259 OID 50438)
-- Name: parameter; Type: TABLE; Schema: su; Owner: -
--

CREATE TABLE su.parameter (
    parameter_group_code character varying(100) NOT NULL,
    parameter_code character varying(100) NOT NULL,
    parameter_value character varying(200),
    remark character varying(200),
    created_by character varying(100),
    created_date timestamp(3) without time zone,
    created_program character varying(100),
    updated_by character varying(100),
    updated_date timestamp(3) without time zone,
    updated_program character varying(100)
);


--
-- TOC entry 226 (class 1259 OID 50443)
-- Name: password_policy; Type: TABLE; Schema: su; Owner: -
--

CREATE TABLE su.password_policy (
    password_policy_code character varying(10) NOT NULL,
    password_policy_name character varying(100),
    password_policy_description character varying(200),
    password_minimum_length numeric(18,0),
    password_maximum_length numeric(18,0),
    fail_time numeric(18,0),
    password_age numeric(18,0),
    max_dup_password integer,
    using_uppercase boolean,
    using_lowercase boolean,
    using_numeric_char boolean,
    using_special_char boolean,
    active boolean,
    created_by character varying(30),
    created_date timestamp(3) without time zone NOT NULL,
    updated_by character varying(30),
    updated_date timestamp(3) without time zone,
    updated_program character varying(50) NOT NULL,
    created_program character varying(50)
);


--
-- TOC entry 227 (class 1259 OID 50448)
-- Name: profile; Type: TABLE; Schema: su; Owner: -
--

CREATE TABLE su.profile (
    profile_code character varying(20) NOT NULL,
    description character varying(200),
    active boolean NOT NULL,
    created_by character varying(100),
    created_date timestamp with time zone,
    created_program character varying(100),
    updated_by character varying(100),
    updated_date timestamp with time zone,
    updated_program character varying(100),
    profile_type character varying(100)
);


--
-- TOC entry 228 (class 1259 OID 50453)
-- Name: profile_lang; Type: TABLE; Schema: su; Owner: -
--

CREATE TABLE su.profile_lang (
    profile_code character varying(20) NOT NULL,
    language_code character varying(20) NOT NULL,
    profile_name character varying(200),
    created_by character varying(100),
    created_date timestamp with time zone,
    created_program character varying(100),
    updated_by character varying(100),
    updated_date timestamp with time zone,
    updated_program character varying(100)
);


--
-- TOC entry 229 (class 1259 OID 50458)
-- Name: profile_menu; Type: TABLE; Schema: su; Owner: -
--

CREATE TABLE su.profile_menu (
    profile_code character varying(20) NOT NULL,
    menu_code character varying(20) NOT NULL,
    created_by character varying(100),
    created_date timestamp with time zone,
    created_program character varying(100),
    updated_by character varying(100),
    updated_date timestamp with time zone,
    updated_program character varying(100)
);


--
-- TOC entry 230 (class 1259 OID 50461)
-- Name: program; Type: TABLE; Schema: su; Owner: -
--

CREATE TABLE su.program (
    program_code character varying(50) NOT NULL,
    program_name character varying(200),
    program_path character varying(200),
    system_code character varying(5),
    module_code character varying(5),
    created_by character varying(100),
    created_date timestamp with time zone,
    created_program character varying(100),
    updated_by character varying(100),
    updated_date timestamp with time zone,
    updated_program character varying(100)
);


--
-- TOC entry 231 (class 1259 OID 50466)
-- Name: program_label; Type: TABLE; Schema: su; Owner: -
--

CREATE TABLE su.program_label (
    program_code character varying(50) NOT NULL,
    field_name character varying(200) NOT NULL,
    language_code character varying(20) NOT NULL,
    label_name character varying(1000),
    system_code character varying(5),
    module_code character varying(5),
    created_by character varying(100),
    created_date timestamp with time zone,
    created_program character varying(100),
    updated_by character varying(100),
    updated_date timestamp with time zone,
    updated_program character varying(100)
);


--
-- TOC entry 232 (class 1259 OID 50471)
-- Name: report_template; Type: TABLE; Schema: su; Owner: -
--

CREATE TABLE su.report_template (
    id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    value character varying NOT NULL,
    created_by character varying(50),
    created_date timestamp without time zone,
    created_program character varying(50),
    updated_by character varying(50),
    updated_date timestamp without time zone,
    updated_program character varying(50),
    type character varying(50)
);


--
-- TOC entry 233 (class 1259 OID 50477)
-- Name: role; Type: TABLE; Schema: su; Owner: -
--

CREATE TABLE su.role (
    id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    name character varying(100),
    active boolean,
    description character varying(255),
    created_by character varying(50),
    created_date timestamp(3) without time zone,
    create_program character varying(50),
    updated_by character varying(50),
    updated_date timestamp(3) without time zone,
    updated_program character varying(50)
);


--
-- TOC entry 234 (class 1259 OID 50483)
-- Name: role_user; Type: TABLE; Schema: su; Owner: -
--

CREATE TABLE su.role_user (
    user_id bigint NOT NULL,
    role_id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    created_by character varying(50),
    created_date timestamp(3) without time zone,
    create_program character varying(50),
    updated_by character varying(50),
    updated_date timestamp(3) without time zone,
    updated_program character varying(50)
);


--
-- TOC entry 235 (class 1259 OID 50487)
-- Name: user; Type: TABLE; Schema: su; Owner: -
--

CREATE TABLE su."user" (
    user_id bigint NOT NULL,
    user_name character varying(50),
    password_policy_code character varying(10),
    password_hash text,
    default_lang character varying(20),
    lockout_enabled boolean DEFAULT true,
    force_change_password boolean,
    start_effective_date timestamp without time zone,
    end_effective_date timestamp without time zone,
    last_change_password timestamp without time zone,
    access_failed_count integer,
    concurrency_stamp character varying(50),
    security_stamp character varying(50),
    email character varying(200),
    phone_number character varying(20),
    firstname character varying(200),
    lastname character varying(200),
    normalized_user_name character varying(50),
    lockout_end timestamp with time zone,
    active boolean DEFAULT true,
    created_by character varying(100),
    created_date timestamp with time zone,
    created_program character varying(100),
    updated_by character varying(100),
    updated_date timestamp with time zone,
    updated_program character varying(100),
    firstname_th character varying(200),
    lastname_th character varying(200),
    ripple boolean,
    inputstyle character varying(30),
    menumode character varying(30),
    colorscheme character varying(30),
    color character varying(100),
    scale integer
);


--
-- TOC entry 236 (class 1259 OID 50494)
-- Name: user_profile; Type: TABLE; Schema: su; Owner: -
--

CREATE TABLE su.user_profile (
    user_id bigint NOT NULL,
    profile_code character varying(20) NOT NULL,
    created_by character varying(100),
    created_date timestamp(3) without time zone,
    created_program character varying(100),
    updated_by character varying(100),
    updated_date timestamp(3) without time zone,
    updated_program character varying(100)
);


--
-- TOC entry 237 (class 1259 OID 50497)
-- Name: user_user_id_seq; Type: SEQUENCE; Schema: su; Owner: -
--

ALTER TABLE su."user" ALTER COLUMN user_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME su.user_user_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 3527 (class 0 OID 107195)
-- Dependencies: 238
-- Data for Name: language; Type: TABLE DATA; Schema: db; Owner: -
--

INSERT INTO db.language VALUES ('EN', 'English', true, 'Script', '2022-08-18 00:00:00', 'Script', 'Script', '2022-08-18 00:00:00', 'Script');
INSERT INTO db.language VALUES ('TH', 'Thai', true, 'Script', '2022-08-18 00:00:00', 'Script', 'Script', '2022-08-18 00:00:00', 'Script');


--
-- TOC entry 3528 (class 0 OID 107202)
-- Dependencies: 239
-- Data for Name: language_lang; Type: TABLE DATA; Schema: db; Owner: -
--

INSERT INTO db.language_lang VALUES ('EN', 'EN', 'English', 'Script', '2022-08-18 00:00:00', 'Script', 'Script', '2022-08-18 00:00:00', 'Script');
INSERT INTO db.language_lang VALUES ('EN', 'TH', 'อังกฤษ', 'Script', '2022-08-18 00:00:00', 'Script', 'Script', '2022-08-18 00:00:00', 'Script');
INSERT INTO db.language_lang VALUES ('TH', 'EN', 'Thai', 'Script', '2022-08-18 00:00:00', 'Script', 'Script', '2022-08-18 00:00:00', 'Script');
INSERT INTO db.language_lang VALUES ('TH', 'TH', 'ไทย', 'Script', '2022-08-18 00:00:00', 'Script', 'Script', '2022-08-18 00:00:00', 'Script');


--
-- TOC entry 3530 (class 0 OID 107219)
-- Dependencies: 241
-- Data for Name: list_item; Type: TABLE DATA; Schema: db; Owner: -
--

INSERT INTO db.list_item VALUES ('Module', 'DB', 'ข้อมูลตั้งต้น', 'Master', 1, NULL, NULL, NULL, NULL, true, 'Por', '2023-01-04 09:16:49.383+00', NULL, 'Por', '2023-01-04 09:16:52.725+00', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO db.list_item VALUES ('Module', 'SU', 'ตั้งค่าระบบ', 'System setting', 5, NULL, NULL, NULL, NULL, true, 'Por', '2023-01-04 09:16:49.383+00', NULL, 'Por', '2023-01-04 09:16:52.725+00', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);


--
-- TOC entry 3529 (class 0 OID 107214)
-- Dependencies: 240
-- Data for Name: list_item_group; Type: TABLE DATA; Schema: db; Owner: -
--

INSERT INTO db.list_item_group VALUES ('Module', 'Module', NULL, 'sql', '2023-01-28 11:14:00.772+00', 'DB', 'admin', '2023-05-16 07:52:50.554+00', 'DBRT12');


--
-- TOC entry 3505 (class 0 OID 50398)
-- Dependencies: 216
-- Data for Name: activity_log; Type: TABLE DATA; Schema: su; Owner: -
--



--
-- TOC entry 3507 (class 0 OID 50405)
-- Dependencies: 218
-- Data for Name: activity_type; Type: TABLE DATA; Schema: su; Owner: -
--

INSERT INTO su.activity_type VALUES ('1001', 'Login Success', '1000', '[UserCode]has successfully logged-in ', true, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO su.activity_type VALUES ('1002', 'Login Failed', '1000', '[UserCode] has logged in failed', true, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO su.activity_type VALUES ('1003', 'Logout Success', '1000', '[UserCode] has successfully logged out', true, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO su.activity_type VALUES ('1004', 'Logout Failed', '1000', '[UserCode] has logged in failed', true, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO su.activity_type VALUES ('3010', 'Change Password', '3000', '[UserCode] has change password', true, NULL, NULL, NULL, NULL, NULL, NULL);


--
-- TOC entry 3508 (class 0 OID 50411)
-- Dependencies: 219
-- Data for Name: content; Type: TABLE DATA; Schema: su; Owner: -
--



--
-- TOC entry 3510 (class 0 OID 50418)
-- Dependencies: 221
-- Data for Name: email_template; Type: TABLE DATA; Schema: su; Owner: -
--

INSERT INTO su.email_template VALUES ('SU001', 'ARMS : New Password Request', '<p>Email : [EMAIL]</p><p>User : [USERNAME]</p><p>New password : [NEWPASSWORD]</p>', 'migrated', '2023-05-30 15:14:10.892', 'migrated', 'migrated', '2023-05-30 15:14:10.892', 'migrated');


--
-- TOC entry 3511 (class 0 OID 50423)
-- Dependencies: 222
-- Data for Name: menu; Type: TABLE DATA; Schema: su; Owner: -
--

INSERT INTO su.menu VALUES ('99SU00002', 'SURT02', '99SU00000', 'ccs', 'pi pi-bars', true, 'migrate', '2023-01-27 06:47:22.294+00', 'migrate', 'admin', '2023-02-07 06:34:06.464+00', NULL);
INSERT INTO su.menu VALUES ('99SU00003', 'SURT03', '99SU00000', 'ccs', 'pi pi-lock', true, 'admin', '2023-02-07 00:16:28.724+00', NULL, 'admin', '2023-02-07 00:18:39.577+00', NULL);
INSERT INTO su.menu VALUES ('99SU00004', 'SURT04', '99SU00000', 'ccs', 'pi pi-users', true, 'admin', '2023-02-07 06:33:45.098+00', NULL, 'admin', '2023-02-07 06:33:45.098+00', NULL);
INSERT INTO su.menu VALUES ('99SU00001', 'SURT01', '99SU00000', 'ccs', 'pi pi-desktop', true, 'migrate', '2023-01-27 06:47:22.294+00', 'migrate', 'phasit_s', '2024-01-07 17:24:06.919573+00', 'SURT02');
INSERT INTO su.menu VALUES ('99SU00000', NULL, NULL, 'ccs', 'pi pi-lock', true, 'migrate', '2023-01-27 06:47:19.269+00', 'migrate', 'phasit_s', '2024-01-08 01:40:40.630274+00', 'SURT02');


--
-- TOC entry 3512 (class 0 OID 50428)
-- Dependencies: 223
-- Data for Name: menu_label; Type: TABLE DATA; Schema: su; Owner: -
--

INSERT INTO su.menu_label VALUES ('99SU00004', 'EN', 'User Management', 'ccs', 'admin', '2023-02-07 06:33:45.098+00', NULL, 'admin', '2023-02-07 06:33:45.098+00', NULL);
INSERT INTO su.menu_label VALUES ('99SU00004', 'TH', 'กำหนดผู้ใช้งาน', 'ccs', 'admin', '2023-02-07 06:33:45.098+00', NULL, 'admin', '2023-02-07 06:33:45.098+00', NULL);
INSERT INTO su.menu_label VALUES ('99SU00003', 'EN', 'Permission Management', 'ccs', 'admin', '2023-02-07 00:16:28.724+00', NULL, 'admin', '2023-02-07 00:18:39.577+00', NULL);
INSERT INTO su.menu_label VALUES ('99SU00003', 'TH', 'กำหนดสิทธิ์การใช้งานโปรแกรม', 'ccs', 'admin', '2023-02-07 00:16:28.724+00', NULL, 'admin', '2023-02-07 00:18:39.577+00', NULL);
INSERT INTO su.menu_label VALUES ('99SU00002', 'EN', 'Menu Management', NULL, 'migrate', '2023-01-27 06:47:28.45+00', 'migrate', 'admin', '2023-02-07 06:34:06.464+00', NULL);
INSERT INTO su.menu_label VALUES ('99SU00002', 'TH', 'กำหนดเมนู', NULL, 'migrate', '2023-01-27 06:47:28.45+00', 'migrate', 'admin', '2023-02-07 06:34:06.464+00', NULL);
INSERT INTO su.menu_label VALUES ('99SU00001', 'EN', 'Program Management', NULL, 'migrate', '2023-01-27 06:47:28.45+00', 'migrate', 'phasit_s', '2024-01-07 17:24:06.919575+00', 'SURT02');
INSERT INTO su.menu_label VALUES ('99SU00001', 'TH', 'กำหนดโปรแกรม', NULL, 'migrate', '2023-01-27 06:47:28.45+00', 'migrate', 'phasit_s', '2024-01-07 17:24:06.919576+00', 'SURT02');
INSERT INTO su.menu_label VALUES ('99SU00000', 'EN', 'System Setting', NULL, 'migrate', '2023-01-27 06:47:28.45+00', 'migrate', 'phasit_s', '2024-01-08 01:40:40.630278+00', 'SURT02');
INSERT INTO su.menu_label VALUES ('99SU00000', 'TH', 'ตั้งค่าระบบ', NULL, 'migrate', '2023-01-27 06:47:28.45+00', 'migrate', 'phasit_s', '2024-01-08 01:40:40.630279+00', 'SURT02');


--
-- TOC entry 3513 (class 0 OID 50433)
-- Dependencies: 224
-- Data for Name: message; Type: TABLE DATA; Schema: su; Owner: -
--

INSERT INTO su.message VALUES ('STD00000', 'EN', 'Successfully', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00000', 'TH', 'สำเร็จ', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00001', 'TH', 'ข้อมูบ', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00001', 'EN', 'Info', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00002', 'TH', 'คำเตือน', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00002', 'EN', 'Warning', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00003', 'TH', 'พบข้อผิดพลาด', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00003', 'EN', 'Error', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00004', 'TH', 'ไม่มีข้อความ', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00004', 'EN', 'No Message', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00005', 'TH', 'กรุณาตรวจสอบการเชื่อมต่อ', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00005', 'EN', 'Please check the connection', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00006', 'TH', 'ไม่พบ URL ที่ร้องขอ ({{0}}) บนเซิร์ฟเวอร์นี้', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00006', 'EN', 'The requested URL({{0}}) was not found on this server', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00007', 'TH', 'กรุณาระบุ', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00007', 'EN', 'Must be entered.', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00008', 'TH', 'กรุณาระบุอย่างน้อย {{0}} {{1}}', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00008', 'EN', 'Please enter at least {{0}} {{1}}.', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00009', 'TH', 'กรุณาระบุไม่เกิน {{0}} {{1}}', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00009', 'EN', 'Please enter no more than {{0}} {{1}}.', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00010', 'TH', 'ละทิ้งการเปลี่ยนแปลงหรือไม่', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00010', 'EN', 'Discard the changes?', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00011', 'TH', 'กรุณาบันทึกข้อมูลก่อนทำการคัดลอก', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00011', 'EN', 'Please save data before copy.', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00012', 'TH', 'คัดลอกข้อมูลสำเร็จ', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00012', 'EN', 'Copy data successfully', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00013', 'TH', 'กรุณากรอกข้อมูลให้ถูกต้อง', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00013', 'EN', 'Please enter correct information.', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00014', 'TH', 'บันทึกข้อมูลสำเร็จ', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00014', 'EN', 'Data saved successfully.', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00015', 'TH', 'กรุณายืนยันการลบข้อมูล', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00015', 'EN', 'Please confirm to delete !', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00016', 'TH', 'การลบข้อมูลสำเร็จ', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00016', 'EN', 'Successfully deleted', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00017', 'TH', 'รูปแบบข้อมูลไม่ถูกต้อง', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');
INSERT INTO su.message VALUES ('STD00017', 'EN', 'Pattern invalid', NULL, 'sql', '2024-01-05 00:45:02.634', 'sql', 'sql', '2024-01-05 00:45:02.634', 'sql');


--
-- TOC entry 3514 (class 0 OID 50438)
-- Dependencies: 225
-- Data for Name: parameter; Type: TABLE DATA; Schema: su; Owner: -
--

INSERT INTO su.parameter VALUES ('Email', 'Host', 'smtp.gmail.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO su.parameter VALUES ('Email', 'Port', '587', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO su.parameter VALUES ('Email', 'Security', '1', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO su.parameter VALUES ('Email', 'SenderName', 'admin', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO su.parameter VALUES ('Configuration', 'FormUrl', 'https://pe-arms.clins.app/form/', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO su.parameter VALUES ('Configuration', 'EntryUrl', 'https://pe-arms.clins.app/entry', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO su.parameter VALUES ('Configuration', 'WebUrl', 'http://localhost:4200', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO su.parameter VALUES ('Configuration', 'IdentityUrl', 'https://localhost:5000', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO su.parameter VALUES ('ContentPath', 'ApiUrl', 'https://localhost:7258', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO su.parameter VALUES ('ContentPath', 'DisplayUrl', 'https://localhost:7258', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO su.parameter VALUES ('Email', 'Password', 'kkylrgxjsksqgwlz', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO su.parameter VALUES ('Email', 'UserName', 'softchon.ks2@gmail.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL);


--
-- TOC entry 3515 (class 0 OID 50443)
-- Dependencies: 226
-- Data for Name: password_policy; Type: TABLE DATA; Schema: su; Owner: -
--



--
-- TOC entry 3516 (class 0 OID 50448)
-- Dependencies: 227
-- Data for Name: profile; Type: TABLE DATA; Schema: su; Owner: -
--

INSERT INTO su.profile VALUES ('admin', 'ADMIN', true, 'earth', '2022-11-11 08:46:24.406+00', 'sql', 'phasit_s', '2024-01-08 03:59:48.25278+00', 'SURT03', NULL);


--
-- TOC entry 3517 (class 0 OID 50453)
-- Dependencies: 228
-- Data for Name: profile_lang; Type: TABLE DATA; Schema: su; Owner: -
--



--
-- TOC entry 3518 (class 0 OID 50458)
-- Dependencies: 229
-- Data for Name: profile_menu; Type: TABLE DATA; Schema: su; Owner: -
--

INSERT INTO su.profile_menu VALUES ('admin', '99SU00000', 'phasit_s', '2024-01-08 03:59:48.25276+00', 'SURT03', 'phasit_s', '2024-01-08 03:59:48.252771+00', 'SURT03');
INSERT INTO su.profile_menu VALUES ('admin', '99SU00001', 'phasit_s', '2024-01-08 03:59:48.252772+00', 'SURT03', 'phasit_s', '2024-01-08 03:59:48.252772+00', 'SURT03');
INSERT INTO su.profile_menu VALUES ('admin', '99SU00002', 'phasit_s', '2024-01-08 03:59:48.252772+00', 'SURT03', 'phasit_s', '2024-01-08 03:59:48.252775+00', 'SURT03');
INSERT INTO su.profile_menu VALUES ('admin', '99SU00003', 'phasit_s', '2024-01-08 03:59:48.252775+00', 'SURT03', 'phasit_s', '2024-01-08 03:59:48.252775+00', 'SURT03');
INSERT INTO su.profile_menu VALUES ('admin', '99SU00004', 'phasit_s', '2024-01-08 03:59:48.252775+00', 'SURT03', 'phasit_s', '2024-01-08 03:59:48.252775+00', 'SURT03');


--
-- TOC entry 3519 (class 0 OID 50461)
-- Dependencies: 230
-- Data for Name: program; Type: TABLE DATA; Schema: su; Owner: -
--

INSERT INTO su.program VALUES ('SUMT00', 'Identity', '-', 'ccs', NULL, NULL, '2022-03-18 07:33:01.43+00', 'SUMT02', 'admin', '2023-02-05 11:30:39.496+00', NULL);
INSERT INTO su.program VALUES ('DEMO', 'Demo', '', 'ccs', 'DEMO', 'por', '2022-12-20 08:04:55.82+00', 'sql', 'nantanut.c', '2023-02-19 05:07:05.493+00', 'SURT01');
INSERT INTO su.program VALUES ('SURT01', 'Program', 'su/surt01', 'ccs', 'SU', 'migrate', '2023-01-27 06:47:15.547+00', 'migrate', 'phasit_s', '2024-01-07 14:37:06.845487+00', 'SURT01');
INSERT INTO su.program VALUES ('SURT02', 'Menu', 'su/surt02', 'ccs', 'SU', 'migrate', '2023-01-27 06:47:15.547+00', 'migrate', 'phasit_s', '2024-01-07 17:20:39.063606+00', 'SURT01');
INSERT INTO su.program VALUES ('SURT03', 'Permission Management', 'su/surt03', 'ccs', 'SU', 'admin', '2023-02-07 00:09:38.917+00', NULL, 'phasit_s', '2024-01-08 00:34:29.873163+00', 'SURT01');
INSERT INTO su.program VALUES ('ALL', 'All Program', '', 'ccs', 'ALL', 'por', '2022-12-20 08:04:55.82+00', 'sql', 'phasit_s', '2024-01-08 01:40:45.146795+00', 'SURT01');
INSERT INTO su.program VALUES ('SURT04', 'User Management', 'su/surt04', 'ccs', 'SU', 'admin', '2023-02-07 06:32:59.857+00', NULL, 'phasit_s', '2024-01-08 03:57:07.124488+00', 'SURT01');


--
-- TOC entry 3520 (class 0 OID 50466)
-- Dependencies: 231
-- Data for Name: program_label; Type: TABLE DATA; Schema: su; Owner: -
--

INSERT INTO su.program_label VALUES ('ALL', 'ConfirmNewPassword', 'TH', 'ยืนยันรหัสผ่านใหม่', 'ccs', 'ALL', 'admin', '2023-05-21 23:52:16.868+00', 'SURT01', 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'ConfirmNewPassword', 'EN', 'Confirm New Password', 'ccs', 'ALL', 'admin', '2023-05-21 23:54:02.261+00', 'SURT01', 'admin', '2023-05-21 23:54:02.261+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Copy', 'EN', 'Copy', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:54:02.261+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Copy', 'TH', 'คัดลอก', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:33.865+00', NULL, 'admin', '2023-05-21 23:54:02.261+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Delete', 'EN', 'Delete', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:16.868+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Delete', 'TH', 'ลบ', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:33.865+00', NULL, 'admin', '2023-05-21 23:54:02.261+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Detail', 'EN', 'Detail', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'admin', '2023-05-21 23:52:16.868+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Detail', 'TH', 'รายละเอียด', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'admin', '2023-05-21 23:54:02.261+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Edit', 'EN', 'Edit', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'admin', '2023-05-21 23:52:16.868+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Edit', 'TH', 'แก้ไข', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'admin', '2023-05-21 23:54:02.261+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'From', 'EN', 'From', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:16.868+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'From', 'TH', 'จาก', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:33.865+00', NULL, 'admin', '2023-05-21 23:54:02.261+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Greeting', 'TH', 'สวัสดี', 'ccs', 'ALL', 'admin', '2023-05-21 20:51:51.107+00', 'SURT01', 'admin', '2023-05-21 23:54:02.261+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Greeting', 'EN', 'Hi', 'ccs', 'ALL', 'admin', '2023-05-21 20:52:21.367+00', 'SURT01', 'admin', '2023-05-21 23:52:16.868+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Label', 'EN', 'Label', '', 'ALL', 'admin', '2023-01-30 23:57:24.537+00', NULL, 'admin', '2023-05-21 23:52:16.868+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Label', 'TH', 'เครื่องหมาย', '', 'ALL', 'admin', '2023-01-30 23:57:24.537+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'NewPassword', 'TH', 'รหัสผ่านใหม่', 'ccs', 'ALL', 'admin', '2023-05-21 23:52:16.868+00', 'SURT01', 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'NewPassword', 'EN', 'New Password', 'ccs', 'ALL', 'admin', '2023-05-21 23:54:02.261+00', 'SURT01', 'admin', '2023-05-21 23:54:02.261+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'NotFound', 'EN', 'Data not found', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'admin', '2023-05-21 23:54:02.261+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'NotFound', 'TH', 'ไม่พบข้อมูล', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'admin', '2023-05-21 23:54:02.261+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'OldPassword', 'TH', 'รหัสผ่านเดิม', 'ccs', 'ALL', 'admin', '2023-05-21 23:52:16.868+00', 'SURT01', 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'OldPassword', 'EN', 'Old Password', 'ccs', 'ALL', 'admin', '2023-05-21 23:54:02.261+00', 'SURT01', 'admin', '2023-05-21 23:54:02.261+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'PleaseConfirm', 'EN', 'Please Confirm', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'admin', '2023-05-21 23:52:16.868+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'PleaseConfirm', 'TH', 'กรุณายืนยัน', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'admin', '2023-05-21 23:54:02.261+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'PleaseSelect', 'TH', 'โปรดใส่ข้อมูล', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'admin', '2023-05-21 23:54:02.261+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'PleaseSelect', 'EN', 'Please Select', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'admin', '2023-05-21 23:52:16.868+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Save', 'EN', 'Save', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'admin', '2023-05-21 23:54:02.261+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Save', 'TH', 'บันทึก', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'admin', '2023-05-21 23:54:02.261+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Search', 'EN', 'Search', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'admin', '2023-05-21 23:54:02.261+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Search', 'TH', 'ค้นหา', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'SetupNewPassword', 'TH', 'ตั้งค่ารหัสผ่านใหม่', 'ccs', 'ALL', 'admin', '2023-05-21 23:52:16.868+00', 'SURT01', 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'SetupNewPassword', 'EN', 'Setup New Password', 'ccs', 'ALL', 'admin', '2023-05-21 23:54:02.261+00', 'SURT01', 'admin', '2023-05-21 23:54:02.261+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Status', 'EN', 'Status', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'admin', '2023-05-21 23:54:02.261+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Status', 'TH', 'สถานะ', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'admin', '2023-05-21 23:54:02.261+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'To', 'EN', 'To', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:16.868+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'To', 'TH', 'ถึง', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:33.865+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Update', 'TH', 'อัปเดต', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'admin', '2023-05-21 23:54:02.261+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Update', 'EN', 'Update', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'admin', '2023-05-21 23:54:02.261+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Upload', 'EN', 'Upload', '', 'ALL', 'admin', '2023-01-30 23:57:24.537+00', NULL, 'admin', '2023-05-21 23:52:16.868+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'View', 'EN', 'View', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:16.868+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'View', 'TH', 'ดู', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Default', 'EN', 'Default', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Submit', 'TH', 'Submit', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Upload', 'TH', 'อัพโหลด', NULL, 'ALL', 'admin', '2023-01-30 23:57:24.537+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Submit', 'EN', 'Submit', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Disabled', 'EN', 'Disabled', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Link', 'EN', 'Link', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Severities', 'EN', 'Severities', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Default', 'TH', 'Default', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Disabled', 'TH', 'Disabled', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Link', 'TH', 'Link', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Severities', 'TH', 'Severities', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Primary', 'TH', 'Primary', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Primary', 'EN', 'Primary', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Secondary', 'TH', 'Secondary', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Secondary', 'EN', 'Secondary', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Success', 'TH', 'Success', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Success', 'EN', 'Success', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Info', 'TH', 'Info', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Info', 'EN', 'Info', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Warning', 'TH', 'Warning', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Warning', 'EN', 'Warning', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Help', 'TH', 'Help', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Help', 'EN', 'Help', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Danger', 'TH', 'Danger', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Danger', 'EN', 'Danger', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Plain', 'TH', 'Plain', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Plain', 'EN', 'Plain', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Text', 'TH', 'Text', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Text', 'EN', 'Text', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Outlined', 'TH', 'Outlined', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Outlined', 'EN', 'Outlined', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'ButtonSet', 'TH', 'Button Set', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'ButtonSet', 'EN', 'Button Set', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Templating', 'TH', 'Templating', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Templating', 'EN', 'Templating', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'PrimeNG', 'TH', 'PrimeNG', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'PrimeNG', 'EN', 'PrimeNG', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Icons', 'TH', 'Icons', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Icons', 'EN', 'Icons', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Search', 'TH', 'Search', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Search', 'EN', 'Search', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Raised', 'TH', 'Raised', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Raised', 'EN', 'Raised', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Rounded', 'TH', 'Rounded', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Rounded', 'EN', 'Rounded', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'RoundedIcons', 'TH', 'Rounded Icons', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'RoundedIcons', 'EN', 'Rounded Icons', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'RoundedText', 'TH', 'Rounded Text', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'RoundedText', 'EN', 'Rounded Text', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'RoundedOutlined', 'TH', 'Rounded Outlined', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'RoundedOutlined', 'EN', 'Rounded Outlined', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Loading', 'TH', 'Loading', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Loading', 'EN', 'Loading', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Save', 'TH', 'Save', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Save', 'EN', 'Save', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Delete', 'TH', 'Delete', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Delete', 'EN', 'Delete', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'LinearChart', 'TH', 'Linear Chart', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'LinearChart', 'EN', 'Linear Chart', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'PieChart', 'TH', 'Pie Chart', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'PieChart', 'EN', 'Pie Chart', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'PolarAreaChart', 'TH', 'Polar Area Chart', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'PolarAreaChart', 'EN', 'Polar Area Chart', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'BarChart', 'TH', 'Bar Chart', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'BarChart', 'EN', 'Bar Chart', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'DoughnutChart', 'TH', 'Doughnut Chart', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'DoughnutChart', 'EN', 'Doughnut Chart', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'RadarChart', 'TH', 'Radar Chart', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'RadarChart', 'EN', 'Radar Chart', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'FloatLabel', 'TH', 'Float Label', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'FloatLabel', 'EN', 'Float Label', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'InputText', 'TH', 'InputText', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'InputText', 'EN', 'InputText', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'AutoComplete', 'TH', 'AutoComplete', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'AutoComplete', 'EN', 'AutoComplete', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'LeftIcon', 'TH', 'Left Icon', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'LeftIcon', 'EN', 'Left Icon', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'RightIcon', 'TH', 'Right Icon', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'RightIcon', 'EN', 'Right Icon', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Calendar', 'TH', 'Calendar', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Calendar', 'EN', 'Calendar', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Chips', 'TH', 'Chips', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Chips', 'EN', 'Chips', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'InputMask', 'TH', 'InputMask', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'InputMask', 'EN', 'InputMask', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'InputNumber', 'TH', 'InputNumber', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'InputNumber', 'EN', 'InputNumber', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'InputGroup', 'TH', 'InputGroup', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'InputGroup', 'EN', 'InputGroup', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Dropdown', 'TH', 'Dropdown', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Dropdown', 'EN', 'Dropdown', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'MultiSelect', 'TH', 'MultiSelect', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'MultiSelect', 'EN', 'MultiSelect', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Textarea', 'TH', 'Textarea', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Textarea', 'EN', 'Textarea', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Vertical', 'TH', 'Vertical', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Vertical', 'EN', 'Vertical', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Name', 'TH', 'Name', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Name', 'EN', 'Name', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Email', 'TH', 'Email', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Email', 'EN', 'Email', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Age', 'TH', 'Age', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Age', 'EN', 'Age', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'VerticalGrid', 'TH', 'Vertical Grid', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'VerticalGrid', 'EN', 'Vertical Grid', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Horizontal', 'TH', 'Horizontal', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Horizontal', 'EN', 'Horizontal', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Inline', 'TH', 'Inline', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Inline', 'EN', 'Inline', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Firstname', 'TH', 'Firstname', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Firstname', 'EN', 'Firstname', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Lastname', 'TH', 'Lastname', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Lastname', 'EN', 'Lastname', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'HelpText', 'TH', 'Help Text', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'HelpText', 'EN', 'Help Text', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'HelpTextRequiredUsernameAndPassword', 'TH', 'Enter your username to reset your password.', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'HelpTextRequiredUsernameAndPassword', 'EN', 'Enter your username to reset your password.', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Username', 'TH', 'Username', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Username', 'EN', 'Username', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Advanced', 'TH', 'Advanced', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Advanced', 'EN', 'Advanced', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Address', 'TH', 'Address', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Address', 'EN', 'Address', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'City', 'TH', 'City', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'City', 'EN', 'City', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'State', 'TH', 'State', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'State', 'EN', 'State', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Zip', 'TH', 'Zip', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('DEMO', 'Zip', 'EN', 'Zip', 'ccs', 'DEMO', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'DDMMYYYY', 'EN', 'dd/mm/yyyy', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:16.868+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'DDMMYYYY', 'TH', 'วัน/เดือน/ปี ค.ศ.', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'RippleEffect', 'EN', 'Ripple Effect', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:16.868+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Setting', 'TH', 'ตั้งค่า', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'MenuType', 'EN', 'Menu Type', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:16.868+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'MenuType', 'TH', 'ประเภทเมนู', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'InputStyle', 'EN', 'InputStyle', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:16.868+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'RippleEffect', 'TH', 'คลื่นน้ำอนิเมชั่น', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'DarkMode', 'EN', 'Dark Mode', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:16.868+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'DarkMode', 'TH', 'โหมดมืด', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Theme', 'EN', 'Theme', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:16.868+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Theme', 'TH', 'ธีม', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Scale', 'EN', 'Scale', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:16.868+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Scale', 'TH', 'ขนาด', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Setting', 'EN', 'Setting', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:16.868+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'InputStyle', 'TH', 'ประเภทอินพุต', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT01', 'ProgramManagement', 'EN', 'Program Management', 'ccs', 'SU', 'phasit_s', '2024-01-07 14:33:36.74553+00', 'SURT01', 'phasit_s', '2024-01-07 14:37:06.845491+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT01', 'ProgramManagement', 'TH', 'กำหนดโปรแกรม', 'ccs', 'SU', 'phasit_s', '2024-01-07 14:33:23.025113+00', 'SURT01', 'phasit_s', '2024-01-07 14:37:06.845492+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT01', 'Detail', 'EN', 'Detail', 'ccs', 'SU', 'phasit_s', '2024-01-07 14:36:18.675544+00', 'SURT01', 'phasit_s', '2024-01-07 14:37:06.845497+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT01', 'Detail', 'TH', 'รายละเอียด', 'ccs', 'SU', 'phasit_s', '2024-01-07 14:36:16.243623+00', 'SURT01', 'phasit_s', '2024-01-07 14:37:06.845493+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT01', 'Field', 'EN', 'Field', 'ccs', 'SU', 'phasit_s', '2024-01-07 14:36:18.675548+00', 'SURT01', 'phasit_s', '2024-01-07 14:37:06.8455+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT01', 'Field', 'TH', 'ชื่อจุดแสดงข้อความ', 'ccs', 'SU', 'phasit_s', '2024-01-07 14:36:16.243625+00', 'SURT01', 'phasit_s', '2024-01-07 14:37:06.845493+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT01', 'Label', 'EN', 'Label', 'ccs', 'SU', 'phasit_s', '2024-01-07 14:36:18.675549+00', 'SURT01', 'phasit_s', '2024-01-07 14:37:06.845505+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT01', 'Label', 'TH', 'ข้อความที่แสดง', 'ccs', 'SU', 'phasit_s', '2024-01-07 14:36:16.24363+00', 'SURT01', 'phasit_s', '2024-01-07 14:37:06.845494+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT01', 'ModuleCode', 'EN', 'Module Code', 'ccs', 'SU', 'phasit_s', '2024-01-07 14:36:18.675549+00', 'SURT01', 'phasit_s', '2024-01-07 14:37:06.845505+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT01', 'ModuleCode', 'TH', 'รหัสโมดูล', 'ccs', 'SU', 'phasit_s', '2024-01-07 14:36:16.243621+00', 'SURT01', 'phasit_s', '2024-01-07 14:37:06.845494+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT01', 'ProgramCode', 'EN', 'Program Code', 'ccs', 'SU', 'phasit_s', '2024-01-07 14:36:18.67555+00', 'SURT01', 'phasit_s', '2024-01-07 14:37:06.845506+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT01', 'ProgramCode', 'TH', 'รหัสโปรแกม', 'ccs', 'SU', 'phasit_s', '2024-01-07 14:36:16.243606+00', 'SURT01', 'phasit_s', '2024-01-07 14:37:06.845495+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT01', 'ProgramDetail', 'EN', 'Program Detail', 'ccs', 'SU', 'phasit_s', '2024-01-07 14:36:18.67555+00', 'SURT01', 'phasit_s', '2024-01-07 14:37:06.845507+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT01', 'ProgramDetail', 'TH', 'รายละเอียดโปรแกรม', 'ccs', 'SU', 'phasit_s', '2024-01-07 14:36:16.243624+00', 'SURT01', 'phasit_s', '2024-01-07 14:37:06.845496+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT01', 'ProgramName', 'EN', 'Program Name', 'ccs', 'SU', 'phasit_s', '2024-01-07 14:36:18.67555+00', 'SURT01', 'phasit_s', '2024-01-07 14:37:06.845507+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT01', 'ProgramName', 'TH', 'ชื่อโปรแกรม', 'ccs', 'SU', 'phasit_s', '2024-01-07 14:36:16.243618+00', 'SURT01', 'phasit_s', '2024-01-07 14:37:06.845496+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT01', 'ProgramPath', 'EN', 'Program Path', 'ccs', 'SU', 'phasit_s', '2024-01-07 14:36:18.675551+00', 'SURT01', 'phasit_s', '2024-01-07 14:37:06.845508+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT01', 'ProgramPath', 'TH', 'ที่อยู่โปรแกรม', 'ccs', 'SU', 'phasit_s', '2024-01-07 14:36:16.24362+00', 'SURT01', 'phasit_s', '2024-01-07 14:37:06.845497+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT02', 'MenuNameEN', 'EN', 'Menu Name (EN)', 'ccs', 'SU', 'phasit_s', '2024-01-07 17:19:28.740009+00', 'SURT01', 'phasit_s', '2024-01-07 17:20:39.063614+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT02', 'MenuNameEN', 'TH', 'ชื่อเมนู (ภาษาอังกฤษ)', 'ccs', 'SU', 'phasit_s', '2024-01-07 17:19:26.521001+00', 'SURT01', 'phasit_s', '2024-01-07 17:20:39.063614+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT02', 'MenuNameTH', 'EN', 'Menu Name (TH)', 'ccs', 'SU', 'phasit_s', '2024-01-07 17:19:28.74001+00', 'SURT01', 'phasit_s', '2024-01-07 17:20:39.063615+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT02', 'MenuNameTH', 'TH', 'ชื่อเมนู (ภาษาไทย)', 'ccs', 'SU', 'phasit_s', '2024-01-07 17:19:26.520998+00', 'SURT01', 'phasit_s', '2024-01-07 17:20:39.063615+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT02', 'ProgramCode', 'EN', 'Program Code', 'ccs', 'SU', 'phasit_s', '2024-01-07 17:19:28.740013+00', 'SURT01', 'phasit_s', '2024-01-07 17:20:39.063615+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT02', 'ProgramCode', 'TH', 'รหัสโปรแกรม', 'ccs', 'SU', 'phasit_s', '2024-01-07 17:19:26.520996+00', 'SURT01', 'phasit_s', '2024-01-07 17:20:39.063616+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT02', 'ProgramName', 'EN', 'Menu Management', 'ccs', 'SU', 'phasit_s', '2024-01-07 17:19:28.740017+00', 'SURT01', 'phasit_s', '2024-01-07 17:20:39.063616+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT02', 'ProgramName', 'TH', 'กำหนดเมนู', 'ccs', 'SU', 'phasit_s', '2024-01-07 17:19:26.520985+00', 'SURT01', 'phasit_s', '2024-01-07 17:20:39.063617+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Icon', 'EN', 'Icon', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:16.868+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Icon', 'TH', 'ไอคอน', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'admin', '2023-05-21 23:52:37.013+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT03', 'Description', 'EN', 'Description', 'ccs', 'SU', 'phasit_s', '2024-01-08 00:34:01.611993+00', 'SURT01', 'phasit_s', '2024-01-08 00:34:29.873168+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT03', 'Description', 'TH', 'รายละเอียด', 'ccs', 'SU', 'phasit_s', '2024-01-08 00:34:00.19434+00', 'SURT01', 'phasit_s', '2024-01-08 00:34:29.873168+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT03', 'Detail', 'EN', 'Detail', 'ccs', 'SU', 'phasit_s', '2024-01-08 00:34:01.611996+00', 'SURT01', 'phasit_s', '2024-01-08 00:34:29.873168+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT03', 'Detail', 'TH', 'รายละเอียด', 'ccs', 'SU', 'phasit_s', '2024-01-08 00:34:00.194339+00', 'SURT01', 'phasit_s', '2024-01-08 00:34:29.873169+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT03', 'Menu', 'EN', 'Menu', 'ccs', 'SU', 'phasit_s', '2024-01-08 00:34:01.611996+00', 'SURT01', 'phasit_s', '2024-01-08 00:34:29.873169+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT03', 'Menu', 'TH', 'เมนู', 'ccs', 'SU', 'phasit_s', '2024-01-08 00:34:00.194343+00', 'SURT01', 'phasit_s', '2024-01-08 00:34:29.87317+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT03', 'ProfileCode', 'EN', 'Profile Code', 'ccs', 'SU', 'phasit_s', '2024-01-08 00:34:01.611997+00', 'SURT01', 'phasit_s', '2024-01-08 00:34:29.87317+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT03', 'ProfileCode', 'TH', 'รหัสโปรไฟล์', 'ccs', 'SU', 'phasit_s', '2024-01-08 00:34:00.194336+00', 'SURT01', 'phasit_s', '2024-01-08 00:34:29.873171+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT03', 'ProfileMenu', 'EN', 'Profile Menu', 'ccs', 'SU', 'phasit_s', '2024-01-08 00:34:01.611997+00', 'SURT01', 'phasit_s', '2024-01-08 00:34:29.873171+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT02', 'Detail', 'EN', 'Detail', 'ccs', 'SU', 'phasit_s', '2024-01-07 17:19:28.739996+00', 'SURT01', 'phasit_s', '2024-01-07 17:20:39.063611+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT02', 'Detail', 'TH', 'รายละเอียด', 'ccs', 'SU', 'phasit_s', '2024-01-07 17:19:26.520997+00', 'SURT01', 'phasit_s', '2024-01-07 17:20:39.063611+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT02', 'MainMenu', 'EN', 'Main Menu', 'ccs', 'SU', 'phasit_s', '2024-01-07 17:19:28.740002+00', 'SURT01', 'phasit_s', '2024-01-07 17:20:39.063612+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT02', 'MainMenu', 'TH', 'เมนูหลัก', 'ccs', 'SU', 'phasit_s', '2024-01-07 17:19:26.520995+00', 'SURT01', 'phasit_s', '2024-01-07 17:20:39.063612+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT02', 'MenuCode', 'EN', 'Menu Code', 'ccs', 'SU', 'phasit_s', '2024-01-07 17:19:28.740003+00', 'SURT01', 'phasit_s', '2024-01-07 17:20:39.063612+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT02', 'MenuCode', 'TH', 'รหัสเมนู', 'ccs', 'SU', 'phasit_s', '2024-01-07 17:19:26.520992+00', 'SURT01', 'phasit_s', '2024-01-07 17:20:39.063613+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT02', 'MenuName', 'EN', 'Menu Name', 'ccs', 'SU', 'phasit_s', '2024-01-07 17:19:28.740008+00', 'SURT01', 'phasit_s', '2024-01-07 17:20:39.063613+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT02', 'MenuName', 'TH', 'ชื่อเมนู', 'ccs', 'SU', 'phasit_s', '2024-01-07 17:19:26.520994+00', 'SURT01', 'phasit_s', '2024-01-07 17:20:39.063614+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT03', 'ProfileMenu', 'TH', 'โปรไฟล์เมนู', 'ccs', 'SU', 'phasit_s', '2024-01-08 00:34:00.194344+00', 'SURT01', 'phasit_s', '2024-01-08 00:34:29.873171+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT03', 'ProfileName', 'EN', 'Profile Name', 'ccs', 'SU', 'phasit_s', '2024-01-08 00:34:01.611997+00', 'SURT01', 'phasit_s', '2024-01-08 00:34:29.873172+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT03', 'ProfileName', 'TH', 'ชื่อโปรไฟล์', 'ccs', 'SU', 'phasit_s', '2024-01-08 00:34:00.194339+00', 'SURT01', 'phasit_s', '2024-01-08 00:34:29.873172+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT03', 'ProgramName', 'EN', 'Program Name', 'ccs', 'SU', 'phasit_s', '2024-01-08 00:34:01.611998+00', 'SURT01', 'phasit_s', '2024-01-08 00:34:29.873173+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT03', 'ProgramName', 'TH', 'กำหนดสิทธิ์การใช้งานโปรแกรม', 'ccs', 'SU', 'phasit_s', '2024-01-08 00:34:00.194307+00', 'SURT01', 'phasit_s', '2024-01-08 00:34:29.873173+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Accept', 'EN', 'Accept', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'phasit_s', '2024-01-08 01:40:45.146808+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Accept', 'TH', 'ยอมรับ', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'phasit_s', '2024-01-08 01:40:45.146808+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Active', 'EN', 'Active', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:57.384+00', NULL, 'phasit_s', '2024-01-08 01:40:45.146809+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Active', 'TH', 'ใช้งาน', 'ccs', 'ALL', 'admin', '2023-01-31 00:41:33.865+00', NULL, 'phasit_s', '2024-01-08 01:40:45.146809+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Add', 'EN', 'Add', '', 'ALL', 'admin', '2023-01-30 23:57:42.489+00', NULL, 'phasit_s', '2024-01-08 01:40:45.146809+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Add', 'TH', 'เพิ่ม', '', 'ALL', 'admin', '2023-01-30 23:57:24.537+00', NULL, 'phasit_s', '2024-01-08 01:40:45.14681+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'AddNewOrder', 'EN', 'Add New Order', '', 'ALL', 'admin', '2023-01-30 23:57:24.537+00', NULL, 'phasit_s', '2024-01-08 01:40:45.14681+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'AddNewOrder', 'TH', 'เพิ่มรายการ', '', 'ALL', 'admin', '2023-01-30 23:57:24.537+00', NULL, 'phasit_s', '2024-01-08 01:40:45.146815+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Back', 'EN', 'Back', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'phasit_s', '2024-01-08 01:40:45.146815+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Back', 'TH', 'ย้อนกลับ', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'phasit_s', '2024-01-08 01:40:45.146873+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Cancel', 'EN', 'Cancel', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'phasit_s', '2024-01-08 01:40:45.146877+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Cancel', 'TH', 'ยกเลิก', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'phasit_s', '2024-01-08 01:40:45.146877+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Characters', 'EN', 'Characters', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'phasit_s', '2024-01-08 01:40:45.146877+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Characters', 'TH', 'ตัวอักษร', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'phasit_s', '2024-01-08 01:40:45.146877+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Choose', 'EN', 'Choose', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'phasit_s', '2024-01-08 01:40:45.146878+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Choose', 'TH', 'เลือก', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'phasit_s', '2024-01-08 01:40:45.146878+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Clear', 'EN', 'Clear', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'phasit_s', '2024-01-08 01:40:45.146878+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Clear', 'TH', 'ล้างข้อมูล', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'phasit_s', '2024-01-08 01:40:45.146878+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Confirm', 'EN', 'Confirm', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'phasit_s', '2024-01-08 01:40:45.146879+00', 'SURT01');
INSERT INTO su.program_label VALUES ('ALL', 'Confirm', 'TH', 'ยืนยัน', NULL, 'ALL', 'por', '2022-11-11 10:49:37.763+00', 'sql', 'phasit_s', '2024-01-08 01:40:45.146879+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'ProfileCode', 'TH', 'รหัสโปรไฟล์', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:42.490823+00', 'SURT01', 'phasit_s', '2024-01-08 03:55:42.490823+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'ProgramName', 'TH', 'กำหนดผู้ใช้งาน', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:42.490801+00', 'SURT01', 'phasit_s', '2024-01-08 03:55:42.49081+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'UserName', 'TH', 'รหัสผู้ใช้งาน', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:42.490812+00', 'SURT01', 'phasit_s', '2024-01-08 03:55:42.490812+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'Detail', 'EN', 'Detail', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:44.322123+00', 'SURT01', 'phasit_s', '2024-01-08 03:57:07.124494+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'Detail', 'TH', 'รายละเอียด', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:42.490811+00', 'SURT01', 'phasit_s', '2024-01-08 03:57:07.124496+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'Email', 'EN', 'Email', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:44.322127+00', 'SURT01', 'phasit_s', '2024-01-08 03:57:07.124497+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'Email', 'TH', 'อีเมล', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:42.490816+00', 'SURT01', 'phasit_s', '2024-01-08 03:57:07.124498+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'Firstname', 'EN', 'Firstname', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:44.322129+00', 'SURT01', 'phasit_s', '2024-01-08 03:57:07.124498+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'Firstname', 'TH', 'ชื่อ', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:42.490814+00', 'SURT01', 'phasit_s', '2024-01-08 03:57:07.124498+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'FirstnameTh', 'EN', 'Firstname (TH)', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:44.322131+00', 'SURT01', 'phasit_s', '2024-01-08 03:57:07.124499+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'FirstnameTh', 'TH', 'ชื่อ (ภาษาไทย)', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:42.490815+00', 'SURT01', 'phasit_s', '2024-01-08 03:57:07.124499+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'Lastname', 'EN', 'Lastname', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:44.322131+00', 'SURT01', 'phasit_s', '2024-01-08 03:57:07.124499+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'Lastname', 'TH', 'นามสกุล', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:42.490814+00', 'SURT01', 'phasit_s', '2024-01-08 03:57:07.124499+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'LastnameTh', 'EN', 'Lastname (TH)', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:44.322132+00', 'SURT01', 'phasit_s', '2024-01-08 03:57:07.124499+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'LastnameTh', 'TH', 'นามสกุล (ภาษาไทย)', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:42.490815+00', 'SURT01', 'phasit_s', '2024-01-08 03:57:07.1245+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'Password', 'EN', 'Password', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:44.322133+00', 'SURT01', 'phasit_s', '2024-01-08 03:57:07.1245+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'Password', 'TH', 'รหัสผ่าน', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:42.490812+00', 'SURT01', 'phasit_s', '2024-01-08 03:57:07.1245+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'PasswordConfirm', 'EN', 'Confirm Password', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:44.322133+00', 'SURT01', 'phasit_s', '2024-01-08 03:57:07.1245+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'PasswordConfirm', 'TH', 'ยืนยันรหัสผ่าน', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:42.490813+00', 'SURT01', 'phasit_s', '2024-01-08 03:57:07.124501+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'PhoneNumber', 'EN', 'Phone Number', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:44.322136+00', 'SURT01', 'phasit_s', '2024-01-08 03:57:07.124501+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'PhoneNumber', 'TH', 'เบอร์โทรศัพท์', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:42.490817+00', 'SURT01', 'phasit_s', '2024-01-08 03:57:07.124501+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'Profile', 'EN', 'Profile', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:44.322136+00', 'SURT01', 'phasit_s', '2024-01-08 03:57:07.124501+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'Profile', 'TH', 'โปรไฟล์', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:42.490822+00', 'SURT01', 'phasit_s', '2024-01-08 03:57:07.124502+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'ProfileCode', 'EN', 'Profile Code', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:44.322137+00', 'SURT01', 'phasit_s', '2024-01-08 03:57:07.124502+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'ProgramName', 'EN', 'User Management', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:44.322137+00', 'SURT01', 'phasit_s', '2024-01-08 03:57:07.124502+00', 'SURT01');
INSERT INTO su.program_label VALUES ('SURT04', 'UserName', 'EN', 'Username', 'ccs', 'SU', 'phasit_s', '2024-01-08 03:55:44.322137+00', 'SURT01', 'phasit_s', '2024-01-08 03:57:07.124502+00', 'SURT01');


--
-- TOC entry 3521 (class 0 OID 50471)
-- Dependencies: 232
-- Data for Name: report_template; Type: TABLE DATA; Schema: su; Owner: -
--

INSERT INTO su.report_template VALUES ('c8232e36-6fa0-4e7d-aac8-615425b59249', '<link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link
    href="https://fonts.googleapis.com/css2?family=Prompt:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&display=swap"
    rel="stylesheet">
<style>
    @page {
        size: A4 landscape;
        margin: 0;
        padding: 0;
    }

    * {
        font-family: ''Prompt'', sans-serif;
    }

    .report-html-content {
        width: 297mm;
        height: 210mm;
        margin: 0;
        padding: 0;
        color: black;
    }

    .table {
        width: 100%;
    }

    .tagColor {
        margin-top: 5px;
        margin-left: 5px;
        width: 200px;
        height: 30px;
        background-color: #78a4ac;
    }

    .columnImage img {
        text-align: center;
        max-width: 150px;
        height: auto;
        image-rendering: pixelated;
    }

    .columnWidth {
        width: 33.33%;
    }

    .columnLogo {
        text-align: center;
        vertical-align: middle;
    }

    .columnProfile {
        width: 26%;
    }

    .columnContend {
        margin-left: 10px;
        width: 74%;
    }

    .tagContend {
        margin-left: 10px;
    }

    .columnFooter {
        width: 25%;
    }

    .tagFooter {
        margin-top: 118px;
        margin-left: 10px;
        margin-right: 5px;
        width: 200px;
        height: 30px;
        background-color: #78a4ac;
    }

    .title {
        margin-top: 5px;
        margin-bottom: 0px;
    }

    #profile-img img {
        width: 255px;
        image-rendering: pixelated;
        height: auto;
        object-fit: cover;
        margin-left: 10px;
    }

    .text-center {
        text-align: center;
    }

    .sub-center {
        text-align: center;
        font-size: 14px;
    }

    .text-initial {
        font-style: italic;
    }

    .f-16 {
        font-size: 16px;
    }

    .hr {
        height: 1px;
        width: 90%;
        background-color: #444;
        margin: 10px 0;
    }

    .w-90 {
        width: 90%;
    }

    .mt-0 {
        margin-top: 0;
    }

    .mb-0 {
        margin-bottom: 0;
    }

    .last-box {
        display: flex;
        align-items: end;
        justify-content: end;
        margin-right: 5px;
    }
</style>
<div class="report-html-content">
    <table class="table">
        <tr>
            <td class="columnWidth">
                <div class="tagColor"></div>
            </td>
        </tr>
        <tr>
            <td class="columnWidth"></td>
            <td class="columnWidth columnLogo">
                <div class="columnImage">
                    <img src="[r.certificate-logo]">
                </div>
            </td>
            <td class="columnWidth"></td>
        </tr>
    </table>
    <div class="text-center">
        <b>
            <h3 class="title">
                Animal Heath Certificate
            </h3>
            <h3 class="title">
                National Primate Research Center of Thailand - Chulalongkorn University (NPRCT-CU)
            </h3>
        </b>
    </div>
    <br>
    <table class="table">
        <tr>
            <td class="columnProfile">
                <div id="profile-img">
                    <img src="[r.profile-img]">
                </div>
            </td>
            <td class="columnContend">
                <div class="tagContend">
                    <div class="f-16">
                        <b>Species: </b> <span class="text-initial">[r.species]</span> <b>| Animal ID: </b>
                        [r.animal-id]
                        <b>| Microchip ID:
                        </b> [r.microchip-id]
                    </div>
                    <div class="f-16">
                        <b>Sex: </b> [r.sex] <b>| Body Weight: </b> [r.bodyweight] kg <b>| Age: </b> [r.age]
                    </div>
                    <div class="f-16">
                        <b>Genetic Marker: </b> [r.genetic-marker] <b>| Haplotype: </b> [r.haplotype]
                    </div>
                    <div class="hr"></div>
                    <div class="f-16">
                        <b>SPF status of animal</b>
                    </div>
                    <div class="f-16 w-90" style="display: flex;height: 3em;">
                        <span>
                            <b>Free from: </b> [r.free-from]
                        </span>
                    </div>
                    <br>
                    <div class="f-16">
                        <b>Radiographic result: [r.radiographic-result]</b>
                    </div>
                    <div class="f-16">
                        <b>Complete blood count result: [r.complete-blood-count-result]</b>
                    </div>
                    <div class="f-16">
                        <b>Blood chemistry result: [r.blood-chemistry-result]</b>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <div class="sub-center">
        <p>
            I have reviewed this animal health status that has good quality. This animal can be used in the protocal
            approved by the NPRCT-CU IACUC committees.
        </p>
    </div>
    <br><br><br>
    <div>
        <table class="table">
            <tr>
                <td class="columnFooter"></td>
                <td class="columnFooter"></td>
                <td class="columnFooter" rowspan="2">
                    <div class="text-center">
                        <p class="mb-0 mt-0">
                            ......................................................................................</p>
                        <p class="mb-0 mt-0">(Taratorn Kemthong, DVM)</p>
                        <p class="mb-0 mt-0">Attending Veterinarian</p>
                        <p class="mb-0 mt-0" style="white-space: nowrap;">National Primate Research Center of Thailand
                        </p>
                        <p class="mb-0 mt-0">Chulalongkorn University</p>
                        <p class="mb-0 mt-0">........../........../..........</p>
                    </div>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td>
                    <div class="tagFooter"></div>
                </td>
            </tr>
        </table>
    </div>
</div>', NULL, NULL, NULL, NULL, NULL, NULL, 'monkey_certificate');


--
-- TOC entry 3522 (class 0 OID 50477)
-- Dependencies: 233
-- Data for Name: role; Type: TABLE DATA; Schema: su; Owner: -
--



--
-- TOC entry 3523 (class 0 OID 50483)
-- Dependencies: 234
-- Data for Name: role_user; Type: TABLE DATA; Schema: su; Owner: -
--



--
-- TOC entry 3524 (class 0 OID 50487)
-- Dependencies: 235
-- Data for Name: user; Type: TABLE DATA; Schema: su; Owner: -
--

INSERT INTO su."user" OVERRIDING SYSTEM VALUE VALUES (11, 'phasit_s', '001', 'AQAAAAIAAYagAAAAEDZKSMQFV8fUq4bfwGPIloNkKW7f08jkCPEvoAv76tslMjldpTebWsUb0ILSYRRV2w==', 'TH', true, false, '2023-03-23 13:31:42.644', NULL, '2023-12-14 00:00:00', 0, 'c8e14b90-958d-4be3-8ebb-c3f7ee439cbe', 'JQKYZZ3TIC42CRX74DOPZNGMX2RJIF3K', 'phasit_s@softsquaregroup.com', '0921922791', 'Phasit', 'Sritonchai', 'PHASIT_S', '2024-01-07 20:46:53.189494+00', true, NULL, NULL, NULL, NULL, NULL, NULL, 'ภาษิต', 'ศรีตนชัย', true, 'outlined', 'static', 'dark', '#e80567', 14);
INSERT INTO su."user" OVERRIDING SYSTEM VALUE VALUES (1, 'admin', '001', 'AQAAAAIAAYagAAAAEOry+JKXhdzfkTrruJR48bLePxjeZ2n1Q0eoiINbvDRgKwEqzvMp1oIphlB4VGVZtA==', 'EN', false, false, '2022-04-30 00:00:00', NULL, '2023-11-24 00:00:00', 0, '8d71341a-1506-4574-98c7-b630b9111982', 'SBZZVX634EL4SQLT2T5X4VQBBZF3B4HN', 'admin1234@hotmail.com', '0937777848', 'Administrator', 'Softsquaregroup', 'ADMIN', '2023-12-27 09:15:09.241788+00', true, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, false, 'outlined', 'static', 'dark', '#3B82F6', 14);


--
-- TOC entry 3525 (class 0 OID 50494)
-- Dependencies: 236
-- Data for Name: user_profile; Type: TABLE DATA; Schema: su; Owner: -
--

INSERT INTO su.user_profile VALUES (1, 'admin', 'admin', '2023-11-24 02:29:31.488', 'SURT04', 'admin', '2023-11-24 02:29:31.488', 'SURT04');
INSERT INTO su.user_profile VALUES (11, 'admin', 'admin', '2024-01-08 03:51:58.358', 'SURT04', 'admin', '2024-01-08 03:51:58.359', 'SURT04');


--
-- TOC entry 3538 (class 0 OID 0)
-- Dependencies: 217
-- Name: activity_log_id_seq; Type: SEQUENCE SET; Schema: su; Owner: -
--

SELECT pg_catalog.setval('su.activity_log_id_seq', 2285, true);


--
-- TOC entry 3539 (class 0 OID 0)
-- Dependencies: 220
-- Name: content_id_seq; Type: SEQUENCE SET; Schema: su; Owner: -
--

SELECT pg_catalog.setval('su.content_id_seq', 1091, true);


--
-- TOC entry 3540 (class 0 OID 0)
-- Dependencies: 237
-- Name: user_user_id_seq; Type: SEQUENCE SET; Schema: su; Owner: -
--

SELECT pg_catalog.setval('su.user_user_id_seq', 49, true);


--
-- TOC entry 3342 (class 2606 OID 107218)
-- Name: list_item_group db_list_item_group_pk; Type: CONSTRAINT; Schema: db; Owner: -
--

ALTER TABLE ONLY db.list_item_group
    ADD CONSTRAINT db_list_item_group_pk PRIMARY KEY (list_item_group_code);


--
-- TOC entry 3344 (class 2606 OID 107228)
-- Name: list_item db_list_item_mua_un; Type: CONSTRAINT; Schema: db; Owner: -
--

ALTER TABLE ONLY db.list_item
    ADD CONSTRAINT db_list_item_mua_un UNIQUE (list_item_code_mua);


--
-- TOC entry 3346 (class 2606 OID 107226)
-- Name: list_item db_list_item_pk; Type: CONSTRAINT; Schema: db; Owner: -
--

ALTER TABLE ONLY db.list_item
    ADD CONSTRAINT db_list_item_pk PRIMARY KEY (list_item_group_code, list_item_code);


--
-- TOC entry 3348 (class 2606 OID 107230)
-- Name: list_item db_list_item_sequence_un; Type: CONSTRAINT; Schema: db; Owner: -
--

ALTER TABLE ONLY db.list_item
    ADD CONSTRAINT db_list_item_sequence_un UNIQUE (list_item_group_code, sequence);


--
-- TOC entry 3338 (class 2606 OID 107201)
-- Name: language pk_language; Type: CONSTRAINT; Schema: db; Owner: -
--

ALTER TABLE ONLY db.language
    ADD CONSTRAINT pk_language PRIMARY KEY (language_code);


--
-- TOC entry 3340 (class 2606 OID 107208)
-- Name: language_lang pk_language_lang; Type: CONSTRAINT; Schema: db; Owner: -
--

ALTER TABLE ONLY db.language_lang
    ADD CONSTRAINT pk_language_lang PRIMARY KEY (language_code, language_code_forname);


--
-- TOC entry 3306 (class 2606 OID 50611)
-- Name: email_template email_template_pkey; Type: CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.email_template
    ADD CONSTRAINT email_template_pkey PRIMARY KEY (email_template_code);


--
-- TOC entry 3302 (class 2606 OID 50613)
-- Name: activity_type pk_activity_type; Type: CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.activity_type
    ADD CONSTRAINT pk_activity_type PRIMARY KEY (activity_type_code);


--
-- TOC entry 3300 (class 2606 OID 50615)
-- Name: activity_log pk_activitylog; Type: CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.activity_log
    ADD CONSTRAINT pk_activitylog PRIMARY KEY (id);


--
-- TOC entry 3304 (class 2606 OID 50617)
-- Name: content pk_content; Type: CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.content
    ADD CONSTRAINT pk_content PRIMARY KEY (id);


--
-- TOC entry 3308 (class 2606 OID 50619)
-- Name: menu pk_menu; Type: CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.menu
    ADD CONSTRAINT pk_menu PRIMARY KEY (menu_code);


--
-- TOC entry 3310 (class 2606 OID 50621)
-- Name: menu_label pk_menu_lang; Type: CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.menu_label
    ADD CONSTRAINT pk_menu_lang PRIMARY KEY (menu_code, language_code);


--
-- TOC entry 3312 (class 2606 OID 50623)
-- Name: message pk_message; Type: CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.message
    ADD CONSTRAINT pk_message PRIMARY KEY (message_code, language_code);


--
-- TOC entry 3314 (class 2606 OID 50625)
-- Name: parameter pk_parameter; Type: CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.parameter
    ADD CONSTRAINT pk_parameter PRIMARY KEY (parameter_group_code, parameter_code);


--
-- TOC entry 3316 (class 2606 OID 50627)
-- Name: password_policy pk_password_policy; Type: CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.password_policy
    ADD CONSTRAINT pk_password_policy PRIMARY KEY (password_policy_code);


--
-- TOC entry 3318 (class 2606 OID 50629)
-- Name: profile pk_profile; Type: CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.profile
    ADD CONSTRAINT pk_profile PRIMARY KEY (profile_code);


--
-- TOC entry 3320 (class 2606 OID 50631)
-- Name: profile_lang pk_profile_lang; Type: CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.profile_lang
    ADD CONSTRAINT pk_profile_lang PRIMARY KEY (profile_code, language_code);


--
-- TOC entry 3322 (class 2606 OID 50633)
-- Name: profile_menu pk_profile_menu; Type: CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.profile_menu
    ADD CONSTRAINT pk_profile_menu PRIMARY KEY (profile_code, menu_code);


--
-- TOC entry 3324 (class 2606 OID 50635)
-- Name: program pk_program; Type: CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.program
    ADD CONSTRAINT pk_program PRIMARY KEY (program_code);


--
-- TOC entry 3330 (class 2606 OID 50637)
-- Name: role pk_role; Type: CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.role
    ADD CONSTRAINT pk_role PRIMARY KEY (id);


--
-- TOC entry 3332 (class 2606 OID 50639)
-- Name: role_user pk_role_user; Type: CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.role_user
    ADD CONSTRAINT pk_role_user PRIMARY KEY (user_id, role_id);


--
-- TOC entry 3334 (class 2606 OID 50641)
-- Name: user pk_user; Type: CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su."user"
    ADD CONSTRAINT pk_user PRIMARY KEY (user_id);


--
-- TOC entry 3336 (class 2606 OID 50643)
-- Name: user_profile pk_userpro_file; Type: CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.user_profile
    ADD CONSTRAINT pk_userpro_file PRIMARY KEY (user_id, profile_code);


--
-- TOC entry 3326 (class 2606 OID 50645)
-- Name: program_label program_label_pk; Type: CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.program_label
    ADD CONSTRAINT program_label_pk PRIMARY KEY (program_code, field_name, language_code);


--
-- TOC entry 3328 (class 2606 OID 50647)
-- Name: report_template report_template_pk; Type: CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.report_template
    ADD CONSTRAINT report_template_pk PRIMARY KEY (id);


--
-- TOC entry 3362 (class 2606 OID 107231)
-- Name: list_item db_list_item_fk; Type: FK CONSTRAINT; Schema: db; Owner: -
--

ALTER TABLE ONLY db.list_item
    ADD CONSTRAINT db_list_item_fk FOREIGN KEY (list_item_group_code) REFERENCES db.list_item_group(list_item_group_code);


--
-- TOC entry 3361 (class 2606 OID 107209)
-- Name: language_lang fk_language_lang_language; Type: FK CONSTRAINT; Schema: db; Owner: -
--

ALTER TABLE ONLY db.language_lang
    ADD CONSTRAINT fk_language_lang_language FOREIGN KEY (language_code_forname) REFERENCES db.language(language_code) ON DELETE CASCADE;


--
-- TOC entry 3349 (class 2606 OID 50843)
-- Name: activity_log fk_activity_log_activity_type; Type: FK CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.activity_log
    ADD CONSTRAINT fk_activity_log_activity_type FOREIGN KEY (activity_type_code) REFERENCES su.activity_type(activity_type_code);


--
-- TOC entry 3352 (class 2606 OID 50853)
-- Name: menu_label fk_menu_lang_menu; Type: FK CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.menu_label
    ADD CONSTRAINT fk_menu_lang_menu FOREIGN KEY (menu_code) REFERENCES su.menu(menu_code) ON DELETE CASCADE;


--
-- TOC entry 3350 (class 2606 OID 50858)
-- Name: menu fk_menu_menu_main_menu; Type: FK CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.menu
    ADD CONSTRAINT fk_menu_menu_main_menu FOREIGN KEY (main_menu) REFERENCES su.menu(menu_code);


--
-- TOC entry 3351 (class 2606 OID 50863)
-- Name: menu fk_menu_program; Type: FK CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.menu
    ADD CONSTRAINT fk_menu_program FOREIGN KEY (program_code) REFERENCES su.program(program_code);


--
-- TOC entry 3353 (class 2606 OID 50873)
-- Name: profile_lang fk_profile_lang_profile; Type: FK CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.profile_lang
    ADD CONSTRAINT fk_profile_lang_profile FOREIGN KEY (profile_code) REFERENCES su.profile(profile_code) ON DELETE CASCADE;


--
-- TOC entry 3354 (class 2606 OID 50878)
-- Name: profile_menu fk_profile_menu_menu; Type: FK CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.profile_menu
    ADD CONSTRAINT fk_profile_menu_menu FOREIGN KEY (menu_code) REFERENCES su.menu(menu_code);


--
-- TOC entry 3355 (class 2606 OID 50883)
-- Name: profile_menu fk_profile_menu_profile; Type: FK CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.profile_menu
    ADD CONSTRAINT fk_profile_menu_profile FOREIGN KEY (profile_code) REFERENCES su.profile(profile_code);


--
-- TOC entry 3359 (class 2606 OID 50888)
-- Name: user_profile fk_user_profile_profile; Type: FK CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.user_profile
    ADD CONSTRAINT fk_user_profile_profile FOREIGN KEY (profile_code) REFERENCES su.profile(profile_code);


--
-- TOC entry 3360 (class 2606 OID 50893)
-- Name: user_profile fk_user_profile_user; Type: FK CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.user_profile
    ADD CONSTRAINT fk_user_profile_user FOREIGN KEY (user_id) REFERENCES su."user"(user_id);


--
-- TOC entry 3357 (class 2606 OID 50898)
-- Name: role_user pk_role_user_role; Type: FK CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.role_user
    ADD CONSTRAINT pk_role_user_role FOREIGN KEY (role_id) REFERENCES su.role(id);


--
-- TOC entry 3358 (class 2606 OID 50903)
-- Name: role_user pk_role_user_user; Type: FK CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.role_user
    ADD CONSTRAINT pk_role_user_user FOREIGN KEY (user_id) REFERENCES su."user"(user_id);


--
-- TOC entry 3356 (class 2606 OID 50908)
-- Name: program_label program_label_fk; Type: FK CONSTRAINT; Schema: su; Owner: -
--

ALTER TABLE ONLY su.program_label
    ADD CONSTRAINT program_label_fk FOREIGN KEY (program_code) REFERENCES su.program(program_code);


-- Completed on 2024-01-08 04:25:23

--
-- PostgreSQL database dump complete
--

