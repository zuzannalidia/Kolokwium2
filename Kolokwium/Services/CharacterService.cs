using Kolokwium.Data;
using Kolokwium.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kolokwium.Services;

namespace ExampleTest2.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly DatabaseContext _context;

        public CharacterService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<CharacterDTO> GetCharacterByIdAsync(int characterId)
        {
            var character = await _context.Characters
                .Where(c => c.Id == characterId)
                .Select(c => new CharacterDTO
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    CurrentWeight = c.CurrentWeight,
                    MaxWeight = c.MaxWeight,
                    BackpackItems = c.Backpacks.Select(b => new BackpackDTO
                    {
                        ItemName = b.Item.Name,
                        ItemWeight = b.Item.Weight,
                        Amount = b.Amount
                    }).ToList(),
                    Titles = c.CharacterTitles.Select(ct => new TitleDTO
                    {
                        Name = ct.Title.Name,
                        AcquiredAt = ct.AcquiredAt
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return character;
        }

        public async Task<List<BackpackDTO>> AddItemsToBackpackAsync(int characterId, AddItemsDTO addItemsDTO)
        {
            var character = await _context.Characters
                .Include(c => c.Backpacks)
                .ThenInclude(b => b.Item)
                .FirstOrDefaultAsync(c => c.Id == characterId);

            if (character == null)
            {
                throw new KeyNotFoundException("Nie znaleziono Charactera");
            }

            var items = await _context.Items.Where(i => addItemsDTO.ItemIds.Contains(i.Id)).ToListAsync();
            if (items.Count != addItemsDTO.ItemIds.Count)
            {
                throw new KeyNotFoundException("Niektóre przedmioty nie istnieją");
            }

            int totalNewWeight = 0;
            foreach (var item in items)
            {
                totalNewWeight += item.Weight;
            }

            if (character.CurrentWeight + totalNewWeight > character.MaxWeight)
            {
                throw new InvalidOperationException("Character nie może tyle nieść");
            }

            foreach (var item in items)
            {
                var backpack = character.Backpacks.FirstOrDefault(b => b.ItemId == item.Id);
                if (backpack == null)
                {
                    character.Backpacks.Add(new Backpack
                    {
                        ItemId = item.Id,
                        CharacterId = character.Id,
                        Amount = 1
                    });
                }
                else
                {
                    backpack.Amount += 1;
                }
            }

            character.CurrentWeight += totalNewWeight;
            await _context.SaveChangesAsync();

            return character.Backpacks.Select(b => new BackpackDTO
            {
                ItemName = b.Item.Name,
                ItemWeight = b.Item.Weight,
                Amount = b.Amount
            }).ToList();
        }
    }
}
