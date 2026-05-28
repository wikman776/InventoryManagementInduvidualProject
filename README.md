# Inventory Management System

A console-based inventory management application built in C#.

This project was created as part of my C# studies and focuses on object-oriented programming, input validation, file handling, and CRUD operations.

## Features

* Add products
* Update products
* Delete products
* View all products
* Generate inventory reports
* Save and load data using JSON
* Automatic product ID generation
* Input validation system
* Console-based user interface
* Export reports to text files

## Technologies Used

* C#
* .NET
* System.Text.Json
* LINQ
* Object-Oriented Programming

## Product System

Each product contains:

* Product ID
* Product Name
* Quantity
* Price

Example Product ID:

```text
P0001
```

## Report System

The application can generate inventory reports containing:

* Total inventory value
* Total product quantity
* Highest stock product
* Lowest stock product
* Inventory value per product

Reports can also be exported to a text file.

## Save System

The project uses JSON serialization to save and load product data automatically between sessions.

Saved data includes:

* Product list
* Next available product ID

## Learning Goals

This project was created to practice:

* Classes and objects
* Lists and collections
* Methods and helper classes
* Input validation
* CRUD operations
* JSON serialization
* File handling
* Separation of concerns
* Console application design

## Possible Future Improvements

* Search products by name
* Product categories
* Sorting system
* Pagination
* Better console UI
* Database integration
* Unit testing
* GUI version

## How To Run

1. Clone the repository
2. Open the solution in Visual Studio
3. Run the application

## Author

Frans Wikman
