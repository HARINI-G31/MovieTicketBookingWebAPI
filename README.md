# Movie Ticket Booking System

## 1. Project Overview

The Movie Ticket Booking System is an ASP.NET Core Web API application designed to manage movies, theatres, screens, seats, shows, and customer bookings.

The application follows a layered architecture to separate API endpoints, business logic, and database operations.

## 2. Technology Stack

- ASP.NET Core Web API (.NET 10)
- Entity Framework Core 10
- SQL Server Express
- C#
- Postman for API testing
- Git and GitHub for version control

## 3. Project Architecture

The application follows this structure:

- **Controllers:** Handle HTTP requests and responses.
- **Services:** Contain application business logic.
- **Repositories:** Handle database operations.
- **Models:** Represent database entities.
- **DTOs:** Define API request and response objects.
- **Data:** Contains the Entity Framework Core DbContext.

## 4. Database

Database name: `MovieTicketBookingDB`

The planned database contains eight tables:

1. Movies
2. Theatres
3. Screens
4. Seats
5. Shows
6. ShowSeats
7. Bookings
8. BookingSeats

## 5. Development Progress

### Movie Module — Completed

Implemented operations:

- Create Movie
- Get All Movies
- Get Movie by ID
- Update Movie
- Delete Movie
- Search Movies by Title

Database migration created and applied. API endpoints tested successfully using Postman.

### Theatre Module — Completed

Implemented operations:

- Create Theatre
- Get All Theatres
- Get Theatre by ID
- Update Theatre
- Delete Theatre

Database migration created and applied. API endpoints tested successfully using Postman.

### Upcoming Modules

- Screen Module
- Seat Module
- Show Module
- ShowSeat Management
- Booking Module

## 6. Planned Improvements

- Pagination for list endpoints
- Input validation
- Global exception handling
- Entity relationships and navigation properties
- Additional business-rule validation

## 7. Version Control

Git is used to track development milestones, with changes committed as individual modules and shared features are completed.

## Module Implementation Status

### 1. Movie Module
- [x] Movie model
- [x] Movie DTOs
- [x] Movie repository
- [x] Movie service
- [x] Movies controller
- [x] Database migration
- [x] CRUD API testing

### 2. Theatre Module
- [x] Theatre model
- [x] Theatre DTOs
- [x] Theatre repository
- [x] Theatre service
- [x] Theatres controller
- [x] Database migration
- [x] CRUD API testing

### 3. Screen Module
- [x] Screen model
- [x] Screen DTOs
- [x] Screen repository
- [x] Screen service
- [x] Screens controller
- [x] Theatre-Screen relationship
- [x] Database migration
- [x] CRUD API testing


### 4. Seat Module
- [x] Seat model
- [x] Seat DTOs
- [x] Seat repository
- [x] Seat service
- [x] Seats controller
- [x] Single seat creation
- [x] Bulk seat creation
- [x] Screen-Seat relationship
- [x] Database constraints and migration
- [x] CRUD API testing

### 5. Show Module
- [x] Show model
- [x] Show DTOs
- [x] Show repository
- [x] Show service
- [x] Shows controller
- [x] Movie-Show relationship
- [x] Screen-Show relationship
- [x] Show scheduling overlap check
- [x] Database constraints and migration
- [x] Create show API testing
- [x] Get all shows with filters
- [x] Get show by ID
- [x] Get shows by Movie ID
- [x] Get shows by Theatre ID
- [x] Get shows by Date
- [x] Update show API testing
- [x] Delete show API testing
