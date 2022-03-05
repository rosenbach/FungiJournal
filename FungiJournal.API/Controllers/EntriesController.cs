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
        public IActionResult GetAll() => Ok(dataAccess.GetEntriesAsync().Result);

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id) => Ok(dataAccess.GetEntryAsync(id).Result);

        [HttpPost]
        public ActionResult<Entry> PostEntry([FromBody] Entry entry)
        {
            dataAccess.AddEntryAsync(entry);

            return CreatedAtAction(nameof(GetById),
                                   new { id = entry.EntryId },
                                   entry);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Entry>> DeleteEntry(int id)
        {
            Entry entryToDelete = await dataAccess.GetEntryAsync(id);

            if (entryToDelete == null)
            {
                return NotFound();
            }
            else
            {
                await dataAccess.DeleteEntryAsync(entryToDelete);
            }
            return Ok(entryToDelete);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntry([FromRoute] int id,
                                                        [FromBody] Entry entry)
        {
            if (id != entry.EntryId)
            {
                return BadRequest();
            }
            else
            {
                await dataAccess.UpdateEntryAsync(id, entry);
            }
            return NoContent();
        }
    }
}
