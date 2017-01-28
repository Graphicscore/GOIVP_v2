using GOIV_WPF.classes.projects;
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
    /// Interaktionslogik für ProjectExplorer.xaml
    /// </summary>
    public partial class ProjectExplorer : MetroWindow
    {
        public ProjectExplorer()
        {
            InitializeComponent();
            generateSampleProjectData();
        }

        public ProjectInfo SelectedProject { get; set; }

        private void generateSampleProjectData()
        {
            DataContext = Enumerable.Range(1, 4)
                                .Select(x => new ProjectInfo()
                                {
                                    ProjectName = "Project Nr." + x.ToString(),
                                    ProjectPath = new System.IO.DirectoryInfo("C:/"),
                                    Version = "1." + x.ToString(),
                                    LastModified = DateTime.Now,
                                    CreationTime = DateTime.Now.AddYears(1),
                                    Icon = null
                                });
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
