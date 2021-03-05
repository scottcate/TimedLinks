using System;
using System.Linq;

namespace scheduled_links
{

    public class GoLink
    {
        public GoLink(string vanity, string destination)
        {
            this.Vanity = vanity;
            this.Destination = destination;
            this.Schedules = new Schedule[0];
        }
        public string Vanity { get; set; }
        public string Destination { get; set; }

        public Schedule[] Schedules { get; set; }

        public string RedirectLink()
        {
            return RedirectLink(DateTime.UtcNow);
        }

        public string RedirectLink(DateTime pointInTime)
        {
            var link = Destination;
            var now = DateTime.UtcNow;
            var active = Schedules.Where(s =>
                s.End > now && //hasn't ended
                s.Start < now //already started
                ).OrderBy(s => s.Start); //order by start to process first link

            foreach (var sched in active.ToArray())
            {
                if (sched.IsActive(pointInTime))
                {
                    link = sched.Link;
                    break;
                }
            }

            return link;
        }
    }
}
