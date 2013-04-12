using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DDay.iCal;

namespace DDayIcalRecurrence
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Submit_click(object sender, EventArgs e)
        {
            
            var allDays = new List<IWeekDay>()
                              {
                                  new WeekDay(DayOfWeek.Sunday),
                                  new WeekDay(DayOfWeek.Monday),
                                  new WeekDay(DayOfWeek.Tuesday),
                                  new WeekDay(DayOfWeek.Wednesday),
                                  new WeekDay(DayOfWeek.Thursday),
                                  new WeekDay(DayOfWeek.Friday),
                                  new WeekDay(DayOfWeek.Saturday),
                              };

            var weekdays = allDays.Where(d => d.DayOfWeek != DayOfWeek.Saturday && d.DayOfWeek != DayOfWeek.Sunday).ToList();

            var iCal = new iCalendar();
            var evt = iCal.Create<Event>();
            evt.Summary = title.Value;
            evt.Start = new iCalDateTime(DateTime.Parse(start.Value));
            evt.End = new iCalDateTime(DateTime.Parse(end.Value));

            var rp = new RecurrencePattern();
            switch (freq.Value)
            {
                case "DAILY":
                    rp = new RecurrencePattern { Frequency = FrequencyType.Daily };
                    
                    if (repeat.SelectedValue == "everyWeekday")
                    {
                        rp.ByDay = weekdays;
                    }
                    else if (repeat.SelectedValue == "everyDay")
                    {
                        rp.ByDay = allDays;
                    }
                    
                    break;
                case "WEEKLY":
                    rp = new RecurrencePattern { Frequency = FrequencyType.Weekly };
                    rp.Interval = Int32.Parse(interval.Value);
                    foreach (var item in byweekday.Items)
                    {
                        var listItem = item as ListItem;
                        if (listItem.Selected)
                        {
                            rp.ByDay.Add(new WeekDay(listItem.Value));
                        }
                    }
                    break;
            }

            rp.Until = DateTime.Parse(until.Value);

            evt.RecurrenceRules.Add(rp);
            
            occurrences.InnerText = "";
            var occurrenceDates = evt.GetOccurrences(evt.Start.Date, rp.Until);

            foreach (var occ in occurrenceDates)
            {
                occurrences.Controls.Add(new HtmlGenericControl("div"){ InnerText = occ.ToString()});
            }
            
        }
    }
}
