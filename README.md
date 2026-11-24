# 📚 Shahriar BookStore

> *"In honor of Shahriar, the great Persian poet."*  
> This project is named after **Seyyed Mohammad Hossein Behjat Tabrizi (Shahriar)** — a legendary Iranian poet whose works inspire love, culture, and beauty.

## 🧩 About the Project

**Shahriar BookStore** is a modern **ASP.NET Core Web API** project designed as an online bookstore system.  
It follows **Domain-Driven Design (DDD)** principles, implements the **CQRS (Command Query Responsibility Segregation)** pattern, and is structured around **Clean Architecture** for maximum scalability and maintainability.

The purpose of this project is to demonstrate a clean, layered, and enterprise-level architecture for managing books, authors, and categories in a bookstore domain.

## ⚙️ Technologies & Tools

This project is built using the following technologies and design principles:

- **.NET 8 (ASP.NET Core Web API)**
- **Entity Framework Core**
- **SQL Server**
- **Mongo DB**
- **CQRS Pattern** (Command and Query separation)
- **Domain-Driven Design (DDD)**
- **Clean Architecture**
- **Repository & Service Pattern**
- **MediatR** for command and query handling
- **Swagger** for API documentation
- **Dependency Injection**

## 🏗️ Architecture Overview

The system is built with a **Clean Architecture** approach that separates core logic from external dependencies, ensuring testability and scalability.

## Authentication setup

The API host targets .NET 9, and requires SQL Server with permission to use transaction-owned `sp_getapplock`. Configure `ConnectionStrings__DefaultConnection` and `Jwt__SecretKey` via environment variables or ASP.NET user secrets; the key must contain at least 32 UTF-8 bytes of cryptographically random material (for example, `openssl rand -base64 48`). Never commit the key. Optional `Jwt__Issuer` and `Jwt__Audience` default to `ShahriarBookStore` and `ShahriarBookStoreClient`. The same key is also used to protect stored OTP hashes; rotating it invalidates pending codes and JWTs. Run `dotnet ef database update --project Infrastructure --startup-project Facade.Presentation` before starting the API. Before applying the migration, resolve any duplicate `user.Users.PhoneNumber` values; the migration adds a unique index. If an unmanaged `user.Tokens` table was previously created outside EF migrations, reconcile it before applying this migration. Rollback requires handling passwordless accounts first.

`POST /api/Auth/login` accepts `{phoneNumber,password}`. `POST /api/Auth/request-otp` accepts `{phoneNumber}` and gives a generic response, regardless of whether the account exists. `POST /api/Auth/verify-otp` accepts `{phoneNumber,code}` and creates a passwordless user when needed. Both successful login and verification return `{accessToken,refreshToken}`. Refresh and logout accept `{refreshToken}` at `/api/Auth/refresh-token` and `/api/Auth/logout`; refresh rotates the token, and logout revokes the associated access and refresh tokens. Existing registered passwords remain supported. **Do not expose the API to untrusted clients yet:** existing user and administrative endpoints are not protected by authorization or ownership checks. In particular anonymous user edits and password registration can undermine phone ownership and enable account takeover, including pre-registration of another person's number. JWT authentication alone does not secure those endpoints; implement access control, ownership verification, and verified registration before deployment.

OTP delivery is deliberately **development-only logging**: under `ASPNETCORE_ENVIRONMENT=Development`, inspect the API console logs for the code. Never enable development logging in production. Outside Development, OTP requests fail closed with HTTP 503 because no sender is configured. Before deploying OTP login, implement `IOtpSender` with a real provider and replace the `UnavailableOtpSender` registration in `Program.cs`. Codes are valid for five minutes, limited to five attempts, single-use, and requests are throttled per phone for 60 seconds. SQL Server transaction-owned application locks serialize verification and refresh operations across instances. Configure application-wide rate limits and monitoring at the gateway as well; per-phone throttling is not a substitute for IP/device abuse protection. Password login has no built-in rate limit. Delivery occurs before the transaction commits, so provider delivery and database commit cannot be atomic: a database failure can leave a delivered but unusable code. Integration/concurrency tests against a real SQL Server are required before deployment. Token expiration timestamps are stored in UTC; discard tokens created by earlier local-time development versions when upgrading.

