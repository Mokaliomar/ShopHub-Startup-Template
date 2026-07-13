# ShopHub Startup

A clean ASP.NET Core MVC startup template designed for students to build E-Commerce projects using the Repository Pattern and Entity Framework Core.

## Features

- ASP.NET Core MVC
- Entity Framework Core
- Repository Pattern
- SQL Server Integration
- Identity Authentication
- Bootstrap UI
- AdminLTE Dashboard
- DataTables Integration
- Toastr Notifications
- File Upload Support
- Session Configuration
<!-- - SweetAlert2 -->
<!-- - TinyMCE Support (Optional) -->

## Included Modules

### Category
- Create Category ✅
- View Categories ✅
- Edit Category ✅
- Delete Category ✅

### Product
- Create Product ✅
- Upload Product Image ✅
- View Products ✅
- Edit Product ✅
- Delete Product ✅

## Project Structure

```
BusinessLogic/
    BL/
    DTOs

DataAccess/
    Data/
    Migrations/
    Models/
    Repositories/

Entities/
    Models/
    ViewModels/
myshop.Web/
    Areas/
        Admin/
            Controllers/
            Views/
    Controllers/
    Views/
    ViewModels/
    wwwroot/
```

## Technologies

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- LINQ
- Bootstrap 5
- AdminLTE 3
- jQuery
- DataTables

## Database ✅

Update the connection string inside:

```
appsettings.json
```

Then run:

```bash
Update-Database
```

or

```bash
dotnet ef database update
```

## Default Features

- Repository Pattern
- Dependency Injection
- CRUD Operations
- File Upload
- Entity Relationships
- ViewModels
- TempData Notifications

<!-- ## Notes

This template is intended as a starting point for educational E-Commerce projects. Students are expected to extend it with additional features such as:

- Shopping Cart
- Orders
- Payments
- Reviews
- Wishlist
- Authentication Enhancements
- Dashboard Analytics -->

<!-- ## License

Educational Use Only. -->

## 🔑 Demo Accounts

To test the application's different roles and functionalities, you can use the following pre-configured accounts:

| Role | Email | Password |
| :--- | :--- | :--- |
| **Admin** | OmarAdmin@gmail.com | OmarAdmin12345# |
| **Customer / User** | OmarMohamed@yahoo.com | OmarMohamed200512345# |

> **Note:** Make sure you have executed the database migrations or database script before attempting to log in with these credentials.


## Project Screens
### Login / Register Screens
![Login Screen](./Docs/Screenshots/Login%20Screen.png)
![Register Screen](./Docs/Screenshots/Register%20Screen.png)

### Customer Home Page
![Customer Home Page](./Docs/Screenshots/User%20Home%20Page%20Screen.png)

### Customer View Product Details
![View Product Details](./Docs/Screenshots/Product%20Details%20Screen.png)

### Admin View Product Dashboard
![Admin Product Dashboard](./Docs/Screenshots/Admin%20View%20Product%20Screen.png)

### Admin Create Product
![Admin Create Product](./Docs/Screenshots/Admin%20Create%20Product.png)

### Admin Edit Product
![Admin Edit Product](./Docs/Screenshots/Admin%20Edit%20Product%20Screen.png)

### Admin View Categories
![Admin View Categories](./Docs/Screenshots/Admin%20View%20Categories%20Screen.png)

### Admin Create Category
![Admin Create Category](./Docs/Screenshots/Admin%20Create%20Category%20Screen.png)

### Admin Edit Category
![Admin Edit Category](./Docs/Screenshots/Admin%20Edit%20Category%20Screen.png)

### Admin Delete Category
![Admin Delete Category](./Docs/Screenshots/Admin%20Delete%20Category%20Screen.png)