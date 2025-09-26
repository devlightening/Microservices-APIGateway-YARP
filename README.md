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

<img width="971" height="337" alt="Thanks to TokenService, we can easily create tokens through the interface" src="https://github.com/user-attachments/assets/429d6544-46df-462b-8d40-1ed3170bf891" />


The generated token is then used in **Postman/Insomnia** when making requests to the secured endpoint `/api1`.

---

## 🔒 API_1 - Authentication & Authorization

When accessing `API_1` through the Gateway, a **valid JWT** must be provided. Otherwise, access is denied.

✅ Successful request with JWT:
<img width="1422" height="752" alt="API 1 can be accessed through authentication with tokens generated through the token service" src="https://github.com/user-attachments/assets/00c08d2c-a539-47e3-af19-0c706749523b" />


❌ Unauthorized attempt (no or invalid token):
<img width="1400" height="653" alt="401 Unauthorized" src="https://github.com/user-attachments/assets/5f076f0f-5324-4e9e-9f9c-0815a4aa4b6a" />


---

## 🌐 API_2 and API_3

Unlike `API_1`, these services do not require authentication.
They can be accessed freely via the Gateway:

* Request to `/api2`:
<img width="1381" height="637" alt="API 2  can be configured with both inmemory and config" src="https://github.com/user-attachments/assets/9eb58cec-26d9-4fdb-b6a7-bd79daba04e3" />

* Request to `/api3`:
<img width="1370" height="665" alt="api3" src="https://github.com/user-attachments/assets/0c15d857-bfb4-487b-be38-e02be4a55cb8" />

---
