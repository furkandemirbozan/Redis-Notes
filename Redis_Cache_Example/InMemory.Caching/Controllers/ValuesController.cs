using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IMemoryCache _memoryCache; // IMemoryCache inject edilir.
        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }


        //[HttpGet("set/{name}")]
        //public void SetName(string name)
        //{
        //    _memoryCache.Set("name", name);           // IMemoryCache Set metodu ile veri eklenir.

        //}
        //[HttpGet]
        //public string GetName()
        //{
        //    if (_memoryCache.TryGetValue<string>("name",out string name))
        //    {
        //        return name.Substring(3);
        //    }
        //    return "";   // IMemoryCache Get metodu ile veri alınır.
        //}

        [HttpGet("setDate")]
        public void SetDate()
        {
            _memoryCache.Set("date", DateTime.Now, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30), // 30 saniye sonra cache silinir.
                SlidingExpiration = TimeSpan.FromSeconds(5) // 5 sanıye boyunca cache kullanılmazsa silinir.
            });
        }
        [HttpGet("getDate")]

        public DateTime GetDate()
        {
            return _memoryCache.Get<DateTime>("date");
        }
    }
}
