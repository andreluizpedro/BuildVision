﻿using System.Windows;

namespace BuildVision.UI.Controls.Indicators
{
    public class UpToDateProjectsIndicator : ValueIndicator
    {
        static UpToDateProjectsIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(UpToDateProjectsIndicator), new FrameworkPropertyMetadata(typeof(UpToDateProjectsIndicator)));
        }
    }
}