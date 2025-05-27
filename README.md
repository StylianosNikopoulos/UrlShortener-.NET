# 🔗 URL Shortener - .NET Core Web App

This project is a **URL shortening service** inspired by platforms like `bit.ly`. It allows users to submit long URLs, receive shortened versions, and later resolve those shortened URLs back to the original addresses. Built using **ASP.NET Core Web API** for the backend and **ASP.NET Core MVC** for the frontend.

---

## 📋 Goals

> Using C# and .NET Core, create a small web application that:
>
> 1. Receives a URL in a web view and shortens it  
> 2. Generates a unique, shorter URL using `localhost` as a configurable base  
> 3. Returns the shortened URL  
> 4. Redirects to the original URL via the shortened one  
> 5. Tracks visits to each shortened URL  
>

---

## 🚀 Features

- ✅ Shorten long URLs to compact, unique short codes
- 🔁 Redirect short URLs to their original counterparts
- 📊 Track number of times a short URL is accessed
- 💾 Persistent storage using **Entity Framework Core + MySQL**
- 🛠️ Configurable base URL via appsettings (no recompilation needed)
- 📱 Web API with Swagger UI + Clean MVC frontend
- 🧪 Unit tests for handlers and services
- 🚦 GitHub Actions CI for build and test automation

---

## 🧱 Tech Stack

- **.NET 7 / .NET Core**
- **ASP.NET Core Web API**
- **ASP.NET Core MVC**
- **Entity Framework Core**
- **MySQL**
- **Swagger**
- **GitHub Actions** (CI)
- **xUnit / Moq** (Unit Testing)

---

## 🗂️ Project Structure

```plaintext
UrlShortener.API         # Web API project
├── Controllers
├── Handlers             
├── Services             # Service layer for business logic
├── Data                 # EF Core DB context and models
├── Requests/Responses   # DTOs for API
└── Program.cs

UrlShortener.MVC         # MVC frontend project
├── Controllers
├── Views
├── wwwroot/js           # JS for front-end behavior
├── Services             # HttpClient for API communication
└── Requests/Responses
