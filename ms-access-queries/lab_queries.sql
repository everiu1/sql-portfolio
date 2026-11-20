-- =====================================================================
-- MS Access query exercises
-- Databases: GardenCompanyEvelina.accdb, NorthwindsEvelina.accdb
-- SQL dialect: Microsoft Access (Jet/ACE SQL)
-- =====================================================================


-- =====================================================================
-- GARDEN COMPANY DATABASE
-- =====================================================================

-- Lab task A
-- Number of order lines (sales) per product category and customer country.
-- Joins 5 tables, groups by category and country, sorted by category.
SELECT Categories.CategoryName, Customers.Country, Count(Customers.City) AS [CountOfCity]
FROM (((Customers INNER JOIN Orders ON Customers.[CustomerID] = Orders.[CustomerID]) INNER JOIN [Order Details] ON Orders.[OrderID] = [Order Details].[OrderID]) INNER JOIN Products ON Products.[ProductID] = [Order Details].[ProductID]) INNER JOIN Categories ON Categories.[CategoryID] = Products.[CategoryID]
GROUP BY Categories.CategoryName, Customers.Country
ORDER BY Categories.CategoryName;


-- Lab task B (helper query)
-- For every category and customer: total quantity, number of order lines
-- and revenue after discount (UnitPrice * Quantity * (1 - Discount)).
SELECT Products.CategoryID, Customers.CustomerID, Sum([Order Details].Quantity) AS [SumOfQuantity], Sum([Order Details].[UnitPrice]*[Order Details].[Quantity]*(1-[Order Details].[Discount])) AS [Revenue], Count([Order Details].Quantity) AS [CountOfQuantity]
FROM ((Customers INNER JOIN Orders ON Customers.[CustomerID] = Orders.[CustomerID]) INNER JOIN [Order Details] ON Orders.[OrderID] = [Order Details].[OrderID]) INNER JOIN Products ON Products.[ProductID] = [Order Details].[ProductID]
GROUP BY Products.CategoryID, Customers.CustomerID;


-- Lab task B (main query, built on the helper query)
-- For every category: number of customers, total revenue
-- and average quantity per order line.
SELECT [Lab B helping].CategoryID, Count([Lab B helping].CustomerID) AS [CountOfCustomerID], Sum([Lab B helping].Revenue) AS [SumOfRevenue], Sum([Lab B helping].SumOfQuantity)/Sum([Lab B helping].CountOfQuantity) AS [AvgQnt]
FROM [Lab B helping]
GROUP BY [Lab B helping].CategoryID;


-- Lab task C (base query)
-- Sales value (extended price) for each order line with its category
-- and customer country.
SELECT Categories.CategoryID, Customers.Country, [Order Details Extended].ExtendedPrice
FROM (((Customers INNER JOIN Orders ON Customers.[CustomerID] = Orders.[CustomerID]) INNER JOIN [Order Details Extended] ON Orders.[OrderID] = [Order Details Extended].[OrderID]) INNER JOIN Products ON [Order Details Extended].[ProductID] = Products.[ProductID]) INNER JOIN Categories ON Categories.[CategoryID] = Products.[CategoryID];


-- Lab task C (crosstab)
-- Sales by category (rows) and customer country (columns),
-- with a total per category.
TRANSFORM Sum([Lab task C simple].ExtendedPrice) AS [SumOfExtendedPrice]
SELECT [Lab task C simple].CategoryID, Sum([Lab task C simple].ExtendedPrice) AS [Total Of ExtendedPrice]
FROM [Lab task C simple]
GROUP BY [Lab task C simple].CategoryID
PIVOT [Lab task C simple].Country;


-- =====================================================================
-- NORTHWIND DATABASE
-- =====================================================================

-- Lab task D (base query)
-- Quantity sold for each order line, with product category
-- and the country of the employee who handled the order.
SELECT Categories.CategoryName, [Order Details].Quantity, Employees.Country
FROM (((Categories INNER JOIN Products ON Categories.[CategoryID] = Products.[CategoryID]) INNER JOIN [Order Details] ON Products.[ProductID] = [Order Details].[ProductID]) INNER JOIN Orders ON Orders.[OrderID] = [Order Details].[OrderID]) INNER JOIN Employees ON Employees.[EmployeeID] = Orders.[EmployeeID];


-- Lab task D (crosstab)
-- Quantity sold by category (rows) and employee country (columns),
-- with a total per category.
TRANSFORM Sum([Lab D Categories Query].Quantity) AS [SumOfQuantity]
SELECT [Lab D Categories Query].CategoryName, Sum([Lab D Categories Query].Quantity) AS [Total Of Quantity]
FROM [Lab D Categories Query]
GROUP BY [Lab D Categories Query].CategoryName
PIVOT [Lab D Categories Query].Country;


-- Lab task E
-- Suppliers whose total sales revenue exceeds 20,000 (GROUP BY + HAVING).
SELECT Suppliers.CompanyName, Sum([Order Details].[Quantity]*[Order Details].[UnitPrice]) AS [Revenue]
FROM (Suppliers INNER JOIN Products ON Suppliers.[SupplierID] = Products.[SupplierID]) INNER JOIN [Order Details] ON Products.[ProductID] = [Order Details].[ProductID]
GROUP BY Suppliers.CompanyName
HAVING (((Sum([Order Details].[Quantity]*[Order Details].[UnitPrice]))>20000));