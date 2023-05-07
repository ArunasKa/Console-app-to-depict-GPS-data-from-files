﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpsDataRepresentation.Models
{
    public class GpsData
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime GpsTime { get; set; }
        public int Speed { get; set; }
        public int Angle { get; set; }
        public int Altitude { get; set; }
        public int Satellites { get; set; }
    }
}