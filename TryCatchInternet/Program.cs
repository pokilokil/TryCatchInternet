using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Xml;

namespace Lesson2

//Задача "Хватит ли мне денег на переезд в Америку ,если я переведу свои рубли в долларовую валюту"
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Курс доллара составляет 
            //курс доллара. Http - http response.
            //double cource = 79.99;
            // GET по адресу:        http://www.cbr.ru/scripts/XML_daily.asp



            try
            {
                string url = "http://www.cbr.ru/scripts/XML_daily.asp";
                //ответ на запрос в Интернет
                WebResponse response = null;
                //для получения ответа
                StreamReader strReader = null;
                //сам запрос по ссылке
                WebRequest request = WebRequest.Create(url);
                //получаем ответ из Интернета с сайта ЦБ
                response = request.GetResponse();
                //открываем ответ на чтение   
                strReader = new StreamReader(response.GetResponseStream());
                //читаем ответ в строку
                string line = strReader.ReadToEnd();
                //нужно закрыть соединение
                //void Close. это процедура. она закрывает наш респонс. никакого результа не возвращает.
                response.Close();
                //теперь есть строка line. надо в ней найти курс Доллара
                //строка - массив символов (есть длина, есть поиск, есть цикл for по массиву..)
                //возможности C# - поиск по строке встроен

                int posUSD = line.IndexOf("USD");//ищем USD
                int positionValue = line.IndexOf("Value", posUSD);
                int posBeginCourse = positionValue + 6;
                string course = line.Substring(posBeginCourse, 7);
                double courceUSD = Convert.ToDouble(course);

                Console.WriteLine(courceUSD);
            }
            catch (Exception ex)
            {
                Save(ex);
                bool ok = SaveF(ex);
                Console.WriteLine("file " + ok);
            }
            Console.ReadKey();
        }
        static void Save(Exception ex)
        {
            string text = "Что-то пошло не так";
            using (StreamWriter sw = File.AppendText("log.txt"))
            {
                sw.WriteLine(text);
                sw.WriteLine(ex.StackTrace, ex.Source, ex.Message);
            }
            Console.WriteLine(text);
        }
        static bool SaveF(Exception ex)
        {
            bool okFile = false;
            string text = "Что-то пошло не так";
            try
            {

           
                using (StreamWriter sw = File.AppendText("log.txt"))
                {
                    sw.WriteLine(text);
                    sw.WriteLine(ex.StackTrace, ex.Source, ex.Message);
                    okFile = true; 
                }
            }
            catch (Exception)
            {

                okFile = false;
            }
            Console.WriteLine(text);
            return okFile;
        }
    }
}