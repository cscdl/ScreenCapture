using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScreenCapture
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CaptureScreenshot(object sender, RoutedEventArgs e)
        {
            // Get the dimensions of the main window
            var target = this;
            var width = (int)target.ActualWidth;
            var height = (int)target.ActualHeight;

            // Create a render target
            var renderTarget = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);

            // Render the visual (main window)
            renderTarget.Render(target);

            // Save the bitmap to a file in D:\Pictures
            string filePath = @"D:\Pictures\screenshot.png";
            SaveBitmapToFile(renderTarget, filePath);

            MessageBox.Show($"Screenshot saved as {filePath}");
        }

        private void SaveBitmapToFile(RenderTargetBitmap bitmap, string filename)
        {
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            using (var stream = new FileStream(filename, FileMode.Create))
            {
                encoder.Save(stream);
            }
        }
    }
}
