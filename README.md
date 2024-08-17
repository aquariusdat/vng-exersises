Database: PostgreSql
Framework: .NET 8
Architecture, Pattern: Clean Architecture, Outbox Pattern, 
Implementations: MediatR, FluentValidation, Quartz, MailKit, Asp.Versioning, Serilog, ...

Please follow the steps below step by step:

1. Using docker-compose to setup PostgreSql with Port 5555:
    docker compose -f VNG-Exercises/docker-compose.dev.infrastructure.yml up -d
2. Open VNG-Exercises.sln file on vng-exersises\VNG-Exercises
3. Open Package Manage Console to create database (Note: choose src\VNGExercises.Persistence in Default project on Package Management Console):
    Update-Database
4. Run VNGExercises.API project

I apologize because I wasn't careful when reading email. I thought I must to do all three exercises. I think I need to explain about my project.
Exercise 1: I created two background jobs when running the project. The first background job will find user accounts that haven't updated their password in 6 months. Then, I convert them into OutboxMessages (Outbox Pattern) and save them on the Database. The other job will take 20 OutboxMessages to send emails to the user.
Exercise 2: I created a RESTful API using .NET 8 to manage a list of books. And API ver 1 I store data on the database. The API ver 2 I store data on in-memory. Then, I created a middleware to validate the header "xAuth" like exercise 2 describes.
Exercise 3: I haven't done it yet.