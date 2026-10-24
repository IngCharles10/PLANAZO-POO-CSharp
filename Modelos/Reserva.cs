namespace Planazo.Modelos;

public enum EstadoReserva
{
    Activa,
    Cancelada
}

public class Reserva
{
    public int Id { get; private set; }
    public string NombrePersona { get; private set; }
    public Evento EventoReservado { get; private set; }
    public int CantidadEntradas { get; private set; }
    public decimal ValorTotal { get; private set; }
    public EstadoReserva Estado { get; private set; }

    public Reserva(int id, string nombrePersona, Evento eventoReservado, int cantidadEntradas)
    {
        Id = id;
        NombrePersona = nombrePersona;
        EventoReservado = eventoReservado;
        CantidadEntradas = cantidadEntradas;
        ValorTotal = eventoReservado.CalcularPrecioFinal() * cantidadEntradas;
        Estado = EstadoReserva.Activa;
    }

    public void Cancelar()
    {
        if (Estado == EstadoReserva.Cancelada)
            throw new InvalidOperationException($"La reserva {Id} ya fue cancelada anteriormente.");

        Estado = EstadoReserva.Cancelada;
    }

    public override string ToString()
    {
        return $"[{Id}] {NombrePersona} - {EventoReservado.Nombre} - {CantidadEntradas} entradas - Total: {ValorTotal:C} - Estado: {Estado}";
    }
}