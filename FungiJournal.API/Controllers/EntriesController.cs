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
        public async Task<IActionResult> GetAll()
        {
            return Ok(await dataAccess.GetEntriesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await dataAccess.GetEntryAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostEntry([FromBody] Entry entry)
        {
            await dataAccess.AddEntryAsync(entry);

            return CreatedAtAction(nameof(GetById),
                                   new { id = entry.EntryId },
                                   entry);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntry(int id)
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
                await dataAccess.UpdateEntryAsync(entry);
            }
            return NoContent();
        }
    }
}
