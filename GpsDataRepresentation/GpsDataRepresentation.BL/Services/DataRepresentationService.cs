﻿using GpsDataRepresentation.GpsDataRepresentation.BL.Interfaces;
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
            var keys = SateliteReacurance.Keys.ToList();

            for (int i = maxSateliteCount / 95; i > 0; i--)
            {
                for (int j = 0; j < SateliteReacurance.Count; j+=1)
                {
                    Console.Write(values[j]/100 >= i-1 ? "███" : "   ");
                }
                Console.WriteLine();
            }

            foreach (var key in keys)
            {
                Console.Write(key < 10 ? "0"+ key + " " : key + " ");
            }
            Console.WriteLine();

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
        public List<GpsData> ReadCsv()
        {
            var GpsData = new List<GpsData>();
            using (var reader = new StreamReader(@"C:\Users\aruna\Desktop\C# užduotis/2019-08.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var values = reader.ReadLine().Split(',');
                    var GpsElement = new GpsData
                    {
                        Latitude = double.Parse(values[0]),
                        Longitude = double.Parse(values[1]),
                        GpsTime = DateTime.Parse(values[2]),
                        Speed = int.Parse(values[3]),
                        Angle = int.Parse(values[4]),
                        Altitude = int.Parse(values[5]),
                        Satellites = int.Parse(values[6]),
                    };
                    GpsData.Add(GpsElement);
                }
            }
            return GpsData;
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
        public List<int> SortOutSpeed(List<GpsData> GpsData)
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
