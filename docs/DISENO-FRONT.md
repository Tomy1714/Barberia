# Diseño del Front — Barbería (Asp_Presentaciones)

Fecha: 2026-05-29

## Contexto
Solución .NET 10 en 4 capas:
- `Lib_Negocio` — 27 entidades + 27 auditorías + lógica (BD). No se toca.
- `Asp_servicios` — API REST por entidad + `LoginController` (`https://localhost:7179`).
- `Lib_Presentaciones` — consume la API por HTTP (`XxxPresentacion`: Consultar/ConsultarAuditoria/Guardar/Modificar/Eliminar, todos con parámetro `rol`).
- `Asp_Presentaciones` — front Razor Pages (`https://localhost:7072`). **Aquí se trabaja.**

## Alcance
- **Patrón CRUD sólido y reutilizable** + subconjunto núcleo construido a fondo:
  Login, Home por rol, y Ventanas de: Personas, Empleados, Barberos, Recepcionistas,
  Administradores, Clientes, Usuarios, Servicios, Sedes, Citas.
- El resto de entidades es replicable copiando el patrón.

## Decisiones
- **Auditorías:** se agregó a cada controller de `Asp_servicios` el endpoint
  `GET /api/{Entidad}/auditoria` que expone `negocio.ListarAuditoria()` (ya existente en negocio).
  Es el único cambio en el back; necesario porque la capa consumidora ya lo invoca.
- **Autenticación:** Session de ASP.NET (`IdUsuario`, `IdPersona`, `Rol`, `Email`).
  `Login` usa `LoginPresentacion` → `/api/Login`.
- **Roles:** menú y acceso por rol.
  - Administrador: CRUD total + auditorías.
  - Recepcionista: Citas y Clientes; ve Barberos/Servicios/Sedes.
  - Barbero: su agenda de Citas (cambia estado); consulta.
  - Cliente: crea/ve sus Citas (sede, servicio, barbero, horario).
- **Herencia** (Personas → Empleados → Barberos/Recepcionistas/Administradores; Personas → Clientes):
  son relaciones por FK; se representan con selects y agrupación en el menú.

## Estructura de páginas
- `Pages/Account/Login`, logout.
- `Pages/Index` — dashboard por rol.
- `Pages/Ventanas/{Entidad}` — pestañas "Registros" (tabla + modal CRUD) y "Auditoría" (solo lectura).
- `Pages/Shared/_Layout` — branding barbería, nav por rol, estado de sesión.
- Base: `PaginaBase` (sesión/rol), `Sesion` (helper), `barberia.css`.

## Entrega
- Configurado para localhost con los puertos actuales (front 7072 / API 7179), ambos a la vez.
- `COMO-EJECUTAR.md` con pasos.
- No se ejecuta en esta máquina (requiere .NET 10 SDK + SQL Server con BD `Barberia`);
  se incluye muestra visual (mockups + capturas).
