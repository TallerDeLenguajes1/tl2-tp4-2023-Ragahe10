namespace tl2_tp4_2023_Ragahe10;
using System.Text.Json;
public static class AccesoADatosCPedidos{
    public static List<Pedido> Obtener(){
        List<Pedido> pedidos = null;
        if (File.Exists("Pedidos.json")){
            string json = File.ReadAllText("Pedidos.json");
            pedidos = JsonSerializer.Deserialize<List<Pedido>>(json);
        } else {
            Console.WriteLine("No existe el json");
        }
        return pedidos;
    }
    public static void Guardar(List<Pedido> Pedidos){
        var json = JsonSerializer.Serialize(Pedidos);
        File.WriteAllText("Pedidos.json",json);
    }
}