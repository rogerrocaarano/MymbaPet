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
- Node.js

### Base de datos
- Configurar la cadena de conexión de la base de datos mediante el archivo `appsettings.json` o variables de entorno.
- Las migraciones se aplicarán de manera automática al iniciar la aplicación.

### Módulos node
#### Tailwindcss
Los archivos css base de tailwind se encuentran en el directorio tools/tailwindcss
Exportar el archivo css minimizado de tailwind en el directorio wwwroot/css mediante el siguiente comando:

`npx tailwindcss -i tools/tailwindcss/input.css -o wwwroot/css/tailwindcss.css --watch`

### Configuración IMAP
Para el envío de correos electronicos configurar MailSettings.

### Uso de variables de entorno
Al momento de cargar variables de entorno del sistema para reemplazar los valores de appsettings.json se admiten los
siguientes prefijos `DOTNET_` o `MYMBA_`
