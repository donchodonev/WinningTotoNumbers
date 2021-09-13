using System;
using System.IO;
using System.Text;
using IronWebScraper;

namespace AykutScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            var scraper = new BlogScraper();
            scraper.Init();
            scraper.Start();
        }
    }

    class BlogScraper : WebScraper
    {
        public override void Init()
        {
            this.Request("http://www.toto.bg/results/6x49", Parse);
        }

        public override void Parse(Response response)
        {
            var result = response.GetElementsByTagName("span");

            foreach (var item in result)
            {
                if (item.ParentNode.ParentNode.ParentNode.TextContentClean.StartsWith("Печеливши числа"))
                {

                    string winningNums = item.ParentNode.ParentNode.ParentNode.TextContentClean + Environment.NewLine;
                    var pathToFile = File.ReadAllText("path to file.txt");

                    Console.Clear();
                    Console.WriteLine($"Зададен път до файла с печеливши числа {pathToFile}");
                    Console.WriteLine();
                    Console.WriteLine("Промени от \"path to file\" файлът в папката на скрипта, ако искаш да го промениш");
                    Console.WriteLine();
                    Console.WriteLine(winningNums);


                    if (!File.Exists(pathToFile))
                    {
                        Console.WriteLine("Такъв път не съществува !");
                        Console.WriteLine();
                        Console.WriteLine("Ще се опитам да го създам");
                        Console.WriteLine();

                        try
                        {
                            File.AppendAllText(pathToFile, winningNums);
                            Console.WriteLine("Готово :)!");
                            Console.WriteLine();
                            Console.WriteLine("Натисни клавиш за приключване");
                            Console.ReadKey();
                            break;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Не става ;(");
                            Console.WriteLine();
                            Console.WriteLine("Натисни клавиш за приключване");
                            Console.ReadKey();
                            break;
                        }
                    }

                    File.AppendAllText(pathToFile, winningNums);

                    Console.WriteLine("Натисни клавиш за приключване");

                    Console.ReadKey();

                    break;
                }
            }
        }
    }
}
