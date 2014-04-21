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
using System.Linq;
using System.Text;

namespace Utilities
{
    class SimpleCommandlineparser
    {
        public string BoolParser(string[] arguments, string command, string shortcommand, ref bool boolvalue, ref bool found)
        {
            string message = "";
            Int32 integervalue = -1;
            Double doublevalue = -1.0;
            string stringvalue = "";
            message = Parser(arguments, command, shortcommand, true, ref boolvalue, false, ref integervalue, false, ref doublevalue, false, ref stringvalue, ref found);
            return message;
        }

        public string IntegerParser(string[] arguments, string command, string shortcommand, ref Int32 integervalue, ref bool found)
        {
            string message = "";
            bool boolvalue = true;
            Double doublevalue = -1.0;
            string stringvalue = "";
            message = Parser(arguments, command, shortcommand, false, ref boolvalue, true, ref integervalue, false, ref doublevalue, false, ref stringvalue, ref found);
            return message;
        }

        public string DoubleParser(string[] arguments, string command, string shortcommand, ref double doublevalue, ref bool found)
        {
            string message = "";
            bool boolvalue = true;
            Int32 integervalue = -1;
            string stringvalue = "";
            message = Parser(arguments, command, shortcommand, false, ref boolvalue, false, ref integervalue, true, ref doublevalue, false, ref stringvalue, ref found);
            return message;
        }

        public string StringParser(string[] arguments, string command, string shortcommand, ref string stringvalue, ref bool found)
        {
            string message = "";
            bool boolvalue = true;
            Double doublevalue = -1.0;
            Int32 integervalue = -1;
            message = Parser(arguments, command, shortcommand, false, ref boolvalue, false, ref integervalue, false, ref doublevalue, true, ref stringvalue, ref found);
            return message;
        }

        public string Parser(string[] arguments, string command, string shortcommand, bool convertbool, ref bool boolvalue, bool convertinteger, ref Int32 integervalue, bool convertdouble, ref double doublevalue, bool convertstring, ref string stringvalue, ref bool found)
        {
            string value = "NOTFOUND";
            found = false;
            string message = "";

            try
            {
                for (int i = 0; i < arguments.Length; i++)
                {
                    if (arguments[i].ToUpper().StartsWith(command.ToUpper()))
                    {
                        found = true;
                        message += "Found command " + command + "\n";
                        if (arguments[i].ToUpper() == command.ToUpper())  //command match value is in next argument
                        {
                            if ((i + 1) < arguments.Length)
                            {
                                value = arguments[i + 1];
                                message += "Found value=" + value + "\n";
                            }

                        }
                        else //value is within same argument
                        {
                            value = arguments[i].Substring(command.Length, arguments[i].Length - command.Length);


                            if (value.StartsWith("="))
                            {
                                value = value.Substring(1, value.Length - 1);
                            }
                            message += "Found value=" + value + "\n";
                        }
                    }
                    else if (arguments[i].ToUpper().StartsWith(shortcommand.ToUpper()))
                    {
                        found = true;
                        message += "Found shortcommand " + shortcommand + "\n";
                        if (arguments[i].ToUpper() == shortcommand.ToUpper())  //command match value is in next argument
                        {
                            if ((i + 1) < arguments.Length)
                            {
                                value = arguments[i + 1];
                                message += "Found value=" + value + "\n";
                            }

                        }
                        else //value is within same argument
                        {
                            value = arguments[i].Substring(shortcommand.Length, arguments[i].Length - shortcommand.Length);

                            if (value.StartsWith("="))
                            {
                                value = value.Substring(1, value.Length - 1);
                            }
                            message += "Found value=" + value + "\n";
                        }
                    }

                }

                //postprocess value
                if (found == false)
                {
                    found = false;
                    return message;
                }
                message += "Processing value=" + value + "\n";

                if (convertbool == true)
                {
                    if (found == true)
                    {
                        boolvalue = true;
                        message += "boolvalue = true";
                    }

                    if (value != "")
                    {
                        try
                        {
                            boolvalue = Convert.ToBoolean(value);
                            message += "boolvalue =" + boolvalue.ToString();
                        }
                        catch
                        {
                            message += "Failed to convert bool value for " + value;
                        }
                    }


                }

                if (convertdouble == true)
                {
                    if (value != "")
                    {
                        try
                        {
                            doublevalue = Convert.ToDouble(value);
                            message += "doublevalue =" + doublevalue.ToString();
                        }
                        catch
                        {
                            message += "Failed to convert double value for " + value;
                        }
                    }
                }

                if (convertinteger == true)
                {
                    if (value != "")
                    {
                        try
                        {
                            integervalue = Convert.ToInt32(value);
                            message += "integervalue =" + integervalue.ToString();
                        }
                        catch
                        {
                            message += "Failed to convert integer value for " + value;
                        }
                    }
                }

                if (convertstring == true)
                {
                    if (value != "")
                    {
                        try
                        {
                            stringvalue = value;
                            message += "stringvalue =" + convertstring.ToString();
                        }
                        catch
                        {
                            message += "Failed to convert string value for " + value;
                        }
                    }
                }

            }
            catch (Exception exc)
            {
                message += "Exception: " + exc.Message + "\n";
            }
            return message;
        }
    }
}
