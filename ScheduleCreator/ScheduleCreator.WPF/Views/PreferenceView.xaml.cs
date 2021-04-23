using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScheduleCreator.WPF.Views
{
    /// <summary>
    /// Interaction logic for PreferenceView.xaml
    /// </summary>
    public partial class PreferenceView : UserControl
    {
        public PreferenceView()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        //private void Holiday_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    Holiday.Text = Regex.Replace(Holiday.Text, "[^0-9]+", "");
        //}
    }
}
