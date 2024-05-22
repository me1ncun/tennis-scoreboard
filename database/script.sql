﻿CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE matches (
    id integer GENERATED BY DEFAULT AS IDENTITY,
    player1 integer NOT NULL,
    player2 integer NOT NULL,
    winner integer NOT NULL,
    CONSTRAINT "PK_matches" PRIMARY KEY (id)
);

CREATE TABLE players (
    id integer GENERATED BY DEFAULT AS IDENTITY,
    name text NOT NULL,
    CONSTRAINT "PK_players" PRIMARY KEY (id)
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240521104422_Initial', '9.0.0-preview.3.24172.4');

COMMIT;

