namespace FacturacionInternet;

public class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Cedula { get; set; } = "";
    public string Telefono { get; set; } = "";
    public string Direccion { get; set; } = "";
    public string Correo { get; set; } = "";
    public bool Activo { get; set; } = true;
    public PlanInternet? Plan { get; set; }
}

public class ClienteService
{
    private List<Cliente> clientes = new();

    public void Registrar(Cliente cliente)
    {
        if (string.IsNullOrWhiteSpace(cliente.Nombre) || string.IsNullOrWhiteSpace(cliente.Cedula))
            throw new ArgumentException("Nombre y cédula son obligatorios");

        if (clientes.Any(c => c.Cedula == cliente.Cedula))
            throw new InvalidOperationException("Ya existe un cliente con esa cédula");

        clientes.Add(cliente);
    }

    public Cliente? BuscarPorCedula(string cedula)
    {
        return clientes.FirstOrDefault(c => c.Cedula == cedula);
    }

    public List<Cliente> BuscarPorNombre(string nombre)
    {
        return clientes
            .Where(c => c.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public void Desactivar(string cedula)
    {
        var cliente = BuscarPorCedula(cedula);
        if (cliente != null)
            cliente.Activo = false;
    }

    public void AsignarPlan(string cedula, PlanInternet plan)
    {
        var cliente = BuscarPorCedula(cedula);
        if (cliente == null)
            throw new InvalidOperationException("Cliente no encontrado");
        if (!plan.Activo)
            throw new InvalidOperationException("El plan está desactivado");

        cliente.Plan = plan;
    }

    public List<Cliente> ListarActivos()
    {
        return clientes.Where(c => c.Activo).ToList();
    }
}
