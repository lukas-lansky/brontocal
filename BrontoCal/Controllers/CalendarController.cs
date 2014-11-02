using Lansky.BrontoCal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Lansky.BrontoCal.Controllers
{
	public class CalendarController : ApiController
	{
		private readonly HtmlParser _parser;
		private readonly ICalFormatter _formatter;

		public CalendarController(HtmlParser parser, ICalFormatter formatter)
		{
			if (parser == null || formatter == null)
			{
				throw new ArgumentException();
			}

			this._parser = parser;
			this._formatter = formatter;
		}

		[Route("Calendar")]
		public HttpResponseMessage Get()
		{
			var events = this._parser.GetEvents();

			var icalString = this._formatter.GetICal(
				events.Select(e => new ICalFormatter.VEvent {
					Summary = e.Name,
					Description = e.Description,
					Start = e.From,
					End = e.To }));

			var stringContent = new StringContent(icalString);
			stringContent.Headers.ContentType.MediaType = "text/plain";
			return new HttpResponseMessage(HttpStatusCode.OK) { Content = stringContent };
		}
	}
}