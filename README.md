# Bespoke Bikes API

This API was created to for a tech interview, in which I was given specific criteria, but encouraged "to make reasonable assumptions and design decisions." The assumptions and design decisions are recorded here as best I can.

## Description

BeSpoked Bikes is a high-end bicycle shop that wants to build a backend system to support a new sales tracking application. The goal is  to track bicycle sales, calculate commissions for salespeople, and eventually support quarterly bonuses.

You are tasked with designing and implementing the API layer and supporting backend for this system. The client application will consume your API to display and manage data.

### Technical Details

- **Target Framework:** .Net 10.0
- **API Design:** ASP .NET Controller Web API
- **Local Database:** Sqlite
- **Data Access Layer:** Entity Framework Code-First
- **Unit Tests:** xUnit v3
  - **Test Database:** In-Memory

## How to Run

### Generate Database

Create a User Secret to store the database connection in. Since this is only Sqlite, and a local file, the command is below.
> Note: This should be run in the BespokeBikesApi folder.

```console
dotnet user-secrets set "ConnectionStrings:SqliteConnection" "Data Source=Sales.db"
```

The database file for the local development is not included in the source control, so you will need to run the following command to generate it.
> Note: This should be run in the BespokeBikesApi folder.

```console
dotnet tool install --global dotnet-ef
dotnet ef database update
```

### Build API

Run the command `dotnet build` in either the root folder or the BespokeBikesApi folder.

### Run API

Run the command `dotnet run` in the BespokeBikesApi folder. Navigate to the localhost url given after "Now listening on: ", and then add "/swagger/index.html" to the end in order to navigate to the swagger page.

### Optional: Run Tests

Run the command `dotnet test .\BespokeBikesApi.Tests\BespokeBikesApi.Tests.csproj` from the root folder, or `dotnet test .\BespokeBikesApi.Tests.csproj` from the BespokeBikesApi.Tests folder.

Alternatively, use the .Net Core Test Explorer extension in Visual Studios Code. To install this extension, run the following commands:

```console
code --install-extension hbenl.vscode-test-explorer
code --install-extension ms-vscode.test-adapter-converter
code --install-extension derivitec-ltd.vscode-dotnet-adapter
```

## Assumptions

### Generic

- No `Name` field for a category of person, by itself, can be a unique requirement because there are many people with the same name in America. (i.e. There are many people named "Matt Johnson").

### Customers

- Duplications of a customer are identified by `Name` and `Contact` such that these two datapoints must be unique as a combination.

### Salespersons

- A Salesperson has a `Name`, as no datapoints were given, and that is how American culture defaults to identifying a person.
- Since a `Name` on a person cannot be unique by itself, another field `EmployeeId` is added to uniquely identify a Salesperson. This is a string in order to allow for many types of Ids.

### Bicycles ("Products")

- All Bicycles will have the Type `Bicycle` in the `Products` table.
- Duplications of a Bicycle are identified by Products of `Type` and `Name`. However, for the first iteration of this API, all `Products` have the `Type` of `Bicycle`, so that makes the unique identifier essentially `Name`. This is excluded from the `Name` field not being able to be a unique identifier because it is not for a person, and can be made unique.

### Sales

- Each `Sale` represents a single bicycle being sold.

## Design Decisions

### Database Design

- In order to make the store's inventory extensible, a `Products` table was created instead of a Bicycle table.
- All Bicycles will have the Type `Bicycle` in the `Products` table.
- An `Inventory` table was created since the `PurchasePrice` may be different per `PurchaseDate` of each Product. This table holds the information specific to each individual purchase of a product from a manufacturer.
- A `Sale` is tied to the `Product` record as this will allow for a single `ProductId` to be used for all purchases of that Product, making it easier for users to enter sales.
  - Should a report later be added to calculate profit, the `PurchaseDate` and `SalesDate` can be used to roughly match per-item profit, if such a detailed report is needed.
- `Sales` data includes the `Comission` at the time of the Sale, as the `ComissionPercentage` can be changed on the `Product` at a later date.

### Data Layer: Code-First Entity Framework

Code-First Entity Framework was chosen to give the C# code full control of the relational database structure, and to make it easier to transfer the database connection between SQLite and Postgres (SQLite for local development; Postgres for deployment).

### DataContext Factories

The DbContext has several Factories associated with it's initialization.

The IBespokeBikesContextFactory interface was created to both

(a) allow the ContextFactory to have an implementation that decided between a Sqlite and Postgres Database (Local Dev vs Production) as well as

(b) to allow the Tests suite to be able to implement a In-Memory Database to make tests more stable by having transient data.

The IBespokeBikesContextOptionsFactory interface was created to prevent the ContextFactory from having to know about the details behind the Sqlite or Postgres implementations of the database connection, and to allow the ContextFactory to simply call the appropriate OptionsBuilder for each implementation depending on the configured database.

### Local Database: SQLite

This is a light-weight relational database implementation that can be used to have persistent data for local testing without much overhead.

### Test Framework

#### XUnit

XUnit was chosen as it has a lot of support in .Net and VSCode, but also allows for testing of Controllers. At the moment there are no Controller test cases, but this was my intent.

#### InMemory Database

An In-Memory Database was used for the test cases as it allowed for a relational database that would reset each time the tests were ran.
