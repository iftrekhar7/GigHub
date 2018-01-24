using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GigHub.Dtos;
using AutoMapper;

namespace GigHub.Controllers.api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n =>n.Gig.Artist)
                .ToList();

            

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
            //return notifications.Select(n => new NotificationDto()
            //{
            //    DateTime = n.DateTime,
            //    Gig = new GigDto
            //    {
            //        Artist = new UserDto
            //        {
            //            Id = n.Gig.Artist.Id,
            //            Name = n.Gig.Artist.Name
            //        },
            //        DateTime = n.Gig.DateTime,
            //        Id = n.Gig.Id,
            //        ISCancled = n.Gig.ISCancled,
            //        Venue = n.Gig.Venue
            //    },
            //    OriginalDateTime = n.OriginalDateTime,
            //    OriginalVenue = n.OriginalVenue,
            //    Type = n.Type
            //});
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();

            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();
            notifications.ForEach(n => n.Read());

            _context.SaveChanges();

            return Ok();
        }
    }
}
