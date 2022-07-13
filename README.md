# .NET Rest API

## Entity Framework CLI

This project already have a local tools configuration file, so you should be good to go, otherwise, install local tools using the following command.

```
dotnet new tool-manifest
```

Finally, install Entity Framework CLI using the command below.

 ```
 dotnet tool install dotnet-ef --version "5.0.5"
 ```
 
 ## Database Build-up
 
 Install MySQL Client using the password `admin` for the `root` user and then configure the database using:
 
 ```
 dotnet ef database update
 ```
