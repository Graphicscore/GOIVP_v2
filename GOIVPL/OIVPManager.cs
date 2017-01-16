using GOIVPL.Commands;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace GOIVPL
{
    public class OIVPManager
    {

        /*public static Command[] getCommands(XmlElement[] elements)
        {
            OIVPManager mgr = new OIVPManager();
            List<Command> cmds = new List<Command>();

            foreach (XmlElement element in elements)
            {
                Assembly assem = typeof(GOIVPL.OIVPManager).Assembly;
                Type type = assem.GetType(typeof(Command).Namespace + "." + element.Name);
                Command cmd = XmlTools.DeserializeFromXmlElement<Command>(type, element);
                if (cmd.Elements != null && cmd.Elements.Count > 0)
                {
                    cmd.addSubCommand(getCommands(cmd.Elements.ToArray<XmlElement>()));
                }
                cmds.Add(cmd);
            }

            return cmds.ToArray<Command>();
        }*/
        public static Command[] commandFromXml(XmlElement[] elements)
        {
            List<Command> cmds = new List<Command>();

            foreach (XmlElement element in elements)
            {
                Assembly assem = typeof(GOIVPL.OIVPManager).Assembly;
                Type type = assem.GetType(typeof(Command).Namespace + "." + element.Name);
                Command cmd = XmlTools.DeserializeFromXmlElement<Command>(type, element);
                if (cmd.Elements != null && cmd.Elements.Count > 0)
                {
                    cmd.addSubCommand(cmd.getCommands());
                }
                cmds.Add(cmd);
            }

            return cmds.ToArray<Command>();
        }

        public OIVFile loadOIVByUnextractedFolder(String oivFile)
        {
            return loadOIV(new FileInfo(oivFile));
        }

        public OIVFile loadOIV(FileInfo file)
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(OIVFile));

            FileStream myFileStream = new FileStream(file.FullName, FileMode.Open);

            OIVFile oiv = (OIVFile)mySerializer.Deserialize(myFileStream);

            oiv.Picture = new Bitmap(Bitmap.FromFile(file.Directory.FullName + "\\icon.png"));

            oiv.ContentPath = (file.Directory.FullName + "\\content");

            return oiv;
        }

        public String ExtractOIV(String oivPath, String workingDirectory, ProgressChangedEventHandler progressHandler)
        {
            FileInfo info = new FileInfo(oivPath);
            DirectoryInfo parentDir = info.Directory;
            String outFolder = workingDirectory + "\\" + info.Name + "_extracted";
            String r = ExtractZipFile(oivPath, "", outFolder, progressHandler);
            return r == null ? outFolder : null;
        }

        public void createZipFile(string outPathname, string folderName)
        {

            FileStream fsOut = File.Create(outPathname);
            ZipOutputStream zipStream = new ZipOutputStream(fsOut);

            zipStream.SetLevel(3); //0-9, 9 being the highest level of compression

            zipStream.Password = null;  // optional. Null is the same as not setting. Required if using AES.

            // This setting will strip the leading part of the folder path in the entries, to
            // make the entries relative to the starting folder.
            // To include the full path for each entry up to the drive root, assign folderOffset = 0.
            int folderOffset = folderName.Length + (folderName.EndsWith("\\") ? 0 : 1);

            CompressFolder(folderName, zipStream, folderOffset);

            zipStream.IsStreamOwner = true; // Makes the Close also Close the underlying stream
            zipStream.Close();
        }

        // Recurses down the folder structure
        //
        private void CompressFolder(string path, ZipOutputStream zipStream, int folderOffset)
        {

            string[] files = Directory.GetFiles(path);

            foreach (string filename in files)
            {

                FileInfo fi = new FileInfo(filename);

                string entryName = filename.Substring(folderOffset); // Makes the name in zip based on the folder
                entryName = ZipEntry.CleanName(entryName); // Removes drive from name and fixes slash direction
                ZipEntry newEntry = new ZipEntry(entryName);
                newEntry.DateTime = fi.LastWriteTime; // Note the zip format stores 2 second granularity

                // Specifying the AESKeySize triggers AES encryption. Allowable values are 0 (off), 128 or 256.
                // A password on the ZipOutputStream is required if using AES.
                //   newEntry.AESKeySize = 256;

                // To permit the zip to be unpacked by built-in extractor in WinXP and Server2003, WinZip 8, Java, and other older code,
                // you need to do one of the following: Specify UseZip64.Off, or set the Size.
                // If the file may be bigger than 4GB, or you do not need WinXP built-in compatibility, you do not need either,
                // but the zip will be in Zip64 format which not all utilities can understand.
                //   zipStream.UseZip64 = UseZip64.Off;
                newEntry.Size = fi.Length;

                zipStream.PutNextEntry(newEntry);

                // Zip the file in buffered chunks
                // the "using" will close the stream even if an exception occurs
                byte[] buffer = new byte[4096];
                using (FileStream streamReader = File.OpenRead(filename))
                {
                    StreamUtils.Copy(streamReader, zipStream, buffer);
                }
                zipStream.CloseEntry();
            }
            string[] folders = Directory.GetDirectories(path);
            foreach (string folder in folders)
            {
                CompressFolder(folder, zipStream, folderOffset);
            }
        }

        private String ExtractZipFile(string archiveFilenameIn, string password, string outFolder, ProgressChangedEventHandler progress)
        {

            ZipFile zf = null;
            try
            {
                FileStream fs = File.OpenRead(archiveFilenameIn);
                zf = new ZipFile(fs);
                if (!String.IsNullOrEmpty(password))
                {
                    zf.Password = password;     // AES encrypted entries are handled automatically
                }
                int count = 0;
                foreach (ZipEntry zipEntry in zf)
                {
                    if (!zipEntry.IsFile)
                    {
                        continue;           // Ignore directories
                    }
                    String entryFileName = zipEntry.Name;
                    // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                    // Optionally match entrynames against a selection list here to skip as desired.
                    // The unpacked length is available in the zipEntry.Size property.

                    byte[] buffer = new byte[4096];     // 4K is optimum
                    Stream zipStream = zf.GetInputStream(zipEntry);

                    // Manipulate the output filename here as desired.
                    String fullZipToPath = Path.Combine(outFolder, entryFileName);
                    string directoryName = Path.GetDirectoryName(fullZipToPath);
                    if (directoryName.Length > 0)
                        Directory.CreateDirectory(directoryName);

                    // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                    // of the file, but does not waste memory.
                    // The "using" will close the stream even if an exception occurs.
                    using (FileStream streamWriter = File.Create(fullZipToPath))
                    {
                        StreamUtils.Copy(zipStream, streamWriter, buffer);
                    }
                    count++;
                    if(progress != null)
                    {
                        progress(null, new ProgressChangedEventArgs((int)Math.Round((double)count / (double)zf.Count * 100D,0),null));
                    }
                }
            }
            finally
            {
                if (zf != null)
                {
                    zf.IsStreamOwner = true; // Makes close also shut the underlying stream
                    zf.Close(); // Ensure we release resources
                }
            }
            return null;
        }

        public List<Command> createCommandSetFromFolder(String selectedPath)
        {

            DirectoryInfo folder = new DirectoryInfo(selectedPath);

            Command command = new Command();

            List<Command> cmd = shit(folder, folder, folder);

            return cmd;
        }

        public List<Command> shit(DirectoryInfo rootDir, DirectoryInfo dir, DirectoryInfo archiveDir)
        {

            

            List<Command> commands = new List<Command>();

            foreach (FileInfo sFile in dir.GetFiles())
            {
                add addC = new add();
                addC.Source = checkLeading(sFile.FullName.Replace(rootDir.FullName, ""));
                addC.Name = checkLeading(sFile.FullName.Replace(archiveDir.FullName, ""));
                commands.Add(addC);
            }
            foreach (DirectoryInfo sDir in dir.GetDirectories())
            {
                if (sDir.Name.Contains(".rpf"))
                {
                    archive archive = new archive();
                    archive.Path = checkLeading(sDir.FullName.Replace(archiveDir.FullName, ""));
                    archive.CreateIfNotExists = "True";
                    archive.ArchiveType = "RPF7";

                    archive.addSubCommand(shit(rootDir, sDir, sDir));
                    commands.Add(archive);
                }
                else
                {
                    commands.AddRange(shit(rootDir, sDir, archiveDir));
                }
            }

            return commands;
        }

        private String checkLeading(String input)
        {
            if (input.ToCharArray()[0] == '\\')
            {
                return input.Substring(1, input.Length - 1);
            }
            else
            {
                return input;
            }
        }
    }
}
