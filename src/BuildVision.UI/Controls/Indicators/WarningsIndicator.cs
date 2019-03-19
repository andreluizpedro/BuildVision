﻿using System.Windows;

namespace BuildVision.UI.Controls.Indicators
{
    public class WarningsIndicator : ValueIndicator
    {
        static WarningsIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WarningsIndicator), new FrameworkPropertyMetadata(typeof(WarningsIndicator)));
        }
    }
}