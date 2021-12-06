using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace FinalDerivco
{
    
    class Program
    {
        public static string log = "";
        public static string csv = "There is no csv file located in the directory path , restart application with a csv and change the filepath on line 225 of the program";
        static void Main(string[] args)
        {

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            Thread.Sleep(5000);
            
            var sentence =  AcceptTwoNames();
            int[] occurence = removeDuplicatesAndDisplay(sentence.Item1);

            var data = GoodMatch(occurence, sentence.Item2, sentence.Item3);

            ReadCSVFile();
            Console.WriteLine(csv);
            stopwatch.Stop();
            log = log +"Time take to run application "  + Convert.ToString(stopwatch.ElapsedMilliseconds) + "ms \n";
            _ = ExampleAsyncLog(log);

            Console.ReadLine();
            
        }

        public static int[] subCalc(int[] arg)
        {
            int counter = arg.Length;

            for(int i = 0; i < arg.Length; i++)
            {
                if (arg[i] > 9)
                {
                    counter++;
                }
               
            }

            int[] array = new int[counter];
            int counter2 = 0;
            for(int j = 0; j < arg.Length; j++)
            {
                if (arg[j] > 9)
                {
                    int value1 = arg[j] / 10;
                    int value2 = arg[j] % 10;

                    array[j] = value1;
                    array[j+1] = value2;
                    counter2++;
                }
                else
                {
                    array[j + counter2] = arg[j];
                }
            }
           
            return array;
        }

        public static (int[],string,int) GoodMatch(int[] arr, string p1, string p2)
        {
            int lenght = arr.Length;
            int counter = 0;
            string resultMatch;
            int perc = 0;

            string personA = p1;
            string personB = p2;

            int[] sumOfArray = new int[counter];
            int total = 0;
            
            for (int j = 0; j < arr.Length; j++)
            {
                if (arr[j] > 9)
                {
                    int[] rematch = subCalc(arr);
                    List<int> list = rematch.ToList();
                    Console.WriteLine(String.Join(",", list));
                    return GoodMatch(rematch, personA, personB);
                }
                
            }

            if (lenght != 2)
            {
                if (lenght % 2 == 0)
                {
                    counter = lenght / 2;
                    int[] sumOfArrays = new int[counter];
                    int arLast = lenght;
                    for (int i = 0; i < counter; i++)
                    {
                        if (i <= counter - 1)
                        {
                            total = arr[i] + arr[arLast - 1];
                            sumOfArrays[i] = total;
                            arLast--;
                        }
                    }
                    sumOfArray = sumOfArrays;
                    List<int> list = sumOfArray.ToList();
                    Console.WriteLine(String.Join(",", list));
                    return GoodMatch(sumOfArray, personA, personB);

                }
                else
                {
                    counter = (lenght / 2) + 1;
                    int[] sumOfArrayss = new int[counter];
                    int arLast = lenght;
                    for (int i = 0; i < counter; i++)
                    {
                        if (i < (counter - 1))
                        {
                            total = arr[i] + arr[arLast - 1];
                            sumOfArrayss[i] = total;
                            arLast--;
                        }
                        if (i == (counter - 1))
                        {
                            sumOfArrayss[i] = arr[counter - 1];
                        }
                    }
                    sumOfArray = sumOfArrayss;
                    List<int> list = sumOfArray.ToList();
                    Console.WriteLine(String.Join(",", list));
                    return GoodMatch(sumOfArray, personA, personB);
                }

               

            }
            else
            {
                
                string percent = Convert.ToString(arr[0]) + Convert.ToString(arr[1]);
                perc = Convert.ToInt32(percent);
                if (perc >= 80)
                {
                    percent = Convert.ToString(perc) + "%" + ", Good Match";
                }
                else
                {
                    percent = Convert.ToString(perc) + "%";
                }
                resultMatch = Convert.ToString(personA + " matches " + personB + " " + percent);
     
                
                Console.WriteLine(resultMatch);
                return (sumOfArray= arr,resultMatch,perc);

            }

         
        }
       
        public static int[] removeDuplicatesAndDisplay(string sentence)
        {
            string result = String.Empty;

            ////Remove Duplicates
            for (int i = 0; i < sentence.Length; i++)
            {
                if (!result.Contains(sentence[i]))
                {
                    result += sentence[i];
                }

            }

            int amount = result.Length;
            int[] array = new int[amount];

            int count = 0;

            //Display the occurances of duplicates
            for (int i = 0; i < result.Length; i++)
            {
                for (int j = 0; j < sentence.Length; j++)
                {
                    if (result[i] == sentence[j])
                    {
                        count++;
                    }

                }
                array[i] = Convert.ToInt32(count);
                Console.WriteLine(sentence[i] + " occurs " + count + " times");
                count = 0;
            }

            return array;
        }

        public static (string,string,string) AcceptTwoNames( )
        {
            //Input Name 1
            Console.WriteLine("What is the first name?");
            string name1 = Console.ReadLine();
            if (!name1.All(char.IsLetter))
            {
                Console.WriteLine("Please Enter a valid letter");
                log = log + "Please Enter a valid letter \n";
                return AcceptTwoNames(); 
            }

            Console.WriteLine();

            //Input Name 2
            Console.WriteLine("What is the second name?");
            string name2 = Console.ReadLine();
            if (!name2.All(char.IsLetter))
            {
                Console.WriteLine("Please Enter a valid letter");
                log = log + "Please Enter a valid letter \n";
                return AcceptTwoNames();
            }

            Console.WriteLine();

            string sentence = name1 + "matches" + name2;

            return (sentence.ToLower(), name1, name2);
        }

        static void ReadCSVFile()
        {
            var lines = File.ReadAllLines(@"C:\Users\desha\source\repos\FinalDerivco\FinalDerivco\newTest.csv");
            var list = new List<CsvInfo>();
            csv = "";

            foreach (var line in lines)
            {

                var values = line.Split(',');
                if (values.Length == 1)
                {
                    Console.WriteLine("There is a field missing in the csv, restart application with a valid csv");
                    log = log + "There is a field missing in the csv,restart application with a valid csv \n";
                    break;

                }
                else if (!values[0].All(char.IsLetter) && !values[1].All(char.IsLetter))
                {
                    Console.WriteLine("Csv must contain only alphabets, a field should have only 'Kimberly, f', restart application with a valid csv");
                    log = log + "A line in the csv does not contain alphabets,restart application with a valid csv \n";
                    break;

                }
                else if (values[0].ToLower() == "null")
                {
                    Console.WriteLine("Csv cannot contain null, restart application with a valid csv");
                    log = log + "The line in the csv does not contain alphabets,restart application with a valid csv \n";
                    break;
                }

                var ind = new CsvInfo() { Name = values[0].ToLower(), Gender = values[1].ToLower() };
                list.Add(ind);
            }

            int womensCount = 0;
            int mensCount = 0;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Gender.Contains('f'))
                {
                    womensCount++;
                }
                else
                {
                    mensCount++;
                }

            }

            string[] men = new string[mensCount];
            string[] women = new string[womensCount];

            int wcount = 0;
            int mcount = 0;


            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Gender.Contains('f') && list[i].Name.All(char.IsLetter))
                {
                    women[wcount] = list[i].Name;
                    wcount++;
                }
                else if (list[i].Gender.Contains('m') && list[i].Name.All(char.IsLetter))
                {
                    men[mcount] = list[i].Name;

                    mcount++;
                }
                else
                {
                    Console.WriteLine("The csv is not compatible");
                    log = log + "The csv does not contain a gender of m or f,use a valid csv file \n";
                    break;
                }
            }


            string[] menResult = men.Distinct().ToArray();
            string[] womenResult = women.Distinct().ToArray();

            string sentence;


            List<Order> resultSet = new List<Order>();

            for (int m = 0; m < menResult.Length; m++)
            {
                for (int w = 0; w < womenResult.Length; w++)
                {
                    sentence = menResult[m] + "matches" + womenResult[w];
                    int[] occurence = removeDuplicatesAndDisplay(sentence);
                    var data = GoodMatch(occurence, menResult[m], womenResult[w]);
                    Order order = new Order();
                    order.Percent = data.Item3;
                    order.Result = data.Item2;
                    resultSet.Add(order);
                }
            }

            List<Order> res = resultSet.OrderBy(z => z.Result).OrderByDescending(x => x.Percent).ToList();

            Console.WriteLine();
            string finalRes = "";
            foreach (var item in res)
            {
                finalRes = finalRes + item.Result + "\n";
            }


            Example(finalRes);
        }

        public static void Example(string value)
        {
            string text = value + "\n";
            string path = @"C:\Users\desha\source\repos\FinalDerivco\FinalDerivco\output.txt";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            // Create a new file     
            using (FileStream fs = File.Create(path))
            {

            }

            File.WriteAllText(path, text);

            // Open the stream and read it back.    
            using (StreamReader sr = File.OpenText(path))
            {

                while ((text = sr.ReadLine()) != null)
                {
                    Console.WriteLine(text);
                }
            }

            Console.WriteLine("Navigate to " + path + " to open the output file");
        }

        public static async Task ExampleAsyncLog(string value)
        {
            string text = value + "\n";
            string path = @"C:\Users\desha\source\repos\FinalDerivco\FinalDerivco\log.txt";

            await File.AppendAllTextAsync(path, text);

            Console.WriteLine("Navigate to " + path + " to open the log file.");


        }
    }

    public class CsvInfo
    {
        public string Name { get; set; }

        public string Gender { get; set; }
    }

    public class Order 
    {
        public int Percent { get; set; }

        public string Result { get; set; }
    }
}
