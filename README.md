# SQL Portfolio

Database projects completed during my studies.

**Tools:** MySQL, SQL, MS Access, C# / ASP.NET MVC

---

## 1. Tutoring Management System (MySQL + ASP.NET MVC)
📂 [`tutoring-management-system`](tutoring-management-system)

A web application for managing a tutoring business, built on a relational MySQL database (university databases course).
The project started from a course template, which is why the code namespace is "Autonuoma".

**Database entities:** tutors, students, student guardians, lessons, schedules, payments, learning materials, employees.

**SQL work:**
- Full CRUD for every entity using parameterised `INSERT`, `UPDATE`, `DELETE` and `SELECT` queries ([Repositories](tutoring-management-system/Repositories))
- Master-detail form: a lesson together with the learning materials used in it (many-to-many relationship)
- **Lesson report** ([AtaskaitaRepo.cs](tutoring-management-system/Repositories/AtaskaitaRepo.cs)):
  - `INNER JOIN` / `LEFT JOIN` across 4 tables
  - aggregation with `COUNT`, `SUM`, `AVG`, `MAX` and `GROUP BY`
  - optional user filters (lesson duration range, minimum rating, minimum tutor experience) handled with `IFNULL` in the `WHERE` clause
  - string functions (`CONCAT`, `UPPER`, `LOWER`) and rounding

> Database connection settings in `appsettings.json` have been replaced with placeholders.
## 2. MS Access Query Exercises
📂 [`ms-access-queries`](ms-access-queries)

Lab exercises on the Garden Company and Northwind sample databases. The SQL of every query is in [`lab_queries.sql`](ms-access-queries/lab_queries.sql).

- Multi-table `INNER JOIN`s across up to 5 tables (Customers, Orders, Order Details, Products, Categories, Suppliers, Employees)
- Aggregation with `GROUP BY`, `COUNT` and `SUM`, and filtering groups with `HAVING` (suppliers with revenue over 20,000)
- Calculated fields: revenue after discount = unit price × quantity × (1 − discount), average quantity per order line
- Layered queries: a helper query used as the source of a summary query
- Crosstab queries (`TRANSFORM … PIVOT`): sales by category and customer country, quantity by category and employee country
