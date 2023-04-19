using System.Windows.Forms;

namespace OrderPoints
{
    public class Common
    {
        public static string GetFullPathFile(string extension)
        {
            var saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = @"C:\",
                Title = $"Save {extension} Files",
                DefaultExt = $"{extension}",
                Filter = $"{extension} files (*.{extension})|*.{extension}",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog.FileName;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}