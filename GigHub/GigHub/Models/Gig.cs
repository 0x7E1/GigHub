using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebGrease.Css.Extensions;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCancel { get; private set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {
            IsCancel = true;
            var notification = Notification.GigCanceled(this);
            Attendances.Select(a => a.Attendee).ForEach(a => a.Notify(notification));

            //foreach (var attendee in Attendances.Select(a => a.Attendee))
            //{
            //    attendee.Notify(notification);
            //}
        }

        public void Modify(DateTime dateTime, string venue, byte genre)
        {
            var notification = Notification.GigUpdated(this, DateTime, Venue);

            Venue = venue;
            DateTime = dateTime;
            GenreId = genre;

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
}