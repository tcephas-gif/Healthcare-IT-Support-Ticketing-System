# Healthcare IT Ticketing System

A full-stack **ASP.NET Core MVC** application that simulates an enterprise Healthcare IT Support platform.

The application enables users to create, manage, search, and analyze healthcare IT support tickets while providing operational dashboards and business intelligence reporting through interactive visualizations.

---

# Getting Started

## Prerequisites

* Visual Studio 2022 or Visual Studio 2026
* .NET 9 SDK
* SQL Server Express or SQL Server LocalDB (installed with Visual Studio)

## Installation

1. Clone or download this repository.

2. **If downloading as a ZIP, extract the project before opening it in Visual Studio.**

3. Open the solution file:

   ```
   HealthcareTicketingSystem.sln
   ```

   > **Important:** Open the **solution (.sln)** file rather than opening the repository folder directly.

4. Restore NuGet packages if prompted.

5. Verify or update the SQL Server connection string in `appsettings.json` if your LocalDB instance differs from the default configuration.

6. Open the **Package Manager Console** and run:

   ```powershell
   Update-Database
   ```

   This creates the database and automatically seeds the application with sample healthcare support tickets.

7. Press **F5** or click **Start** to launch the application.

The application will open in your default web browser.

---

# Features

* Create, edit, delete, and manage healthcare IT support tickets
* Operational dashboard with key performance indicators (KPIs)
* Search and filter tickets by status, priority, and category
* Priority and status badges for quick ticket identification
* Ticket Details page
* Reports dashboard with operational insights
* Interactive Chart.js visualizations
* Responsive Bootstrap 5 interface
* Entity Framework Core database integration
* Automatically seeded sample data for demonstration

---

# Built With

* ASP.NET Core MVC
* C#
* Entity Framework Core
* SQL Server LocalDB
* Bootstrap 5
* Chart.js
* LINQ

---

# Screenshots

## Home Page

<img width="3194" height="1664" alt="Home Page" src="https://github.com/user-attachments/assets/c6095cdd-286e-42fd-8bfd-a4924c782b64" />

---

## Ticket Dashboard

<img width="3174" height="1656" alt="Healthcare Support Tickets Page" src="https://github.com/user-attachments/assets/3481c9d5-6662-4897-80b7-f3a75f2b2612" />

---

## Reports Dashboard

<img width="3194" height="1664" alt="Healthcare Support Reports Page" src="https://github.com/user-attachments/assets/90fb36b8-a6ec-4260-aa1e-e888ce0ff2d3" />

---

## Ticket Details

<img width="3198" height="1662" alt="Details Page" src="https://github.com/user-attachments/assets/81e61e30-e9bd-4786-9203-60d28fd6da76" />

---

## Edit Ticket

<img width="3192" height="1664" alt="Edit Ticket Page" src="https://github.com/user-attachments/assets/b8fdcd49-fb08-4773-9b93-b60008fbeed2" />

---

## Create Ticket

<img width="3196" height="1656" alt="Create Ticket Page" src="https://github.com/user-attachments/assets/7778f0ac-cf5a-4dac-b3b5-244a3fb0362c" />

---

# Future Improvements

* User authentication with ASP.NET Core Identity
* Role-based access control
* Email notifications
* Export reports to PDF and Excel
* Audit logging
* REST API
* Real-time dashboard updates
* Ticket comments and attachments

---

# Author

**Tanisha Cephas**

Information Systems & Technology Student
Old Dominion University
