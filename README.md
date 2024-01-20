# BookShoppingCartMvc
Upgraded to .net 8.0

## Note
There is no admin panel for adding products, I have directly added it to the database. I recommend you to follow the tutorial.

## How to run it
1. clone the project
   `git clone https://github.com/rd003/BookShoppingCart-Mvc`
2. open `appsettings.json` file and update connection string 
   "ConnectionStrings": {
  "conn": "data source=your_server_name;initial catalog=MovieStoreMvc; integrated security=true;encrypt=false"
   }
   
3. Delete `Migrations` folder
4. Open Tools > Package Manager > Package manager console
5. Run these 2 commands
    ```
     (i) add-migration init
     (ii) update-database
     ````
6. Now you can run this project

## How to register as admin and login??

1. Open the `Program.cs` file , you will find these commented lines
   
   ```//using(var scope = app.Services.CreateScope())
   //{
   //    await DbSeeder.SeedDefaultData(scope.ServiceProvider);
   //} ```

  Uncomment these line and run the project. `Now stop the project and comment these lines again.`

2. Now click on login and login with these credentials.
   `username: admin@gmail.com , password: Admin@123`
   
