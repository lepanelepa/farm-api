using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace farm_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController : ControllerBase
    {
        private const string animalsListCacheKey = "animalsList";
        private IMemoryCache _cache;
        private ILogger<AnimalsController> _logger;

        public AnimalsController( 
            IMemoryCache cache,
            ILogger<AnimalsController> logger)
        {           
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));    
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            List<Animal> animals;

            _logger.Log(LogLevel.Information, "Trying to fetch the list of animals from cache.");
            if (_cache.TryGetValue(animalsListCacheKey, out animals))
            {
                _logger.Log(LogLevel.Information, "Animals list found in cache.");
            }
            else
            {
                              
            _logger.Log(LogLevel.Information, "Animals list not found in cache. Fetching from database.");
            animals = new List<Animal>()
            {
                new Animal() { Name = "Cat" },
                new Animal() { Name = "Cow" }
            };

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                        .SetPriority(CacheItemPriority.Normal)
                        .SetSize(1024);
                _cache.Set(animalsListCacheKey, animals, cacheEntryOptions);
            }
            return Ok(animals);           
        }
    }
}
