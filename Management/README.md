# Prueba Técnica

Este proyecto es una API RESTful desarrollada en ASP.NET Core, que permite gestionar usuarios en una base de datos MySQL. La API proporciona operaciones básicas como obtener, agregar y actualizar usuarios.

## Requisitos

- .NET SDK (version 6.0 o superior)
- MySQL Server
- Dapper (para la interacción con la base de datos)
- MediatR

## Instalación

1. Clona el repositorio:
	
2. Crea la base de datos:
    Asegúrate de tener la base de datos MySQL configurada y en ejecución. Crea una base de datos llamada userdb y una tabla user con la siguiente estructura:
```
        CREATE TABLE user (
            id INT AUTO_INCREMENT PRIMARY KEY,
            username VARCHAR(50) NOT NULL,
            email VARCHAR(100) NOT NULL
        );
````
3. Configura la cadena de conexión en la clase UserRepository:
```
    const string connectionString = "server=localhost;port=3306;database=userdb;uid=YOUR_USER;password=YOUR_PASSWORD";
```
4. Restaura las dependencias del proyecto:
    ```bash
   dotnet restore
   ```
   
## Endpoints:
- GET 
    - Obtiene los usuarios
- POST 
   - Agrega un nuevo usuario
   - Cuerpo de la solicitud:
   ```bash
   {
  "UserName": "nombre_usuario",
  "Email": "correo@ejemplo.com"
    }
    ````
- PUT
   - Actualiza un usuario
   - Cuerpo de la solicitud:
    ```bash
        {
      "Id": 1,
      "UserName": "nuevo_nombre_usuario",
      "Email": "nuevo_correo@ejemplo.com"
    }
    ```
