# Sistema de Ventas - API RESTful

## 📋 Descripción

Sistema completo de gestión de ventas con API RESTful desarrollado en **.NET 8** con **Entity Framework Core** y **SQL Server LocalDB**. 

## 🚀 Características Implementadas

### ✅ Entidades del Sistema (5 entidades relacionadas)
- **Usuario**: Gestión de clientes
- **Categoría**: Clasificación de productos  
- **Producto**: Inventario de productos
- **Venta**: Registro de transacciones
- **DetalleVenta**: Detalles específicos de cada venta

### ✅ Operaciones CRUD Completas
- **GET**: Obtener todos los registros y por ID específico
- **POST**: Crear nuevos registros con validaciones
- **PUT**: Actualizar registros existentes
- **DELETE**: Eliminar registros con validaciones de integridad

### ✅ Funcionalidades Adicionales
- **Manejo de Stock**: Actualización automática en ventas
- **Transacciones**: Operaciones de venta atómicas
- **Reportes**: Productos más vendidos y ventas por fecha
- **Validaciones**: Control de errores y validaciones de negocio
- **Documentación Swagger**: API completamente documentada

## 🏗️ Arquitectura

```
SistemaDeVentas/
├── Controllers/          # Controladores API
│   ├── UsuariosController.cs
│   ├── CategoriasController.cs  
│   ├── ProductosController.cs
│   ├── VentasController.cs
│   └── DetallesVentaController.cs
├── Models/              # Entidades del dominio
│   ├── Usuario.cs
│   ├── Categoria.cs
│   ├── Producto.cs
│   ├── Venta.cs
│   └── DetalleVenta.cs
├── Data/               # Contexto de base de datos
│   └── SistemaVentasContext.cs
└── Program.cs          # Configuración de la aplicación
```

## 🗄️ Base de Datos

### Modelo de Datos
- **Usuarios** (Id, Nombre, Email, Teléfono, Dirección, Estado, FechaCreación)
- **Categorías** (Id, Nombre, Descripción, Estado, FechaCreación)
- **Productos** (Id, Nombre, Descripción, Precio, Stock, Estado, CategoriaId, FechaCreación)
- **Ventas** (Id, NumeroVenta, FechaVenta, Total, Estado, UsuarioId, Observaciones)
- **DetallesVenta** (Id, Cantidad, PrecioUnitario, Subtotal, VentaId, ProductoId)

### Relaciones
- `Categoria` → `Productos` (1:N)
- `Usuario` → `Ventas` (1:N)
- `Venta` → `DetallesVenta` (1:N)
- `Producto` → `DetallesVenta` (1:N)

## 🛠️ Tecnologías Utilizadas

- **.NET 8.0**
- **ASP.NET Core Web API**
- **Entity Framework Core 8.0**
- **SQL Server LocalDB**
- **Swagger/OpenAPI**
- **FluentValidation**

## 📊 Endpoints de la API

### 🧑 Usuarios
- `GET /api/Usuarios` - Obtener todos los usuarios
- `GET /api/Usuarios/{id}` - Obtener usuario por ID
- `POST /api/Usuarios` - Crear nuevo usuario
- `PUT /api/Usuarios/{id}` - Actualizar usuario
- `DELETE /api/Usuarios/{id}` - Eliminar usuario

### 🏷️ Categorías
- `GET /api/Categorias` - Obtener todas las categorías
- `GET /api/Categorias/{id}` - Obtener categoría por ID
- `POST /api/Categorias` - Crear nueva categoría
- `PUT /api/Categorias/{id}` - Actualizar categoría
- `DELETE /api/Categorias/{id}` - Eliminar categoría

### 📦 Productos
- `GET /api/Productos` - Obtener todos los productos
- `GET /api/Productos/{id}` - Obtener producto por ID
- `GET /api/Productos/categoria/{categoriaId}` - Productos por categoría
- `POST /api/Productos` - Crear nuevo producto
- `PUT /api/Productos/{id}` - Actualizar producto
- `DELETE /api/Productos/{id}` - Eliminar producto

### 🛒 Ventas
- `GET /api/Ventas` - Obtener todas las ventas
- `GET /api/Ventas/{id}` - Obtener venta por ID
- `GET /api/Ventas/usuario/{usuarioId}` - Ventas por usuario
- `POST /api/Ventas` - Crear nueva venta (con detalles)
- `PUT /api/Ventas/{id}/estado` - Actualizar estado de venta
- `DELETE /api/Ventas/{id}` - Eliminar venta

### 📋 Detalles de Venta
- `GET /api/DetallesVenta` - Obtener todos los detalles
- `GET /api/DetallesVenta/{id}` - Obtener detalle por ID
- `GET /api/DetallesVenta/venta/{ventaId}` - Detalles por venta
- `GET /api/DetallesVenta/producto/{productoId}` - Detalles por producto
- `GET /api/DetallesVenta/reportes/productos-mas-vendidos` - Reporte productos
- `GET /api/DetallesVenta/reportes/ventas-por-fecha` - Reporte por fechas

## 🧪 Cómo Probar la API

### 1. Ejecutar el Proyecto
```bash
dotnet run
```

### 2. Acceder a Swagger
Abre tu navegador en: `https://localhost:5001` o `http://localhost:5000`

### 3. Ejemplos de Uso

#### Crear una Categoría:
```json
POST /api/Categorias
{
  "nombre": "Electrónicos",
  "descripcion": "Dispositivos electrónicos y gadgets",
  "estado": true
}
```

#### Crear un Usuario:
```json
POST /api/Usuarios
{
  "nombre": "Juan Pérez",
  "email": "juan@example.com",
  "telefono": "123456789",
  "direccion": "Calle 123 #45-67",
  "estado": true
}
```

#### Crear un Producto:
```json
POST /api/Productos
{
  "nombre": "Smartphone",
  "descripcion": "Teléfono inteligente última generación",
  "precio": 699.99,
  "stock": 25,
  "categoriaId": 1,
  "estado": true
}
```

#### Crear una Venta:
```json
POST /api/Ventas
{
  "usuarioId": 1,
  "observaciones": "Venta realizada en efectivo",
  "detallesVenta": [
    {
      "productoId": 1,
      "cantidad": 2
    },
    {
      "productoId": 2,
      "cantidad": 1
    }
  ]
}
```

## 📈 Características Avanzadas

### Control de Stock
- Actualización automática de inventario al crear ventas
- Validación de stock disponible antes de confirmar venta
- Restauración de stock al eliminar ventas

### Transacciones
- Operaciones de venta completamente atómicas
- Rollback automático en caso de errores
- Integridad de datos garantizada

### Validaciones de Negocio
- Emails únicos para usuarios
- Nombres únicos para categorías y productos
- Validación de stock suficiente
- Control de integridad referencial

### Reportes
- Productos más vendidos con estadísticas
- Ventas por rango de fechas
- Métricas de desempeño

## 🔧 Configuración

### Cadena de Conexión
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SistemaVentasDB;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true"
  }
}
```

### CORS
Configurado para permitir cualquier origen durante desarrollo.

## 📝 Notas Importantes

1. **Base de Datos**: Se crea automáticamente al ejecutar la aplicación
2. **Datos Semilla**: Incluye categorías, usuarios y productos de ejemplo
3. **Swagger**: Página principal configurada en la ruta raíz
4. **Validaciones**: Implementadas tanto a nivel de modelo como de negocio
5. **Manejo de Errores**: Respuestas estructuradas con mensajes descriptivos

## 🎯 Sistema Completo

Este sistema cumple con todos los requisitos solicitados:

✅ **5 entidades relacionadas**: Usuario, Producto, Categoría, Venta, DetalleVenta  
✅ **Operaciones CRUD completas**: GET, POST, PUT, DELETE para cada entidad  
✅ **Tecnología .NET**: Implementado en .NET 8.0  
✅ **Base de datos**: SQL Server LocalDB configurada  
✅ **Documentación**: Swagger completamente configurado  
✅ **Validaciones y control de errores**: Implementado en toda la API  

¡El sistema está listo para usar y puede ser expandido según las necesidades del negocio! 