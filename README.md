# Microservices - API Gateway with YARP

This project demonstrates how to build an **API Gateway** using **YARP (Yet Another Reverse Proxy)** in a microservices architecture.  
It provides routing, transformation, and **JWT authentication** for securing APIs.

---

## 🚀 Project Overview

- **API Gateway** routes requests to downstream services via YARP.  
- **Microservices**:  
  - `API_1` → Protected with **JWT Authentication**  
  - `API_2` → Public API (no authentication)  
  - `API_3` → Public API (no authentication)  
- **JWT Authentication** is configured both in Gateway and API_1.  
- **Token Generation** is handled by a dedicated `TokenService`.

---

## 📂 Architecture

The following diagram explains the high-level architecture:

```mermaid
flowchart TD
    Client[Client / Insomnia or Postman] -->|Request| Gateway[API Gateway (YARP)]
    Gateway -->|/api1| API1[API_1 (JWT Auth Required)]
    Gateway -->|/api2| API2[API_2]
    Gateway -->|/api3| API3[API_3]

    API1 -->|Validate Token| AuthDB[(JWT Validation)]
````

---

## 🔑 Token Generation

Authentication in `API_1` requires a valid JWT. Tokens can be generated via `TokenService`.
Endpoint: 👉 **[https://localhost:7121/generate-token](https://localhost:7121/generate-token)**

Thanks to the UI, we can easily create tokens:

![Token Generation](Images/Thanks%20to%20TokenService,%20we%20can%20easily%20create%20tokens%20through%20the%20interface..png)

The generated token is then used in **Postman/Insomnia** when making requests to the secured endpoint `/api1`.

---

## 🔒 API_1 - Authentication & Authorization

When accessing `API_1` through the Gateway, a **valid JWT** must be provided. Otherwise, access is denied.

✅ Successful request with JWT:
![API1 Auth](Images/api1%20Gateway%20Authentication%20&%20Authorization%20.png)

❌ Unauthorized attempt (no or invalid token):
![Unauthorized](Images/Authentication%20was%20not%20performed%20or%20was%20performed%20incorrectly.png)

---

## 🌐 API_2 and API_3

Unlike `API_1`, these services do not require authentication.
They can be accessed freely via the Gateway:

* Request to `/api2`:
  ![API2 Gateway](Images/api2%20Gateway.png)

* Request to `/api3`:
  ![API3 Gateway](Images/api3%20Gateway.png)

---

## 📌 JWT Key Setup

The signing key is defined in `appsettings.json`.
A secure, minimum **256-bit key** must be used for HS256 algorithm.

![Token Key](Images/Role-Based%20Security%20Token%20Key%20Generation.png)

---


