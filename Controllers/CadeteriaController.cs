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
        cadeteria = new Cadeteria("PedidosYa", 4265192);
        cadeteria.Cadetes.Add([])
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
}
