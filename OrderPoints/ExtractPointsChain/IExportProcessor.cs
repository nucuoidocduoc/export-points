using OrderPoints.ExtractPointsChain.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace OrderPoints.ExtractPointsChain
{
    public interface IExportProcessor : IPointProcessor
    {
    }

    public class ExportProcessor : PointProcessorBase, IExportProcessor
    {
        public override void Implement(PointProcessContext processContext)
        {
            var fullpath = Common.GetFullPathFile("csv");
            if (string.IsNullOrEmpty(fullpath))
            {
                return;
            }
            var csv = new StringBuilder();
            var groupByLayerClassifications = processContext.Classifications.GroupBy(x => x.Path.Layer);

            foreach (var group in groupByLayerClassifications)
            {
                foreach (var classification in group)
                {
                    classification.Elements.ForEach(x => csv.AppendLine(string.Format("{0},{1},{2}", x.Center.X, x.Center.Y, classification.Path.Layer)));
                }
            }

            File.WriteAllText(fullpath, csv.ToString());
            MessageBox.Show("Export hoàn tất");
        }
    }
}