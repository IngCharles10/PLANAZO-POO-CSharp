using Planazo.Modelos;

namespace Planazo.Servicios;

public class GestorReservas
{
    private readonly List<Evento> _eventos = new();
    private readonly List<Reserva> _reservas = new();
    private int _siguienteIdReserva = 1;

    public GestorReservas()
    {
        CargarEventosIniciales();
    }

    private void CargarEventosIniciales()
    {
        _eventos.Add(new Cine(1, "Película A", DateTime.Now.AddDays(3), 20000m, 50));
        _eventos.Add(new Cine(2, "Película B", DateTime.Now.AddDays(5), 18000m, 40));
        _eventos.Add(new Concierto(3, "Concierto A", DateTime.Now.AddDays(10), 50000m, 100));
        _eventos.Add(new Concierto(4, "Concierto B", DateTime.Now.AddDays(15), 70000m, 80));
        _eventos.Add(new Taller(5, "Taller A", DateTime.Now.AddDays(7), 30000m, 20));
        _eventos.Add(new Taller(6, "Taller B", DateTime.Now.AddDays(12), 25000m, 15));
    }

    public List<Evento> ObtenerEventos() => _eventos;

    public Evento? BuscarEvento(int id)
    {
        return _eventos.FirstOrDefault(e => e.Id == id);
    }

    public Reserva CrearReserva(string nombrePersona, int idEvento, int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentException("La cantidad de entradas debe ser mayor a cero.");

        var evento = BuscarEvento(idEvento)
            ?? throw new ArgumentException($"No existe un evento con id {idEvento}.");

        if (!evento.TieneCuposDisponibles(cantidad))
            throw new InvalidOperationException($"No hay suficientes cupos. Disponibles: {evento.CuposDisponibles}.");

        evento.ReducirCupos(cantidad);

        var reserva = new Reserva(_siguienteIdReserva++, nombrePersona, evento, cantidad);
        _reservas.Add(reserva);
        return reserva;
    }

    public void CancelarReserva(int idReserva)
    {
        var reserva = _reservas.FirstOrDefault(r => r.Id == idReserva)
            ?? throw new ArgumentException($"No existe una reserva con id {idReserva}.");

        reserva.Cancelar(); // lanza InvalidOperationException si ya estaba cancelada
        reserva.EventoReservado.DevolverCupos(reserva.CantidadEntradas);
    }

    public List<Reserva> ObtenerReservas() => _reservas;

    public (int cantidadActivas, int entradasActivas, decimal valorActivas) ObtenerResumen()
    {
        var activas = _reservas.Where(r => r.Estado == EstadoReserva.Activa).ToList();
        int cantidad = activas.Count;
        int entradas = activas.Sum(r => r.CantidadEntradas);
        decimal valor = activas.Sum(r => r.ValorTotal);
        return (cantidad, entradas, valor);
    }
}