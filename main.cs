using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Crossinform
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sWatch = new Stopwatch();
            var threeLetters = new List<string>();

            Console.Write("Укажите путь к файлу:");
            string adress = Console.ReadLine(); //запрашиваем путь к файлу
            sWatch.Start();
            string test = ReadFile(adress); //метод счиывает текст из файла и возвращает как переменную типа string
            threeLetters = Combinations(test); //делим текст на слова, находим все возможные комбинации триплетов, записываем их в list
            var result = threeLetters.Where(l => l.All(chr => char.IsLetter(chr))).GroupBy(l => l).OrderByDescending(l => l.Count()).Take(10).Select(l => l.Key); //группируем, сортируем и оставляем только топ-10 триплетов
            PrintRes(result); //выводим результат
            sWatch.Stop();
            Console.WriteLine("\n" + sWatch.ElapsedMilliseconds.ToString());
            Console.ReadLine();
        }
        static string ReadFile(string adress)
        {
            FileStream file1 = new FileStream(adress, FileMode.Open);
            StreamReader reader = new StreamReader(file1); 
            string test = reader.ReadToEnd(); 
            reader.Close();
            return test;
        }

        static List<string> Combinations(string fileText)
        {
            char[] splitChars = { ' ', '-', ',', '.', ':', '?', '!', '\t', '\n' };
            var letters = new List<string>();

            string[] words = fileText.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                if (word.Length > 2)
                {
                    for (int i = 3; i <= word.Length; i++)
                    {
                        letters.Add(word.Substring(i - 3, 3));
                    }
                }
            }
            return letters;
        }

        static void PrintRes(IEnumerable<string> res)
        {
            int a = 0;
            foreach (var i in res)
            {
                System.Console.Write(i);
                if (a < 9)
                    System.Console.Write(", ");
                a++;
            }
        }
    }
}
