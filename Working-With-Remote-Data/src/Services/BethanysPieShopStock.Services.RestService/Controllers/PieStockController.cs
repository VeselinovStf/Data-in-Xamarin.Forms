using System;
using System.Threading.Tasks;
using BethanysPieShopStock.Services.RestService.Models;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShopStock.Services.RestService.Controllers
{
    [Route("api/[controller]")]
    public class PieStockController : Controller
    {
        private readonly IPieService _pieService;

        public PieStockController(IPieService pieService)
        {
            _pieService = pieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPies()
        {
            var allPies = await _pieService.GetPies();
            return Ok(allPies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPieById(Guid id)
        {
            var pie = await _pieService.GetPieById(id);
            return Ok(pie);
        }

        [HttpPost]
        public async Task<IActionResult> AddPie([FromBody] Pie pie)
        {
            if (pie == null)
                return BadRequest();

            if (pie.PieName == string.Empty)
            {
                ModelState.AddModelError("Pie name", "The pie name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPie = await _pieService.AddPie(pie);

            return Created("pie", createdPie);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePie([FromBody] Pie pie)
        {
            if (pie == null)
                return BadRequest();

            if (pie.PieName == string.Empty )
            {
                ModelState.AddModelError("Pie name", "The pie name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pieToUpdate = await _pieService.GetPieById(pie.Id);

            if (pieToUpdate == null)
                return NotFound();

            await _pieService.UpdatePie(pie);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePie(Guid id)
        {
            if (id == null || id == Guid.Empty)
                return BadRequest();

            var pieToDelete = await _pieService.GetPieById(id);
            if (pieToDelete == null)
                return NotFound();

            await _pieService.DeletePie(id);

            return NoContent();//success
        }
    }
}
