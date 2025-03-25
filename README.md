# 🧩 Portfolio Project API

A clean, modular Web API built with **ASP.NET Core**, designed for managing personal portfolio content — including projects, skills, achievements, and contact form messages.  
Built using **Clean Architecture**, **JWT Authentication**, and **CQRS with MediatR**.

---

## 🚀 Features

✅ JWT Authentication & Role-based Authorization (Admin, User)  
✅ Clean Architecture (Domain, Application, Infrastructure, Persistence, API)  
✅ CRUD endpoints for Projects, Skills, Technologies, Achievements, and Contacts  
✅ Email Sending via Contact Form  
✅ CQRS with MediatR  
✅ Entity Framework Core & Code-First Migrations  
✅ FluentValidation for input validation  
✅ Swagger with Bearer Token Authorization  
✅ Admin/User Role seeding with default credentials

---

## 📁 Project Structure

- **API/** – ASP.NET Core Web API layer (Controllers, Middleware, Swagger)
- **Application/** – Application layer  
  - `Features/` – CQRS (Commands, Queries, Handlers, DTOs, Validators)  
  - `Services/` – Application-level services (e.g., IJwtService)  
  - `Repositories/` – Interfaces for Read/Write Repositories  
- **Domain/** – Domain entities, value objects  
- **Infrastructure/** – Cross-cutting concerns (JWT, Email, Identity configs)  
- **Persistence/** – Data access layer  
  - `Contexts/` – EF Core DbContext  
  - `Repositories/` – Concrete implementations of Read/Write Repositories  
  - `Data/` – Seed data (DbInitializer)  
  - `Migrations/` – EF Core migrations  
- **README.md** – Project documentation

---

## ⚙️ Getting Started

### 🔧 1. Clone the Repository

git clone https://github.com/JaFidAn/PortfolioProject.git
cd PortfolioProject

🛠 2. Configure appsettings.json
Open API/appsettings.json and update these sections:

"ConnectionStrings": {
  "DefaultConnection": "Your SQL Server connection string here"
},
"JwtSettings": {
  "SecretKey": "YourSuperSecretKeyHere",
  "Issuer": "PortfolioApp",
  "Audience": "PortfolioAppUsers",
  "ExpiryMinutes": 60
},
"EmailSettings": {
  "Host": "smtp.gmail.com",
  "Port": 587,
  "EnableSsl": true,
  "UserName": "your-email@gmail.com",
  "Password": "your-app-password"
}

🧱 3. Apply Migrations

dotnet ef database update --project Persistence --startup-project API

▶️ 4. Run the Application

dotnet run --project API

🌐 5. Open Swagger UI
Go to:
https://localhost:5001/swagger

🔐 Authentication & Roles
This API uses JWT Bearer Authentication and supports role-based access control.

🧾 Available Roles
Admin

User (assigned by default on registration)

🔑 Endpoints
Register: POST /api/portfolio/auth/register

Login: POST /api/portfolio/auth/login

🔒 Example JWT Token Response:

{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
Use this token in Swagger or your client app to access secured endpoints.

🔐 How to Use JWT in Swagger
Click the 🔐 Authorize button in Swagger UI.

Paste your token with the word Bearer:
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Click Authorize and start testing secured routes.

👤 Default Admin User (Seeded Automatically)
Email	Password	Role
r.alagezov@gmail.com	R@sim1984	Admin
✉️ Contact Form & Email Sending
The API includes a contact form endpoint that allows users to send a message directly via email.

📬 Endpoint
POST /api/contacts

📨 Example Payload
{
  "fullName": "John Doe",
  "email": "john@example.com",
  "message": "I’m interested in working with you!"
}
The message is saved to the database and also sent to your email address using the SMTP settings.

📝 License
This project is open-source and free to use for personal or commercial purposes.

Licensed under the MIT License.

👨‍💻 Author
Created with ❤️ by Rasim Alagezov

📫 Email: r.alagezov@gmail.com

🌐 GitHub: https://github.com/JaFidAn

📄 LinkedIn: https://www.linkedin.com/in/rasim-alagezov/

Feel free to reach out for collaboration or feedback!
