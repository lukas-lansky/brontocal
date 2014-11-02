using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lansky.BrontoCal.Services
{
	public class ICalFormatter
	{
		public class VEvent
		{
			public string Summary { get; set; }

			public string Description { get; set; }

			public DateTime Start { get; set; }

			public DateTime End { get; set; }
		}

		public string GetICal(IEnumerable<VEvent> events)
		{
			return @"BEGIN:VCALENDAR
VERSION:2.0" + events.Select(e => @"
BEGIN:VEVENT
DTSTART:" + e.Start.ToString(@"yyyyMMddTHHmmssZ") + @"
DTEND:" + e.End.ToString(@"yyyyMMddTHHmmssZ") + @"
SUMMARY:" + e.Summary + @"
DESCRIPTION:" + e.Description + @"
END:VEVENT").Aggregate("", (s, e) => s + e) + @"
END:VCALENDAR";
		}
	}
}