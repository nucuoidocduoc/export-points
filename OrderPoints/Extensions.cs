using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System.Collections.Generic;

namespace OrderPoints
{
    public static class Extensions
    {
        public static List<T> ToList<T>(this SelectionSet selectionSet)
        {
            var listElement = new List<T>();
            foreach (T element in selectionSet)
            {
                listElement.Add(element);
            }
            return listElement;
        }

        public static bool IsPointOnPolyline(this Polyline pl, Point3d pt)
        {
            bool isOn = false;

            for (int i = 0; i < pl.NumberOfVertices; i++)
            {
                Curve3d seg = null;

                var segType = pl.GetSegmentType(i);

                if (segType == SegmentType.Arc)
                {
                    seg = pl.GetArcSegmentAt(i);
                }
                else if (segType == SegmentType.Line)
                {
                    seg = pl.GetLineSegmentAt(i);
                }

                if (seg != null)
                {
                    isOn = seg.IsOn(pt);

                    if (isOn)
                    {
                        break;
                    }
                }
            }

            return isOn;
        }

        public static bool IsPointOnLine(this Line pl, Point3d pt)
        {
            return pl.GetGeCurve().IsOn(pt);
        }
    }
}