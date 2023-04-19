using Autodesk.AutoCAD.DatabaseServices;
using OrderPoints.ExtractPointsChain.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace OrderPoints.ExtractPointsChain
{
    public interface IClassificationProcessor : IPointProcessor
    {
    }

    public class ClassificationProcessor : PointProcessorBase, IClassificationProcessor
    {
        public override void Implement(PointProcessContext processContext)
        {
            if (processContext.Classifications == null)
            {
                processContext.Classifications = new List<Classification<Curve, Circle>>();
            }

            if (processContext.Paths == null || !processContext.Paths.Any())
            {
                MessageBox.Show("Không tìm thấy path");
                return;
            }
            foreach (var path in processContext.Paths)
            {
                if (path is Line line)
                {
                    var circlesMatch = processContext.Circles.Where(x => line.IsPointOnLine(x.Center)).OrderBy(x => line.GetDistAtPoint(x.Center)).ToList();
                    if (circlesMatch.Any())
                    {
                        processContext.Classifications.Add(new Classification<Curve, Circle>
                        {
                            Path = line,
                            Elements = circlesMatch
                        });
                    }
                }
                else if (path is Polyline pl)
                {
                    var circlesMatch = processContext.Circles.Where(x => pl.IsPointOnPolyline(x.Center)).OrderBy(x => pl.GetDistAtPoint(x.Center)).ToList();
                    if (circlesMatch.Any())
                    {
                        processContext.Classifications.Add(new Classification<Curve, Circle>
                        {
                            Path = pl,
                            Elements = circlesMatch
                        });
                    }
                }
            }
            _next?.Implement(processContext);
        }
    }
}