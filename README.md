# Corporate Asset Inventory

A secure internal system to track IT assets and manage employee assignments.

📖 Project Overview

This project is a centralized IT asset management system designed to track corporate hardware (laptops, monitors, peripherals) and map them to specific employees. It focuses on clean architecture, relational data management, and web security best practices.
🛠️ Tech Stack

    Frontend: HTML5, CSS3, JavaScript (Fetch API)

    Backend: C# ASP.NET Core Web API, LINQ

    Database: MySQL (Entity Framework Core, Relational Data Mapping)

| Key Features & Implementation

    Asset Lifecycle Management: Track devices through different statuses: In Stock, Assigned, or In Repair.

    Relational Mapping: Implemented One-to-Many relationships to link multiple devices to a single employee.

    Dynamic Filtering: Real-time search by asset name or serial number powered by secure LINQ queries.

    Database Seeding: Automated initial data population for testing and development.

| Security Implementation (Core Focus)

As an aspiring Cybersecurity Analyst, I would like to implement specific measures to mitigate common web vulnerabilities:

    XSS Mitigation (Cross-Site Scripting): On the frontend, data is rendered exclusively using .textContent instead of .innerHTML to prevent the execution of malicious scripts.

    SQL Injection Prevention: By utilizing Entity Framework Core and LINQ, all database queries are parameterized, ensuring the system is immune to SQL-based attacks.

    Server-Side Validation: All incoming data is validated on the backend to ensure data integrity and prevent malformed inputs.

| Future Improvements

    Implementing JWT-based Authentication.

    Adding an Audit Log to track who changed asset statuses and when.

    Developing a Dashboard with Chart.js for inventory statistics.
