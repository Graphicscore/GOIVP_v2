using MahApps.Metro;
using MahApps.Metro.Controls;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GOIV_WPF.views
{
    /// <summary>
    /// Interaktionslogik für DiffView.xaml
    /// </summary>
    public partial class DiffView : MetroWindow
    {
        ///SMandatoryPacksData/Paths
        ///
        public DiffView(String original, String modified)
        {
            InitializeComponent();

            // get the current app style (theme and accent) from the application
            Tuple<AppTheme, Accent> theme = ThemeManager.DetectAppStyle(Application.Current);

            // now change app style to the custom accent and current theme
            ThemeManager.ChangeAppStyle(Application.Current,
                                        ThemeManager.GetAccent("DiffAccent"),
                                        theme.Item1);

            dView.Focusable = false;
            dView.LeftText = original;
            dView.RightText = modified;
        }

        private void onLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void dView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ThemeManager.ChangeAppStyle(Application.Current,
                                        ThemeManager.GetAccent("Emerald"),
                                    ThemeManager.GetAppTheme("BaseLight"));
        }
    }
}
