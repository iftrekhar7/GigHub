using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class UserNotification
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get; private set; }

        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; private set; }

        public ApplicationUser User { get; private set; }

        public Notification Notification { get; private set; }

        public bool IsRead { get; set; }
        protected UserNotification() // when we use custome constructor we should always default constructor
                                  //bcz entity framework can not call this constructor to create an instance of user notification
        {
        }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            if(user == null)
            throw new ArgumentNullException("user");

            if (notification == null)
                throw new NullReferenceException("notification"); //initialize auto property

            User = user;
            Notification = notification;
        }

        public void Read()
        {
            IsRead = true;
        }
    }
}