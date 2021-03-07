using System;
using System.Xml;
using HtmlAgilityPack;

public class TorrentParser : Object
{
	public FromNameToUrl(string base Url, string Search)
	{
		return url + Search;
	}
	public GetHtmlDocFromUrl(string Url)
    {
		HtmlWeb web = new HtmlWeb();
		var HtmlDoc = web.Load(Url);
		return HtmlDoc;
    }
	
}
