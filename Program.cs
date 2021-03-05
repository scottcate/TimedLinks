using System;

namespace scheduled_links
{

    class Program
    {
        static void Main(string[] args)
        {
            var testTimes = new DateTime[] {
                DateTime.Parse("2021/01/04 08:29Z").ToUniversalTime(),
                DateTime.Parse("2021/01/04 08:30Z").ToUniversalTime(),
                DateTime.Parse("2021/01/04 08:30:01Z").ToUniversalTime(),
                DateTime.Parse("2021/01/04 08:59:59Z").ToUniversalTime(),
                DateTime.Parse("2021/01/04 09:00Z").ToUniversalTime(),
                DateTime.Parse("2021/01/04 09:30Z").ToUniversalTime()
            };

            var first = new GoLink("1234", "originallink")
            {
                Schedules = new Schedule[]{
                    new Schedule {
                        Cron = "30 8 * * MON-FRI",
                        Link = "for 8:30-9 go here",
                        DurationMinutes = 30
                    }
                }
            };
            foreach (DateTime d in testTimes)
            {
                Console.WriteLine($"Test Time {d.ToString("T")}: {first.RedirectLink(d)}");
            }

        }
    }
}
