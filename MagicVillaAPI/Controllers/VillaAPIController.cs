using MagicVillaAPI.Data;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVillaAPI.Controllers
{
    //[Route("api/VillaAPI")]
    [Route("api/VillaAPI")] //Do not change it (route)    
    [ApiController]
    public class VillaAPIController : ControllerBase
    {

        //private readonly ILogging _logger;
        public VillaAPIController( ) 
        {             
        }
        
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
            if (id == 0) {                
                return BadRequest(); 
            }
            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);            
            if (villa == null) { return NotFound(); }             
            return Ok(villa);             
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO) 
        {
            //When not using [ApiController], you can use. ModelState.Valid like this
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            
            //Custom Validation:
            if(VillaStore.villaList.FirstOrDefault(u=>u.Name.ToLower() == villaDTO.Name.ToLower() ) != null) 
            {
                ModelState.AddModelError("CustomError", "Villa already Exists");
                return BadRequest(ModelState);
            }

            if (villaDTO == null) { return BadRequest(villaDTO); }
            if(villaDTO.Id > 0) { return StatusCode(StatusCodes.Status500InternalServerError); }
            villaDTO.Id = VillaStore.villaList
                            .OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villaDTO);
            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id},villaDTO);
        }

        [HttpDelete("{id:int}", Name ="DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0) { return BadRequest(); }
            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            if (villa == null) { return NotFound(); }
            VillaStore.villaList.Remove(villa);
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        public IActionResult UpdateVilla(int id, [FromBody]VillaDTO villaDTO)
        { 
            if (villaDTO == null || id != villaDTO.Id ) { return BadRequest();}
            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            villa.Name = villaDTO.Name;
            villa.Sqft = villaDTO.Sqft;
            villa.Occupancy = villaDTO.Occupancy;
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO) {
            if (patchDTO == null || id == 0) { return BadRequest(); }
            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            if (villa == null) { return BadRequest(); }
            patchDTO.ApplyTo(villa, ModelState);

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return NoContent();
        }



    }

}

