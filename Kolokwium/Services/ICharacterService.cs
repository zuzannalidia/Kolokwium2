using Kolokwium.Models;
namespace Kolokwium.Services;

public interface ICharacterService
{
    Task<CharacterDTO> GetCharacterByIdAsync(int characterId);
    Task<List<BackpackDTO>> AddItemsToBackpackAsync(int characterId, AddItemsDTO addItemsDTO);
}