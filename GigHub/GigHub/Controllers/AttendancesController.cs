using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers
{
    [RoutePrefix("api/attendances")]
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [Route("attend")]
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            if (dto == null) return BadRequest("Attend(): Data Transfer Object is NULL!");

            var userId = User.Identity.GetUserId();

            if (_context.Attendances.Any(i => i.GigId == dto.GigId && i.AttendeeId == userId))
                return BadRequest("The attendance already exist.");

            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }

        [Route("remove")]
        [HttpPost]
        public IHttpActionResult RemoveGig(AttendanceDto dbo)
        {
            if (dbo == null) return BadRequest("Data Transfer Object is NULL!");

            var gig = _context.Attendances.Single(g => g.GigId == dbo.GigId);

            _context.Attendances.Remove(gig);
            _context.SaveChanges();

            return Ok("Gig was been deleted.");
        }
    }
}
