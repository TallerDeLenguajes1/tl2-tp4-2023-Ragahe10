namespace tl2_tp4_2023_Ragahe10;
using System.Text.Json;
public static class AccesoADatosCadeteria{
    public static Cadeteria Obtener(){
        Cadeteria cadet = null;
        if (File.Exists("Cadeteria.json")){
            string json = File.ReadAllText("Cadeteria.json");
            cadet = JsonSerializer.Deserialize<Cadeteria>(json);
        } else {
            Console.WriteLine("No existe el json");
        }
        return cadet;
    }
}