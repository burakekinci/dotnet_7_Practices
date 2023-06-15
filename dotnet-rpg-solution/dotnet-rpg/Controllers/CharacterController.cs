using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public ActionResult<List<Character>> GetAll()
        {
            return Ok(_characterService.GetAllCharacters());
        }

        [HttpGet("GetSingle")]
        public ActionResult<Character> GetSingle([FromQuery] int id)
        {
            return Ok(_characterService.GetCharacterById(id));
        }

        [HttpPost]
        public ActionResult<List<Character>> AddCharacter(Character newCharacter)
        {
            return Ok(_characterService.AddCharacter(newCharacter));
        }



    }
}