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