using Planazo.Modelos;
using Planazo.Servicios;

var gestor = new GestorReservas();
bool salir = false;

while (!salir)
{
    MostrarMenu();
    string? opcion = Console.ReadLine();

    try
    {
        switch (opcion)
        {
            case "1":
                VerEventos();
                break;
            case "2":
                ConsultarEvento();
                break;
            case "3":
                ReservarEntradas();
                break;
            case "4":
                VerReservas();
                break;
            case "5":
                CancelarReserva();
                break;
            case "6":
                VerResumen();
                break;
            case "0":
                salir = true;
                break;
            default:
                Console.WriteLine("Opción inválida.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    if (!salir)
    {
        Console.WriteLine("\nPresiona una tecla para continuar...");
        Console.ReadKey();
    }
}

void MostrarMenu()
{
    Console.Clear();
    Console.WriteLine("================================");
    Console.WriteLine("        PLANAZO - EVENTOS");
    Console.WriteLine("================================\n");
    Console.WriteLine("1. Ver eventos disponibles");
    Console.WriteLine("2. Consultar evento");
    Console.WriteLine("3. Reservar entradas");
    Console.WriteLine("4. Ver reservas realizadas");
    Console.WriteLine("5. Cancelar una reserva");
    Console.WriteLine("6. Ver resumen de reservas");
    Console.WriteLine("0. Salir\n");
    Console.Write("Seleccione una opción: ");
}

void VerEventos()
{
    Console.WriteLine("\n--- Eventos disponibles ---");
    foreach (var evento in gestor.ObtenerEventos())
        Console.WriteLine(evento);
}

void ConsultarEvento()
{
    Console.Write("\nIngrese el id del evento: ");
    int id = LeerEntero();
    var evento = gestor.BuscarEvento(id);
    Console.WriteLine(evento is null ? "No se encontró el evento." : evento.ToString());
}

void ReservarEntradas()
{
    Console.Write("\nNombre de la persona: ");
    string nombre = Console.ReadLine() ?? "";

    Console.Write("Id del evento: ");
    int idEvento = LeerEntero();

    Console.Write("Cantidad de entradas: ");
    int cantidad = LeerEntero();

    var reserva = gestor.CrearReserva(nombre, idEvento, cantidad);
    Console.WriteLine($"Reserva creada con éxito: {reserva}");
}

void VerReservas()
{
    Console.WriteLine("\n--- Reservas realizadas ---");
    foreach (var reserva in gestor.ObtenerReservas())
        Console.WriteLine(reserva);
}

void CancelarReserva()
{
    Console.Write("\nId de la reserva a cancelar: ");
    int id = LeerEntero();
    gestor.CancelarReserva(id);
    Console.WriteLine("Reserva cancelada correctamente.");
}

void VerResumen()
{
    var (cantidad, entradas, valor) = gestor.ObtenerResumen();
    Console.WriteLine("\n--- Resumen de reservas activas ---");
    Console.WriteLine($"Reservas activas: {cantidad}");
    Console.WriteLine($"Entradas reservadas: {entradas}");
    Console.WriteLine($"Valor total: {valor:C}");
}

int LeerEntero()
{
    int valor;
    while (!int.TryParse(Console.ReadLine(), out valor))
        Console.Write("Valor inválido, intente de nuevo: ");
    return valor;
}