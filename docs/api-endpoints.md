# SmartEvent — API Endpoints

This document records the endpoints currently visible in Swagger and the CRUD endpoints that are not yet available.

## Legend

- ✅ Implemented
- ❌ Not implemented
- 🚧 In progress


## Overview

| Resource | Implemented | Not implemented |
|---|---:|---:|
| Categories | 3 | 2 |
| Events | 3 | 2 |
| Reservations | 2 | 3 |
| Users | 6 | 1 |
| **Total** | **14** | **9** |

## Categories

### Implemented

| Method | Route | Description | Status |
|---|---|---|---|
| GET | `/api/Categories/{id}` | Retrieves a category by its ID. | ✅ |
| PUT | `/api/Categories/{id}` | Updates a category by its ID. | ✅ |
| DELETE | `/api/Categories/{id}` | Deletes a category by its ID. | ✅ |

### Not implemented

| Method | Route | Description | Status |
|---|---|---|---|
| GET | `/api/Categories` | Retrieves all categories. | ❌ |
| POST | `/api/Categories` | Creates a new category. | ❌ |

## Events

### Implemented

| Method | Route | Description | Status |
|---|---|---|---|
| GET | `/api/Events/{id}` | Retrieves an event by its ID. | ✅ |
| PUT | `/api/Events/{id}` | Updates an event by its ID. Only users with the `Admin` or `Organizer` role are authorized to perform this action. | ✅ |
| DELETE | `/api/Events/{id}` | Deletes an event by its ID. Only users with the `Admin` role are authorized to perform this action. | ✅ |

### Not implemented

| Method | Route | Description | Status |
|---|---|---|---|
| GET | `/api/Events` | Retrieves all events. | ❌ |
| POST | `/api/Events` | Creates a new event. | ❌ |

## Reservations

### Implemented

| Method | Route | Description | Status |
|---|---|---|---|
| GET | `/api/Reservations/{id}` | Retrieves a reservation by its ID. | ✅ |
| DELETE | `/api/Reservations/{id}` | Deletes a reservation by its ID. Only users with the `Admin` role are authorized to perform this action. | ✅ |

### Not implemented

| Method | Route | Description | Status |
|---|---|---|---|
| GET | `/api/Reservations` | Retrieves all reservations. | ❌ |
| POST | `/api/Reservations` | Creates a new reservation. | ❌ |
| PUT | `/api/Reservations/{id}` | Updates a reservation by its ID. | ❌ |

## Users

### Implemented

| Method | Route | Description | Status |
|---|---|---|---|
| GET | `/api/Users/{id}` | Retrieves a user by ID. Returns `404 Not Found` if the user does not exist. | ✅ |
| GET | `/api/Users/{email}` | Retrieves a user by email address. | ✅ |
| GET | `/api/Users` | Retrieves all users. | ✅ |
| PUT | `/api/Users/{id}` | Updates a user's information by ID. Only users with the `Admin` role are authorized to perform this action. | ✅ |
| PUT | `/api/Users/me` | Updates the authenticated user's profile. Authentication is required. | ✅ |
| DELETE | `/api/Users/{id}` | Deletes a user by ID. | ✅ |


### Not implemented

| Method | Route | Description | Status |
|---|---|---|---|
| POST | `/api/Users` | Creates a new user. | ❌ |
