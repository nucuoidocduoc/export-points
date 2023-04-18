using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using System.IO;
using System.Linq;

namespace OrderPoints
{
    public class Command
    {
        [CommandMethod("ExportPoints")]
        public void ExportPoints()
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;
            var editor = doc.Editor;

            using (Transaction acTrans = db.TransactionManager.StartTransaction())
            {
                // Request for objects to be selected in the drawing area
                PromptSelectionResult acSSPrompt = editor.GetSelection();

                // If the prompt status is OK, objects were selected
                if (acSSPrompt.Status == PromptStatus.OK)
                {
                    var selectedObjects = acSSPrompt.Value.ToList<SelectedObject>();
                    var entities = selectedObjects.Select(x => acTrans.GetObject(x.ObjectId, OpenMode.ForWrite));
                    var firstSelectedObject = entities.FirstOrDefault(x => x is Polyline) as Polyline;
                    var circles = entities.Where(x => x is Circle).Select(x => x as Circle).ToList();

                    // Save the new object to the database

                    var pointsOrdered = circles.Select(x => new { x.Center.X, x.Center.Y, Length = firstSelectedObject.GetDistAtPoint(x.Center) }).ToList().OrderBy(x => x.Length);

                    using (StreamWriter writer = new StreamWriter(@"D:\abc.txt"))
                    {
                        foreach (var point in pointsOrdered)
                        {
                            writer.WriteLine($"{point.X}, {point.Y}, {point.Length}");
                        }
                    }
                    acTrans.Commit();
                }

                // Dispose of the transaction
            }
        }
    }
}