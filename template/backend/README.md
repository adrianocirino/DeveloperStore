# Ambev Developer Evaluation - Sales API

This project implements a complete CRUD API for managing sales records following Domain-Driven Design (DDD) principles.

### Run the Application

```bash

# Run the Web API project
dotnet run --project src/Ambev.DeveloperEvaluation.WebApi
```

The API will be available at:
- **HTTP**: http://localhost:8080
- **HTTPS**: https://localhost:8081
- **Swagger UI**: http://localhost:8080/swagger

## API Endpoints

### Sales Management

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/sales` | Get paginated list of sales with filtering |
| `GET` | `/api/sales/{id}` | Get sale by ID |
| `POST` | `/api/sales` | Create new sale |
| `PUT` | `/api/sales/{id}` | Update existing sale |
| `PATCH` | `/api/sales/{id}/cancel` | Cancel sale |
| `DELETE` | `/api/sales/{id}` | Delete sale |

### Query Parameters

The `/api/sales` endpoint supports the following query parameters:

- **Pagination**: `_page`, `_size`
- **Ordering**: `_order` (e.g., "saleDate desc", "totalAmount asc")
- **Filtering**: 
  - `customerId`: Filter by customer external ID
  - `branchId`: Filter by branch external ID
  - `status`: Filter by sale status
  - `minDate`/`maxDate`: Date range filtering
  - `minAmount`/`maxAmount`: Amount range filtering

### Example Requests

#### Create a Sale

```json
POST /api/sales
{
  "saleNumber": "SALE-001",
  "customer": {
    "externalId": "CUST-001",
    "name": "Adriano Cirino",
    "email": "adriano@example.com",
    "phone": "(79) 99999-9999"
  },
  "branch": {
    "externalId": "BRANCH-001",
    "name": "Main Store",
    "address": "123 Main St",
    "city": "Itabaiana",
    "state": "SE"
  },
  "items": [
    {
      "product": {
        "externalId": "PROD-001",
        "name": "Beer Brand X",
        "description": "Premium beer",
        "category": "Beverages",
        "brand": "Brand X"
      },
      "quantity": 5,
      "unitPrice": 10.00
    }
  ]
}
```

#### Get Sales with Filtering

```
GET /api/sales?_page=1&_size=20&_order=saleDate desc&minDate=2024-01-01&status=Active
```