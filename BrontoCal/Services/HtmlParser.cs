using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

/*
<div style="clear: right;" class="akce even"> 

<h3>
<a href="/co-delame/kalendar-akci/detail/6863" title="Podzimní IMPROCAMP - Podrobnosti a přihlášení"> 
Podzimní IMPROCAMP
</a> 
<img src="http://brontosaurus.cz/plugins/content/bis/images/nic_bront.png" alt="Program " title="Program " width="60" class="ikona-pr"> </h3>

<p class="datum">30.10 - 2.11.2014</p>

<div class="seznam_text">
 
<p>Aplikovaná improvizace aneb jak impro pomáhá. Workshop pro zkušené i začínající lektory ...</p>
<p>
<strong>Typ akce:</strong> Vzdělávací
<strong>Věk:</strong> neomezeno 
<strong>Místo:</strong> Švýcárna
<strong>Účastnický poplatek (Kč):</strong> 2900
</p>
<hr class="cleaner">
 
</div>
</div>
*/

namespace Lansky.BrontoCal.Services
{
	public class HtmlParser
	{
		public class BrontoEvent
		{
			public DateTime From { get; set; }

			public DateTime To { get; set; }

			public string Name { get; set; }

			public string Description { get; set; }
		}

		public readonly string ListUrl = "http://brontosaurus.cz/co-delame/kalendar-akci/";

		public IEnumerable<BrontoEvent> GetEvents()
		{
			var listHtml = System.Text.Encoding.UTF8.GetString(
				new WebClient().DownloadData(this.ListUrl));
			
			var listTree = new HtmlDocument();
			listTree.LoadHtml(listHtml);

			foreach (var node in listTree
				.DocumentNode
				.SelectNodes("//div[starts-with(@class, 'akce')]"))
			{
				var dateInterval = node.SelectSingleNode("p[@class='datum']").InnerText;

				if (dateInterval.IndexOf('-') == -1) continue;

				var from = dateInterval.Split('-')[0].Trim();
				var to = dateInterval.Split('-')[1].Trim();

				yield return new BrontoEvent {
					Name = node.SelectSingleNode("h3/a").InnerText
				};
			}
		}
	}
}