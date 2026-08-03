# Inventory Management System

A console-based inventory management application built in C#.

This project was created as part of my C# studies and focuses on object-oriented programming, input validation, CRUD operations, LINQ, and database integration using Entity Framework Core.

---

## Features

- Add products
- Update products
- Delete products
- View all products
- Generate inventory reports
- Store product data in a SQL Server database
- Automatic product code generation
- Input validation system
- Console-based user interface
- Export reports to text files

---

## Technologies Used

- C#
- .NET
- Entity Framework Core
- SQL Server LocalDB
- LINQ
- Object-Oriented Programming

---

## Product System

Each product contains:

- Product ID
- Product Code
- Product Name
- Quantity
- Price

Example Product Code:

```text
P0001
```

Product codes are unique and are used to identify individual products.

---

## Database

The application uses Entity Framework Core and SQL Server LocalDB to store product data.

Database operations are handled through a repository class that provides functionality for:

- Creating products
- Reading products
- Updating products
- Deleting products

Entity Framework Core migrations are included in the project, allowing the database structure to be recreated on another computer.

The database also enforces unique product codes.

---

## Report System

The application can generate inventory reports containing:

- Total inventory value
- Total product quantity
- Highest stock product
- Lowest stock product
- Inventory value per product

Reports can also be exported to a text file.

---

## Learning Goals

This project was created to practice:

- Classes and objects
- Lists and collections
- Methods and helper classes
- Input validation
- CRUD operations
- Entity Framework Core
- SQL database integration
- Repository pattern
- LINQ
- Separation of concerns
- Console application design

---

## Possible Future Improvements

- Search products by name
- Product categories
- Sorting system
- Pagination
- Better console UI
- Unit testing
- GUI version

---

## How To Run

1. Clone the repository.
2. Open the solution in Visual Studio.
3. Open the Package Manager Console.
4. Create the local database by running:

```powershell
Update-Database
```

5. Run the application.

The project uses SQL Server LocalDB, so SQL Server LocalDB must be available on the computer.

---

## Author

Frans Wikman