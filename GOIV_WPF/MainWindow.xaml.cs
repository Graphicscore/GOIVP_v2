using GOIV_WPF.Properties;
using GOIV_WPF.UI;
using GOIV_WPF.Utils;
using GOIV_WPF.views;
using GOIVPL;
using GOIVPL.Commands;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.SimpleChildWindow;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using Xceed.Wpf.Toolkit;

namespace GOIV_WPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        OIVFile oivFile = new OIVFile();

        private TreeViewItem backupRootNode;
        private TreeViewItem lastSelectedTreeViewNode;

        private PropertiesManager propertiesManager;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void onLoaded(object sender, RoutedEventArgs e)
        {
            oivFile.MetaData.NameChanged += MetaData_NameChanged;

            oivFile.MetaData.Author.AuthorNameChanged += Author_AuthorNameChanged;
            oivFile.MetaData.Author.AuthorActionLinkChanged += Author_AuthorActionLinkChanged;
            oivFile.MetaData.Author.AuthorFacebookChanged += Author_AuthorFacebookChanged;
            oivFile.MetaData.Author.AuthorTwitterChanged += Author_AuthorTwitterChanged;
            oivFile.MetaData.Author.AuthorWebChanged += Author_AuthorWebChanged;
            oivFile.MetaData.Author.AuthorYoutubeChanged += Author_AuthorYoutubeChanged;

            oivFile.MetaData.Description.DescriptionTextChanged += Description_DescriptionTextChanged;
            oivFile.MetaData.Description.DescriptionFooterTextChanged += Description_DescriptionFooterTextChanged;
            oivFile.MetaData.Description.DescriptionFooterLinkChanged += Description_DescriptionFooterLinkChanged;

            oivFile.MetaData.LargeDescription.DescriptionTextChanged += LargeDescription_DescriptionTextChanged;
            oivFile.MetaData.LargeDescription.DescriptionFooterTextChanged += LargeDescription_DescriptionFooterTextChanged;
            oivFile.MetaData.LargeDescription.DescriptionFooterLinkChanged += LargeDescription_DescriptionFooterLinkChanged;
            oivFile.MetaData.LargeDescription.LargeDescriptioDisplayNameChanged += LargeDescription_LargeDescriptioDisplayNameChanged;

            oivFile.MetaData.License.DescriptionTextChanged += License_DescriptionTextChanged;
            oivFile.MetaData.License.DescriptionFooterTextChanged += License_DescriptionFooterTextChanged;
            oivFile.MetaData.License.DescriptionFooterLinkChanged += License_DescriptionFooterLinkChanged;

            oivFile.MetaData.Version.VersionMajorChanged += Version_VersionMajorChanged;
            oivFile.MetaData.Version.VersionMinorChanged += Version_VersionMinorChanged;
            oivFile.MetaData.Version.VersionTagChanged += Version_VersionTagChanged;

            oivFile.Picture = FindResource("IMAGE_DEFAULT") as Bitmap;
            oivFile.PictureChanged += OivFile_PictureChanged;

            oivFile.clear();

            propertiesManager = PropertiesManager.getInstance(this);
            propertiesManager.load();

            backupRootNode = treeview_files.Items.GetItemAt(0) as TreeViewItem;
        }

        private void OivFile_PictureChanged(object sender, PropertyChangedEventArgs e)
        {
            image_preview.Dispatcher.Invoke(new Action(() =>
            {
                MemoryStream ms = new MemoryStream();
                oivFile.Picture.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Position = 0;
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = ms;
                bi.EndInit();
                image_preview.Source = bi;
            }));
        }

        private void Version_VersionTagChanged(object sender, PropertyChangedEventArgs e)
        {
            tbx_version_tag.Dispatcher.Invoke(new Action(() => { tbx_version_tag.Text = e.PropertyName; }));
            label_preview_version.Dispatcher.Invoke(new Action(() =>
            {
                String versionString = "";
                versionString = oivFile.MetaData.Version.Major.ToString() + "." + oivFile.MetaData.Version.Minor.ToString();
                versionString = string.IsNullOrWhiteSpace(oivFile.MetaData.Version.Tag) == false && checkbox_version_tag.IsChecked == true ? versionString + " (" + oivFile.MetaData.Version.Tag + ")" : versionString;
                label_preview_version.Content = versionString;
            }));
        }

        private void Version_VersionMinorChanged(object sender, PropertyChangedEventArgs e)
        {
            numeric_version_minor.Dispatcher.Invoke(new Action(() => { numeric_version_minor.Value = int.Parse(e.PropertyName); }));
            label_preview_version.Dispatcher.Invoke(new Action(() =>
            {
                String versionString = "";
                versionString = oivFile.MetaData.Version.Major.ToString() + "." + oivFile.MetaData.Version.Minor.ToString();
                versionString = string.IsNullOrWhiteSpace(oivFile.MetaData.Version.Tag) == false && checkbox_version_tag.IsChecked == true ? versionString + " (" + oivFile.MetaData.Version.Tag + ")" : versionString;
                label_preview_version.Content = versionString;
            }));
        }

        private void Version_VersionMajorChanged(object sender, PropertyChangedEventArgs e)
        {
            numeric_version_major.Dispatcher.Invoke(new Action(() => { numeric_version_major.Value = int.Parse(e.PropertyName); }));
            label_preview_version.Dispatcher.Invoke(new Action(() =>
            {
                String versionString = "";
                versionString = oivFile.MetaData.Version.Major.ToString() + "." + oivFile.MetaData.Version.Minor.ToString();
                versionString = string.IsNullOrWhiteSpace(oivFile.MetaData.Version.Tag) == false && checkbox_version_tag.IsChecked == true ? versionString + " (" + oivFile.MetaData.Version.Tag + ")" : versionString;
                label_preview_version.Content = versionString;
            }));
        }

        private void License_DescriptionFooterLinkChanged(object sender, PropertyChangedEventArgs e)
        {
            tbx_license_footer_link.Dispatcher.Invoke(new Action(() => { tbx_license_footer_link.Text = e.PropertyName; }));
            tbx_license_footer_link.Dispatcher.Invoke(new Action(() => { checkLicenseToggle(); }));
        }

        private void License_DescriptionFooterTextChanged(object sender, PropertyChangedEventArgs e)
        {
            tbx_license_footer_text.Dispatcher.Invoke(new Action(() => { tbx_license_footer_text.Text = e.PropertyName; }));
            tbx_license_footer_text.Dispatcher.Invoke(new Action(() => { checkLicenseToggle(); }));
        }

        private void License_DescriptionTextChanged(object sender, PropertyChangedEventArgs e)
        {
            tbx_license.Dispatcher.Invoke(new Action(() => { tbx_license.Text = e.PropertyName; }));
        }

        private void LargeDescription_LargeDescriptioDisplayNameChanged(object sender, PropertyChangedEventArgs e)
        {
            tbx_large_description_footer_displayname.Dispatcher.Invoke(new Action(() => { tbx_large_description_footer_displayname.Text = e.PropertyName; }));
            tbx_large_description_footer_displayname.Dispatcher.Invoke(new Action(() => { checkLargeDescriptionToggle(); }));
            label_preview_large_description_displayname.Dispatcher.Invoke(new Action(() => { label_preview_large_description_displayname.Content = e.PropertyName; }));
        }

        private void LargeDescription_DescriptionFooterLinkChanged(object sender, PropertyChangedEventArgs e)
        {
            tbx_large_description_footer_link.Dispatcher.Invoke(new Action(() => { tbx_large_description_footer_link.Text = e.PropertyName; }));
            tbx_large_description_footer_link.Dispatcher.Invoke(new Action(() => { checkLargeDescriptionToggle(); }));
        }

        private void LargeDescription_DescriptionFooterTextChanged(object sender, PropertyChangedEventArgs e)
        {
            tbx_large_description_footer.Dispatcher.Invoke(new Action(() => { tbx_large_description_footer.Text = e.PropertyName; }));
            tbx_large_description_footer.Dispatcher.Invoke(new Action(() => { checkLargeDescriptionToggle(); }));
            label_preview_large_description_footer.Dispatcher.Invoke(new Action(() => { label_preview_large_description_footer.Content = e.PropertyName; }));
        }

        private void LargeDescription_DescriptionTextChanged(object sender, PropertyChangedEventArgs e)
        {
            tbx_large_description.Dispatcher.Invoke(new Action(() => { tbx_large_description.Text = e.PropertyName; }));
            label_preview_large_description.Dispatcher.Invoke(new Action(() => { label_preview_large_description.Content = e.PropertyName; }));
        }

        private void Description_DescriptionFooterLinkChanged(object sender, PropertyChangedEventArgs e)
        {
            tbx_description_footer_link.Dispatcher.Invoke(new Action(() => { tbx_description_footer_link.Text = e.PropertyName; }));
            tbx_description_footer_link.Dispatcher.Invoke(new Action(() => { checkDescriptionToggle(); }));
        }

        private void Description_DescriptionFooterTextChanged(object sender, PropertyChangedEventArgs e)
        {
            tbx_description_footer_text.Dispatcher.Invoke(new Action(() => { tbx_description_footer_text.Text = e.PropertyName; }));
            tbx_description_footer_text.Dispatcher.Invoke(new Action(() => { checkDescriptionToggle(); }));
            label_preview_description_footer.Dispatcher.Invoke(new Action(() => { label_preview_description_footer.Content = e.PropertyName; }));
        }

        private void Description_DescriptionTextChanged(object sender, PropertyChangedEventArgs e)
        {
            tbx_description.Dispatcher.Invoke(new Action(() => { tbx_description.Text = e.PropertyName; }));
            label_preview_description.Dispatcher.Invoke(new Action(() => { label_preview_description.Content = e.PropertyName; }));
        }

        private void Author_AuthorYoutubeChanged(object sender, PropertyChangedEventArgs e)
        {
            tbx_author_youtube.Dispatcher.Invoke(new Action(() => { tbx_author_youtube.Text = e.PropertyName; }));
        }

        private void Author_AuthorWebChanged(object sender, PropertyChangedEventArgs e)
        {
            tbx_author_web.Dispatcher.Invoke(new Action(() => { tbx_author_web.Text = e.PropertyName; }));
        }

        private void Author_AuthorTwitterChanged(object sender, PropertyChangedEventArgs e)
        {
            tbx_author_twitter.Dispatcher.Invoke(new Action(() => { tbx_author_twitter.Text = e.PropertyName; }));
        }

        private void Author_AuthorFacebookChanged(object sender, PropertyChangedEventArgs e)
        {
            tbx_author_facebook.Dispatcher.Invoke(new Action(() => { tbx_author_facebook.Text = e.PropertyName; }));
        }

        private void Author_AuthorActionLinkChanged(object sender, PropertyChangedEventArgs e)
        {
            tbx_author_label.Dispatcher.Invoke(new Action(() => { tbx_author_label.Text = e.PropertyName; }));
        }

        private void Author_AuthorNameChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            label_preview_author.Dispatcher.Invoke(new Action(() => { label_preview_author.Content = e.PropertyName; }));
            tbx_author_name.Dispatcher.Invoke(new Action(() => { tbx_author_name.Text = e.PropertyName; }));
            label_preview_createdby.Dispatcher.Invoke(new Action(() => { label_preview_createdby.Content = e.PropertyName; }));

        }

        private void MetaData_NameChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            treeview_files.Dispatcher.Invoke(new Action(() =>
            {

                if (treeview_files.Items != null && treeview_files.Items.Count > 0)
                {
                    Object o = getTreeViewRootNode();
                    if(o == null)
                    {
                        int r = treeview_files.Items.Add(backupRootNode);
                        o = treeview_files.Items.GetItemAt(r);
                    }
                    (o as TreeViewItem).Header = e.PropertyName;
                }

                label_preview_packagename.Content = e.PropertyName;
                tbx_package_name.Text = e.PropertyName;
            }));
        }

        private void tbx_package_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            oivFile.MetaData.Name = tbx_package_name.Text;
        }

        private void cbx_target_game_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbx_target_game.SelectedIndex)
            {
                case 0:
                    oivFile.Target = OIVFile.TargetTypes.FIVE;
                    break;
            }
        }

        private void numeric_version_major_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            oivFile.MetaData.Version.Major = (int)numeric_version_major.Value;
        }

        private void numeric_version_minor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            oivFile.MetaData.Version.Minor = (int)numeric_version_minor.Value;
        }

        private void tbx_version_tag_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbx_version_tag.Text.Length > 0 && !string.IsNullOrWhiteSpace(tbx_version_tag.Text))
            {
                oivFile.MetaData.Version.Tag = tbx_version_tag.Text;
                checkbox_version_tag.IsChecked = true;
            }
            else
            {
                oivFile.MetaData.Version.Tag = null;
                checkbox_version_tag.IsChecked = false;
            }

        }

        private void checkLargeDescriptionToggle()
        {
            Boolean result = false;
            if (tbx_large_description_footer.Text.Length == 0 || string.IsNullOrWhiteSpace(tbx_large_description_footer.Text))
            {
                if (tbx_large_description_footer_link.Text.Length == 0 || string.IsNullOrWhiteSpace(tbx_large_description_footer_link.Text))
                {
                    if (tbx_large_description_footer_displayname.Text.Length == 0 || string.IsNullOrWhiteSpace(tbx_large_description_footer_displayname.Text))
                    {
                        result = true;
                    }
                }
            }

            if (result)
            {
                toggle_large_description_footer.IsChecked = false;
            }
            else
            {
                toggle_large_description_footer.IsChecked = true;
            }
        }

        private void checkDescriptionToggle()
        {
            Boolean result = false;
            if (tbx_description_footer_text.Text.Length == 0 || string.IsNullOrWhiteSpace(tbx_description_footer_text.Text))
            {
                if (tbx_description_footer_link.Text.Length == 0 || string.IsNullOrWhiteSpace(tbx_description_footer_link.Text))
                {
                    result = true;
                }
            }

            if (result)
            {
                toggle_description_footer.IsChecked = false;
            }
            else
            {
                toggle_description_footer.IsChecked = true;
            }
        }

        private void checkLicenseToggle()
        {
            Boolean result = false;
            if (tbx_license_footer_text.Text.Length == 0 || string.IsNullOrWhiteSpace(tbx_license_footer_text.Text))
            {
                if (tbx_license_footer_link.Text.Length == 0 || string.IsNullOrWhiteSpace(tbx_license_footer_link.Text))
                {
                    result = true;
                }
            }

            if (result)
            {
                toggle_license_footer.IsChecked = false;
            }
            else
            {
                toggle_license_footer.IsChecked = true;
            }
        }

        private void tbx_description_footer_text_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbx_description_footer_text.Text.Length > 0 && !string.IsNullOrWhiteSpace(tbx_description_footer_text.Text))
            {
                oivFile.MetaData.Description.FooterLinkTitle = tbx_description_footer_text.Text;
            }
            else
            {
                oivFile.MetaData.Description.FooterLinkTitle = null;
            }
            checkDescriptionToggle();
        }

        private void tbx_description_footer_link_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbx_description_footer_link.Text.Length > 0 && !string.IsNullOrWhiteSpace(tbx_description_footer_link.Text))
            {
                oivFile.MetaData.Description.FooterLink = tbx_description_footer_link.Text;
            }
            else
            {
                oivFile.MetaData.Description.FooterLink = null;
            }
            checkDescriptionToggle();
        }

        private void tbx_description_TextChanged(object sender, TextChangedEventArgs e)
        {
            oivFile.MetaData.Description.setText(tbx_description.Text);
        }

        private void tbx_large_description_TextChanged(object sender, TextChangedEventArgs e)
        {
            oivFile.MetaData.LargeDescription.setText(tbx_large_description.Text);
        }

        private void tbx_large_description_footer_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbx_large_description_footer.Text.Length > 0 && !string.IsNullOrWhiteSpace(tbx_large_description_footer.Text))
            {
                oivFile.MetaData.LargeDescription.FooterLinkTitle = tbx_large_description_footer.Text;
            }
            else
            {
                oivFile.MetaData.LargeDescription.FooterLinkTitle = null;
            }
            checkLargeDescriptionToggle();
        }

        private void tbx_large_description_footer_link_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbx_large_description_footer_link.Text.Length > 0 && !string.IsNullOrWhiteSpace(tbx_large_description_footer_link.Text))
            {
                oivFile.MetaData.LargeDescription.FooterLink = tbx_large_description_footer_link.Text;
            }
            else
            {
                oivFile.MetaData.LargeDescription.FooterLink = null;
            }
            checkLargeDescriptionToggle();
        }

        private void tbx_large_description_footer_displayname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbx_large_description_footer_displayname.Text.Length > 0 && !string.IsNullOrWhiteSpace(tbx_large_description_footer_displayname.Text))
            {
                oivFile.MetaData.LargeDescription.DisplayName = tbx_large_description_footer_displayname.Text;
            }
            else
            {
                oivFile.MetaData.LargeDescription.DisplayName = null;
            }
            checkLargeDescriptionToggle();
        }

        private void tbx_author_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            oivFile.MetaData.Author.DisplayName = tbx_author_name.Text;
        }

        private void tbx_author_label_TextChanged(object sender, TextChangedEventArgs e)
        {
            oivFile.MetaData.Author.ActionLink = tbx_author_label.Text;
        }

        private void tbx_author_web_TextChanged(object sender, TextChangedEventArgs e)
        {
            oivFile.MetaData.Author.Web = tbx_author_web.Text;
        }

        private void tbx_author_facebook_TextChanged(object sender, TextChangedEventArgs e)
        {
            oivFile.MetaData.Author.Facebook = tbx_author_facebook.Text;
        }

        private void tbx_author_twitter_TextChanged(object sender, TextChangedEventArgs e)
        {
            oivFile.MetaData.Author.Twitter = tbx_author_twitter.Text;
        }

        private void tbx_author_youtube_TextChanged(object sender, TextChangedEventArgs e)
        {
            oivFile.MetaData.Author.Youtube = tbx_author_youtube.Text;
        }

        private async void button_files_importfolder_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dlg = DialogUtility.createOpenFolderDialog("");
            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var folder = dlg.FileName;
                var controller = await this.ShowProgressAsync("Please wait , we are importing your folder", dlg.FileName);
                controller.SetCancelable(true);
                controller.Canceled += Controller_Canceled;
                controller.SetIndeterminate();
                List<Command> commands = await Task.Run(() => new OIVPManager().createCommandSetFromFolder(dlg.FileName));
                oivFile.ContentPath = dlg.FileName;
                oivFile.Content.ICommands = commands;
                getTreeViewRootNode().ItemsSource = oivFile.ICommands;
                await controller.CloseAsync();
            }
        }

        private void Controller_Canceled(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void button_open_oiv_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dlg = DialogUtility.createOpenFileDialog("");

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var folder = dlg.FileName;
                var controller = await this.ShowProgressAsync(FindResource("STRING_IMPORT_OIV_WAIT") as String, FindResource("STRING_IMPORT_OIV_EXTRACTING") as String);
                ProgressChangedEventHandler progressHandler = new ProgressChangedEventHandler((s, ex) =>
                {
                    double value = (ex as ProgressChangedEventArgs).ProgressPercentage / 100D;
                    if (value > 1D)
                    {
                        value = 1D;
                    }
                    controller.SetProgress(value);
                });
                OIVPManager manager = new OIVPManager();
                String resultPath = await Task.Run(() => manager.ExtractOIV(dlg.FileName, PropertiesManager.getInstance().getWorkingDirectory(),progressHandler));
                controller.SetIndeterminate();
                controller.SetMessage(FindResource("STRING_IMPORT_OIV_PARSE") as String);
                OIVFile loadedOIV = await Task.Run(() => manager.loadOIVByUnextractedFolder(resultPath + "\\assembly.xml"));
                getTreeViewRootNode().ItemsSource = null;
                getTreeViewRootNode().ItemsSource = loadedOIV.ICommands;
                await Task.Run(() => updateOIV(loadedOIV));
                await controller.CloseAsync();
            }
        }

        private void updateOIV(OIVFile newOIV)
        {
            oivFile.Version = newOIV.Version;
            oivFile.Target = newOIV.Target;
            oivFile.Picture = newOIV.Picture;
            oivFile.MetaData.update(newOIV.MetaData);
            oivFile.ID = newOIV.ID;
            oivFile.Content = newOIV.Content;
            oivFile.Color.update(newOIV.Color);
            oivFile.ContentPath = newOIV.ContentPath;
        }

        private void tbx_license_footer_text_TextChanged(object sender, TextChangedEventArgs e)
        {
            oivFile.MetaData.License.FooterLinkTitle = tbx_license_footer_text.Text;
        }

        private void tbx_license_footer_link_TextChanged(object sender, TextChangedEventArgs e)
        {
            oivFile.MetaData.License.FooterLink = tbx_license_footer_link.Text;
        }

        private void tbx_license_TextChanged(object sender, TextChangedEventArgs e)
        {
            oivFile.MetaData.License.setText(tbx_license.Text);
        }

        private async void button_export_oiv_Click(object sender, RoutedEventArgs e)
        {

            List<GOIVPropertyContainer> invalidList = oivFile.validate();

            String invalidFieldString = Environment.NewLine;

            foreach (GOIVPropertyContainer info in invalidList)
            {
                invalidFieldString += "\u2022 " + info.attribute.getDisplayText() + Environment.NewLine;
            }

            if (invalidList.Count > 0)
            {
                await this.ShowMessageAsync(FindResource("STRING_PROPERTIES_INVALID_TITLE") as String, FindResource("STRING_PROPERTIES_INVALID_MESSAGE") as String + invalidFieldString);
                return;
            }

            CommonSaveFileDialog dlg = new CommonSaveFileDialog();
            dlg.AddToMostRecentlyUsedList = true;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.ShowPlacesList = true;
            dlg.DefaultFileName = oivFile.MetaData.Name;

            CommonFileDialogFilter oivFiler = new CommonFileDialogFilter();
            oivFiler.DisplayName = FindResource("STRING_OIV_FILE") as String;
            oivFiler.Extensions.Add("oiv");
            dlg.Filters.Add(oivFiler);

            CommonFileDialogFilter zipFiler = new CommonFileDialogFilter();
            zipFiler.DisplayName = FindResource("STRING_ZIP_FILE") as String;
            zipFiler.Extensions.Add("zip");
            dlg.Filters.Add(zipFiler);

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var folder = dlg.FileName;
                var controller = await this.ShowProgressAsync(FindResource("STRING_EXPORT_OIV_WAIT") as String, FindResource("STRNG_EXPORT_OIV_PACKING") as String);
                controller.SetIndeterminate();
                await Task.Run(() =>
                {
                    String packageOutDir = folder + ".packing";
                    String contentOutDir = packageOutDir + "\\content\\";

                    /*if(new DirectoryInfo(packageOutDir).Exists)
                    {
                        MessageDialogResult result = await this.ShowMessageAsync(FindResource("STRING_EXPORT_DIR_ALREDY") as String)
                    }*/

                    DirectoryCopy(oivFile.ContentPath, packageOutDir, true);

                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.ConformanceLevel = ConformanceLevel.Fragment;
                    //XmlWriter xml = XmlWriter.Create(, settings);
                    XmlTextWriter writer = new XmlTextWriter(folder + ".packing" + "\\assembly.xml", Encoding.UTF8);
                    writer.Formatting = Formatting.Indented;
                    XmlTools.SerializeToXmlElement(oivFile).WriteTo(writer);
                    writer.Flush();
                    writer.Close();
                    writer.Dispose();

                    if (oivFile.Picture != null)
                    {
                        oivFile.Picture.Save(folder + ".packing" + "\\icon.png");
                    }

                });
                await controller.CloseAsync();
            }
        }

        private void button_files_clear_Click(object sender, RoutedEventArgs e)
        {
            getTreeViewRootNode().ItemsSource = null;
        }


#pragma warning disable CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt.
        private async void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
#pragma warning restore CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt.
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = System.IO.Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = System.IO.Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private async void menu_preview_setimage_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dlg = new CommonOpenFileDialog();
            dlg.IsFolderPicker = false;
            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;
            dlg.ShowPlacesList = true;

            CommonFileDialogFilter oivFiler = new CommonFileDialogFilter();
            oivFiler.DisplayName = FindResource("STRING_PNG_FILE") as String;
            oivFiler.Extensions.Add("png");
            dlg.Filters.Add(oivFiler);

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                try
                {
                    System.Drawing.Image b = Bitmap.FromFile(dlg.FileName);
                    if (b != null)
                    {

                        GraphicsUnit g = GraphicsUnit.Pixel;
                        RectangleF bounds = b.GetBounds(ref g);
                        if (bounds.Width == 128 && bounds.Height == 128)
                        {
                            oivFile.Picture = new Bitmap(b);
                        }
                        else
                        {
                            throw new Exception(FindResource("STRING_EXCEPTION_INVALID_IMAGE_SIZE") as String);
                        }
                    }
                }
                catch (Exception ex)
                {
                    await this.ShowMessageAsync(FindResource("STRING_INVALID_IMAGE_TITLE") as String, FindResource("STRING_INVALID_IMAGE_MESSAGE") as String + Environment.NewLine + "Error : " + ex.Message, MessageDialogStyle.Affirmative);
                }
            }
        }

        public BitmapImage Convert(Bitmap src)
        {
            if (src == null)
            {
                return null;
            }
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        private void menu_preview_setcolor_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.ColorDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var wpfColor = System.Windows.Media.Color.FromArgb(dialog.Color.A, dialog.Color.R, dialog.Color.G, dialog.Color.B);
                panel_iamge_preview.Background = new SolidColorBrush(wpfColor);
            }
        }

        private void menu_preview_background_setcolor_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.ColorDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var wpfColor = System.Windows.Media.Color.FromArgb(dialog.Color.A, dialog.Color.R, dialog.Color.G, dialog.Color.B);
                grid_preview_background.Background = new SolidColorBrush(wpfColor);
            }
        }

        private void menu_preview_package_toggleblackwhite_Click(object sender, RoutedEventArgs e)
        {
            previewToggleBlackWhite();
        }

        private void previewToggleBlackWhite()
        {
            if ((label_preview_author.Foreground as SolidColorBrush).Color == (FindResource("BRUSH_WHITE") as SolidColorBrush).Color)
            {
                label_preview_author.Foreground = (FindResource("BRUSH_BLACK") as SolidColorBrush);
                label_preview_packagename.Foreground = (FindResource("BRUSH_BLACK") as SolidColorBrush);
            }
            else
            {
                label_preview_author.Foreground = (FindResource("BRUSH_WHITE") as SolidColorBrush);
                label_preview_packagename.Foreground = (FindResource("BRUSH_WHITE") as SolidColorBrush);
            }

        }

        private void checkbox_version_tag_Unchecked(object sender, RoutedEventArgs e)
        {
            label_preview_version.Dispatcher.Invoke(new Action(() =>
            {
                String versionString = "";
                versionString = oivFile.MetaData.Version.Major.ToString() + "." + oivFile.MetaData.Version.Minor.ToString();
                versionString = string.IsNullOrWhiteSpace(oivFile.MetaData.Version.Tag) == false && checkbox_version_tag.IsChecked == true ? versionString + " (" + oivFile.MetaData.Version.Tag + ")" : versionString;
                label_preview_version.Content = versionString;
            }));
        }

        private void checkbox_version_tag_Checked(object sender, RoutedEventArgs e)
        {
            label_preview_version.Dispatcher.Invoke(new Action(() =>
            {
                String versionString = "";
                versionString = oivFile.MetaData.Version.Major.ToString() + "." + oivFile.MetaData.Version.Minor.ToString();
                versionString = string.IsNullOrWhiteSpace(oivFile.MetaData.Version.Tag) == false && checkbox_version_tag.IsChecked == true ? versionString + " (" + oivFile.MetaData.Version.Tag + ")" : versionString;
                label_preview_version.Content = versionString;
            }));
        }

        private void TreeViewSelectItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Command cmd = e.NewValue as Command;
            if (cmd != null)
            {
                switch(cmd.GetType().Name)
                {
                    case "add":
                        if((cmd as add).isXML())
                        {
                            treeview_files.ContextMenu = treeview_files.Resources["XmlFileContext"] as ContextMenu;
                        }
                        else
                        {
                            treeview_files.ContextMenu = treeview_files.Resources["FileContext"] as ContextMenu;
                        }
                        break;
                    case "archive":
                        treeview_files.ContextMenu = treeview_files.Resources["ArchiveContext"] as ContextMenu;
                        break;
                    case "xml":
                        treeview_files.ContextMenu = treeview_files.Resources["XmlContext"] as ContextMenu;
                        break;
                    default:
                        break;
                }
            }
            lastSelectedTreeViewNode = e.OriginalSource as TreeViewItem;
        }

        private void button_settings_Click(object sender, RoutedEventArgs e)
        {
            flyout_settings.IsOpen = true;
        }

        public void openWorkingDirDialog()
        {
            Task.Run(async () =>
            {
                await this.ShowMessageAsync(FindResource("STRING_WORKING_DIR_POPUP_TITLE") as String, FindResource("STRING_WORKING_DIR_POPUP_MESSAGE") as String, MessageDialogStyle.Affirmative, null);
            });
        }

        public void onPropertiesChanged()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                tbx_settings_working_dir.Text = propertiesManager.getWorkingDirectory();
            }));
        }

        private void button_settings_workingdir_change_Click(object sender, RoutedEventArgs e)
        {
            propertiesManager.changeWorkingDir();
        }

        private TreeViewItem getTreeViewRootNode()
        {
            return treeview_files.Items.GetItemAt(0) as TreeViewItem;
        }

        private void FileContextConvertXml_Click(object sender, RoutedEventArgs e)
        {
            xml cxml = new xml();
            TreeViewItem item = TreeViewUtils.ContainerFromItem(treeview_files.ItemContainerGenerator, treeview_files.SelectedItem);
            TreeViewItem parent = TreeViewUtils.GetSelectedTreeViewItemParent(item) as TreeViewItem;
            Command dataContext = parent.DataContext as Command;
            int index = dataContext.ICommands.IndexOf(item.DataContext as Command);
            cxml.Path = (item.DataContext as add).Content;
            cxml.LocalXmlFilePath = (item.DataContext as add).Source;
            dataContext.ICommands.RemoveAt(index);
            dataContext.ICommands.Insert(index, cxml);;
            parent.Items.Refresh();
        }

        private void FileContextConvertFile_Click(object sender, RoutedEventArgs e)
        {
            add cadd = new add();
            TreeViewItem item = TreeViewUtils.ContainerFromItem(treeview_files.ItemContainerGenerator, treeview_files.SelectedItem);
            TreeViewItem parent = TreeViewUtils.GetSelectedTreeViewItemParent(item) as TreeViewItem;

            Command dataContext = parent.DataContext as Command;
            int index = dataContext.ICommands.IndexOf(item.DataContext as Command);

            cadd.Content = (item.DataContext as xml).Path;

            if (parent.DataContext.GetType() == typeof(archive))
            {
                cadd.Source = (parent.DataContext as archive).Path + "\\" + cadd.Content;
            }
            else
            {
                cadd.Source = cadd.Content;
            }
            
            
            dataContext.ICommands.RemoveAt(index);
            dataContext.ICommands.Insert(index, cadd);
            parent.DataContext = dataContext;
            parent.Items.Refresh();
        }

        private void FileContextEditXpath_Click(object sender, RoutedEventArgs e)
        {
            XPathWindow window = new XPathWindow(ref oivFile,treeview_files.SelectedItem as xml);
            if (window.ShowDialog() == true)
            {
                TreeViewItem item = TreeViewUtils.ContainerFromItem(treeview_files.ItemContainerGenerator, treeview_files.SelectedItem);
                Command cmd = item.DataContext as Command;
                cmd.ICommands.AddRange(window.commands);
                (TreeViewUtils.GetSelectedTreeViewItemParent(item) as TreeViewItem).Items.Refresh();
            }
           
        }
    }
}
