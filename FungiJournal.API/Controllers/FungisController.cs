using FungiJournal.API.Classes;
using FungiJournal.DataAccess;
using FungiJournal.DataAccess.Importer;
using FungiJournal.Domain.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FungiJournal.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FungisController : ControllerBase
    {
        private readonly IDataAccess dataAccess;
        public FungisController(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        [HttpGet]
        [EnableCors]
        public async Task<IActionResult> GetAll([FromQuery] FungiQueryParameters queryParameters)
        {
            IQueryable<Fungi> fungis = dataAccess.GetFungis();

            if (queryParameters.HasFungiId())
            {
                fungis = fungis
                    .Where(e => e.FungiId == queryParameters.FungiId);
            }

            if (queryParameters.HasFoodValue())
            {
                fungis = fungis
                    .Where(e => e.FoodValue == queryParameters.FoodValue);
            }

            if (!string.IsNullOrEmpty(queryParameters.Name))
            {
                fungis = fungis
                    .Where(e => e.Name.Contains(queryParameters.Name));
            }

            if (!string.IsNullOrEmpty(queryParameters.Season))
            {
                fungis = fungis
                    .Where(e => e.Season.Contains(queryParameters.Season));
            }

            fungis = fungis
                .Skip(queryParameters.Size * (queryParameters.Page - 1))
                .Take(queryParameters.Size);

            return Ok(await fungis.ToArrayAsync());
        }

        [EnableCors]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await dataAccess.GetFungisAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await dataAccess.GetFungiAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostFungi([FromBody] Fungi fungi)
        {
            await dataAccess.AddFungiAsync(fungi);

            return CreatedAtAction(nameof(GetById),
                                   new { id = fungi.FungiId },
                                   fungi);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> PostUpdateFungi([FromRoute] int id,
                                        [FromBody] Fungi fungi)
        {
            if (id != fungi.FungiId)
            {
                return BadRequest();
            }
            else
            {
                await dataAccess.UpdateFungiAsync(fungi);
            }
            return NoContent();
        }

        //temporary method just to import the local fungis.
        ///may be reused and improved for a future upload/import feature
        //[HttpPost("import")]
        //public async Task<IActionResult> PostImportedFungis()
        //{
        //    List<Fungi> fungis = FungiImporter.Read(@"C:\Users\m_kae\source\repos\FungiJournal\FungiJournal.DataAccess\Importer\pilze_small.txt");

        //    for (int i = 0; i < fungis.Count; i++)
        //    {
        //        await PostFungi(fungis[i]);
        //    }

        //    return Ok(fungis[0]);
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFungi(int id)
        {
            Fungi fungiToDelete = await dataAccess.GetFungiAsync(id);

            if (fungiToDelete == null)
            {
                return NotFound();
            }
            else
            {
                await dataAccess.DeleteFungiAsync(fungiToDelete);
            }
            return Ok(fungiToDelete);
        }

        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAllFungis()
        {
            List<Fungi> fungisToDelete = await dataAccess.GetFungisAsync();

            for (int i = 0; i < fungisToDelete.Count; i++)
            {
                if (fungisToDelete[i] == null)
                {
                    return NotFound();
                }
                else
                {
                    await dataAccess.DeleteFungiAsync(fungisToDelete[i]);
                }
            }

            return Ok(fungisToDelete);
        }
    

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFungi([FromRoute] int id,
                                                        [FromBody] Fungi fungi)
        {
            return await PostUpdateFungi(id, fungi);
        }
    }
}
