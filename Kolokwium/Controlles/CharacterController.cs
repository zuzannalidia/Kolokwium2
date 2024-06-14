using Kolokwium.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Kolokwium.Models;

namespace ExampleTest2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharactersController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("{characterId}")]
        public async Task<IActionResult> GetCharacter(int characterId)
        {
            var character = await _characterService.GetCharacterByIdAsync(characterId);
            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        [HttpPost("{characterId}/backpacks")]
        public async Task<IActionResult> AddItemsToBackpack(int characterId, [FromBody] AddItemsDTO addItemsDTO)
        {
            try
            {
                var backpackItems = await _characterService.AddItemsToBackpackAsync(characterId, addItemsDTO);
                return Ok(backpackItems);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}