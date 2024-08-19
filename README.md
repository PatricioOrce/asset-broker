# Challenge Tecnico .NET - PPI Broker

Para desarrollar esta API Rest utilicé:
- .Net 8 (Clean Architecture)
- Swagger para documentación de API
- Autenticación con JWT
- Sql Server como base de datos para manejo de cuentas y ordenes
- Entity Framework Core para la conexión a la base de datos
- XUnit para desarrollar tests unitarios
- Docker para despliegue

El sistema esta desarrollado con una arquitectura limpia pensando en su mantenibilidad, escalabilidad y organización, separando responsabilidades.
Implementa patrones como Repository, CQRS, MediatR y Strategy, este ultimo para manejar los diferentes calculos de precio total de los activos.


## Levantar el sistema:
A fines practicos (aunque poco recomendables), las environments estan en las appsettings, y se utilizan 2 connection strings diferentes ya que utilizo dos
contextos diferentes, para usuarios (Cuentas) y para las ordenes (Core), por lo que va a ser necesario ejecutar las migraciones especificando el context.
Dejo los posibles comandos a utilizar a continuacion:
- Add-Migration {migration_name} -Context {context} -o Persistence/Migrations/{core o accounts}
- Update-Database -Context {context}
PD: Se debe establecer Infrastructre como proyecto de inicio para poder ejecutar las migraciones.
En cuanto a Docker, esta configurado para que se pueda utilizar Swagger
Dejo los posibles comandos a utilizar a continuacion:
- docker build -t dotnetapp .
- docker run -d -p 8080:80 --name webapp dotnetapp

