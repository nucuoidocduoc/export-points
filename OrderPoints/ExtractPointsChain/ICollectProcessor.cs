using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using OrderPoints.ExtractPointsChain.Data;
using System.Linq;

namespace OrderPoints.ExtractPointsChain
{
    public interface ICollectProcessor : IPointProcessor
    {
    }

    public class CollectProcessor : PointProcessorBase, ICollectProcessor
    {
        public override void Implement(PointProcessContext processContext)
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;
            var editor = doc.Editor;

            using (Transaction acTrans = db.TransactionManager.StartTransaction())
            {
                // Request for objects to be selected in the drawing area
                var acSSPrompt = editor.SelectAll();

                // If the prompt status is OK, objects were selected
                if (acSSPrompt.Status == PromptStatus.OK)
                {
                    var selectedObjects = acSSPrompt.Value.ToList<SelectedObject>();
                    var entities = selectedObjects.Select(x => acTrans.GetObject(x.ObjectId, OpenMode.ForRead));

                    processContext.Paths = entities.Where(x => x is Polyline || x is Line).Select(x => x as Curve).ToList();
                    processContext.Circles = entities.Where(x => x is Circle).Select(x => x as Circle).ToList();

                    _next?.Implement(processContext);
                    acTrans.Abort();
                }

                // Dispose of the transaction
            }
        }
    }
}