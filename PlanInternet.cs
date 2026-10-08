namespace FacturacionInternet;

public class PlanInternet
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public int VelocidadMbps { get; set; }
    public decimal PrecioMensual { get; set; }
    public bool Activo { get; set; } = true;

    public PlanInternet(int id, string nombre, int velocidadMbps, decimal precioMensual)
    {
        if (precioMensual <= 0)
            throw new ArgumentException("El precio debe ser mayor que 0");

        Id = id;
        Nombre = nombre;
        VelocidadMbps = velocidadMbps;
        PrecioMensual = precioMensual;
    }
}
