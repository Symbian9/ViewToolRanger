/* 
 *	Copyright (C) 2006-2011 huha
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
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using ShowLicense;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //check for single instance
            Process current = Process.GetCurrentProcess();
            Process[] allnamedprocesses = Process.GetProcessesByName(current.ProcessName);
            int k = allnamedprocesses.Length;
            if (k > 1)
            {
                //MessageBox.Show("More than one single instance running ("+k.ToString()+") - aborting","Error");
                return;
            }

            string licensefile = "license.txt";
            string settingsfile = "settings.ini";

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (File.Exists(settingsfile) == false)
            {
                
                if (File.Exists(licensefile) == false)
                {
                    MessageBox.Show("License file license.txt not found - aborting installation", "Installation Error");
                    return;
                }
                Application.Run(new License(licensefile));
            }


            
            
            Application.Run(new Form1(args));
        }
    }
}
