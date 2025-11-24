# RBCProjectMVC

*RBCProjectMVC* is a web-based Employee Management System built with **ASP.NET Core MVC**. It allows managing employee records, searching, sorting, exporting data to PDF or Excel, and visualizing department statistics using charts. Employee photos and additional information can also be handled easily.

---

## Features

- **Employee Management**: View, add, edit, and delete employees.
- **Live Search & Sorting**: Search employees and sort columns dynamically.
- **PDF Export**: Export employee list to PDF with current filters applied.
- **Excel Export**: Download employee data as Excel.
- **Chart Visualization**: Department-wise employee counts displayed as histograms.
- **Responsive UI**: Built with Bootstrap 5 and jQuery for interactive frontend.

---

## Technologies

- **.NET 8**: Backend framework.
- **Entity Framework Core**: ORM for database operations.
- **SQL Server**: Database.
- **Select.HtmlToPdf.NetCore**: PDF generation.
- **Chart.js**: Charts and histograms.
- **Bootstrap 5 & jQuery**: Frontend UI.

---

## Prerequisites

- .NET 8 SDK
- SQL Server
- NuGet packages:
  - `Select.HtmlToPdf.NetCore`
  - `Microsoft.EntityFrameworkCore.SqlServer`
  - `Microsoft.Data.SqlClient`
  - `jQuery` and `Bootstrap 5`

---

## Setup and Run

1. Clone the repository and navigate to the folder:

```bash
git clone <[repo-url](https://github.com/cosqun5/RBC_Project.git)>
cd RBCProjectMVC

