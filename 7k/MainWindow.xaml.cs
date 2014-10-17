using _7k.Model.ContextElement.Task;
using _7k.Model.ContextElement.Task.InnerDotNet;
using _7k.UserInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _7k
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AbstractTask at = new IdnRemoveEmptyParagraphBeforeAndAfterStyles();
        }

        private void OnClickNew(object sender, RoutedEventArgs args)
        {
            MyDesignerCanvas.Children.Clear();
        }

        private void OnClickRotateLeft(object sender, RoutedEventArgs args)
        {
            Rotate(-90);
        }

        private void OnClickRotateRight(object sender, RoutedEventArgs args)
        {
            Rotate(90);
        }

        // sort of a hack, only values of 90 (right) or -90 (left) make sense
        // for demo purposes only
        private void Rotate(double angle)
        {
            foreach (DesignerItem item in MyDesignerCanvas.SelectedItems)
            {
                FrameworkElement element = item.Content as FrameworkElement;
                if (element != null)
                {
                    RotateTransform rotateTransform = element.LayoutTransform as RotateTransform;
                    if (rotateTransform == null)
                    {
                        rotateTransform = new RotateTransform();
                        element.LayoutTransform = rotateTransform;
                    }

                    rotateTransform.Angle = (rotateTransform.Angle + angle) % 360;
                    Canvas.SetLeft(item, Canvas.GetLeft(item) - (item.Height - item.Width) / 2);
                    Canvas.SetTop(item, Canvas.GetTop(item) - (item.Width - item.Height) / 2);
                    double width = item.Width;
                    item.Width = item.Height;
                    item.Height = width;
                }
            }
        }

        private void onDragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            Thumb thumb = e.Source as Thumb;

            double left = Canvas.GetLeft(thumb) + e.HorizontalChange;
            double top = Canvas.GetTop(thumb) + e.VerticalChange;

            Canvas.SetLeft(thumb, left);
            Canvas.SetTop(thumb, top);

            if (left < 1) Debug.WriteLine(left);

            // Update lines's layouts
            //UpdateLines(thumb);     
        }
    }
}
