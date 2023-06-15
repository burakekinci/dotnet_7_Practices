using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Services.CharacterService.Abstract;

namespace dotnet_rpg.Services.CharacterService.Concrete
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character { Name = "Sam", Id=1}
        };

        public List<Character> AddCharacter(Character newCharacter)
        {
            characters.Add(newCharacter);
            return (characters);
        }

        public List<Character> GetAllCharacters()
        {
            return characters;
        }

        public Character GetCharacterById(int id)
        {
            var character = characters.FirstOrDefault(c => c.Id == id);
            if (character is not null)
            {
                return character;
            }
            throw new Exception("Character not found");
        }
    }
}