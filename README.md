## Descripción del proyecto

¡Hola!

Estamos desarrollando una aplicación innovadora que te permitirá acceder a la historia clínica de tu mascota las 24
horas del día, los 7 días de la semana. Sabemos lo importante que es para vos tener toda la información médica de tu 
mascota siempre a mano, ya sea para una consulta rutinaria o en una emergencia.

Nos dirigimos a todos los amantes de los animales, desde individuos particulares que cuidan de sus mascotas en casa 
hasta miembros de refugios de animales que necesitan gestionar la información médica de sus rescatados de manera 
eficiente. Queremos asegurarnos de que nuestra aplicación ofrezca la mejor experiencia posible y sea una 
verdadera solución a los desafíos que enfrentas diariamente.

## Entorno de desarrollo

### Requisitos
- .NET framework 8.0
- PostgreSQL

### Configuración de la base de datos
1. Crear un usuario en PostgreSQL con permisos para crear bases de datos.
2. Crear una base de datos con un nombre a elección.
3. Añadir a appsettings.Development.json la cadena de conexión a la base de datos creada.
4. Ejecutar el comando `dotnet ef database update` para crear las tablas necesarias en la base de datos.