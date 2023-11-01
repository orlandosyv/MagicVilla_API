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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.villaList);
        }

        //[HttpGet] // Get is always predetermined by default (not necessary to put here)
        [HttpGet("{id:int}", Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        [ProducesResponseType(StatusCodes.Status404NotFound)]         
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0) { return BadRequest(); }
            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);            
            if (villa == null) { return NotFound(); }             
            return Ok(villa);             
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO) 
        { 
            if(villaDTO == null) { return BadRequest(villaDTO); }
            if(villaDTO.Id > 0) { return StatusCode(StatusCodes.Status500InternalServerError); }
            villaDTO.Id = VillaStore.villaList.OrderByDescending(v => v.Id)
                          .FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villaDTO);
            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id},villaDTO);
        }
        


    }

}

