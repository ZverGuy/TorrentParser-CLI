using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace TorrentParser_CLI
{
    class Program
    {
        static int SearchInt { get; set; }
        static int DelayInt { get; set; }
        static void Main(string[] args)
        {


            Console.WriteLine("Напишите поисковый запрос в TorLook!!!");

            string Search = Console.ReadLine();


            Console.WriteLine("Введмте Кол-во результатов");


            string SearchCount = Console.ReadLine();


            Console.WriteLine("Введмте Задержку (в МС)");


            string Delay = Console.ReadLine();

            TorrentParser torrentParser = new TorrentParser();
            torrentParser.baseurl = "https://torlook.info/";
            string LinkedUrl = torrentParser.FromNameToUrl("https://torlook.info/", Search);

            

            var doc = torrentParser.GetHtmlDocFromUrl(LinkedUrl);

            var collection = torrentParser.GetResultDivCollection(doc);
            try
            {
                SearchInt = int.Parse(SearchCount);
                DelayInt = int.Parse(Delay);

            }
            catch(FormatException)
            {
                Console.WriteLine("Необходимо писать цифрами, а не буквами!");
                Console.WriteLine("Нажмите любую кнопку для завершения работы");
                Console.ReadKey();
                Environment.Exit(0);
            }

            try
            {
                for (int i = 1; i < SearchInt; i++)
                {


                    //Сбор Данных
                    var name = doc.DocumentNode.SelectSingleNode("//*[@id=\"resultsDiv\"]/*[contains(@class, \"webResult item\")][" + i + "]/p/a").InnerText;
                    var UrlToTorrent = doc.DocumentNode.SelectSingleNode("//*[@id=\"resultsDiv\"]/*[contains(@class, \"webResult item\")][" + i + "]/p/a").Attributes["href"].Value;
                    var size = doc.DocumentNode.SelectSingleNode("//*[@id=\"resultsDiv\"]/*[contains(@class, \"webResult item\")][" + i + "]/*[@class=\"h2\"]/*[@class=\"webResultTitle\"]/*[@class=\"size\"]").InnerText.Replace("&nbsp;", " ");
                    var seeders = doc.DocumentNode.SelectSingleNode("//*[@id=\"resultsDiv\"]/*[contains(@class, \"webResult item\")][" + i + "]/*[@class=\"h2\"]/*[@class=\"webResultTitle\"]/*[@class=\"torstat\"]/*[@class=\"seeders\"]").InnerText;
                    var leechers = doc.DocumentNode.SelectSingleNode("//*[@id=\"resultsDiv\"]/*[contains(@class, \"webResult item\")][" + i + "]/*[@class=\"h2\"]/*[@class=\"webResultTitle\"]/*[@class=\"torstat\"]/*[@class=\"leechers\"]").InnerText;
                    var magneturl = torrentParser.GetMagneUrlFromHtmlDoc(doc, i);
                    Console.WriteLine("Name: " + name);
                    Console.WriteLine("Url : " + UrlToTorrent);
                    Console.WriteLine("Size: " + size);
                    Console.WriteLine("Seeders: " + seeders);
                    Console.WriteLine("Leechers: " + leechers);
                    Console.WriteLine("Magnet: " + magneturl);
                    //Вывод Данный
                    Console.WriteLine("#######################################################################");
                    Thread.Sleep(DelayInt);



                }
            }catch(NullReferenceException)
            {
                Console.WriteLine("Невозможно достать информацию, наверное конец списка");
            }
            
            Console.WriteLine("Парсинг закончен!!! Нажмите Enter для выхода!");
            while(true)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
