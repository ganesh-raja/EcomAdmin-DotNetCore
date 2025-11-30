## E-Commerce Admin Panel (ASP.NET Core MVC)

This project is a simple E-Commerce Admin Site built using **ASP.NET Core MVC** with full CRUD operations.
It includes modules for managing Admin Login, Categories, and Products.

### ✨ Features

- ASP.NET Core MVC architecture (Model–View–Controller)
- CRUD operations (Create, Read, Update, Delete)
- Simple admin UI for managing e-commerce data
- Uses Entity Framework Core for database operations

### ✅ SQL Tables with Data Types

**1. adminlogin Table**
```bash
CREATE TABLE [dbo].[adminlogin](
    [id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [uname] VARCHAR(50) NOT NULL,
    [pass] VARCHAR(50) NOT NULL,
    [access] BIT NOT NULL,
    [createddate] DATETIME NULL
);
```

**2. Categories Table**
```bash
CREATE TABLE [dbo].[Categories](
    [CategoryId] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [Name] VARCHAR(100) NULL,
    [ImageUrl] VARCHAR(200) NULL,
    [IsActive] BIT NULL,
    [CreatedDate] DATETIME NULL
);
```

**3. Products Table**
```bash
CREATE TABLE [dbo].[Products](
    [ProductId] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [Name] VARCHAR(100) NULL,
    [Description] VARCHAR(MAX) NULL,
    [Price] INT NULL,
    [Quantity] INT NULL,
    [ImageUrl] VARCHAR(200) NULL,
    [CategoryId] INT NULL,
    [CreatedDate] DATETIME NULL
);
```

### 🔌 Database Connection String (appsettings.json)

Add your SQL connection string inside appsettings.json like this:

**✅ Your Connection String**
```bash
{
  "ConnectionStrings": {
    "DefaultConnections": "Data Source=YOUR_SERVER_NAME\\SQLEXPRESS;Initial Catalog=YOUR_DATABASE_NAME;Integrated Security=True;TrustServerCertificate=True"
  }
}
```
