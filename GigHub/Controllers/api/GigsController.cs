using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers.api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
            //if (gig.ISCancled)
            //    return NotFound();
        }

        [HttpDelete]
        public IHttpActionResult Cancle(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attende))
                .Single(g => g.Id == id && g.ArtistId == userId);

            //gig.ISCancled = true;

            //var notification = Notification.GigCanceled(gig);

            //foreach (var attendee in gig.Attendances.Select(a => a.Attende))
            //{
            //    attendee.Notify(notification);
           // }


            gig.Cancle();
            
            _context.SaveChanges();
            return Ok();

        }
    }
}
