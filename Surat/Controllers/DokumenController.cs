using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Surat.Application;

namespace Surat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DokumenController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateDokumen(SerahDokumenCommand command)
        {
            var handler = new SerahDokumenHandler();
            try
            {
                var result = handler.Handle(command);
                return Ok($"New DokumenId : {result}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
