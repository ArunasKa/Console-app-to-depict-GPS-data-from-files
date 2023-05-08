using GpsDataRepresentation.GpsDataRepresentation.BL.Interfaces;
using GpsDataRepresentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GpsDataRepresentation.GpsDataRepresentation.BL.Services
{
    public class DataRepresentationService : IDataRepresentationService
    {
        public DataRepresentationService()
        {

        }
        private SortedDictionary<int, int> CountRecurrence(List<int> itemList)
        {
            SortedDictionary<int, int> RecurrenceList = new SortedDictionary<int, int>();
            foreach (int item in itemList)
            {
                if (RecurrenceList.ContainsKey(item))
                {
                    RecurrenceList[item]++;
                }
                else
                {
                    RecurrenceList[item] = 1;
                }
            }
            return RecurrenceList;
        }
        private SortedDictionary<int, int> CountRecurrenceOfSpeed(List<int> itemList)
        {
            SortedDictionary<int, int> RecurrenceList = new SortedDictionary<int, int>();

           for(var i = 0; i < itemList.Max(); i += 10)
            {
                var count = itemList.Count(n => n > i && n < i + 10);
                if (count > 0)
                {
                    RecurrenceList[i]= count;
                }
            }
            return RecurrenceList;
        }
        public void MakeSateliteHistogram(List<int> SateliteList)
        {
            var SateliteReacurance = CountRecurrence(SateliteList);


            var maxSateliteCount = SateliteReacurance.Values.Max();
            var values = SateliteReacurance.Values.ToList();

            for(var i = 0; i < values.Count;i++)
            {
                values[i] = values[i]/ 100 < 1 ? 1 : values[i] / 100;
            }

            for (int i = maxSateliteCount / 99; i > 0; i--)
            {
                for (int j = 0; j < SateliteReacurance.Count; j+=1)
                {
                    Console.Write(values[j] >= i ? "███" : "   ");
                }

                Console.WriteLine();
            }
            var keys = SateliteReacurance.Keys.ToList();

                foreach (var key in keys)
                {
                  Console.Write(key < 10 ? "0"+ key + " " : key + " ");
                }

        }
        public void MakeSpeedHistogram(List<int> SpeedList)
        {
            var SpeedReacurance = CountRecurrenceOfSpeed(SpeedList);

            var values = SpeedReacurance.Values.ToList();
            var Keys = SpeedReacurance.Keys.ToList();
            Console.WriteLine();
            for (var i  = 0; i< values.Count; i++)
            {

                Console.Write($"{"[" + Keys[i] + " - " + (Keys[i] + 10) + "]" + " | ",15}");
                Console.Write(new string('█', values[i] / 100 < 1 ? 1 : values[i] / 100));
                Console.WriteLine(" | " + values[i]);

            }
        }
        public GpsData[] ReadJSON()
        {
            return JsonSerializer.Deserialize<GpsData[]>(File.ReadAllText(@"C:\Users\aruna\Desktop\C# užduotis/2019-07.json"));
        }
        public GpsData[] ReadCsv()
        {
            return JsonSerializer.Deserialize<GpsData[]>(File.ReadAllText(@"C:\Users\aruna\Desktop\C# užduotis/2019-07.json"));
        }

        public List<int> SortOutSatelites(GpsData[] GpsData)
        {
            var SateliteList = new List<int>();
            foreach (var elem in GpsData)
            {
                SateliteList.Add(elem.Satellites);
            }
            return SateliteList;
        }

        public List<int> SortOutSpeed(GpsData[] GpsData)
        {
            var SateliteList = new List<int>();
            foreach (var elem in GpsData)
            {
                SateliteList.Add(elem.Speed);
            }
            return SateliteList;

        }

       
    }
}
