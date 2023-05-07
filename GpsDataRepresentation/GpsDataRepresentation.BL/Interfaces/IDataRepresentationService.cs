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
        void MakeSateliteHistogram(List<int> SateliteList);
        List<int> SortOutSatelites(GpsData[] GpsData);
    }
}
