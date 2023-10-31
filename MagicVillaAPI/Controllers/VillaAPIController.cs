using MagicVillaAPI.Data;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagicVillaAPI.Controllers
{
    //[Route("api/VillaAPI")]
    [Route("api/VillaAPI")] //Do not change it (route)    
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        //Http EndPoint
        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.villaList);
        }

        //[HttpGet] // Get is always predetermined by default (not necessary to put here)
        [HttpGet("id:int")] 
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0) { return BadRequest(); }
            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);            
            if (villa == null) { return NotFound(); }             
            return Ok(villa);             
        }
    }

}

