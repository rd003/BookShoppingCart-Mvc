# BookShoppingCartMvc (A basic e-comm system for beginners)📚🛒

It is a source code of the youtube tutorial on [Build a Full E-commerce Application Using .NET Core 10, SQL Server 2025, MVC | Complete Tutorial](https://youtu.be/_NzPJSofid8?si=jHuiACNd3dxzT1Go). Initially it was designed to explain how shopping cart 🛒 works in dot net core mvc. But now it has more features except payment gateway. A ⭐ in repository is highly appreciated, helps to promote my content.

📢 Initially , this project was built with `.net 7` and `sql server 2022`. But it is `Upgraded to .net 10.0` and `sql server 2025` and I will try to keep it up to date.

## Tech stack 🧑‍💻

   - Dotnet core mvc (.Net 10.0)
   - MS SQLServer 2025 (Database)
   - Entity Framework Core (ORM)
   - Identity Core (Authentication)
   - Bootstrap 5 (frontend)

## Tools I have used and their alternative (Updated versions)

- Visual Studio 2026 (Alternatives : .NET SDK + VS Code or .NET SDK + JetBrains Rider).
- Microsoft Sql Server Management Studio (Alternative : mssql extension for vscode / dbeaver).
- Instead of manually installing `sql server`, you can also used `sql server` which is spun up in `docker`.

**Note:** Every tool and tech is free for personal use. 

## Video tutorial 📺

[Build a Full E-commerce Application Using .NET Core 10, SQL Server, MVC | Complete Tutorial](https://youtu.be/_NzPJSofid8?si=jHuiACNd3dxzT1Go)

## How to run the project?🌐

### 1. With docker compse (Quickest way)

It is the quickest way to run the application. You don't need to install anything on your system, except docker. Make sure you have installed `Docker` in your machine. Now, run the following command

```bash
docker compose up -d
```

- Your application will be served at `http://localhost:8080/`.
- Admin's `username` and `password` is given below (How to logged-in with admin account??🧑‍💻🧑‍💻).

**Note:** If you want to debug application and want to modify the project, I would recommend to follow the second approach.

### 2. Manually setup every thing (Recommended for developers)

Make sure:
- Either Dotnet sdk 10.0 or VisualStudio 2026 pre-installed in your machine
- pre-installed Sql server 2025 or spun up it in docker container

Now, you can follow these steps:

1. Open the command prompt. Go to a directory where you want to clone this project. Use this command to clone the project.

```bash
git clone https://github.com/rd003/BookShoppingCart-Mvc
```

2. Go to the directory where you have cloned this project, open the directory `BookShoppingCart-Mvc`. You will find a file with name `BookShoppingCartMvc.sln`. Double click on this file and this project will be opened in Visual Studio.

3. Open `appsettings.json` file and update connection string.

```json
"ConnectionStrings": {
  "conn": "data source=your_server_name;initial catalog=MovieStoreMvc; integrated security=true;encrypt=false"
}
```

**Note:** It is not mandatory to install `sql server 2025` in your machine. You can spin up the `sql server` in docker container and use that for this application. But in this case your connection string will be different `Server=localhost,1433;Database=BookShoppingCartMvc;User Id=sa;Password=your_password;TrustServerCertificate=True`.

4. Run the project.

📢 When you run the project for the first time, it will do following things:

- It will generate the database
- It will seed some data
- It will create an account for `admin`

## How to logged-in with admin account?? 🧑‍💻🧑‍💻

Click on the link named `login` and get logged-in with these credentials.

```text
username: admin@gmail.com

password: Admin@123
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
