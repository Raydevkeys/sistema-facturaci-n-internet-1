namespace FacturacionInternet;

public class Factura
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public decimal Monto { get; set; }
    public DateTime FechaEmision { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public string Estado { get; set; } = "Pendiente";
    public decimal TotalPagado { get; set; } = 0;

    public decimal BalancePendiente => Monto - TotalPagado;

    public void ActualizarEstado()
    {
        if (BalancePendiente <= 0)
            Estado = "Pagada";
        else if (DateTime.Today > FechaVencimiento)
            Estado = "Vencida";
        else
            Estado = "Pendiente";
    }
}

public class FacturaService
{
    private List<Factura> facturas = new();
    private int siguienteId = 1;

    public void GenerarFacturasDelMes(List<Cliente> clientes)
    {
        var hoy = DateTime.Today;

        foreach (var cliente in clientes.Where(c => c.Activo && c.Plan != null))
        {
            bool yaTieneFactura = facturas.Any(f =>
                f.ClienteId == cliente.Id &&
                f.FechaEmision.Month == hoy.Month &&
                f.FechaEmision.Year == hoy.Year);

            if (yaTieneFactura) continue;

            facturas.Add(new Factura
            {
                Id = siguienteId++,
                ClienteId = cliente.Id,
                Monto = cliente.Plan!.PrecioMensual,
                FechaEmision = hoy,
                FechaVencimiento = hoy.AddDays(15)
            });
        }
    }

    public List<Factura> ObtenerPorCliente(int clienteId)
    {
        return facturas.Where(f => f.ClienteId == clienteId).ToList();
    }

    public List<Factura> ObtenerPorEstado(string estado)
    {
        foreach (var f in facturas) f.ActualizarEstado();
        return facturas.Where(f => f.Estado == estado).ToList();
    }

    public Factura? BuscarPorId(int id)
    {
        return facturas.FirstOrDefault(f => f.Id == id);
    }
}
