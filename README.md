# Travel Agency Wep Application

## Overview

The system will provide the users the
opportunity to plan their trips. The system enables users to reserve a tour by themselves
or through an agent of the company, later they can edit and update the reservations. The
reservations can be made for the hotel rooms or the tours. The tour will include several
sightseeing places that are usually visited by tourists. Along with that, they will also have
the ability to participate in activities like festivals and concerts. After the tour is over the
user will be able to review the tour or the guide and rate them. The guide will also have
the right to add feedback about the tour. To make things easier and beneficial for the
agency we added multiple features to make the system more convenient and easier to
use. For example, a discount can be applied to any reservation made by a customer,
which can help the agency promote its tours. In summary, we provide in our system what
most travel agencies would need, from reservations to activities and sightseeing places,
to discounts for the reservations.

## Build Instructions
- Clone the project using Visual Studio 2019. Older versions may not be able to work well with .NET Core 3.1.
- After cloning, double click to the TravelAgency.sln in Solution Explorer, so that project loads nicely.
- Create the website's database using the backup located in here.
  - The .bak file will backup the design. The SQL file will provide you some data to seed.
  - If you are going to backup database in your local computer, you can do it on SQL Server Management Stduio. The database was created in SQL Server version 13, so if you have an older version of SQL Server, the .bak file won't work. For this issue, CREATE DATABASE scripts are also provided for another SQL database clients.
- Adjust your connection string, located at TravelAgencyEntity/TravelAgencyContext.cs - at OnConfiguring function (line 51).
  - If you are using SQL Server, you only need to change the Data Source parameter to your connection, a.k.a. Server Name.
  - If you are using clients other than SQL Server, you will also need to change the function that builds the connection. Instead of UseSqlServer, you will need to call UseMySql, UsePostgreSql etc.
- Finally, you will need to choose your startup project. Right click your solution, select Properties, and there set up the following projects as Multiple Startup Projects:
  - TravelAgencyAPI (at top)
  - TravelAgencyApp (right beneath the API)
- You are all set 🎉

## Documentation

For the documentation, you can check out the project [website](https://cs353-travel-agency-system.github.io/).


