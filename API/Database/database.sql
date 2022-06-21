CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE users (
    "Id" uuid NOT NULL,
    "UserName" text NOT NULL,
    "FullName" text NOT NULL,
    "Email" text NOT NULL,
    "Password" text NOT NULL,
    CONSTRAINT "PK_users" PRIMARY KEY ("Id")
);

CREATE TABLE notes (
    "Id" uuid NOT NULL,
    "Title" character varying(200) NOT NULL,
    "Content" text NULL,
    "Priority" integer NOT NULL,
    "UserId" uuid NOT NULL,
    CONSTRAINT "PK_notes" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_notes_users_UserId" FOREIGN KEY ("UserId") REFERENCES users ("Id") ON DELETE CASCADE
);

INSERT INTO users ("Id", "Email", "FullName", "Password", "UserName")
VALUES ('839825b0-017b-4d68-9d66-d270f9c5167d', 'john_doe@gmail.com', 'John Doe', 'AQAAAAEAACcQAAAAEBHySPor5SHQGHomzXOtc2/qHdkS8NsOyUCgXv2vpcHvhE9vqxapNN58amAkOtNaBg==', 'john123');
INSERT INTO users ("Id", "Email", "FullName", "Password", "UserName")
VALUES ('d5b5d210-bb70-4368-a7ab-a2c85d42dd9b', 'johancruyff_47@gmail.com', 'Johan Cruyff', 'AQAAAAEAACcQAAAAEBHySPor5SHQGHomzXOtc2/qHdkS8NsOyUCgXv2vpcHvhE9vqxapNN58amAkOtNaBg==', 'johanCF');

CREATE INDEX "IX_notes_Title" ON notes ("Title");

CREATE INDEX "IX_notes_UserId" ON notes ("UserId");

CREATE UNIQUE INDEX "IX_users_Email" ON users ("Email");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220621031031_CreateInitialTables', '6.0.6');

COMMIT;