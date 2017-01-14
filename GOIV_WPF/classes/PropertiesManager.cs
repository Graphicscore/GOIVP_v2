using GOIV_WPF.Utils;
using MahApps.Metro.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIV_WPF.Properties
{
    class PropertiesManager
    {
        private const String WORKING_DIR_PROP = "working_dir";

        private String workingDirectory;
        private MainWindow window;

        public PropertiesManager(MainWindow window)
        {
            Settings.Default.Reload();
            this.window = window;
            this.workingDirectory = getWorkingDirectory() != null ? getWorkingDirectory() : null;
            initWorkingDir();
        }

        public String getWorkingDirectory()
        {
            String result = Settings.Default[WORKING_DIR_PROP] as String;
            result = result.Length == 0 ? null : result;
            return result;
        }

        public void setWorkingDirectory(String workingDirectory)
        {
            this.workingDirectory = workingDirectory;
            setProperty(WORKING_DIR_PROP, workingDirectory);
        }

        private void setProperty(String key, Object value)
        {
            Settings.Default[key] = value;
            Settings.Default.Save();
            Settings.Default.Reload();
            window.onPropertiesChanged();
        }

        public void changeWorkingDir()
        {
            CommonOpenFileDialog dlg = DialogUtility.createOpenFolderDialog("Select working directory");
            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                setWorkingDirectory(dlg.FileName);
            }
        }

        public void load()
        {
            window.onPropertiesChanged();
        }

        private void initWorkingDir()
        {
            //Check if we dont have a working directory set
            if(workingDirectory == null)
            {
                window.openWorkingDirDialog();
                CommonOpenFileDialog dlg = DialogUtility.createOpenFolderDialog("Select working directory");
                if(dlg.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    setWorkingDirectory(dlg.FileName);
                }
                else
                {
                    initWorkingDir();
                }
            }
        }
    }
}
