using GpsDataRepresentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpsDataRepresentation.GpsDataRepresentation.BL.Interfaces
{
    public interface IDataRepresentationService
    {
        GpsData[] ReadJSON();
        GpsData[] ReadCsv();
        void MakeSateliteHistogram(List<int> SateliteList);
        void MakeSpeedHistogram(List<int> SpeedList);
        List<int> SortOutSatelites(GpsData[] GpsData);
        List<int> SortOutSpeed(GpsData[] GpsData);
    }
}
