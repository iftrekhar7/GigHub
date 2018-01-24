using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using GigHub.Dtos;

namespace GigHub.Controllers
{
    
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto/*[FromBody]int gigId*/)//web api by default does not look for scaller parameter like int in the request body.it expect ehem in the url. so we use from body attribute
        {
            var userId = User.Identity.GetUserId();
            if (_context.Attendances.Any(a => a.AttendeId == userId && a.GigId == dto.GigId))
                return BadRequest("The attendance already exists");
            
            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeId = userId
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok();
        }

        public IHttpActionResult DeleteAttendance(int id)
        {
            var userId = User.Identity.GetUserId();
            var attendance = _context.Attendances
                .SingleOrDefault(a => a.AttendeId == userId && a.GigId == id);

            if (attendance == null)
                return NotFound();
            _context.Attendances.Remove(attendance);
            _context.SaveChanges();

            return Ok(id);
        }
    }

    
}
