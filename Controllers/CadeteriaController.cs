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
        return Ok(cadeteria.Pedidos);
    }
    [HttpGet]
    [Route("Cadetes")]
    public ActionResult<string> GetCadetes(){
        return Ok(cadeteria.Cadetes);
    }
    [HttpGet]
    [Route("Informe")]
    public ActionResult<string> GetInforme(){
        return Ok(cadeteria.GetInforme());
    }
    [HttpPost ("AddPedido")]
    public ActionResult<Pedido> AddPedido(string nombre, string direccion, long telefono, string datosRef,  string observacion){
        cadeteria.TomarPedido(nombre,direccion,telefono, datosRef, observacion);
        var ped = cadeteria.Pedidos.FirstOrDefault(p=> p.Numero == cadeteria.Pedidos.Count()-1);
        if(ped!=null){
            AccesoADatosPedidos.Guardar(cadeteria.Pedidos);
            return Ok(ped);
        }
        return StatusCode(500,"no se tomó el pedido");
    }
    [HttpPut ("AsignarPedido")]
    public ActionResult<Pedido> AsignarPedido(int idCadete, int numPedido){
        var pedido = cadeteria.Pedidos.FirstOrDefault(p=>p.Numero == numPedido);
        var cadete = cadeteria.Cadetes.FirstOrDefault(c=>c.Id == idCadete);
        if(pedido != null){
            if(cadete != null){
                pedido.IdCadete = idCadete;
                AccesoADatosPedidos.Guardar(cadeteria.Pedidos);
                return Ok(pedido);
            }
            return NotFound("Cadete inexistente");
        }
        return NotFound("Pedido inexistente");
    }
    [HttpPut ("CambiarEstadoPedido")]

    public ActionResult<Pedido> CambiarEstadoPedido(int numPedido, Estado estado){
        var pedido = cadeteria.Pedidos.FirstOrDefault(p=>p.Numero == numPedido);
        if(pedido != null){
            if(pedido.Estado == Estado.SinEntregar){
                pedido.Estado = estado;
                AccesoADatosPedidos.Guardar(cadeteria.Pedidos);
                return Ok(pedido);
            }else{
                if(pedido.Estado == Estado.Entregado){
                    return NotFound("El pedido ya fué Entregado");
                }else{
                    return NotFound("El pedido ya fué Cancelado");
                }
            }
        }
        return NotFound("Pedido inexistente");
    }
    [HttpPut ("CambiarCadetePedido")]
    public ActionResult<Pedido> CambiarCadetePedido(int idCadete, int numPedido){
        return AsignarPedido(idCadete, numPedido);
    }
}
