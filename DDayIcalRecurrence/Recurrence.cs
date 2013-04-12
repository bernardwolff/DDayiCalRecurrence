using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DDay.iCal;

namespace DDayIcalRecurrence
{
    public class Recurrence
    {
        // From: http://sourcefield.blogspot.com/2011/06/dday-ical-example.html
        public void Example()
        {
            // #1: Monthly meetings that occur on the last Wednesday from 6pm - 7pm

            // Create an iCalendar
            var iCal = new iCalendar();

            // Create the event
            var evt = iCal.Create<Event>();
            evt.Summary = "Test Event";
            evt.Start = new iCalDateTime(2008, 1, 1, 18, 0, 0); // Starts January 1, 2008 @ 6:00 P.M.
            evt.Duration = TimeSpan.FromHours(1);
            

            // Add a recurrence pattern to the event
            var rp = new RecurrencePattern {Frequency = FrequencyType.Monthly};
            rp.ByDay.Add(new WeekDay(DayOfWeek.Wednesday, FrequencyOccurrence.Last));
            evt.RecurrenceRules.Add(rp);
         
            // #2: Yearly events like holidays that occur on the same day each year.
            // The same as #1, except:
            var rp2 = new RecurrencePattern {Frequency = FrequencyType.Yearly};
            evt.RecurrenceRules.Add(rp2);

            // #3: Yearly events like holidays that occur on a specific day like the first monday.
            // The same as #1, except:
            var rp3 = new RecurrencePattern {Frequency = FrequencyType.Yearly};
            rp3.ByMonth.Add(3);
            rp3.ByDay.Add(new WeekDay(DayOfWeek.Monday, FrequencyOccurrence.First));
            evt.RecurrenceRules.Add(rp3);



            /*
            Note that all events occur on their start time, no matter their
            recurrence pattern. So, for example, you could occur on the first Monday
            of every month, but if your event is scheduled for a Friday (i.e.
            evt.Start = new iCalDateTime(2008, 3, 7, 18, 0, 0)), then it will first
            occur on that Friday, and then the first Monday of every month after
            that.
             
             this can be worked around by doing this:
             IPeriod nextOccurrence = pattern.GetNextOccurrence(dt);
             evt.Start = nextOccurrence.StartTime;
             */
        }
    }
}