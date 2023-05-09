using Geolocation;
using GpsDataRepresentation.GpsDataRepresentation.BL.Interfaces;
using GpsDataRepresentation.Models;
using System.Text.Json;

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
            Console.WriteLine();
            for (int i = maxSateliteCount / 100; i > 0; i--)
            {
                
                for (int j = 0; j < SateliteReacurance.Count; j+=1)
                {
                    if (i == maxSateliteCount / 100 && j == SateliteReacurance.Count-1)
                    {
                        Console.Write("    "+maxSateliteCount+"  hits");
                    }
                    Console.Write(values[j]/100 >= i-1 ? "███" : "   ");
                    if (i == 1 && j == SateliteReacurance.Count - 1)
                    {
                        Console.Write(" 0  hits");

                    }


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
        public List<GpsData> ReadJSON()
        {
            return JsonSerializer.Deserialize<List<GpsData>>(File.ReadAllText(@"C:\Users\aruna\Desktop\C# užduotis/2019-07.json"));
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
        public List<int> SortOutSatelites(List<GpsData> GpsData)
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
        public void GetRoadSection(List<GpsData> fileJson, List<GpsData> fileCsv)
        {
            var file = fileCsv.Concat(fileJson).ToList();
            var TempStartPoint = new GpsData();
            var TempEndPoint = new GpsData();
            var startPoint = new GpsData();
            var endPoint = new GpsData();
            double distance = 0;
            double TempDistance = 0;
            double averageSpeed = file.Select(x=>x.Speed).Max();
            List<int> tripSpeeds= new List<int>();

            for(int i = 6; i < file.Count - 1; i++)
            {
                if (file[i].Satellites == 0)
                {
                    var TempTripTime = TempEndPoint.GpsTime.Subtract(TempStartPoint.GpsTime).TotalSeconds;
                    var TempAverageSpeed = TempDistance / (TempTripTime / 3600);
                    if (TempDistance > 100 && TempAverageSpeed <= averageSpeed)
                    {
                        averageSpeed = TempAverageSpeed;
                        distance = TempDistance;
                        startPoint = new GpsData
                        {
                            Latitude = TempStartPoint.Latitude,
                            Longitude = TempStartPoint.Longitude,
                            GpsTime = TempStartPoint.GpsTime,
                            Speed = TempStartPoint.Speed,
                            Angle = TempStartPoint.Angle,
                            Altitude = TempStartPoint.Altitude,
                            Satellites = TempStartPoint.Satellites,

                        };
                        endPoint = new GpsData
                        {
                            Latitude = TempEndPoint.Latitude,
                            Longitude = TempEndPoint.Longitude,
                            GpsTime = TempEndPoint.GpsTime,
                            Speed = TempEndPoint.Speed,
                            Angle = TempEndPoint.Angle,
                            Altitude = TempEndPoint.Altitude,
                            Satellites = TempEndPoint.Satellites,

                        };
                        
                        tripSpeeds.Clear();
                    }
                    TempStartPoint = new GpsData
                    {
                        Latitude = file[i].Latitude,
                        Longitude = file[i].Longitude,
                        GpsTime = file[i].GpsTime,
                        Speed = file[i].Speed,
                        Angle = file[i].Angle,
                        Altitude = file[i].Altitude,
                        Satellites = file[i].Satellites,

                    };
                    TempDistance = 0;
                }
                else
                {
                    TempEndPoint = new GpsData
                    {
                        Latitude = file[i].Latitude,
                        Longitude = file[i].Longitude,
                        GpsTime = file[i].GpsTime,
                        Speed = file[i].Speed,
                        Angle = file[i].Angle,
                        Altitude = file[i].Altitude,
                        Satellites = file[i].Satellites,

                    };
                    TempDistance += GeoCalculator.GetDistance(file[i].Latitude, file[i].Longitude, file[i + 1].Latitude, file[i + 1].Longitude, 3)* 1.60934;
                    tripSpeeds.Add(file[i].Speed);
                }
            }

            var tripTime = endPoint.GpsTime.Subtract(startPoint.GpsTime).TotalSeconds;
            averageSpeed = distance / (tripTime/3600);
            Console.WriteLine();
            Console.WriteLine($"Fastest road section of at least 100km was driven over {tripTime}s and was {String.Format("{0:0.00}", distance)}km long" );
            Console.WriteLine($"Start position {startPoint.Latitude}; {startPoint.Longitude}");
            Console.WriteLine($"Start gps time {startPoint.GpsTime}" );
            Console.WriteLine($"End position {endPoint.Latitude};{endPoint.Longitude}" );
            Console.WriteLine($"End gps time {endPoint.GpsTime}" );
            Console.WriteLine($"Average speed: {String.Format("{0:0.00}", averageSpeed)}km /h" );
        }

    }
}
