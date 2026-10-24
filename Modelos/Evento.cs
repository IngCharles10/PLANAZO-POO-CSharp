namespace Planazo.Modelos;

public abstract class Evento
{
    public int Id { get; protected set; }
    public string Nombre { get; protected set; }
    public DateTime Fecha { get; protected set; }
    public decimal PrecioBase { get; protected set; }
    public int CapacidadMaxima { get; protected set; }
    public int CuposDisponibles { get; protected set; }

    protected Evento(int id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
    {
        Id = id;
        Nombre = nombre;
        Fecha = fecha;
        PrecioBase = precioBase;
        CapacidadMaxima = capacidadMaxima;
        CuposDisponibles = capacidadMaxima;
    }

    public abstract decimal CalcularPrecioFinal();

    public bool TieneCuposDisponibles(int cantidad)
    {
        return cantidad > 0 && cantidad <= CuposDisponibles;
    }

    public void ReducirCupos(int cantidad)
    {
        CuposDisponibles -= cantidad;
    }

    public void DevolverCupos(int cantidad)
    {
        CuposDisponibles += cantidad;
        if (CuposDisponibles > CapacidadMaxima)
            CuposDisponibles = CapacidadMaxima;
    }

    public override string ToString()
    {
        return $"[{Id}] {Nombre} - {Fecha:dd/MM/yyyy} - Precio: {CalcularPrecioFinal():C} - Cupos: {CuposDisponibles}/{CapacidadMaxima}";
    }
}