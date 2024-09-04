using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace Distributed.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IDistributedCache _distributedCache; // IDistributedCache inject edilir.
        public ValuesController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpGet("set")]
        public async Task<IActionResult> Set(string name,string surname)
        {
            // IDistributedCache SetString metodu ile veri eklenir.
            await _distributedCache.SetStringAsync("name", name,options:new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30), // 30 saniye sonra cache silinir.
                SlidingExpiration = TimeSpan.FromSeconds(5) // 5 sanıye boyunca cache kullanılmazsa silinir.
            });
            // IDistributedCache Set metodu ile veri eklenir.
            await _distributedCache.SetAsync("surname", Encoding.UTF8.GetBytes(surname), options: new()
            {
                AbsoluteExpiration=DateTime.Now.AddSeconds(30), // 30 saniye sonra cache silinir.
                SlidingExpiration = TimeSpan.FromSeconds(5) // 5 sanıye boyunca cache kullanılmazsa silinir.
            }); // IDistributedCache Set metodu ile veri eklenir.

            return Ok();
        }
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var name = await _distributedCache.GetStringAsync("name"); // IDistributedCache GetString metodu ile veri alınır.
            var surnameBinary= await _distributedCache.GetAsync("surname"); // IDistributedCache Get metodu ile veri alınır.

            var surname = Encoding.UTF8.GetString(surnameBinary); // Binary veri stringe çevrilir.
            return Ok(new { name, surname });
        }
    }
}
