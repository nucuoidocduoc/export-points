using Autodesk.AutoCAD.DatabaseServices;
using System.Collections.Generic;

namespace OrderPoints.ExtractPointsChain.Data
{
    public class PointProcessContext
    {
        public List<Curve> Paths { get; set; }

        public List<Circle> Circles { get; set; }

        public List<Classification<Curve, Circle>> Classifications { get; set; }
    }
}