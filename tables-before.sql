\set ON_ERROR_STOP
\set ECHO all
BEGIN;


CREATE TABLE "public"."areas"( 
	"description" varchar,
	"restaurantid" int NOT NULL,
	"id" int NOT NULL);

CREATE TABLE "public"."aspnetroleclaims"( 
	"id" int NOT NULL,
	"roleid" varchar(450) NOT NULL,
	"claimtype" varchar,
	"claimvalue" varchar);

CREATE TABLE "public"."aspnetroles"( 
	"id" varchar(450) NOT NULL,
	"name" varchar(256),
	"normalizedname" varchar(256),
	"concurrencystamp" varchar);

CREATE TABLE "public"."aspnetuserclaims"( 
	"id" int NOT NULL,
	"userid" varchar(450) NOT NULL,
	"claimtype" varchar,
	"claimvalue" varchar);

CREATE TABLE "public"."aspnetuserlogins"( 
	"loginprovider" varchar(128) NOT NULL,
	"providerkey" varchar(128) NOT NULL,
	"providerdisplayname" varchar,
	"userid" varchar(450) NOT NULL);

CREATE TABLE "public"."aspnetuserroles"( 
	"userid" varchar(450) NOT NULL,
	"roleid" varchar(450) NOT NULL);

CREATE TABLE "public"."aspnetusertokens"( 
	"userid" varchar(450) NOT NULL,
	"loginprovider" varchar(128) NOT NULL,
	"name" varchar(128) NOT NULL,
	"value" varchar);

CREATE TABLE "public"."aspnetusers"( 
	"id" varchar(450) NOT NULL,
	"username" varchar(256),
	"normalizedusername" varchar(256),
	"email" varchar(256),
	"normalizedemail" varchar(256),
	"emailconfirmed" boolean NOT NULL,
	"passwordhash" varchar,
	"securitystamp" varchar,
	"concurrencystamp" varchar,
	"phonenumber" varchar,
	"phonenumberconfirmed" boolean NOT NULL,
	"twofactorenabled" boolean NOT NULL,
	"lockoutend" timestamp with time zone,
	"lockoutenabled" boolean NOT NULL,
	"accessfailedcount" int NOT NULL);

CREATE TABLE "public"."people"( 
	"id" varchar(450) NOT NULL,
	"firstname" varchar,
	"lastname" varchar,
	"email" varchar,
	"phone" varchar,
	"discriminator" varchar NOT NULL,
	"customerid" int,
	"managerid" int,
	"restaurantid" int,
	"memberid" int,
	"staffid" int,
	"staff_managerid" varchar(450));

CREATE TABLE "public"."reservationstatuses"( 
	"desc" varchar,
	"id" int NOT NULL);

CREATE TABLE "public"."reservationtypes"( 
	"description" varchar,
	"id" int NOT NULL);

CREATE TABLE "public"."reservations"( 
	"startdatetime" timestamp(7) NOT NULL,
	"enddatetime" timestamp(7) NOT NULL,
	"guests" int NOT NULL,
	"notes_requirements" varchar,
	"reservationtypeid" int NOT NULL,
	"customerid" varchar(450),
	"reservationstatusid" int NOT NULL,
	"userid" int,
	"id" int NOT NULL);

CREATE TABLE "public"."restaurants"( 
	"name" varchar,
	"address" varchar,
	"phone" varchar,
	"email" varchar,
	"capacity" int NOT NULL,
	"id" int NOT NULL);

CREATE TABLE "public"."sittingstatuses"( 
	"description" varchar,
	"id" int NOT NULL);

CREATE TABLE "public"."sittingtypes"( 
	"description" varchar,
	"id" int NOT NULL);

CREATE TABLE "public"."sittings"( 
	"sittingtypeid" int NOT NULL,
	"startdatetime" timestamp(7) NOT NULL,
	"enddatetime" timestamp(7) NOT NULL,
	"capacity" int NOT NULL,
	"sittingstatusid" int NOT NULL,
	"id" int NOT NULL);

CREATE TABLE "public"."tablereservations"( 
	"reservationid" int NOT NULL,
	"tablesittingid" int,
	"id" int NOT NULL);

CREATE TABLE "public"."tablesittings"( 
	"sittingid" int NOT NULL,
	"tableid" int NOT NULL,
	"id" int NOT NULL);

CREATE TABLE "public"."tables"( 
	"tableno" varchar,
	"tablecapacity" int NOT NULL,
	"areaid" int NOT NULL,
	"id" int NOT NULL);

CREATE TABLE "public"."user"( 
	"managerid" varchar(450),
	"staffid" varchar(450),
	"memberid" varchar(450),
	"id" int NOT NULL,
	"description" varchar);

CREATE TABLE "public"."__efmigrationshistory"( 
	"migrationid" varchar(150) NOT NULL,
	"productversion" varchar(32) NOT NULL);

COMMIT;
