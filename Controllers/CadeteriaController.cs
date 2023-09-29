using Microsoft.AspNetCore.Mvc;

namespace tl2_tp4_2023_Ragahe10.Controllers;

[ApiController]
[Route("[controller]")]
public class CadeteriaController : ControllerBase
{
    private Cadeteria cadeteria;
    private readonly ILogger<CadeteriaController> _logger;
    public CadeteriaController(ILogger<CadeteriaController> logger){
        _logger = logger;
        cadeteria = Cadeteria.Instance;
    }
    
    [HttpGet]
    public ActionResult<string> GetCadeteria(){
        return Ok(cadeteria);
    }
    [HttpGet]
    [Route("Pedidos")]
    public ActionResult<string> GetPedidos(){
        return Ok(cadeteria.GetPedidos());
    }
    [HttpGet]
    [Route("Cadetes")]
    public ActionResult<string> GetCadetes(){
        return Ok(cadeteria.GetCadetes());
    }
    [HttpGet]
    [Route("Informe")]
    public ActionResult<string> GetInforme(){
        return Ok(cadeteria.GetInforme());
    }
    [HttpPost ("AddPedido")]
    public ActionResult<Pedido> AddPedido(string nombre, string direccion, long telefono, string datosRef,  string observacion){
        var ped = cadeteria.TomarPedido(nombre,direccion,telefono, datosRef, observacion);
        if(ped!=null){
            return Accepted(ped);
        }
        return StatusCode(500,"no se tomó el pedido");
    }
    [HttpPut ("AsignarPedido")]
    public ActionResult<Pedido> AsignarPedido(int idCadete, int numPedido){
        var pedido = cadeteria.AsignarPedido(idCadete,numPedido);
        if(pedido != null){
            return Accepted(pedido);
        }
        return NotFound("Error del Servidor");
    }
    [HttpPut ("CambiarEstadoPedido")]

    public ActionResult<Pedido> CambiarEstadoPedido(int numPedido, Estado estado){
        var pedido = cadeteria.CambiarEstadoPedido(numPedido,estado);
        if(pedido != null){
            return Accepted(pedido);
        }
        return NotFound("Pedido inexistente");
    }
    [HttpPut ("CambiarCadetePedido")]
    public ActionResult<Pedido> CambiarCadetePedido(int idCadete, int numPedido){
        return AsignarPedido(idCadete, numPedido);
    }
}
