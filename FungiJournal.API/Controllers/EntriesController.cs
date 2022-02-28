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
        public IActionResult GetById([FromRoute] int id) => Ok(dataAccess.GetEntry(id));

        [HttpPost]
        public ActionResult<Entry> PostEntry([FromBody] Entry entry)
        {
            dataAccess.AddEntry(entry);

            return CreatedAtAction(nameof(GetById),
                                   new { id = entry.EntryId },
                                   entry);
        }

        [HttpDelete("{id}")]
        public ActionResult<Entry> DeleteEntry(int id)
        {
            Entry entryToDelete = dataAccess.GetEntry(id);

            if (entryToDelete == null)
            {
                return NotFound();
            }
            else
            {
                dataAccess.DeleteEntry(id);
            }
            return Ok(entryToDelete);
        }

    }
}
