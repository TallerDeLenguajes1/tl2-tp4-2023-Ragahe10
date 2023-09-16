namespace EspacioCadeteria;
using System.Linq;
public enum Estado {
    SinEntregar,
    Cancelado,
    Entregado
}

public class Cadeteria {
    private string nombre;
    private int telefono;
    private int numPed;
    private List<Cadete> cadetes;
    private List<Pedido> pedidos;
    // PROPIEDADES
    public string Nombre { get => nombre; set => nombre = value; }
    public int Telefono { get => telefono; set => telefono = value; }
    public int NumPed { get => numPed; set => numPed = value; }
    public List<Cadete> Cadetes { get => cadetes; set => cadetes = value; }
    public List<Pedido> Pedidos { get => pedidos; set => pedidos = value; }

    // CONSTRUCTORES
    public Cadeteria(string nombre, int telefono) {
        Nombre = nombre;
        Telefono = telefono;
        Cadetes = new List<Cadete>();
        Pedidos = new List<Pedido>();
        NumPed = 1;
    }

    // METODOS
    public Pedido TomarPedido(string nombre, string direccion, int telefono, string datosRef,  string observacion) {
        var cliente = new Cliente(nombre, direccion, telefono,datosRef);
        var pedido = new Pedido(NumPed,observacion,cliente);
        NumPed++;
        return pedido;
    }
    public void AsignarPedido(int id, Pedido ped){
        var cad = Cadetes.FirstOrDefault(c=>c.Id == id);
        cad.Pedidos.Add(ped.Numero);
    }
    public void MoverPedido(List<Pedido>listP, int numeroPed, int id) {
        Pedido pedido = null;
        foreach (var cad in Cadetes)
        {
            if(cad.Id != id){
                pedido = listP.FirstOrDefault(p=> p.Numero==cad.QuitarPedido(numeroPed));
            }
        }
        if(pedido != null){
            AsignarPedido(id, pedido);
        }
    }
    public float PedPromedioCad(){
        // int pedidos = 0;
        // foreach (var c in Cadetes)
        // {
        //     pedidos += c.CantidadPedidos(Pedidos,0); 
        // }
        // return pedidos/Cadetes.Count();
        return Pedidos.Count()/Cadetes.Count();
    }
    public float TotalaPagar(){
        float monto=0;
        foreach (var cad in Cadetes){
            monto = monto +JornalACobrar(cad.Id);
        }
        return monto;
    }
    public float JornalACobrar(int idCadete) {
        return Pedidos.Count(p => p.IdCadete == idCadete && p.Estado == Estado.Entregado)*500;
    }
}
public class Cadete {
    private int id;
    private string nombre;
    private string direccion;
    private int telefono;
    private List<int> pedidos;

    // PROPIEDADES
    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public int Telefono { get => telefono; set => telefono = value; }
    public List<int> Pedidos { get => pedidos; set => pedidos = value; }

    public Cadete(int id, string nombre, string direccion, int telefono)
    {
        Id = id;
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
        Pedidos = new List<int>();
    }
    public void TomarPedido(int p) {
        Pedidos.Add(p);
    }
    public int QuitarPedido(int numPed) {
        foreach (var p in Pedidos){
            if(p == numPed){
                Pedidos.Remove(p);
                return p;
            }
        }
        return -1;
    }
    public int CantidadPedidos(List<Pedido> listaP,int op){
        int cant=0;
        switch (op){
            case 1:
                cant = listaP.Count(p=> p.Estado == Estado.Entregado && p.IdCadete == Id);
                break;
            case 2:
                cant = listaP.Count(p=> p.Estado == Estado.SinEntregar && p.IdCadete == Id);
                break;
            case 3:
                cant = listaP.Count(p=> p.Estado == Estado.Cancelado && p.IdCadete == Id);
                break;
            default:
                cant = listaP.Count();
                break;
        }
        return cant;
    }
}

public class Pedido {
    private int numero;
    private string observacion;
    private Estado estado;
    private Cliente client;
    private int idCadete;


    public int Numero { get => numero; set => numero = value; }
    public string Observacion { get => observacion; set => observacion = value; }
    public Estado Estado { get => estado; set => estado = value; }
    public Cliente Client { get => client; set => client = value; }
    public int IdCadete { get => idCadete; set => idCadete = value; }

    public Pedido(int numero, string observacion, Cliente cliente){
        Numero = numero;
        Observacion = observacion;
        Estado = Estado.SinEntregar;
        Client = cliente;
        IdCadete = 0;
    }

    public void EntregarPedido() {
        Estado = Estado.Entregado;
    }
    public void CancelarPedido() {
        Estado = Estado.Cancelado;
    }
}

public class Cliente {
    private string nombre;
    private string direccion;
    private int telefono;
    private string datosRefDireccion;

    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public int Telefono { get => telefono; set => telefono = value; }
    public string DatosRefDireccion { get => datosRefDireccion; set => datosRefDireccion = value; }
    
    public Cliente (string nombre, string direccion, int telefono, string datosRefDireccion) {
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
        DatosRefDireccion = datosRefDireccion;
    }
}