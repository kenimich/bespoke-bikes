# Bespoke Bikes API

This API was created to for a tech interview, in which I was given specific criteria, but encouraged "to make reasonable assumptions and design decisions." The assumptions and design decisions are recorded here as best I can.

## Description

BeSpoked Bikes is a high-end bicycle shop that wants to build a backend system to support a new sales tracking application. The goal is  to track bicycle sales, calculate commissions for salespeople, and eventually support quarterly bonuses.

You are tasked with designing and implementing the API layer and supporting backend for this system. The client application will consume your API to display and manage data.

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

## Design Decisions

### Database Design

- In order to make the store's inventory extensibile, a `Products` table was created instead of a Bicycle table.
- All Bicycles will have the Type `Bicycle` in the `Products` table.
- An `Inventory` table was created since the `PurchasePrice` may be different per `PurchaseDate` of each Product. This table holds the information specific to each individual purchase of a product from a manufacturer.
- A `Sale` is tied to the `Product` record as this will allow for a single `ProductId` to be used for all purchases of that Product, making it easier for users to enter sales.
  - Should a report later be added to calculate profit, the `PurchaseDate` and `SalesDate` can be used to roughly match per-item profit, if such a detailed report is needed.
- `Sales` data includes the `Comission` at the time of the Sale, as the `ComissionPercentage` can be changed on the `Product` at a later date.

### Data Layer: Code-First Entity Framework

Code-First Entity Framework was chosen to give the C# code full control of the relational database structure, and to make it easier to transfer the database connection between SQLite and Postgres (SQLite for local development; Postgres for deployment).

### Database: SQLite

This is a light-weight relational database implementation that can be used to easily mock data and test functionality.

### Test Framework

#### XUnit

#### InMemory Database

## How to Run
