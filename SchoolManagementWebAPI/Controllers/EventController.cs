using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementWebAPI.Data;
using SchoolManagementWebAPI.Models;

namespace SchoolManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public EventController (ApplicationDbContext db)
        {
            this.db = db;
        }
        [Route("GetEvents")]
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await db.Events.ToListAsync();
            return Ok(events);
        }
        [Route("CreateEvent")]
        [HttpPost]
        public async Task<IActionResult> createEvents(Event events)
        {
            db.Events.Add(events);
            await db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEvents),new { id = events.Id}, events);
        }
        [Route("GetEventsById/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetEventsById(int id)
        {
            var eventItem = await db.Events.FindAsync(id);
            if(eventItem == null)
            {
                return NotFound();
            }
            return Ok(eventItem);
        }
        [Route("updateEvent/{id}")]
        [HttpPut]
        public async Task<IActionResult> updateEvent(int id,Event eventItem)
        {
            if(id != eventItem.Id)
            {
                return BadRequest();
            }
            db.Entry(eventItem).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return NoContent();
        }
        [Route("DeleteEvent/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var events = await db.Events.FindAsync(id);
            if(events == null)
            {
                return NotFound();
            }
            db.Events.Remove(events);
            await db.SaveChangesAsync();
            return NoContent();
        }
    }
}
