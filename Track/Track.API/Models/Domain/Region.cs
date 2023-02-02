﻿
namespace Track.API.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }
        public double Area { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public double   Population { get; set; }

        //Reference
        public IEnumerable<Walk> Walks { get; set; }

    }
}
