﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace _7k.Control
{
    class ResizeCanvas : Canvas
    {
        protected override Size MeasureOverride(Size constraint)
        {
            Size availableSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
            
            double maxHeight = 0;
            double maxWidth = 0;

            foreach (UIElement element in base.InternalChildren)
            {
                if (element != null)
                {
                    element.Measure(availableSize);

                    double left = Canvas.GetLeft(element);
                    double top = Canvas.GetTop(element);

                    left += element.DesiredSize.Width;
                    top += element.DesiredSize.Height;

                    maxWidth = maxWidth < left ? left : maxWidth;
                    maxHeight = maxHeight < top ? top : maxHeight;
                }
            }

            return new Size { Height = maxHeight, Width = maxWidth };
        }
    }
}
