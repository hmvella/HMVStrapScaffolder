using System;
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
    /// Interaction logic for AddCancelButtonControl.xaml
    /// </summary>
    public partial class AddCancelButtonControl : UserControl, IComponentConnector
    {
        public static readonly RoutedEvent AddClickEvent = EventManager.RegisterRoutedEvent(
        "AddButtonClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(AddCancelButtonControl));


        // Provide CLR accessors for the event
        public event RoutedEventHandler AddButtonClick
        {
            add { AddHandler(AddClickEvent, value); }
            remove { RemoveHandler(AddClickEvent, value); }
        }

        public AddCancelButtonControl()
        {
            InitializeComponent();
        }

        // This method raises the Tap event
        void RaiseAddClickEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(AddCancelButtonControl.AddClickEvent);
            RaiseEvent(newEventArgs);
        }

        public void AddButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseAddClickEvent();
        }

        
    }
}
