using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Products.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Product
            {
                CreatedOn = DateTime.Now.AddDays(index),
                Name = Summaries[rng.Next(Summaries.Length)],
                Description = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
