CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;
CREATE TABLE "organizations" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_organizations" PRIMARY KEY,
    "Name" TEXT NOT NULL,
    "Regon" TEXT NOT NULL,
    "Nip" TEXT NOT NULL,
    "Address_City" TEXT NULL,
    "Address_Street" TEXT NULL
);

CREATE TABLE "contacts" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_contacts" PRIMARY KEY AUTOINCREMENT,
    "FirstName" TEXT NOT NULL,
    "LastName" TEXT NOT NULL,
    "Email" TEXT NOT NULL,
    "PhoneNumber" TEXT NOT NULL,
    "BirthDate" TEXT NOT NULL,
    "Created" TEXT NOT NULL,
    "Category" INTEGER NOT NULL,
    "OrganizationId" INTEGER NOT NULL,
    CONSTRAINT "FK_contacts_organizations_OrganizationId" FOREIGN KEY ("OrganizationId") REFERENCES "organizations" ("Id") ON DELETE CASCADE
);

INSERT INTO "organizations" ("Id", "Address_City", "Address_Street", "Name", "Nip", "Regon")
VALUES (1, 'Kraków', 'Św. Filipa 17', 'WSEI', '123456', '321321321');
SELECT changes();

INSERT INTO "organizations" ("Id", "Address_City", "Address_Street", "Name", "Nip", "Regon")
VALUES (2, 'Warszawa', 'Wesoła 15', 'Famo', '432432', '123123123');
SELECT changes();


INSERT INTO "contacts" ("Id", "BirthDate", "Category", "Created", "Email", "FirstName", "LastName", "OrganizationId", "PhoneNumber")
VALUES (1, '1980-01-01 00:00:00', 0, '2024-11-12 13:38:12.3766567', 'ewa@wsei.edu.pl', 'Adam', 'Nowak', 1, '123123123');
SELECT changes();

INSERT INTO "contacts" ("Id", "BirthDate", "Category", "Created", "Email", "FirstName", "LastName", "OrganizationId", "PhoneNumber")
VALUES (2, '2001-01-01 00:00:00', 0, '2024-11-12 13:38:12.3766655', 'ola@wsei.edu.pl', 'Ola', 'Nowak', 2, '123123123');
SELECT changes();


CREATE INDEX "IX_contacts_OrganizationId" ON "contacts" ("OrganizationId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20241112123813_Organizations', '9.0.0');

COMMIT;

