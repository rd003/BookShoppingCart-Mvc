# BookShoppingCartMvc (A basic e-comm system for beginners)📚🛒

It is a source code of the youtube tutorial on [book shopping cart in .net core mvc](https://www.youtube.com/watch?v=R4ZLWD89R5w&list=PLP8UhDwXI7f_8r2Rbt7GNwf7eXZqUu_p4). I have tried to explain how shopping cart 🛒 works in dot net core mvc. A ⭐ in repository is highly appreciated, helps to promote my content.

📢 Initially , this project was built with .net 7. But it works fine with .net 6+ and now it is **Upgraded to .net 8.0.** I will try to keep it up to date.

## Tech stack 🧑‍💻

   - Dotnet core mvc (.Net 8)
   - MS SQLServer (Database)
   - Entity Framework Core (ORM)
   - Identity Core (Authentication)
   - Bootstrap 5 (frontend)

## Video tutorial 📺

[Youtube playlist](https://www.youtube.com/watch?v=R4ZLWD89R5w&list=PLP8UhDwXI7f_8r2Rbt7GNwf7eXZqUu_p4)

## How to run the project?🌐

I am assuming that, you have already installed **Visual Studio 2022** (It is the latest as of march,2024) and **MS SQL Server Management Studio** (I am using mssql server 2022 as of march,2024). Now, follow the following steps.

1.Open command prompt. Go to a directory where you want to clone this project. Use this command to clone the project.

```bash
git clone https://github.com/rd003/BookShoppingCart-Mvc
```

2.Go to the directory where you have cloned this project, open the directory `BookShoppingCart-Mvc`. You will find a file with name `BookShoppingCartMvc.sln`. Double click on this file and this project will be opened in Visual Studio.

3.Open `appsettings.json` file and update connection string

```json
"ConnectionStrings": {
  "conn": "data source=your_server_name;initial catalog=MovieStoreMvc; integrated security=true;encrypt=false"
}
```

4.Delete `Migrations` folder.

5.Open Tools > Package Manager > Package manager console

6.Run these 2 commands (works only with Visual studio)

```bash
  add-migration init

  update-database
```

7.Now you can run this project.

## How to register as admin and login?? 🧑‍💻🧑‍💻

1.Open the `Program.cs` file , you will find these commented lines

```c#
//using(var scope = app.Services.CreateScope())
//{
//    await DbSeeder.SeedDefaultData(scope.ServiceProvider);
//}
```

Uncomment these line and run the project. `Now stop the project and comment these lines again.`

2.Now click on login and login with these credentials.

```text
username: admin@gmail.com

password: Admin@123
```

## Data Entry 📈📉

I have provided some data of these 3 tables to test the application.

**⚠️Note: Data entry of Genre and Book is optional, you can do it from admin panel but you must enter some data for OrderStatus.**

- Genre (You can also add it from the admin panel)
- Book (You can also add it from the admin panel)
- OrderStatus (⚠️It Contain Constants. You Can not enter OrderStatus from the Admin panel. It must be added through sql server)

Please, run these scripts in a order. Genre data must be added before book.

### Genre

```sql
  USE [BookShoppingCartMvc]
  GO
  SET IDENTITY_INSERT [dbo].[Genre] ON
  GO
  INSERT [dbo].[Genre] ([Id], [GenreName]) VALUES (1, N'Romance')
  GO
  INSERT [dbo].[Genre] ([Id], [GenreName]) VALUES (2, N'Action')
  GO
  INSERT [dbo].[Genre] ([Id], [GenreName]) VALUES (3, N'Thriller')
  GO
  INSERT [dbo].[Genre] ([Id], [GenreName]) VALUES (4, N'Crime')
  GO
  INSERT [dbo].[Genre] ([Id], [GenreName]) VALUES (5, N'SelfHelp')
  GO
  INSERT [dbo].[Genre] ([Id], [GenreName]) VALUES (6, N'Programming')
  GO
  SET IDENTITY_INSERT [dbo].[Genre] OFF
  GO

```

### Book

```sql
  USE [BookShoppingCartMvc]

  INSERT INTO book (BookName, AuthorName, Price, GenreId)
  VALUES
  ('Pride and Prejudice', 'Jane Austen', 12.99, 1),
  ('The Notebook', 'Nicholas Sparks', 11.99, 1),
  ('Outlander', 'Diana Gabaldon', 14.99, 1),
  ('Me Before You', 'Jojo Moyes', 10.99, 1),
  ('The Fault in Our Stars', 'John Green', 9.99, 1);

  -- Inserting rows for Action genre
  INSERT INTO book (BookName, AuthorName, Price, GenreId)
  VALUES
  ('The Bourne Identity', 'Robert Ludlum', 14.99, 2),
  ('Die Hard', 'Roderick Thorp', 13.99, 2),
  ('Jurassic Park', 'Michael Crichton', 15.99, 2),
  ('The Da Vinci Code', 'Dan Brown', 12.99, 2),
  ('The Hunger Games', 'Suzanne Collins', 11.99, 2);

  -- Inserting rows for Thriller genre
  INSERT INTO book (BookName, AuthorName, Price, GenreId)
  VALUES
  ('Gone Girl', 'Gillian Flynn', 11.99, 3),
  ('The Girl with the Dragon Tattoo', 'Stieg Larsson', 10.99, 3),
  ('The Silence of the Lambs', 'Thomas Harris', 12.99, 3),
  ('Before I Go to Sleep', 'S.J. Watson', 9.99, 3),
  ('The Girl on the Train', 'Paula Hawkins', 13.99, 3);

  -- Inserting rows for Crime genre
  INSERT INTO book (BookName, AuthorName, Price, GenreId)
  VALUES
  ('The Godfather', 'Mario Puzo', 13.99, 4),
  ('The Girl with the Dragon Tattoo', 'Stieg Larsson', 12.99, 4),
  ('The Cuckoo''s Calling', 'Robert Galbraith', 14.99, 4),
  ('In Cold Blood', 'Truman Capote', 11.99, 4),
  ('The Silence of the Lambs', 'Thomas Harris', 15.99, 4);

  -- Inserting rows for SelfHelp genre
  INSERT INTO book (BookName, AuthorName, Price, GenreId)
  VALUES
  ('The 7 Habits of Highly Effective People', 'Stephen R. Covey', 9.99, 5),
  ('How to Win Friends and Influence People', 'Dale Carnegie', 8.99, 5),
  ('Atomic Habits', 'James Clear', 10.99, 5),
  ('The Subtle Art of Not Giving a F*ck', 'Mark Manson', 7.99, 5),
  ('You Are a Badass', 'Jen Sincero', 11.99, 5);

  -- Inserting rows for Programming genre
  INSERT INTO book (BookName, AuthorName, Price, GenreId)
  VALUES
  ('Clean Code', 'Robert C. Martin', 19.99, 6),
  ('Design Patterns: Elements of Reusable Object-Oriented Software', 'Erich Gamma', 17.99, 6),
  ('Code Complete', 'Steve McConnell', 21.99, 6),
  ('The Pragmatic Programmer', 'Andrew Hunt', 18.99, 6),
  ('Head First Design Patterns', 'Eric Freeman', 20.99, 6);

```

### Order Status

```sql
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

## Other useful sql scripts

You also need to add this stored procedure in your database.

```sql
create  procedure [dbo].[Usp_GetTopNSellingBooksByDate]
@startDate datetime,@endDate datetime
as
begin

SET NOCOUNT ON;

with UnitSold as
(
select od.BookId, SUM(od.Quantity) as TotalUnitSold from [order] o 
join OrderDetail od on o.Id = od.OrderId
where o.IsPaid=1 and o.IsDeleted=0 and o.CreateDate between @startDate and @endDate
group by od.BookId
)

select top 5 b.BookName,b.AuthorName,b.[Image],us.TotalUnitSold 
from  UnitSold us
join [Book] b
on us.BookId = b.Id
order by us.TotalUnitSold desc
end
```

## Screenshots

1.Homepage

![homepage](./screenshots/1.jpg)

2.Homepage continued

![homepage2](./screenshots/2.jpg)

3.Login

![login](./screenshots/3.jpg)

4.Registration

![registration](./screenshots/4.jpg)

5.Add To Cart

![add-to-cart](./screenshots/5.jpg)

6.Cart

![cart](./screenshots/6.jpg)

7.Checkout

![cart](./screenshots/7.jpg)

8.Order success

![order_suceess](./screenshots/8_order_success.jpg)

9.Admin Login

![Admin Login](./screenshots/9_admin_login.jpg)

10.Admin Dashboard

![Admin Dashboard](./screenshots/10%20admin%20dashboard.jpg)

11.Orders

![Orders](./screenshots/11%20admin%20orders.jpg)

12.Order Detail

![Order Detail](./screenshots/12%20admin%20order%20detail.jpg)

13.Update Order Status

![Update Order Status](./screenshots/13%20Update%20Order%20Status.jpg)

14.Display Stock

![Display Stock](./screenshots/14%20%20display%20stock.jpg)

15.Update Stock

![Update Stock](./screenshots/15%20update%20stock.jpg)

16.Display Genre

![Display Genre](./screenshots/16%20display%20genres.jpg)

17.Add Genre

![Add Genre](./screenshots/17%20add%20genre.jpg)

18.Update Genre

![Update Genre](./screenshots/18%20Update%20Genre.jpg)

19.Display Books

![Display Books](./screenshots/19%20display%20books.jpg)

20.Add Book

![Add Book](./screenshots/20%20add%20books.jpg)

21.Update Book

![Update Book](./screenshots/21%20update%20book.jpg)

22.Top Selling Books

![Top Selling Books](./screenshots/22%20top%20selling%20books.jpg)

## Thanks

If you find this repository useful, then consider to leave a ⭐.

Connect with me

👉 YouTube: <https://youtube.com/@ravindradevrani>

👉 Twitter: <https://twitter.com/ravi_devrani>

Become a supporter ❣️:
You can buy me a coffee 🍵 : <https://www.buymeacoffee.com/ravindradevrani>

Thanks a lot 🙂🙂
