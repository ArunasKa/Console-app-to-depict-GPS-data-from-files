﻿using GpsDataRepresentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpsDataRepresentation.GpsDataRepresentation.BL.Interfaces
{
    public interface IDataRepresentationService
    {
        List<GpsData> ReadJSON();
        List<GpsData> ReadCsv();
        void MakeSateliteHistogram(List<int> SateliteList);
        void MakeSpeedHistogram(List<int> SpeedList);
        List<int> SortOutSatelites(List<GpsData> GpsData);
        List<int> SortOutSpeed(List<GpsData> GpsData);
        void GetRoadSection(List<GpsData> fileJson, List<GpsData> fileCsv);
    }
}