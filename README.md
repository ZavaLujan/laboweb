# User Management API Documentation

## Base URL

```
https://localhost:44380/api
```

## Endpoints

### Users

#### List All Users

```http
GET /usuarios
```

Returns a list of all users in the system.

**Response Example:**

```json
[
  {
    "idUsuario": 1,
    "codUsuario": "despinoza",
    "nombres": "Espinoza Robles Armando",
    "idRol": 1,
    "desRol": "Supervisor Grupo 01"
  }
]
```

#### Get User by ID

```http
GET /usuarios/getbyid/{id}
```

Returns a specific user by their ID.

**Parameters:**

- `id` (path) - User ID (integer)

**Response Example:**

```json
{
  "idUsuario": 1,
  "codUsuario": "despinoza",
  "nombres": "Espinoza Robles Armando",
  "idRol": 1,
  "desRol": "Supervisor Grupo 01"
}
```

**Error Responses:**

- `404 Not Found` - User not found

#### Validate User

```http
GET /usuarios/validateuser?usuario={username}&password={password}
```

Validates user credentials and returns the user ID if valid.

**Parameters:**

- `usuario` (query) - Username
- `password` (query) - User password

**Response:**

- Returns user ID (integer) if credentials are valid
- Returns -1 if credentials are invalid

#### Create User

```http
POST /usuarios
```

Creates a new user in the system.

**Request Body:**

```json
{
  "codUsuario": "string",
  "claveTxt": "string",
  "nombres": "string",
  "idRol": 0
}
```

**Response:**
Returns the created user object with assigned ID.

**Error Responses:**

- `400 Bad Request` - Invalid user data

#### Update User

```http
PUT /usuarios/{id}
```

Updates an existing user.

**Parameters:**

- `id` (path) - User ID to update

**Request Body:**

```json
{
  "idUsuario": 0,
  "codUsuario": "string",
  "claveTxt": "string",
  "nombres": "string",
  "idRol": 0
}
```

**Error Responses:**

- `400 Bad Request` - Invalid user data
- `404 Not Found` - User not found

#### Delete User

```http
DELETE /usuarios/{id}
```

Deletes a user from the system.

**Parameters:**

- `id` (path) - User ID to delete

**Error Responses:**

- `404 Not Found` - User not found

### Roles

#### List All Roles

```http
GET /rol
```

Returns a list of all available roles in the system.

**Response Example:**

```json
[
  {
    "IdRol": 1,
    "DesRol": "Supervisor Grupo 01"
  },
  {
    "IdRol": 2,
    "DesRol": "Vendedor Grupo 01"
  },
  {
    "IdRol": 3,
    "DesRol": "Supervisor Grupo 02"
  },
  {
    "IdRol": 4,
    "DesRol": "Vendedor Grupo 02"
  }
]
```

## Data Models

### Usuario (User)

```json
{
  "idUsuario": "integer", // User ID (auto-generated)
  "codUsuario": "string", // Username
  "claveTxt": "string", // Password (plain text, only for requests)
  "clave": "byte[]", // Encrypted password (stored)
  "nombres": "string", // Full name
  "idRol": "integer", // Role ID
  "desRol": "string" // Role description (returned in responses)
}
```

### Rol (Role)

```json
{
  "IdRol": "integer", // Role ID
  "DesRol": "string" // Role description
}
```

## Error Handling

The API uses standard HTTP status codes:

- `200 OK` - Success
- `400 Bad Request` - Invalid input
- `404 Not Found` - Resource not found
- `500 Internal Server Error` - Server error

Error responses include appropriate HTTP status codes and may contain additional error details in the response body.

## Security Notes

1. Passwords are encrypted before storage using the `EncriptacionHelper.EncriptarByte()` method
2. User validation is required for protected endpoints
3. Role-based access control is implemented through the `IdRol` property

## Best Practices

1. Always validate user input before sending requests
2. Handle error responses appropriately in your client application
3. Store sensitive information securely
4. Use HTTPS for all API communications
5. Implement proper error handling in your client code

# API Documentation - ABB Catálogo

## Overview

Esta API proporciona endpoints para gestionar el catálogo de productos de ABB, permitiendo operaciones CRUD (Create, Read, Update, Delete) sobre los productos.

## Base URL

```
http://localhost:44380/api
```

## Endpoints

### List Products

Recupera la lista completa de productos.

```
GET /productos
```

#### Response

```json
[
  {
    "IdProducto": 1,
    "IdCategoria": 1,
    "NomProducto": "IEC FOOD SAFE MOTORS",
    "MarcaProducto": "ABB Food Safe Family",
    "ModeloProducto": "Economico",
    "LineaProducto": "Nema Motors",
    "GarantiaProducto": "Dos Años",
    "Precio": 25000.0,
    "Imagen": null,
    "DescripcionTecnica": "...",
    "DescCategoria": "Generadores"
  }
  // ... más productos
]
```

#### Status Codes

- 200: Success
- 404: No products found
- 500: Server error

### Get Product

Recupera un producto específico por su ID.

```
GET /productos/{id}
```

#### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| id   | int  | Product ID  |

#### Response

```json
{
  "IdProducto": 1,
  "IdCategoria": 1,
  "NomProducto": "IEC FOOD SAFE MOTORS",
  "MarcaProducto": "ABB Food Safe Family",
  "ModeloProducto": "Economico",
  "LineaProducto": "Nema Motors",
  "GarantiaProducto": "Dos Años",
  "Precio": 25000.0,
  "Imagen": null,
  "DescripcionTecnica": "...",
  "DescCategoria": "Generadores"
}
```

#### Status Codes

- 200: Success
- 404: Product not found
- 500: Server error

### Create Product

Crea un nuevo producto.

```
POST /productos
```

#### Request Body

```json
{
  "IdCategoria": 1,
  "NomProducto": "Nuevo Producto",
  "MarcaProducto": "ABB",
  "ModeloProducto": "Modelo-X",
  "LineaProducto": "Línea Industrial",
  "GarantiaProducto": "2 Años",
  "Precio": 10000.0,
  "DescripcionTecnica": "Descripción del producto"
}
```

#### Required Fields

- IdCategoria
- NomProducto
- MarcaProducto
- ModeloProducto
- LineaProducto

#### Optional Fields

- GarantiaProducto
- Precio
- Imagen
- DescripcionTecnica

#### Status Codes

- 201: Created
- 400: Bad request
- 500: Server error

### Update Product

Actualiza un producto existente.

```
PUT /productos/{id}
```

#### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| id   | int  | Product ID  |

#### Request Body

```json
{
  "IdProducto": 1,
  "IdCategoria": 1,
  "NomProducto": "Producto Actualizado",
  "MarcaProducto": "ABB",
  "ModeloProducto": "Modelo-X",
  "LineaProducto": "Línea Industrial",
  "GarantiaProducto": "2 Años",
  "Precio": 10000.0,
  "DescripcionTecnica": "Descripción actualizada"
}
```

#### Required Fields

- IdProducto (debe coincidir con el ID en la URL)
- IdCategoria
- NomProducto
- MarcaProducto
- ModeloProducto
- LineaProducto

#### Optional Fields

- GarantiaProducto
- Precio
- Imagen
- DescripcionTecnica

#### Status Codes

- 200: Success
- 400: Bad request
- 404: Product not found
- 500: Server error

### Delete Product

Elimina un producto específico.

```
DELETE /productos/{id}
```

#### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| id   | int  | Product ID  |

#### Status Codes

- 200: Success
- 404: Product not found
- 500: Server error

## Categorías Disponibles

| ID  | Descripción     |
| --- | --------------- |
| 1   | Generadores     |
| 2   | Descargadores   |
| 3   | Transformadores |

## Manejo de Errores

La API retorna errores en el siguiente formato:

```json
{
  "Message": "Descripción del error",
  "ExceptionMessage": "Mensaje detallado del error (solo en desarrollo)",
  "ExceptionType": "Tipo de excepción (solo en desarrollo)",
  "StackTrace": "Stack trace del error (solo en desarrollo)"
}
```

## Ejemplos de Uso

### Crear un Nuevo Producto (cURL)

```bash
curl -X POST \
  http://[your-domain]/api/productos \
  -H 'Content-Type: application/json' \
  -d '{
    "IdCategoria": 1,
    "NomProducto": "Motor Industrial X2000",
    "MarcaProducto": "ABB Motors",
    "ModeloProducto": "X2000-Industrial",
    "LineaProducto": "Motores Industriales",
    "GarantiaProducto": "3 Años",
    "Precio": 15000.00,
    "DescripcionTecnica": "Motor industrial de alta eficiencia"
}'
```

### Actualizar un Producto (cURL)

```bash
curl -X PUT \
  http://[your-domain]/api/productos/1 \
  -H 'Content-Type: application/json' \
  -d '{
    "IdProducto": 1,
    "IdCategoria": 1,
    "NomProducto": "Motor Industrial X2000 - Updated",
    "MarcaProducto": "ABB Motors",
    "ModeloProducto": "X2000-Industrial",
    "LineaProducto": "Motores Industriales",
    "GarantiaProducto": "3 Años",
    "Precio": 16000.00,
    "DescripcionTecnica": "Motor industrial de alta eficiencia actualizado"
}'
```

## Notas Adicionales

- La API utiliza UTF-8 para la codificación de caracteres
- Los campos decimales usan el punto como separador decimal
- Las fechas deben enviarse en formato ISO 8601
- Las imágenes se manejan como arrays de bytes (Base64)
- Todos los endpoints retornan los datos en formato JSON

## Limitaciones

- El tamaño máximo de carga para imágenes es de 10MB
- Las solicitudes tienen un timeout de 30 segundos
- Se permiten máximo 100 solicitudes por minuto por IP
