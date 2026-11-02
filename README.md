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

### ShowSeat Module

**Purpose:** Manages seat availability for individual movie shows.

**Entity:**
- ShowSeatId (Primary Key)
- ShowId (Foreign Key)
- SeatId (Foreign Key)
- Status (Available / Booked)

**Relationships:**
- Each ShowSeat belongs to one Show.
- Each ShowSeat belongs to one physical Seat.
- A unique constraint on (ShowId, SeatId) prevents duplicate seat records for the same show.

**Automatic Seat Initialization:**
- When a new show is created, the system retrieves all physical seats belonging to its screen.
- It creates one ShowSeat record for each physical seat.
- Every newly created ShowSeat starts with status `Available`.
- Clients do not manually create ShowSeat records.

**API Endpoints:**

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/shows/{showId}/seats` | Get all seats for a show |
| GET | `/api/shows/{showId}/seats/available` | Get available seats |
| GET | `/api/shows/{showId}/seats/booked` | Get booked seats |

**Testing:** Automatic seat initialization and all three GET endpoints tested successfully using Postman.

## 8. Booking Module

The Booking module manages customer movie ticket bookings and cancellations.

### Entities
- Booking
- BookingSeat

### Features
- Create a booking for a selected show.
- Select multiple available seats.
- Generate a unique booking reference.
- Calculate the total booking amount on the server.
- Store individual seat prices.
- Automatically mark selected seats as Booked.
- Retrieve all bookings.
- Filter bookings by ShowId and Status.
- Retrieve booking details by BookingId.
- Retrieve bookings by ShowId.
- Cancel confirmed bookings.
- Automatically release seats after cancellation.
- Preserve booking history.

### API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| POST | /api/bookings | Create booking |
| GET | /api/bookings | Get all bookings |
| GET | /api/bookings?showId=2 | Filter by show |
| GET | /api/bookings?status=Confirmed | Filter by status |
| GET | /api/bookings/{id} | Get booking by ID |
| GET | /api/bookings/show/{showId} | Get bookings by show |
| PUT | /api/bookings/{id}/cancel | Cancel booking |

### Database Tables
- Bookings
- BookingSeats

### Transaction Handling
Basic database transactions are used when creating and cancelling bookings to maintain consistency across booking records and show-seat availability.
## Pagination

### Overview

Pagination is implemented for collection/list API endpoints to retrieve records in smaller, manageable pages instead of returning the entire dataset.

The implementation uses reusable pagination components:

- `DTOs/Common/PaginationParams.cs` – Accepts and validates pagination query parameters.
- `DTOs/Common/PagedResponseDto.cs` – Defines the standard paginated response structure.
- `Extensions/PaginationExtensions.cs` – Provides reusable Entity Framework Core pagination logic.

### Pagination Parameters

Paginated endpoints accept the following query parameters:

| Parameter | Default | Description |
|---|---:|---|
| `pageNumber` | 1 | Page number to retrieve |
| `pageSize` | 10 | Number of records per page |

Validation rules:

- Page number cannot be less than 1.
- Page size defaults to 10 when less than 1.
- Maximum page size is restricted to 100.

### Example Requests

```http
GET /api/movies?pageNumber=1&pageSize=10
GET /api/theatres?pageNumber=2&pageSize=5
GET /api/screens?pageNumber=1&pageSize=10
GET /api/seats/screen/1?pageNumber=1&pageSize=20
GET /api/shows?pageNumber=1&pageSize=10
GET /api/bookings?pageNumber=1&pageSize=10
```

Endpoints that support filtering can combine filters with pagination parameters.

### Standard Paginated Response

Example:

```json
{
  "data": [],
  "pageNumber": 1,
  "pageSize": 10,
  "totalRecords": 25,
  "totalPages": 3
}
```

| Property | Description |
|---|---|
| `data` | Records in the current page |
| `pageNumber` | Current page number |
| `pageSize` | Maximum records per page |
| `totalRecords` | Total matching records |
| `totalPages` | Total number of available pages |

### Implementation Details

- Pagination is performed at the database query level using Entity Framework Core.
- Records are ordered before pagination to provide consistent results.
- Repository methods return paginated entity results.
- Service methods map entities into response DTOs while preserving pagination metadata.
- Controllers accept pagination parameters through query strings.

### Notes

- Pagination does not apply to internal queries that require the complete dataset, such as retrieving all physical seats when initializing show-seat availability.
- Movie title search uses a dedicated repository query and is independent of the movie listing page size.
- Pagination does not require database schema changes, so no EF Core migration is needed solely for this feature.