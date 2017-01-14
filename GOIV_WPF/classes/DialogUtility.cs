using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIV_WPF.Utils
{
    class DialogUtility
    {

        public static CommonOpenFileDialog createOpenFolderDialog(String title, String defaultDirectory = null)
        {
            CommonOpenFileDialog dlg = new CommonOpenFileDialog();
            dlg.IsFolderPicker = true;
            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;
            dlg.ShowPlacesList = true;
            dlg.Title = title;
            if (defaultDirectory != null)
            {
                dlg.DefaultDirectory = defaultDirectory;
            }
            return dlg;
        }

        public static CommonOpenFileDialog createOpenFileDialog(String title, Boolean multiSelect = false, String defaultDirectory = null)
        {
            CommonOpenFileDialog dlg = new CommonOpenFileDialog();
            dlg.IsFolderPicker = false;
            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = multiSelect;
            dlg.ShowPlacesList = true;

            if(defaultDirectory != null)
            {
                dlg.DefaultDirectory = defaultDirectory;
            }

            return dlg;
        }
    }
}
