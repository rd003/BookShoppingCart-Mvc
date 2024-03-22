# BookShoppingCartMvc
Upgraded to .net 8.0

## Note
There is no admin panel for adding products, I have directly added it to the database. I recommend you to follow the tutorial.

## How to run it
1. clone the project
   `git clone https://github.com/rd003/BookShoppingCart-Mvc`
2. open `appsettings.json` file and update connection string
 
   ```
     "ConnectionStrings": {
       "conn": "data source=your_server_name;initial catalog=MovieStoreMvc; integrated security=true;encrypt=false"
     }
   ```
   
4. Delete `Migrations` folder
5. Open Tools > Package Manager > Package manager console
6. Run these 2 commands
    ```
     (i) add-migration init
     (ii) update-database
     ````
7. Now you can run this project

## How to register as admin and login??

1. Open the `Program.cs` file , you will find these commented lines
   
   ```
     //using(var scope = app.Services.CreateScope())
     //{
     //    await DbSeeder.SeedDefaultData(scope.ServiceProvider);
     //}
   
   ```

  Uncomment these line and run the project. `Now stop the project and comment these lines again.`

2. Now click on login and login with these credentials.
   `username: admin@gmail.com , password: Admin@123`

## How do I enter products and categories

Well, you can't do it from admin panel. You have to do it manually. You have to enter data directly in the database. You need to enter values in three tables. 

 - Genre
 - Book
 - OrderStatus

Order status have constant data, so I will provide the data. Copy the below script and paste it in your sql editor.

```
   USE [BookShoppingCartMvc]
   GO
   SET IDENTITY_INSERT [dbo].[OrderStatus] ON 
   GO
   INSERT [dbo].[OrderStatus] ([Id], [StatusId], [StatusName]) VALUES (1, 1, N'Pending')
   GO
   INSERT [dbo].[OrderStatus] ([Id], [StatusId], [StatusName]) VALUES (2, 2, N'Shipped')
   GO
   INSERT [dbo].[OrderStatus] ([Id], [StatusId], [StatusName]) VALUES (3, 3, N'Delivered')
   GO
   INSERT [dbo].[OrderStatus] ([Id], [StatusId], [StatusName]) VALUES (4, 4, N'Cancelled')
   GO
   INSERT [dbo].[OrderStatus] ([Id], [StatusId], [StatusName]) VALUES (5, 5, N'Returned')
   GO
   INSERT [dbo].[OrderStatus] ([Id], [StatusId], [StatusName]) VALUES (6, 6, N'Refund')
   GO
   SET IDENTITY_INSERT [dbo].[OrderStatus] OFF
   GO

```

   
