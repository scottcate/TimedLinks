using System;

namespace scheduled_links
{

    class Program
    {
        static void Main(string[] args)
        {
            //Jan 4th, 2021 = Monday
            var testTimes = new DateTime[] {
                DateTime.Parse("2021/01/04 08:29Z").ToUniversalTime(),
                DateTime.Parse("2021/01/04 08:30Z").ToUniversalTime(),
                DateTime.Parse("2021/01/04 08:30:01Z").ToUniversalTime(),
                DateTime.Parse("2021/01/04 08:59:59Z").ToUniversalTime(),
                DateTime.Parse("2021/01/04 09:00Z").ToUniversalTime(),
                DateTime.Parse("2021/01/04 09:00:01Z").ToUniversalTime(),
                DateTime.Parse("2021/01/04 09:30Z").ToUniversalTime()
            };

            //Jan 3rd, 2021 = Sunday
            var wekendTestTimes = new DateTime[] {
                DateTime.Parse("2021/01/03 08:29Z").ToUniversalTime(),
                DateTime.Parse("2021/01/03 08:30Z").ToUniversalTime(),
                DateTime.Parse("2021/01/03 08:30:01Z").ToUniversalTime(),
                DateTime.Parse("2021/01/03 08:59:59Z").ToUniversalTime(),
                DateTime.Parse("2021/01/03 09:00Z").ToUniversalTime(),
                DateTime.Parse("2021/01/03 09:00:01Z").ToUniversalTime(),
                DateTime.Parse("2021/01/03 09:30Z").ToUniversalTime()
            };

            //schedules
            var activeSchedule = new Schedule
            {
                Cron = "30 8 * * MON-FRI",
                Link = "for 8:30-9 go here",
                DurationMinutes = 30
            };

            var earlySchedule = new Schedule
            {
                Start = DateTime.Parse("2021/04/04 08:29Z").ToUniversalTime(),
                Cron = "30 8 * * MON-FRI",
                Link = "for 8:30-9 go here",
                DurationMinutes = 30
            };

            var lateSchedule = new Schedule
            {
                Start = DateTime.Parse("2020/12/31 08:29Z").ToUniversalTime(),
                End = DateTime.Parse("2020/01/01 08:29Z").ToUniversalTime(),
                Cron = "30 8 * * MON-FRI",
                Link = "for 8:30-9 go here",
                DurationMinutes = 30
            };

            var activeLink = new GoLink("1234", "originallink")
            {
                Schedules = new Schedule[] { activeSchedule }
            };

            var earlyLink = new GoLink("1234", "originallink")
            {
                Schedules = new Schedule[] { earlySchedule }
            };

            var lateLink = new GoLink("1234", "originallink")
            {
                Schedules = new Schedule[] { lateSchedule }
            };

            var multiScheduleLink = new GoLink("1234", "originallink")
            {
                Schedules = new Schedule[] { activeSchedule, earlySchedule, lateSchedule }
            };

            System.Console.WriteLine("Expecting 8:30 - 9 Overrides");
            foreach (DateTime d in testTimes)
            {
                Console.WriteLine($"Test Time {d.ToString("T")}: {activeLink.RedirectLink(d)}");
            }


            System.Console.WriteLine("Expecting no overrides: schedule hasn't started yet");
            foreach (DateTime d in testTimes)
            {
                Console.WriteLine($"Test Time {d.ToString("T")}: {earlyLink.RedirectLink(d)}");
            }

            System.Console.WriteLine("Expecting no overrides: schedule is past");
            foreach (DateTime d in testTimes)
            {
                Console.WriteLine($"Test Time {d.ToString("T")}: {lateLink.RedirectLink(d)}");
            }


            System.Console.WriteLine("Expecting 8:30 - 9 Overrides, with all 3 schedules");
            foreach (DateTime d in testTimes)
            {
                Console.WriteLine($"Test Time {d.ToString("T")}: {multiScheduleLink.RedirectLink(d)}");
            }

            
            System.Console.WriteLine("Expecting no overrides: schedule is weekend");
            foreach (DateTime d in wekendTestTimes)
            {
                Console.WriteLine($"Test Time {d.ToString("T")}: {multiScheduleLink.RedirectLink(d)}");
            }
        }
    }
}
