using FungiJournal.DataAccess;
using FungiJournal.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FungiJournal.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EntriesController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAll() => Ok(SQLiteDataAccess.LoadEntries());

        [HttpPost]
        public ActionResult<Entry> PostEntry([FromBody] Entry entry)
        {
            SQLiteDataAccess.AddEntry(entry);

            return Ok(entry);
        }
    }
}
