# Hotel Booking API

This is a C# project for a hotel booking application API, developed using ASP.NET Core, Entity Framework, and SQL Server. The API provides functionality for managing cities, hotels, rooms, customer registration, login, booking registration, and a feature to search for hotels based on proximity to a given address.

## Project Structure

### Controllers/

This directory contains files with the logic of the application's controllers. The methods have been implemented as part of the completed project.

### Models/

The Models directory stores files with the database models. The following models have been developed:

- City
- Hotel
- Room
- User
- Booking

These models serve as instructions for creating the Cities, Hotels, Rooms, Users, and Bookings tables in the database.

### DTO/

DTO (Data Transfer Object) classes are stored in this directory. Some routes expect responses based on these DTOs. You can refer to the project requirements and the return of the repository methods to understand the expected DTO structure.

### Repository/

This directory houses the logic responsible for interacting with the database. The methods for each requirement have been implemented as part of the completed project, adhering to the expected DTO return. The TrybeHotelContext file contains the context for the database connection. All repositories and the context have interfaces, providing contracts for these classes. If a new method for interaction with the database was added, it would be implemented in the repository, with the contract written in the corresponding interface.

### Services/

The Services directory contains services responsible for token generation and the geographic service. These services have been implemented as part of the completed project.

## Database

The project uses SQL Server as the database, and the database schema is defined by the models in the Models/ directory.

## Docker

Docker is utilized to provide a containerized environment for the database. This ensures consistency and ease of deployment.

## Getting Started

To get started with the completed project, follow these steps:

1. Clone the repository to your local machine.
2. Ensure you have Docker installed and running.
3. Build and run the Docker container for the SQL Server database.
4. Open the project in your preferred C# development environment.
5. Run and test the API.

## Dependencies

- ASP.NET Core
- Entity Framework
- SQL Server
- Docker

## Contributing

Contributions are welcome! If you find any issues or have suggestions for improvements, feel free to open an issue or submit a pull request.


