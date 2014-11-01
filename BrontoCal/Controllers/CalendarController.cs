using Lansky.BrontoCal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
		public IEnumerable<HtmlParser.BrontoEvent> Get()
		{
			return this._parser.GetEvents();
		}
	}
}