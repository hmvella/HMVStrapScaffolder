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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HMVScaffolder.Mvc
{
    /// <summary>
    /// Interaction logic for CreateDataContextDialog.xaml
    /// </summary>
    public partial class CreateDataContextDialog : ValidatingDialogWindow, IComponentConnector
    {

        public CreateDataContextDialog()
        {
            this.InitializeComponent();
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            base.TryClose();
        }
    }
}
