namespace tl2_tp4_2023_Ragahe10;
using System.Text.Json;
public class AccesoADatosCadetes{
    public List<Cadete> Obtener(){
        List<Cadete> cadetes = null;
        if (File.Exists("Cadetes.json")){
            string json = File.ReadAllText("Cadetes.json");
            cadetes = JsonSerializer.Deserialize<List<Cadete>>(json);
        } else {
            Console.WriteLine("No existe el json");
        }
        return cadetes;
    }
    public void Guardar(List<Cadete> Cadetes){
        var json = JsonSerializer.Serialize(Cadetes);
        File.WriteAllText("Cadetes.json",json);
    }
}