using Autodesk.AutoCAD.Runtime;
using OrderPoints.ExtractPointsChain;
using OrderPoints.ExtractPointsChain.Data;
using System.Windows;

namespace OrderPoints
{
    public class Command
    {
        [CommandMethod("ExportPointCSV")]
        public void ExportPointsWithOutSelect()
        {
            try
            {
                var processorChain = ChainCombiner.ImplementCombine();
                processorChain.Implement(new PointProcessContext());
            }
            catch
            {
                MessageBox.Show("Gặp lỗi không mong muốn");
            }
        }
    }
}