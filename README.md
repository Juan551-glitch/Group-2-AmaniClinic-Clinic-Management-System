# Amani Clinic Management System

The application manages patients, doctors, appointments, and consultation records using the existing Azure SQL database `AmaniClinicDB`.

## Azure App Service configuration

In the Azure portal, open the App Service, go to **Settings > Environment variables** and add a connection string named `AmaniClinicDB`. Use the Azure SQL connection string for your database, for example:

```text
Server=tcp:<server-name>.database.windows.net,1433;Initial Catalog=AmaniClinicDB;User ID=<user>;Password=<password>;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```

Do not commit real credentials to `appsettings.json`. Azure exposes the configured connection string to the application as `ConnectionStrings__AmaniClinicDB`.

Before using the public booking page, add doctors through **Clinic portal > Doctors**. Booking a visit stores the patient (or updates an existing patient with the same email) and creates a scheduled appointment. The clinic portal also lets staff add patients, update appointment status, and record consultations.
