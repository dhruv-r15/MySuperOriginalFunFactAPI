using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FunFactAPI.Models;
using FunFactAPI.Repositories;

namespace FunFactAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FunFactsController : ControllerBase
    {
        private readonly FunFactRepository _repository;

        public FunFactsController(FunFactRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var funFacts = _repository.GetAll();
            return Ok(new { Message = "As Sheldon would say, 'I'm not crazy, my mother had me tested.'", Facts = funFacts });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var funFact = _repository.GetById(id);
            if (funFact == null)
                return NotFound("This fact seems to have disappeared into a black hole!");
            return Ok(funFact);
        }

        [HttpPost]
        public IActionResult Add(FunFact funFact)
        {
            var newFact = _repository.Add(funFact);
            return CreatedAtAction(nameof(GetById), new { id = newFact.Id }, newFact);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, FunFact funFact)
        {
            if (id != funFact.Id)
                return BadRequest("ID mismatch. Even Leonard wouldn't make this mistake!");

            var updatedFact = _repository.Update(funFact);
            if (updatedFact == null)
                return NotFound("This fact seems to have disappeared into a black hole!");

            return Ok(updatedFact);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _repository.Delete(id);
            if (!result)
                return NotFound("This fact seems to have disappeared into a black hole!");

            return Ok("Fact deleted. Bazinga!");
        }

        [HttpGet("category/{category}")]
        public IActionResult GetByCategory(string category)
        {
            var facts = _repository.GetByCategory(category);
            return Ok(new { Message = $"Facts about {category}? As Sheldon would say, 'Fascinating!'", Facts = facts });
        }

        [HttpGet("character/{character}")]
        public IActionResult GetByCharacter(string character)
        {
            var facts = _repository.GetByCharacter(character);
            return Ok(new { Message = $"Facts associated with {character}? Oh boy, here we go!", Facts = facts });
        }

        [HttpPost("{id}/vote")]
        public IActionResult Vote(int id, [FromQuery] bool upvote)
        {
            var funFact = _repository.Vote(id, upvote);
            if (funFact == null)
                return NotFound("This fact seems to have disappeared into a black hole!");

            return Ok(funFact);
        }
    }
}
