/* 
 *	Copyright (C) 2011 huha
 *
 *  This Program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2, or (at your option)
 *  any later version.
 *   
 *  This Program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU General Public License for more details.
 *   
 *  You should have received a copy of the GNU General Public License
 *  along with GNU Make; see the file COPYING.  If not, write to
 *  the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA. 
 *  http://www.gnu.org/copyleft/gpl.html
 *
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Utilities;




namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        IList<String> file_list = new List<String>();
        IList<String> folder_list = new List<String>();
        IList<Gpx_routepoint> gpx_rtepoints = new List<Gpx_routepoint>();
        bool DEBUG = false;
        string logtext = "Log text of "+DateTime.Now.ToString();
        string name = "";
        string routename = "";
        string FILE_PREFIX = "";
        string FILE_POSTFIX = "_VN";
        string NAME_PREFIX = "";
        string NAME_POSTFIX = "_VN";
        string REVERSE_FILE_PREFIX = "";
        string REVERSE_FILE_POSTFIX = "_RVN";
        string REVERSE_NAME_PREFIX = "";
        string REVERSE_NAME_POSTFIX = "_RVN";

        private struct Gpx_routepoint
        {
            public string Name;
            public string Description;
            public double lon;
            public double lat;
            public double angle1;
            public double angle2;
            public double angle1to2;
            public string voice;
        };

        public Form1(string[] args)
        {
            InitializeComponent();
            SimpleCommandlineparser cmdparser = new SimpleCommandlineparser();
            bool found = false;
            

            if (Directory.Exists(@"C:\Program Files (x86)\Mobile Atlas Creator\atlases") == true)
            {
                textBoxMACFolder.Text = @"C:\Program Files (x86)\Mobile Atlas Creator\atlases";
            }
            checkBoxRenumber.Checked = false;
            checkBoxNewVoice.Checked = true;

            if (File.Exists("settings.ini") == true)
            {
                String filetextall = File.ReadAllText("settings.ini");
                filetextall = filetextall.Replace("\r", "");
                String[] filetext = filetextall.Split('\n');
                try
                {
                    
                    textBoxMACFolder.Text = filetext[0];
                    textBoxDestination.Text = filetext[1];
                    textBoxPNG.Text = filetext[2];
                    textBoxInputGPX.Text = filetext[3];
                    textBoxOutputGPX.Text = filetext[4];
                    textBoxVoiceFolder.Text = filetext[5];
                    textBoxVoiceExtension.Text = filetext[6];
                    checkBoxRenumber.Checked = Convert.ToBoolean(filetext[7]);
                    checkBoxNewVoice.Checked = Convert.ToBoolean(filetext[8]);
                    checkBoxReverseRoutes.Checked = Convert.ToBoolean(filetext[9]);
                    FILE_PREFIX = filetext[10].Replace("FILE_PREFIX=", "");
                    FILE_POSTFIX = filetext[11].Replace("FILE_POSTFIX=", "");
                    NAME_PREFIX = filetext[12].Replace("NAME_PREFIX=", "");
                    NAME_POSTFIX = filetext[13].Replace("NAME_POSTFIX=", "");
                    REVERSE_FILE_PREFIX = filetext[14].Replace("REVERSE_FILE_PREFIX=", "");
                    REVERSE_FILE_POSTFIX = filetext[15].Replace("REVERSE_FILE_POSTFIX=", "");
                    REVERSE_NAME_PREFIX = filetext[16].Replace("REVERSE_NAME_PREFIX=", "");
                    REVERSE_NAME_POSTFIX = filetext[17].Replace("REVERSE_NAME_POSTFIX=", "");
                }
                catch (Exception exc)
                {
                    if (DEBUG)
                        textoutput("<REDForm1 Exception:" + exc.Message + "\n");
                }

            }

            
            

            updatecomboboxatlas();
            
            cmdparser.BoolParser(args, "--DEBUG", "-d", ref DEBUG, ref found);

            /*
            textoutput("Supported formats are:");           
            textoutput("84  Open Street Map Mapnik");
            textoutput("85  Open Street Map Cycle Map");
            textoutput("87  Open Street Map MapQuest");
            textoutput("88  OSM Midnight Commander");
            textoutput("89  OpenStreetMap Hikebikemap.de");
            textoutput("129 Open Piste Map");
            */ 

        }


        private void updatecomboboxatlas()
        {
            try
            {
                DirectoryInfo sourceinfo;
                try
                {
                    sourceinfo = new DirectoryInfo(textBoxMACFolder.Text);
                }
                catch
                {
                    return;
                }
                DirectoryInfo[] dirs = sourceinfo.GetDirectories();
                
                /*
                textoutput("Count is " + comboBoxAtlas.Items.Count.ToString());
                for (int i = 0; i < comboBoxAtlas.Items.Count; i++)
                {
                    textoutput("i=" + i.ToString() + "    item=" + comboBoxAtlas.Items[i].ToString());
                }*/

                int count = comboBoxAtlas.Items.Count;
                for (int i = 0; i < count; i++)
                {
                    //textoutput("removing " + comboBoxAtlas.Items[0].ToString());
                    comboBoxAtlas.Items.RemoveAt(0);
                    //textoutput("Count after remove is " + comboBoxAtlas.Items.Count.ToString());
                }

                foreach (DirectoryInfo dirinfo in dirs)
                {
                    comboBoxAtlas.Items.Add(dirinfo.Name);
                    //textoutput("adding " + dirinfo.Name);
                }
                
            }
            catch //(Exception exc)
            {
                //textoutput("<RED>updatecomboboxatlas Error Exception:" + exc.Message+"\n");
            }
        }


        

        


        /// <summary>
        /// huha´s DirectoryCopy
        /// Copies a directory source including its subdirectories to a directory destination 
        /// If destination does not exists it will be created
        /// </summary>
        /// <param name="string source">string to the path of the source directory</param>
        /// <param name="string destination">string to the path of the destination directory</param>
        /// <param name="string filepattern">Filter for files (use "*" for all files)</param>
        /// <param name="bool overwrite">use "true" for overwriting existing files in the destination directory or otherwise "false" </param>
        /// <param name="bool verbose">use "true" for verbose output and logging</param>
        /// <param name="bool recursive">use "true" for including recursive directories</param>
        public void DirectoryList(string source,  string filepattern, bool verbose, bool recursive)
        {
            // Copy files.

            if (source.Contains(".picasaoriginals") == false) //skip picasa original folders and all subfolders of them
            {
                folder_list.Add(source);
                DirectoryInfo sourcedir = new DirectoryInfo(source);
                FileInfo[] files = sourcedir.GetFiles(filepattern);
                foreach (FileInfo file in files)
                {

                    try
                    {
                        if (file.FullName.Contains("_original") == false)
                        {
                            file_list.Add(file.FullName);
                        }
                    }
                    catch (Exception exc)
                    {
                        textoutput("<RED>DirectoryCopy Error: Could not add to list file " + file.FullName + " - Exception:" + exc.Message+"\n");
                    }

                }

                // subdirectories are being called recursively
                if (recursive)
                {
                    DirectoryInfo sourceinfo = new DirectoryInfo(source);
                    DirectoryInfo[] dirs = sourceinfo.GetDirectories();

                    foreach (DirectoryInfo dir in dirs)
                    {
                        string dirstring = dir.FullName;
                        FileAttributes attributes = dir.Attributes;

                        if (((attributes & FileAttributes.Hidden) != FileAttributes.Hidden) && ((attributes & FileAttributes.System) != FileAttributes.System))
                            DirectoryList(dirstring, filepattern, verbose, recursive);
                    }
                }
            }
        }


        private void textoutput(string textlines)
        {
            logtext += textlines + "\n";
            string text = "";

            if (listView1 != null)
            {
                Color mycolor;
                if (textlines.StartsWith("<RED>") == true)
                {//color red
                    textlines = textlines.Substring(5, textlines.Length - 5);
                    mycolor = Color.Red;
                    
                }
                else if (textlines.StartsWith("<YELLOW>") == true)
                {//color yellow
                    textlines = textlines.Substring(8, textlines.Length - 8);
                    mycolor = Color.Orange;
                   
                }
                else if (textlines.StartsWith("<GREEN>") == true)
                {//color green
                    textlines = textlines.Substring(8, textlines.Length - 8);
                    mycolor = Color.Green;
                    
                }
                else
                {//color black
                    mycolor = Color.Black;
                    
                }

                char[] splitterchars = { '\n' };  //split lines with \n
                string[] lines = textlines.Split(splitterchars);
                foreach (string line in lines)
                {
                    


                    text = line;
                    while (text.Length > 70)   //split long lines
                    {
                        int linelength = 69;
                        for (int i = 69; i >= 4; i--)
                        {
                            if (text[i] == ' ')
                            {
                                linelength = i;
                                break;
                            }
                        }
                        string pretext = text.Substring(0, linelength);

                        listView1.Items.Add(pretext);
                        listView1.Items[listView1.Items.Count - 1].ForeColor = mycolor;
                        listView1.Items[listView1.Items.Count - 1].EnsureVisible();

                       

                        text = "+  " + text.Substring(linelength, text.Length - linelength);

                    }

                    listView1.Items.Add(text);
                    listView1.Items[listView1.Items.Count - 1].ForeColor = mycolor;
                    listView1.Items[listView1.Items.Count - 1].EnsureVisible();

                }

            }
        }

        

        private void buttonDestination_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog selectfolder = new FolderBrowserDialog();
            selectfolder.Description = "Select Destination Folder on Smartphone";
            if (selectfolder.ShowDialog(this) == DialogResult.OK)
                textBoxDestination.Text = selectfolder.SelectedPath;

        }

        
        private void selectfilenamebutton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog selectfolder = new FolderBrowserDialog();
            selectfolder.Description = "Select Mobile Creator Atlas Folder";
            selectfolder.SelectedPath = textBoxMACFolder.Text;
            if (selectfolder.ShowDialog(this) == DialogResult.OK)
                textBoxMACFolder.Text = selectfolder.SelectedPath;
        }

        private void textBoxMACFolder_TextChanged(object sender, EventArgs e)
        {
            updatecomboboxatlas();
        }

        private void buttonProcess_Click(object sender, EventArgs e)
        {
            System.Threading.Thread th = new System.Threading.Thread(MACProcessingThread);
            th.Start();
        }

        private void MACProcessingThread()
        {
            try
            {
                if (Directory.Exists(textBoxDestination.Text) == false)
                {
                    textoutput("<RED>Your destination folder does not exist - create it first\n" + textBoxDestination.Text + "\n\n");
                    return;
                }

                if (Directory.Exists(textBoxMACFolder.Text) == false)
                {
                    textoutput("<RED>Your MOBAC atlas folder does not exist - you selected\n" + textBoxMACFolder.Text + "\n\n");
                    return;
                }

                if (comboBoxAtlas.Text == "")
                {
                    textoutput("<RED>No Atlas name defined\n");
                    return;
                }

                //mapping
                /*
                84	Open Street Map			PNG 	256x256		Mapnik
                85	Open cycle Map (Topo)		PNG 	256x256  	OSM Cycle Map
                87	Open Street map (Direct)	PNG 	256x256		MapQuest
                88	OSM Midnight Commander		PNG 	256x256		OSM CloudMade 999
                89	OSM-Fresh			PNG 	256x256		OpenStreetMap Hikebikemap.de
                129	OpenPiste Map			PNG 	256x256		OpenPisteMapBCL
                */

                //mapping
                textoutput("Mapping Atlas file");

                //copy folder into temp folder
                string tempdir = textBoxMACFolder.Text + @"\tempfolderconverter564956495";
                if (Directory.Exists(tempdir) == false)
                {
                    Directory.CreateDirectory(tempdir);
                }
                DirectoryCopy(textBoxMACFolder.Text + "\\" + comboBoxAtlas.Text, tempdir, "*", true, DEBUG, true);


                string basefolder = tempdir;
                if (DEBUG)
                    textoutput("basefolder="+basefolder);

                string viewranger_foldernumber = "NOTFOUND";
                if (Directory.Exists(basefolder + @"\Mapnik") == true)
                {
                    textoutput("Mapnik found");
                    Directory.Move(basefolder + @"\Mapnik", basefolder + @"\84");
                    viewranger_foldernumber = @"\84";
                }
                else if (Directory.Exists(basefolder + @"\OSM Cycle Map") == true)
                {
                    textoutput("OSM Cycle Map found");
                    Directory.Move(basefolder + @"\OSM Cycle Map", basefolder + @"\85");
                    viewranger_foldernumber = @"\85";
                }
                else if (Directory.Exists(basefolder + @"\MapQuest") == true)
                {
                    textoutput("MapQuest found");
                    Directory.Move(basefolder + @"\MapQuest", basefolder + @"\87");
                    viewranger_foldernumber = @"\87";
                }
                else if (Directory.Exists(basefolder + @"\OSM CloudMade 999") == true)
                {
                    textoutput("OSM CloudMade 999 found");
                    Directory.Move(basefolder + @"\OSM CloudMade 999", basefolder + @"\88");
                    viewranger_foldernumber = @"\88";
                }
                else if (Directory.Exists(basefolder + @"\OpenStreetMap Hikebikemap.de") == true)
                {
                    textoutput("OpenStreetMap Hikebikemap.de found");
                    Directory.Move(basefolder + @"\OpenStreetMap Hikebikemap.de", basefolder + @"\89");
                    viewranger_foldernumber = @"\89";
                }
                else if (Directory.Exists(basefolder + @"\OpenPisteMapBCL") == true)
                {
                    textoutput("OpenPisteMapBCL found");
                    Directory.Move(basefolder + @"\OpenPisteMapBCL", basefolder + @"\129");
                    viewranger_foldernumber = @"\129";
                }
                else
                {
                    textoutput("<RED>Your map cannot be converted to VR. Chose one of the folling formats:\nMapnik\nOSM Cycle Map\nMapQuest\nOSM CloudMade 999\nOpenStreetMap Hikebikemap.de\nOpenPisteMapBCL\n\n" + textBoxDestination.Text + "\n\n");
                }


                //process PNG files
                textoutput("Processing pictures");
                file_list = new List<String>();
                folder_list = new List<String>();
                DirectoryList(basefolder, "*.png",DEBUG,true);
                foreach (string filename in file_list)
                {
                    string newname = filename.Substring(0, filename.Length - 4)+textBoxPNG.Text;
                    File.Move(filename, newname);
                    if (DEBUG)
                        textoutput("Moving "+filename+" to "+newname+"\n");
                }


                //copy to smartphone folder
                textoutput("Copying atlas to destination");
                if (viewranger_foldernumber != "NOTFOUND")
                {
                    //directorycopy
                    DirectoryCopy(basefolder + viewranger_foldernumber, textBoxDestination.Text+viewranger_foldernumber, "*",true,DEBUG,true);
                }

                //delete tempdir
                Directory.Delete(tempdir, true);

                textoutput("All done\n\n");
            }
            catch //(Exception exc)
            {
                //textoutput("<RED>MACProcessingThread Exception:" + exc.Message + "\n");
            }

        }




        /// <summary>
        /// huha´s DirectoryCopy
        /// Copies a directory source including its subdirectories to a directory destination 
        /// If destination does not exists it will be created
        /// </summary>
        /// <param name="string source">string to the path of the source directory</param>
        /// <param name="string destination">string to the path of the destination directory</param>
        /// <param name="string filepattern">Filter for files (use "*" for all files)</param>
        /// <param name="bool overwrite">use "true" for overwriting existing files in the destination directory or otherwise "false" </param>
        /// <param name="bool verbose">use "true" for verbose output and logging</param>
        /// <param name="bool recursive">use "true" for including recursive directories</param>
        public void DirectoryCopy(string source, string destination, string filepattern, bool overwrite, bool verbose, bool recursive)
        {
            if (!File.Exists(destination))
            {
                try
                {
                    Directory.CreateDirectory(destination);
                }
                catch (Exception exc)
                {
                    textoutput("DirectoryCopy Error: Could not create "+destination+" - Exception: "+exc.Message+"\n");
                }
            }

            // Copy files.
            DirectoryInfo sourceDir = new DirectoryInfo(source);
            FileInfo[] files = sourceDir.GetFiles(filepattern);
            foreach (FileInfo file in files)
            {
                if (file.Name != "BackupSettings.dll")  //legacy suuport - do not restore old version which would result in 2 dll files
                {
                    try
                    {
                        if (!File.Exists(destination + "\\" + file.Name))
                        { // file does not exist

                            File.Copy(file.FullName, destination + "\\" + file.Name, false);
                            if (verbose)
                            {
                                textoutput("Copied: " + file.Name);
                            }

                        }
                        else if (overwrite == false) // and file does exist => do not copy
                        {
                            if (verbose)
                            {
                                textoutput("Exists:" + file.Name);
                            }
                        }
                        else // file does exist
                        { // do overwrite files
                            // check for read only protection
                            FileAttributes attribute = File.GetAttributes(destination + "\\" + file.Name);

                            if ((attribute & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                            {
                                if (verbose)
                                {
                                    textoutput("ReadOnly: " + file.Name);
                                }
                            }
                            else if ((attribute & FileAttributes.Hidden) == FileAttributes.Hidden)
                            {
                                if (verbose)
                                {
                                    textoutput("Hidden: " + file.Name);
                                }
                            }
                            else if ((attribute & FileAttributes.System) == FileAttributes.System)
                            {
                                if (verbose)
                                {
                                    textoutput("System: " + file.Name);
                                }
                            }
                            else
                            {
                                File.Copy(file.FullName, destination + "\\" + file.Name, true);
                                if (verbose)
                                {
                                    textoutput("Copied: " + file.Name);
                                }
                            }
                        }

                    }
                    catch (Exception exc)
                    {
                        textoutput("<RED>DirectoryCopy Error: Could not copy file " + file.FullName + " to " + destination + "\\" + file.Name + " - Exception:" + exc.Message+"\n");
                    }
                }
            }

            // subdirectories are being called recursively
            if (recursive)
            {
                DirectoryInfo sourceinfo = new DirectoryInfo(source);
                DirectoryInfo[] dirs = sourceinfo.GetDirectories();

                foreach (DirectoryInfo dir in dirs)
                {
                    string dirstring = dir.FullName;
                    FileAttributes attributes = dir.Attributes;

                    if (((attributes & FileAttributes.Hidden) != FileAttributes.Hidden) && ((attributes & FileAttributes.System) != FileAttributes.System))
                        DirectoryCopy(dirstring, destination + "\\" + dir, filepattern, overwrite, verbose, recursive);
                }
            }
        }

        private void buttonInputGPXSelect_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog selectfile = new OpenFileDialog();
                selectfile.Title = "Select Input .gpx Route File";
                selectfile.Filter = "GPX Route Files (*.gpx)|*.gpx";
                selectfile.FileName = textBoxInputGPX.Text;
                if (selectfile.ShowDialog(this) == DialogResult.OK)
                {
                    textBoxInputGPX.Text = selectfile.FileName;
                    gpxoutputfilename();
                }
            }
            catch (Exception exc)
            {
                textoutput("<RED>Failed to select .gpx file ");
                textoutput("<RED>Exception message is " + exc.Message + "\n");
                return;
            }
        }

        private void textBoxInputGPX_TextChanged(object sender, EventArgs e)
        {
            gpxoutputfilename();
        }

        private void checkBoxNewVoice_CheckedChanged(object sender, EventArgs e)
        {
            gpxoutputfilename();

        }

        private void checkBoxReverseRoutes_CheckedChanged(object sender, EventArgs e)
        {
            gpxoutputfilename();
        }


        private void gpxoutputfilename()
        {
            FileInfo myinputfileinfo;

            try
            {
                myinputfileinfo = new FileInfo(textBoxInputGPX.Text);
            }
            catch
            {
                return;
            }

            if (textBoxInputGPX.Text == "")
            {
                return;
            }
            

            try
            {

                FileInfo myoutputfileinfo = new FileInfo(textBoxOutputGPX.Text);
                if (myoutputfileinfo.DirectoryName != "")
                {
                    string shortname = myinputfileinfo.Name.Substring(0, myinputfileinfo.Name.Length - myinputfileinfo.Extension.Length);

                    if (checkBoxNewVoice.Checked == false)
                    {
                        textBoxOutputGPX.Text = myoutputfileinfo.DirectoryName + "\\" + shortname +  myinputfileinfo.Extension;
                    }
                    else
                    {
                        if (checkBoxReverseRoutes.Checked == false)
                        {
                            textBoxOutputGPX.Text = myoutputfileinfo.DirectoryName + "\\" + FILE_PREFIX + shortname + FILE_POSTFIX + myinputfileinfo.Extension;
                        }
                        else
                        {
                            textBoxOutputGPX.Text = myoutputfileinfo.DirectoryName + "\\" + REVERSE_FILE_PREFIX + shortname + REVERSE_FILE_POSTFIX + myinputfileinfo.Extension;
                        }
                    }
                }
                else
                {
                    string shortname = myinputfileinfo.Name.Substring(0, myinputfileinfo.Name.Length - myinputfileinfo.Extension.Length);

                    if (checkBoxNewVoice.Checked == false)
                    {
                        textBoxOutputGPX.Text = myinputfileinfo.DirectoryName + "\\" +  shortname +  myinputfileinfo.Extension;
                    }
                    else
                    {
                        if (checkBoxReverseRoutes.Checked == false)
                        {
                            textBoxOutputGPX.Text = myinputfileinfo.DirectoryName + "\\" + FILE_PREFIX + shortname + FILE_POSTFIX + myinputfileinfo.Extension;
                        }
                        else
                        {
                            textBoxOutputGPX.Text = myinputfileinfo.DirectoryName + "\\" + REVERSE_FILE_PREFIX + shortname + REVERSE_FILE_POSTFIX + myinputfileinfo.Extension;
                        }
                    }
                }
            }
            catch //(Exception exc)
            {
                string shortname = myinputfileinfo.Name.Substring(0, myinputfileinfo.Name.Length - myinputfileinfo.Extension.Length);

                if (checkBoxNewVoice.Checked == false)
                {
                    textBoxOutputGPX.Text = myinputfileinfo.DirectoryName + "\\" +  shortname +  myinputfileinfo.Extension;
                }
                else
                {
                    if (checkBoxReverseRoutes.Checked == false)
                    {
                        textBoxOutputGPX.Text = myinputfileinfo.DirectoryName + "\\" + FILE_PREFIX + shortname + FILE_POSTFIX + myinputfileinfo.Extension;
                    }
                    else
                    {
                        textBoxOutputGPX.Text = myinputfileinfo.DirectoryName + "\\" + REVERSE_FILE_PREFIX + shortname + REVERSE_FILE_POSTFIX + myinputfileinfo.Extension;
                    }
                }
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Exitfunction();
        }

        private void buttonExit2_Click(object sender, EventArgs e)
        {
            Exitfunction();
        }

        private void Exitfunction()
        {
            String filestring = textBoxMACFolder.Text + "\r\n" + textBoxDestination.Text + "\r\n" + textBoxPNG.Text + "\r\n" + textBoxInputGPX.Text + "\r\n" + textBoxOutputGPX.Text + "\r\n" + textBoxVoiceFolder.Text + "\r\n" + textBoxVoiceExtension.Text + "\r\n" + checkBoxRenumber.Checked.ToString() + "\r\n" + checkBoxNewVoice.Checked.ToString() + "\r\n" + checkBoxReverseRoutes.Checked.ToString() + "\r\nFILE_PREFIX=" + FILE_PREFIX + "\r\nFILE_POSTFIX=" + FILE_POSTFIX + "\r\nNAME_PREFIX=" + NAME_PREFIX + "\r\nNAME_POSTFIX=" + NAME_POSTFIX + "\r\nREVERSE_FILE_PREFIX=" + REVERSE_FILE_PREFIX + "\r\nREVERSE_FILE_POSTFIX=" + REVERSE_FILE_POSTFIX + "\r\nREVERSE_NAME_PREFIX=" + REVERSE_NAME_PREFIX + "\r\nREVERSE_NAME_POSTFIX=" + REVERSE_NAME_POSTFIX;
            File.WriteAllText("settings.ini", filestring);
            File.WriteAllText("logfile.txt", logtext);
            Application.Exit();
        }

        private void buttonProcessGPX_Click(object sender, EventArgs e)
        {
            System.Threading.Thread th = new System.Threading.Thread(GPXProcessingThread);
            th.Start();
        }

        private void GPXProcessingThread()
        {
            try
            {
                textoutput("Reading GPX file");
                XmlDocument doc = new XmlDocument();
                string tmpfile = textBoxInputGPX.Text + ".tmp4387547417";
                string startfile = "";

                try
                {
                    string filestring = File.ReadAllText(textBoxInputGPX.Text);
                    

                    
                        filestring = filestring.Replace("<name><![CDATA[" + NAME_PREFIX,"<name><![CDATA[");
                        filestring = filestring.Replace(NAME_POSTFIX + "]]></name>", "]]></name>");
                        filestring = filestring.Replace("<name><![CDATA[" + REVERSE_NAME_PREFIX, "<name><![CDATA[");
                        filestring = filestring.Replace(REVERSE_NAME_POSTFIX + "]]></name>", "]]></name>");

                        int tagpos1 = filestring.IndexOf("<alarm>");
                        int tagpos2 = filestring.IndexOf("</alarm>");
                        while ((tagpos1 > 0) && (tagpos2 > 0))
                        {
                            string replacestring = filestring.Substring(tagpos1, tagpos2-tagpos1+8);
                            if (DEBUG)
                                textoutput("replacestring=" + replacestring);

                            filestring = filestring.Replace(replacestring, "");
                            tagpos1 = filestring.IndexOf("<alarm>");
                            tagpos2 = filestring.IndexOf("</alarm>");
                        }

                    

                    filestring = filestring.Replace("<extensions>", "<extensions><![CDATA[");
                    filestring = filestring.Replace("</extensions>", "]]></extensions>");

                    if (checkBoxNewVoice.Checked == true)
                    {
                        if (checkBoxReverseRoutes.Checked == false)
                        {
                            filestring = filestring.Replace("<name><![CDATA[", "<name><![CDATA[" + NAME_PREFIX);
                            filestring = filestring.Replace("]]></name>", NAME_POSTFIX + "]]></name>");
                        }
                        else
                        {
                            filestring = filestring.Replace("<name><![CDATA[", "<name><![CDATA[" + REVERSE_NAME_PREFIX);
                            filestring = filestring.Replace("]]></name>", REVERSE_NAME_POSTFIX + "]]></name>");
                        }
                    }

                    int pos1 = filestring.IndexOf("<rte>");                 
                    int pos2 = filestring.IndexOf("</gpx>");
                    if ((pos1 > 0) && (pos2 > 0))
                    {
                        startfile = filestring.Substring(0, pos1 - 1);
                        filestring = filestring.Substring(pos1, pos2 - pos1-1);
                        filestring = "<gpx>" + filestring + "</gpx>";
                    }
                    else
                    {
                        textoutput("<rte> not found in .gpx file - check you have a .gpx route file (no track file and no POI!)");
                        textoutput("<RED> Invalid positions in .gpx file pos1=" + pos1.ToString() + " pos2=" + pos2.ToString()+"\n\n");
                        return;
                    }
                    
                    File.WriteAllText(tmpfile, filestring);
                    doc.Load(tmpfile);
                    
                }
                catch (Exception exc)
                {
                    textoutput("<RED>Failed to load xml document ");
                    textoutput("<RED>Exception message is " + exc.Message+"\n");
                    return;
                }
                
                XmlElement root = doc.DocumentElement;
                
                XmlNodeList nodeallroutes = root.SelectNodes("/gpx/rte");
                int routecount = nodeallroutes.Count;
                textoutput(routecount.ToString() + " routes found");
                int rte_ctr=0;
                foreach (XmlNode noderoute in nodeallroutes)
                {
                    gpx_rtepoints = new List<Gpx_routepoint>();

                    XmlNodeList routenodenames = noderoute.SelectNodes("name");

                    foreach (XmlNode nodename in routenodenames)
                    {
                        routename = nodename.InnerText;
                    }

                    XmlNodeList nodepoints = noderoute.SelectNodes("rtept");

                    if ((checkBoxReverseRoutes.Checked == true)&&(rte_ctr < routecount))
                    {//reverse routes
                        textoutput("Reversing route "+routename);
                        XmlNode clone = noderoute.Clone();
                        XmlNodeList revroutenodenames = noderoute.SelectNodes("name");

                        foreach (XmlNode nodename in revroutenodenames)
                        {
                            nodename.InnerText=routename;
                        }
                        
                        XmlNodeList clone_nodepoints = clone.SelectNodes("rtept");
                        foreach (XmlNode nodename in clone_nodepoints)
                        {
                            clone.RemoveChild(nodename);
                            //textoutput("removing rte node");
                        }
                        
                        

                        for (int i = nodepoints.Count-1; i >= 0; i--)
                        {
                            clone.AppendChild(nodepoints[i]);
                            //textoutput("adding reversed node "+i.ToString());
                        }

                        noderoute.RemoveAll();
                        XmlNode nodegpxroutes = root.SelectSingleNode("/gpx");
                        nodegpxroutes.AppendChild(clone);
                        
                        nodepoints = clone.SelectNodes("rtept");

                        //textoutput("End Reversing route " + routename);
                    }//end reverse routes

                    rte_ctr++;

                    int ctr = 0;
                    foreach (XmlNode nodepoint in nodepoints)
                    {
                        string longitude = nodepoint.Attributes["lon"].Value;
                        string latitude = nodepoint.Attributes["lat"].Value;

                        XmlNodeList nodenames = nodepoint.SelectNodes("name");
                        
                        foreach (XmlNode nodename in nodenames)
                        {
                            name = nodename.InnerText;
                            int wptctr = ctr + 1;
                            if (checkBoxRenumber.Checked == true)
                            {
                                if (checkBoxNewVoice.Checked == true)
                                {
                                    if (checkBoxReverseRoutes.Checked == true)
                                    {
                                        nodename.InnerText = REVERSE_NAME_PREFIX + "WPT" + wptctr.ToString("D4") + REVERSE_NAME_POSTFIX;
                                    }
                                    else
                                    {
                                        nodename.InnerText = NAME_PREFIX + "WPT" + wptctr.ToString("D4") + NAME_POSTFIX;
                                    }

                                }
                                else 
                                {
                                    nodename.InnerText = "WPT" + wptctr.ToString("D4");
                                }
                                

                            }
                        }

                        XmlNodeList nodecmts = nodepoint.SelectNodes("cmt");
                        string cmt = "";
                        foreach (XmlNode nodecmt in nodecmts)
                        {
                            cmt = nodecmt.InnerText;
                            //textoutput("cmt =" + cmt);
                        }

                        /*
                        if (DEBUG)
                        {
                            textoutput(ctr.ToString());
                            textoutput("name=" + name);
                            textoutput("cmt=" + cmt);
                            textoutput("lon=" + longitude);
                            textoutput("lat=" + latitude);
                            textoutput("");
                            
                        }*/

                        Gpx_routepoint myroutepoint = new Gpx_routepoint();
                        myroutepoint.Name = name;
                        myroutepoint.Description = cmt;
                        myroutepoint.angle1 = -1.0;
                        myroutepoint.angle2 = -1.0;
                        myroutepoint.angle1to2 = -1.0;
                        myroutepoint.voice = "none";
                        myroutepoint.lat = Convert.ToDouble(latitude, System.Globalization.NumberFormatInfo.InvariantInfo);
                        myroutepoint.lon = Convert.ToDouble(longitude, System.Globalization.NumberFormatInfo.InvariantInfo);
                        gpx_rtepoints.Add(myroutepoint);

                        /*if (DEBUG)
                        {
                            textoutput("lon=" + myroutepoint.lon.ToString());
                            textoutput("lat=" + myroutepoint.lat.ToString());
                            textoutput("");
                        }*/

                        ctr++;
                        
                    }

                    //process route for nav commands
                    if (checkBoxNewVoice.Checked == true)
                    {
                        calculatevoicenavigation(routename);
                    }

                    //update xml information
                    ctr = 0;
                    foreach (XmlNode nodepoint in nodepoints)
                    {
                        XmlNodeList nodecmts = nodepoint.SelectNodes("cmt");
                        foreach (XmlNode nodecmt in nodecmts)
                        {
                            nodepoint.RemoveChild(nodecmt);
                        }
                                               
                        //create new node
                        XmlNode newnodecmt = doc.CreateElement("cmt");
                        XmlCDataSection cdata = doc.CreateCDataSection(gpx_rtepoints[ctr].Description);
                        //textoutput("update cmt=" + gpx_rtepoints[ctr].Description);
                        newnodecmt.AppendChild(cdata);
                        nodepoint.AppendChild(newnodecmt);

                        ctr++;
                    }

                    //end single route

                }
                
                textoutput("Writing GPX file");
                try
                {
                    doc.Save(tmpfile);
                }
                catch (Exception exc)
                {
                    textoutput("<RED>Failed to load xmlDoc.Load(fs) for writing");
                    textoutput("<RED>Exception message is " + exc.Message+"\n");
                    return;
                }

                string outfilestring = File.ReadAllText(tmpfile);

                outfilestring = outfilestring.Replace("  <rte>\r\n  </rte>\r\n", "");
                outfilestring = outfilestring.Replace("<extensions><![CDATA[", "<extensions>");
                outfilestring = outfilestring.Replace( "]]></extensions>","</extensions>");
                outfilestring = outfilestring.Replace("<gpx>", "");

                File.WriteAllText(textBoxOutputGPX.Text,startfile+ outfilestring);

                File.Delete(tmpfile);

                textoutput("Completed\n\n");

            }
            catch (Exception exc)
            {
                textoutput("<RED>GPXProcessingThread Error Exception:" + exc.Message + "\n");
            }

        }


        private void calculatevoicenavigation(string name)
        {
            try
            {
                
                
                double PHI_A = 0.0;
                double PHI_B = 0.0;
                double LAMB_A = 0.0;
                double LAMB_B = 0.0;
                double a = 0.0;
                double beta = 0.0;
                double b = 0.0;
                double alfa = 0.0;
                double c = 0.0;
                double gamma = 0.0;
                string file = "";
                string path = textBoxVoiceFolder.Text + "\\";
                path = path.Replace(@"\\", @"\");
                string extension = textBoxVoiceExtension.Text;

                if (textBoxVoiceFolder.Text == string.Empty)
                {
                    path = string.Empty;
                }
                

                int elements = gpx_rtepoints.Count;

                if (elements < 3)
                {
                    textoutput("Less than 3 way points in route - cannot calculate direction");
                    return;
                }
                textoutput("Creating new voice file tags for "+name);
                for (int i = 1; i < elements-1; i++)
                {
                    PHI_A = gpx_rtepoints[i - 1].lat/180.0*Math.PI;       //i-1
                    LAMB_A = gpx_rtepoints[i - 1].lon / 180.0 * Math.PI;
                    PHI_B = gpx_rtepoints[i].lat / 180.0 * Math.PI;       //i
                    LAMB_B = gpx_rtepoints[i].lon / 180.0 * Math.PI;

                    a = (Math.Cos(PHI_A) * Math.Sin(PHI_B) - Math.Cos(LAMB_A - LAMB_B) * Math.Cos(PHI_B) * Math.Sin(PHI_A));
                    b = (Math.Cos(PHI_A) * Math.Sin(PHI_B) - Math.Cos(LAMB_A - LAMB_B) * Math.Cos(PHI_A) * Math.Sin(PHI_B));
                    c = Math.Sqrt(1 - Math.Pow((Math.Cos(LAMB_A - LAMB_B) * Math.Cos(PHI_A) * Math.Cos(PHI_B) + Math.Sin(PHI_A) * Math.Sin(PHI_B)), 2));
                    alfa = Math.Acos(a / c) * 180/Math.PI;
                    if ((LAMB_B - LAMB_A) < 0.0)
                    {
                        alfa = alfa * (-1);
                    }
                    

                    PHI_A = gpx_rtepoints[i].lat / 180.0 * Math.PI;       //i
                    LAMB_A = gpx_rtepoints[i].lon / 180.0 * Math.PI;
                    PHI_B = gpx_rtepoints[i+1].lat / 180.0 * Math.PI;       //i+1
                    LAMB_B = gpx_rtepoints[i+1].lon / 180.0 * Math.PI;

                    a = (Math.Cos(PHI_A) * Math.Sin(PHI_B) - Math.Cos(LAMB_A - LAMB_B) * Math.Cos(PHI_B) * Math.Sin(PHI_A));
                    b = (Math.Cos(PHI_A) * Math.Sin(PHI_B) - Math.Cos(LAMB_A - LAMB_B) * Math.Cos(PHI_A) * Math.Sin(PHI_B));
                    c = Math.Sqrt(1 - Math.Pow((Math.Cos(LAMB_A - LAMB_B) * Math.Cos(PHI_A) * Math.Cos(PHI_B) + Math.Sin(PHI_A) * Math.Sin(PHI_B)), 2));
                    beta = Math.Acos(a / c) * 180 / Math.PI;
                    if ((LAMB_B - LAMB_A) < 0.0)
                    {
                        beta = beta * (-1);
                    }

                    gamma = beta - alfa;
                    if (gamma > 180.0)
                    {
                        gamma = gamma - 360.0;
                    }
                    else if (gamma < -180.0)
                    {
                        gamma = gamma + 360.0;
                    }

                    

                    if ((gamma >= -22.5) && (gamma < 22.5))
                    {
                        file = "0Degree";
                    }
                    else if ((gamma >= 22.5) && (gamma < 67.5))
                    {
                        file = "45Degree";
                    }
                    else if ((gamma >= 67.5) && (gamma < 112.5))
                    {
                        file = "90Degree";
                    }
                    else if ((gamma >= 112.5) && (gamma < 157.5))
                    {
                        file = "135Degree";
                    }
                    else if ((gamma >= 157.5) && (gamma < -157.5))
                    {
                        file = "180Degree";
                    }
                    else if ((gamma >= -157.5) && (gamma < -112.5))
                    {
                        file = "225Degree";
                    }
                    else if ((gamma >= -112.5) && (gamma < -67.5))
                    {
                        file = "270Degree";
                    }
                    else if ((gamma >= -67.5) && (gamma < -22.5))
                    {
                        file = "315Degree";
                    }

                    if (DEBUG)
                    {
                        textoutput("i="+(i+1).ToString());
                        textoutput("alfa=" + alfa.ToString());
                        textoutput("beta=" + beta.ToString());                       
                        textoutput("gamma=" + gamma.ToString());
                        textoutput("");
                    }
                    //file = path+file+extension;

                    Gpx_routepoint mygpxroutepoint = gpx_rtepoints[i];
                    mygpxroutepoint.angle1 = alfa;
                    mygpxroutepoint.angle2 = beta;
                    mygpxroutepoint.angle1to2 = gamma;
                    mygpxroutepoint.Description = mygpxroutepoint.Description + "<alarm><sound " + path + file + extension + ">" + path + file + extension + "</sound></alarm>";                    
                    gpx_rtepoints[i] = mygpxroutepoint; 
                }


                //start and end
                Gpx_routepoint mygpxroutepoint_s = gpx_rtepoints[0];
                mygpxroutepoint_s.Description = mygpxroutepoint_s.Description + "<alarm><sound " + path + "Start" + extension + ">" + path + "Start" + extension + "</sound></alarm>";
                gpx_rtepoints[0] = mygpxroutepoint_s;

                Gpx_routepoint mygpxroutepoint_e = gpx_rtepoints[elements-1];
                mygpxroutepoint_e.Description = mygpxroutepoint_e.Description + "<alarm><sound " + path + "End" + extension + ">" + path + "End" + extension + "</sound></alarm>";
                gpx_rtepoints[elements - 1] = mygpxroutepoint_e;

            }
            catch (Exception exc)
            {
                textoutput("<RED>calculatevoicenavigation Exception:" + exc.Message + "\n");
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void buttonClear2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void buttonProcess_Click_1(object sender, EventArgs e)
        {
            System.Threading.Thread th = new System.Threading.Thread(MACProcessingThread);
            th.Start();
        }

        
        

        
        

        
        

    }
}
