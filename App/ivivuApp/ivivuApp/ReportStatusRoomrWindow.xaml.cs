﻿using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ivivuApp
{
    /// <summary>
    /// Interaction logic for ReportRevenueByMonthWindow.xaml
    /// </summary>
    public partial class ReportStatusRoomrWindow : Window
    {
        public ReportStatusRoomrWindow()
        {
            InitializeComponent();
            ReportStatusRoom rpt = new ReportStatusRoom();
            crystalReportsViewer1.ViewerCore.ReportSource = rpt;
        }
    }
}
