using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementWebAPI.Data;
using SchoolManagementWebAPI.Models;

namespace SchoolManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicCalendarController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        public AcademicCalendarController(ApplicationDbContext db)
        {
            this.db = db;
        }
        [Route("GetACalendars")]
        [HttpGet]
        public async Task<IActionResult> GetCalendars()
        {
            var calendars = await db.AcademicCalendars.Include(c => c.Events).ToListAsync();
            return Ok(calendars);
        }
        [Route("GetCalendarById/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetCalendar(int id)
        {
            var calendar = await db.AcademicCalendars.Include(c => c.Events)
                                                           .FirstOrDefaultAsync(c => c.Id == id);
            if (calendar == null)
            {
                return NotFound();
            }
            return Ok(calendar);
        }

        [Route("createCalendar")]
        [HttpPost]
        public async Task<IActionResult> createCalendar(AcademicCalendar calendar)
        {
            db.AcademicCalendars.Add(calendar);
            await db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCalendars),new {id = calendar.Id},calendar);
        }
        [Route("UpdateCalendar/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateCalendar(int id,AcademicCalendar calendar)
        {
            if (id != calendar.Id)
            {
                return BadRequest();
            }
            db.Entry(calendar).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return NoContent();
        }

        [Route("DeleteCalendar/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCalendar(int id)
        {
            var calendar = await db.AcademicCalendars.FindAsync(id);
            if (calendar == null)
            {
                return NotFound();
            }

            db.AcademicCalendars.Remove(calendar);
            await db.SaveChangesAsync();
            return NoContent();
        }
    }
}
