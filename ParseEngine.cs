using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Xml;
using HtmlAgilityPack;

public class TorrentParser : Object
{
	public string itemUrl { get; private set; }
	public string itemstring { get; private set; }
	public string baseurl {get; set; }



	public string FromNameToUrl(string Baseurl, string Search)
	{
		var LinkedUrl = Baseurl + Search;
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
	public string GetMagneUrlFromHtmlDoc(HtmlDocument doc, int index)
	
    {


		try
		{
			string datasrcformagneturl = doc.DocumentNode.SelectSingleNode("//*[@id=\"resultsDiv\"]/*[contains(@class, \"webResult item\")][" + index + "]/*[@class=\"h2\"]/*[@class=\"webResultTitle\"]/*[@class=\"magnet\"]/a").Attributes["data-src"].Value;
			HtmlWeb web2 = new HtmlWeb();
			string baseurlwithdatasrc = baseurl + datasrcformagneturl;
			string rawmagnet = web2.Load(baseurlwithdatasrc).ParsedText;
			Regex regex = new Regex("href='(.+.)'>");
			string match = regex.Match(rawmagnet).ToString().Replace("href='", "").Replace("'>", "");
			return match;
		} catch(NullReferenceException)
        {
			return String.Empty;
        }

	} 
}
		 
