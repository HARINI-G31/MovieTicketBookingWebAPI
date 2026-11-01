# Movie Ticket Booking System

## 1. Project Overview

The Movie Ticket Booking System is an ASP.NET Core Web API application developed using .NET 10.

The application allows users to manage movies, theatres, screens, seats, shows, and movie ticket bookings.

The application uses SQL Server as its database and Entity Framework Core for database operations.

## 2. Technology Stack

- Framework: ASP.NET Core Web API (.NET 10)
- Programming Language: C#
- ORM: Entity Framework Core 10
- Database: Microsoft SQL Server
- API Testing: Postman
- Version Control: Git and GitHub

## 3. Architecture

The application follows a layered architecture.

Controller → Service → Repository → DbContext → SQL Server

### Controllers
Handle incoming HTTP requests and return appropriate HTTP responses.

### Services
Handle application business logic and DTO mapping.

### Repositories
Handle database operations using Entity Framework Core.

### Models
Represent the application's database entities.

### DTOs
Define the request and response data exchanged through API endpoints.

### Data
Contains the ApplicationDbContext used for database communication.

## 4. Database

Database Name: MovieTicketBookingDB

The planned database contains eight main tables:

1. Movies
2. Theatres
3. Screens
4. Seats
5. Shows
6. ShowSeats
7. Bookings
8. BookingSeats

## 5. Development Progress

### Completed: Initial Movie Module

- Movie entity created.
- ApplicationDbContext configured for Movies.
- Movie DTOs created.
- Movie repository interface and implementation created.
- Movie service interface and implementation created.
- MoviesController created.
- Dependency injection configured.
- SQL Server connection configured.
- Initial EF Core migration created and applied.
- Movie CRUD and title search endpoints tested using Postman.

### Planned Improvements

- Pagination and optional title filtering.
- Input validation.
- Improved error handling.
- Movie deletion protection when dependent shows exist.

### Upcoming Modules

- Theatres
- Screens
- Seats
- Shows
- Bookings