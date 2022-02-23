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
        private readonly IDataAccess dataAccess;
        public EntriesController(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(dataAccess.LoadEntries());

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id) => Ok(dataAccess.GetById(id));

        [HttpPost]
        public ActionResult<Entry> PostEntry([FromBody] Entry entry)
        {
            dataAccess.AddEntry(entry);

            return Ok(entry);
        }
    }
}
