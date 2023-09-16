namespace tl2_tp4_2023_Ragahe10;
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
    public Informe GetInforme(){
        return new Informe(Cadetes,Pedidos);
    }
    public Pedido TomarPedido(string nombre, string direccion, int telefono, string datosRef,  string observacion) {
        var cliente = new Cliente(nombre, direccion, telefono,datosRef);
        var pedido = new Pedido(NumPed,observacion,cliente);
        NumPed++;
        return pedido;
    }
    public void AsignarPedido(int id, Pedido ped){
        ped.IdCadete = id;
    }
    public void MoverPedido(int numeroPed, int id) {
        var pedido = Pedidos.FirstOrDefault(p=>p.Numero == numeroPed);
        if(pedido != null){
            pedido.IdCadete = id;
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
            monto = monto +cad.JornalACobrar(Pedidos);
        }
        return monto;
    }
    public float JornalACobrar(int idCadete) {
        var cad = Cadetes.FirstOrDefault(c=>c.Id == idCadete);
        if(cad !=null){
            return cad.JornalACobrar(Pedidos);
        }
        return 0;
    }
}
public class Cadete {
    private int id;
    private string nombre;
    private string direccion;
    private int telefono;

    // PROPIEDADES
    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public int Telefono { get => telefono; set => telefono = value; }

    public Cadete(int id, string nombre, string direccion, int telefono)
    {
        Id = id;
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
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
    public float JornalACobrar(List<Pedido> lp) {
        return lp.Count(p => p.IdCadete == Id && p.Estado == Estado.Entregado)*500;
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
public class Informe{
    private List<CadeteInforme> cadetesInformes;
    private int pedPromedioCad;
    private float montoTotal;
    public Informe(List<Cadete> lc, List<Pedido> lp){
        cadetesInformes = new List<CadeteInforme>();
        int total = 0;
        float monto = 0;
        foreach (var c in lc)
        {
            cadetesInformes.Add(new CadeteInforme(c, lp));
            total += c.CantidadPedidos(lp,0);
            monto += c.JornalACobrar(lp);
        }
        pedPromedioCad = total / cadetesInformes.Count();
        montoTotal = monto;
    }

    public List<CadeteInforme> CadetesInformes { get => cadetesInformes;}
    public int PedPromedioCad { get => pedPromedioCad;}
    public float MontoTotal { get => montoTotal;}
}
public class CadeteInforme{
    private string nombre;
    private int pedidosEntregados;
    private int pedidosSinEntregar;
    private int pedidosCancelados;
    private float sueldo;

    public CadeteInforme(Cadete cadete, List<Pedido> lp){
        nombre = cadete.Nombre;
        pedidosEntregados = cadete.CantidadPedidos(lp,1);
        pedidosSinEntregar = cadete.CantidadPedidos(lp,2);
        pedidosCancelados = cadete.CantidadPedidos(lp,3);
        sueldo = cadete.JornalACobrar(lp);
    }

    public string Nombre { get => nombre;}
    public int PedidosEntregados { get => pedidosEntregados;}
    public int PedidosSinEntregar { get => pedidosSinEntregar;}
    public int PedidosCancelados { get => pedidosCancelados;}
    public float Sueldo { get => sueldo;}
    public int CantidadTotalDePedidos(){
        return pedidosEntregados + pedidosSinEntregar + pedidosCancelados;
    }
}
