# Corporate Asset Inventory

A secure internal system to track IT assets, manage employee assignments, and demonstrate modern web security defensive practices.

📖 Project Overview

This project is a centralized IT asset management system designed to track corporate hardware (laptops, monitors, peripherals) and map them to specific employees. It focuses on clean architecture, strict state-machine business logic, and web security best practices.

🛠️ Tech Stack

    Frontend: HTML5, CSS3, JavaScript (Fetch API, DOM Manipulation)
    Backend: C# ASP.NET Core Web API, LINQ
    Database: MySQL (Entity Framework Core, Code-First Migrations)
    Testing & Automation: xUnit, Selenium WebDriver, DotNetSeleniumExtras

🤖 AI-Assisted Development & Mentorship

    This is my first major full-stack application, developed alongside **Gemini as a technical mentor**. Utilizing AI allowed me to bridge the gap between academic theory and enterprise-level practice, drastically accelerating my engineering and cybersecurity skills.

| Key Features & Implementation

    Asset Lifecycle Management: Track devices through strict lifecycle statuses: In Stock, Assigned, or In Repair.
    Relational Mapping: Implemented One-to-Many relationships using Entity Framework Core to link multiple hardware devices to a single corporate employee.
    Real-Time Client Filtering: High-performance search functionality filtering by asset name or serial number instantly via vanilla JavaScript event listeners.
    Database Seeding: Automated initial relational data population for testing and deterministic environment setups.

| Security Implementation

Implemented rigorous defenses to mitigate common OWASP Top 10 web vulnerabilities:

    XSS Mitigation (Cross-Site Scripting): Dynamic UI generation relies on a custom context-aware HTML entity encoder (`escapeHTML`). Dangerous characters (`<`, `>`, `&`, `"`, `'`) injected via malicious payloads are safely encoded before being injected into innerHTML slots, rendering DOM-based XSS exploits completely harmless.
    SQL Injection Prevention: Handled entirely through the backend using Entity Framework Core and strongly-typed LINQ queries. Every database transaction is automatically parameterized at the database-driver level, rendering standard SQL injection strings inert.
    Strict State-Machine Validation: Implemented identical twin-layered validation logic on both Frontend (JS) and Backend (C# Data Annotations & Controller filters). Assets marked as 'In Stock' or 'In Repair' are blocked from holding employee associations, while 'Assigned' assets strictly require a valid Employee ID.

| Automated E2E Testing

The project includes an isolated testing suite (`AssetManagement.Tests`) built using xUnit and Selenium WebDriver to automate quality assurance and regression checks. 

The test suite simulates real-world human behavior in a real Google Chrome instance to verify:
    - Asynchronous API data load and dynamic table row generation.
    - Real-time client-side table rendering and keyword search filters.
    - Input form constraint validation (handling missing inputs and boundary cases).
    - Modal display state transitions (display: flex/none toggles).
    - Safe execution of critical deletion paths with browser-native JavaScript confirm-box handling.

| Future Improvements

    - Developing a dedicated subpage for comprehensive Employee Management (Create, Read, Update, Delete).
    - Extending Employee metadata to track operational departments and teams (e.g., IT, HR, Finance).
    - Implementing JWT-based (JSON Web Token) Authentication and Role-Based Access Control (RBAC).
    - Adding an immutable server-side Audit Log to track history logs of asset status modifications.
    - Creating a modern statistical dashboard powered by Chart.js for warehouse metrics.