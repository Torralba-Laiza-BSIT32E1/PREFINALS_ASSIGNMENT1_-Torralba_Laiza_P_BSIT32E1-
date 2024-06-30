using Microsoft.AspNetCore.Mvc;
using System;

namespace ProtectedApi
{
    [ApiController]
    [Route("[controller]")]
    public class ValueController : ControllerBase
    {
        private readonly string _owner = "Torralba, Laiza Patromo.";
        private readonly Random _random = new Random();
        private readonly string[] _thingsAboutOwner = new[]
        {
            "I love sleeping",
            "i like playing online games",
            "i like to travel",
            "Im part of LGBTQ",
            "I like talking",
            "Cry baby",
            "I have 3 cats",
            "I like chocolates",
            "Im Introvert",
            "Im short",
        };

        [HttpGet("about/me")]
        public IActionResult AboutMe()
        {
            var thing = _thingsAboutOwner[_random.Next(_thingsAboutOwner.Length)];
            return Ok(thing);
        }

        [HttpGet("about")]
        public IActionResult About()
        {
            return Ok(_owner);
        }

        [HttpPost("about")]
        public IActionResult About([FromBody] NameModel model)
        {
            return Ok($"Hi {model.Name} from {_owner}");
        }
    }

    public class NameModel
    {
        public string? Name { get; set; }
    }
}