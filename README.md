# PLANAZO - Sistema de Reservas de Eventos

Aplicación de consola en C# y .NET para gestionar los eventos de PLANAZO: consultar eventos, reservar entradas, controlar cupos y cancelar reservas. Desarrollada aplicando Programación Orientada a Objetos.

## Funcionalidades

- Ver eventos disponibles con su precio final y cupos.
- Consultar un evento por su id.
- Reservar entradas (valida cantidad y cupos).
- Ver reservas realizadas.
- Cancelar una reserva (devuelve los cupos, no permite cancelar dos veces).
- Ver resumen: reservas activas, entradas reservadas y valor total.

## Cómo ejecutarlo

```
git clone https://github.com/IngCharles10/PLANAZO-POO-CSharp.git
cd PLANAZO-POO-CSharp
dotnet run
```

## Estructura del proyecto

```
PLANAZO-POO-CSharp/
├── Program.cs
├── Modelos/
│   ├── Evento.cs
│   ├── Cine.cs
│   ├── Concierto.cs
│   ├── Taller.cs
│   └── Reserva.cs
├── Servicios/
│   └── GestorReservas.cs
├── Capturas/
└── README.md
```

## Clases y cómo se relacionan

- **Evento** (abstracta): contiene los datos comunes (id, nombre, fecha, precio base, capacidad y cupos) y declara el método abstracto `CalcularPrecioFinal()`.
- **Cine, Concierto, Taller**: heredan de `Evento` e implementan `CalcularPrecioFinal()` a su manera (precio base, +15 % y -10 % respectivamente).
- **Reserva**: asocia una persona con un `Evento`. Guarda la cantidad de entradas, el valor total y su estado (`Activa` o `Cancelada`).
- **GestorReservas**: administra dos listas (`List<Evento>` y `List<Reserva>`), crea y cancela reservas y calcula el resumen. Es quien aplica las validaciones.
- **Program**: solo contiene el menú y la interacción con el usuario; la lógica está en las otras clases.

Relaciones: `Reserva` tiene un `Evento` (asociación) y `GestorReservas` contiene colecciones de ambos (composición).

## Conceptos de POO aplicados

- **Encapsulamiento**: las propiedades usan `protected set` en `Evento` y `private set` en `Reserva`. Los cupos solo cambian mediante `ReducirCupos()` y `DevolverCupos()`, y el estado de la reserva solo mediante `Cancelar()`.
- **Abstracción**: `Evento` es abstracta; no se puede crear un evento "genérico", solo representa lo que tienen en común todos los tipos.
- **Herencia**: `Cine`, `Concierto` y `Taller` heredan atributos y métodos de `Evento` con `: Evento`.
- **Polimorfismo**: `CalcularPrecioFinal()` se llama igual para cualquier evento, pero cada clase devuelve un resultado distinto. `Reserva` y `ToString()` usan el método sin saber qué tipo de evento es.
- **Excepciones**: `ArgumentException` e `InvalidOperationException` en las validaciones, capturadas con `try/catch` en `Program.cs` para que el programa no se cierre.

## Casos de prueba

**1. Reserva válida**
![Reserva válida](Capturas/01-reserva-valida.png)

**2. Más entradas que los cupos disponibles**
![Cupos insuficientes](Capturas/02-cupos-insuficientes.png)

**3. Cantidad negativa**
![Cantidad negativa](Capturas/03-cantidad-negativa.png)

**4. Cancelar una reserva devuelve los cupos**
![Cancelar devuelve cupos](Capturas/04-cancelar-devuelve-cupos.png)

**5. Cancelar dos veces la misma reserva**
![Cancelar dos veces](Capturas/05-cancelar-dos-veces.png)

**6. Consultar un evento inexistente**
![Evento inexistente](Capturas/06-evento-inexistente.png)

**7. Precio final de cada tipo de evento**
![Precios finales](Capturas/07-precios-finales.png)

## Pregunta de sustentación

**¿Por qué utilizaste una clase abstracta `Evento` en lugar de crear clases completamente independientes para Cine, Concierto y Taller? ¿Qué ventajas tiene esta decisión cuando necesitamos agregar un nuevo tipo de evento?**

_(Escribe aquí tu respuesta con tus palabras. Ideas para apoyarte:)_

- Los tres tipos comparten los mismos datos y operaciones; con una clase abstracta se escriben una sola vez en `Evento` en lugar de repetirlos tres veces.
- `GestorReservas` guarda todo en una sola `List<Evento>` y `Reserva` recibe un `Evento`, sin importar si es cine, concierto o taller. Con clases independientes habría que tener tres listas y tres versiones de cada método.
- El método abstracto `CalcularPrecioFinal()` obliga a cada tipo a definir su precio; si se olvida, no compila.
- Para agregar un tipo nuevo (por ejemplo `Festival`) solo se crea una clase que herede de `Evento` e implemente `CalcularPrecioFinal()`. No hay que modificar `Reserva`, `GestorReservas` ni `Program`.