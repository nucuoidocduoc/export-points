using Autodesk.AutoCAD.EditorInput;
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
    }
}