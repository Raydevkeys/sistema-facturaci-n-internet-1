namespace FacturacionInternet;

public class Pago
{
    public int Id { get; set; }
    public int FacturaId { get; set; }
    public decimal Monto { get; set; }
    public DateTime Fecha { get; set; }
    public string Metodo { get; set; } = "Efectivo";
}

public class PagoService
{
    private List<Pago> pagos = new();
    private int siguienteId = 1;

    public void RegistrarPago(Factura factura, decimal monto, string metodo)
    {
        if (monto <= 0)
            throw new ArgumentException("El monto debe ser mayor que 0");

        if (monto > factura.BalancePendiente)
            throw new InvalidOperationException("El pago es mayor que el balance pendiente");

        pagos.Add(new Pago
        {
            Id = siguienteId++,
            FacturaId = factura.Id,
            Monto = monto,
            Fecha = DateTime.Today,
            Metodo = metodo
        });

        factura.TotalPagado += monto;
        factura.ActualizarEstado();
    }

    public List<Pago> HistorialPorFactura(int facturaId)
    {
        return pagos.Where(p => p.FacturaId == facturaId).ToList();
    }

    public decimal BalanceDelCliente(List<Factura> facturasDelCliente)
    {
        return facturasDelCliente.Sum(f => f.BalancePendiente);
    }
}
