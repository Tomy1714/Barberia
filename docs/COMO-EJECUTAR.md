# Cómo ejecutar — Barbería El Navaja (Front ASP.NET Razor Pages)

Guía para levantar el sistema en local. El front es el proyecto **`Asp_Presentaciones`**
y consume la API REST **`Asp_servicios`**, que a su vez usa la base de datos SQL Server.

## 1. Requisitos

| Componente | Versión | Notas |
|---|---|---|
| .NET SDK | **10.0** | El `.csproj` apunta a `net10.0`. Verifica con `dotnet --version`. |
| SQL Server | 2019+ / Express | Base de datos `Barberia`. |
| Navegador | Cualquiera moderno | Chrome, Edge, Firefox. |

> Cadena de conexión usada por el back:
> `server=localhost;database=Barberia;Integrated Security=True;TrustServerCertificate=true;`
> Ajústala en el proyecto de servicios si tu instancia es distinta (p. ej. `server=localhost\\SQLEXPRESS`).

## 2. Puertos

| Proyecto | URL | Puerto |
|---|---|---|
| API (`Asp_servicios`) | `https://localhost:7179` | **7179** |
| Front (`Asp_Presentaciones`) | `https://localhost:7072` | **7072** |

El front ya apunta a la API en `https://localhost:7179` (ver clases `*Presentacion` en `Lib_Presentaciones`).
Si cambias el puerto de la API, actualiza esas URLs.

## 3. Pasos

Abre **dos terminales** en la raíz de la solución (`Barberia/`).

**Terminal 1 — API:**
```bash
cd Asp_servicios
dotnet run
```
Espera a ver `Now listening on: https://localhost:7179`.

**Terminal 2 — Front:**
```bash
cd Asp_Presentaciones
dotnet run
```
Abre el navegador en **https://localhost:7072**.

> Alternativa: abrir la solución en Visual Studio y configurar
> *Multiple startup projects* → `Asp_servicios` + `Asp_Presentaciones` (ambos en *Start*).

## 4. Inicio de sesión y roles

La pantalla de login valida las credenciales contra la tabla `Usuarios` (vía la API) y
guarda el **rol** en la sesión. El menú y los permisos se adaptan al rol:

| Rol | Puede |
|---|---|
| **Administrador** | Todos los CRUD + pestañas de Auditoría de cada entidad. |
| **Recepcionista** | Citas y Clientes (alta/edición/baja); ver Barberos. |
| **Barbero** | Ver su agenda de Citas. |
| **Cliente** | Agendar sus propias citas (elige sede/barbero/servicio/horario). |

Si no hay sesión, cualquier ruta redirige a `/Account/Login`.
Si el rol no tiene permiso, redirige a `/AccesoDenegado`.

## 5. Qué se construyó en el front

- **Infraestructura:** `Program.cs` (sesión), `Infraestructura/Sesion.cs`, `PaginaBase.cs`
  (control de acceso por rol), `FilaAuditoria.cs`.
- **Diseño:** tema oscuro carbón + oro en `wwwroot/css/barberia.css`,
  interacciones de modales en `wwwroot/js/barberia.js`, layout en `Pages/Shared/_Layout.cshtml`.
- **Login / Home:** `Pages/Account/Login`, `Logout`, `AccesoDenegado`, `Index` (panel por rol con KPIs).
- **Ventanas CRUD** (`Pages/Ventanas/`), cada una con tabla, modal de alta/edición,
  confirmación de borrado y **pestaña de Auditoría** (solo Administrador):
  Servicios, Sedes, Personas, Empleados, Clientes, Barberos, Recepcionistas,
  Administradores, Usuarios y **Citas** (entidad central, accesible a todos los roles).
- **Auditoría:** partial reutilizable `Pages/Shared/_AuditoriaTab.cshtml`; la API expone
  `GET /api/{Entidad}/auditoria` (endpoints añadidos a los 27 controladores).

> El patrón quedó definido y replicado sobre el núcleo de entidades. Las 27 restantes se
> generan calcando la misma estructura (PageModel + vista + modal), reutilizando `PaginaBase`
> y `_AuditoriaTab`.

## 6. Muestra visual sin ejecutar

Si aún no tienes .NET 10 instalado, abre directamente en el navegador:

```
docs/muestra/muestra.html
```

Es una maqueta estática (sin backend) que reproduce el aspecto real del sistema:
login, navbar + panel, ventana CRUD con pestañas y la ventana emergente.
