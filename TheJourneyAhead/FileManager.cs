using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TheJourneyAhead
{
    public class FileManager
    {
        #region variables

        enum LoadType { Attributes, Contents };

        LoadType type;

        List<string> temptAttributes;
        List<string> tempContents;
        List<string> tempTBC;

        bool identifierFound = false; //Checks that you are still using file

        #endregion


        #region Methods

        public void WriteContent(string fl, string text)//writes to file
        {

            
            //FileStream fs = new FileStream(filename, FileMode.Append, FileAccess.Write);
            //if (!Directory.Exists(filename))
            //{
           //     System.IO.Directory.CreateDirectory(filename);
            //}
            using (StreamWriter wr = new StreamWriter(fl))            
            {
                wr.AutoFlush = true;                
                wr.Write(text);                
                wr.Flush();                
                wr.Close();

            }


        }

        public void readContent(string fl, List<string> lines)
        {

            using (StreamReader reader = new StreamReader(fl))
            {
                string line = reader.ReadLine();
                lines.Add(line);
            }
        }

        public void LoadContent(string filename, List<List<string>> attributes, List<List<string>> contents, List<string> tbc)
        {
            tempTBC = new List<string>();
            using (StreamReader reader = new StreamReader(filename))
            {

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if (line.Contains("Load=")) //The attribute name e.g Position
                    {
                        temptAttributes = new List<string>();
                        line = line.Remove(0, line.IndexOf("=") + 1);
                        type = LoadType.Attributes;
                    }
                    else
                    {
                        type = LoadType.Contents; //What the attributes contain e.g 350 220
                    }

                    tempContents = new List<string>();
                    string[] lineArray = line.Split(']'); //Seperates the different attributes and contents

                    foreach (string li in lineArray)
                    {
                        string newLine = li.Trim('[', ' ', ']');
                        if (newLine != String.Empty)
                        {
                            if (type == LoadType.Contents)
                                tempContents.Add(newLine);
                            else
                                temptAttributes.Add(newLine);
                        }
                    }

                    if (type == LoadType.Contents && tempContents.Count > 0)
                    {
                        contents.Add(tempContents);
                        attributes.Add(temptAttributes);
                    }


                }
            }

        }

        public void LoadContent(string filename, List<List<string>> attributes, List<List<string>> contents, List<string> tbc, string identifier)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if (line.Contains("EndLoad=") && line.Contains(identifier))
                    {
                        identifierFound = false;
                        break;
                    }
                    else if (line.Contains("Load=") && line.Contains(identifier))
                    {
                        identifierFound = true;
                        continue;
                    }

                    if (identifierFound)
                    {
                        if (line.Contains("Load="))
                        {
                            temptAttributes = new List<string>();
                            line = line.Remove(0, line.IndexOf("=") + 1);
                            type = LoadType.Attributes;
                        }
                        else
                        {
                            tempContents = new List<string>();
                            type = LoadType.Contents;
                        }

                        string[] lineArray = line.Split(']');

                        foreach (string li in lineArray)
                        {
                            string newLine = li.Trim('[', ' ', ']');
                            if (newLine != String.Empty)
                            {
                                if (type == LoadType.Contents)
                                    tempContents.Add(newLine);
                                else
                                    temptAttributes.Add(newLine);
                            }
                        }

                        if (type == LoadType.Contents && tempContents.Count > 0)
                        {
                            contents.Add(tempContents);
                            attributes.Add(temptAttributes);
                        }

                    }
                }
            }
        }


        #endregion


    }
}
