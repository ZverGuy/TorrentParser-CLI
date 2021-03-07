using System;
using System.Collections;
using System.Xml;
using HtmlAgilityPack;

public class TorrentParser : Object
{
	public string itemUrl { get; private set; }
	public string itemstring { get; private set; }

	public string FromNameToUrl(string baseUrl, string Search)
	{
		var LinkedUrl = baseUrl + Search;
		return LinkedUrl;
	}
	public HtmlDocument GetHtmlDocFromUrl(string Url)
	{
		HtmlWeb web = new HtmlWeb();
		var HtmlDoc = web.Load(Url);
		return HtmlDoc;
	}

	public HtmlNodeCollection GetResultDivCollection(HtmlDocument doc)
    {
		HtmlNodeCollection Collection = doc.DocumentNode.SelectNodes("//div[@class=\"webResult item\"]");
		return Collection;
    }

		 
}
