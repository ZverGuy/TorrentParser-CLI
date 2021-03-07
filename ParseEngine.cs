using System;
using System.Collections;
using System.Xml;
using HtmlAgilityPack;

public class TorrentParser : Object
{
	public string itemUrl { get; private set; }
	public string itemstring { get; private set; }
	XmlNamespaceManager oManager = new XmlNamespaceManager(new NameTable());

	public string FromNameToUrl(string baseUrl, string Search)
	{
		var LinkedUrl = baseUrl + Search;
		return LinkedUrl;
	}
	public HtmlDocument GetHtmlDocFromUrl(string Url)
	{
		HtmlWeb web = new HtmlWeb();
		oManager.AddNamespace("ls", Url);
		var HtmlDoc = web.Load(Url);
		return HtmlDoc;
	}
	public void AddXmlNamespace(string url)
	{
		oManager.AddNamespace("ns", url);
		return;
	}
	public HtmlNodeCollection GetResultDivCollection(HtmlDocument doc)
    {
		HtmlNodeCollection Collection = doc.DocumentNode.SelectNodes("//div[@class=\"webResult item\"]");
		return Collection;
    }
	public ArrayList getHrefArray(HtmlNodeCollection collection)
    {
		ArrayList list = new ArrayList();
		foreach (HtmlNode node in collection)
        {
			list.Add(node.SelectSingleNode("//p/a").Attributes["href"].Value);

        }
		return list;

	}
	public string GetItemFromArrayList(ArrayList list )
    {
		foreach ( var item in list)
        {
			itemstring = item.ToString();
        }
		return itemstring;
    }
		 
}
