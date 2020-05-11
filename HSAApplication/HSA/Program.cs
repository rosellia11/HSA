using System;
using System.Collections.Generic;
using System.IO;

namespace HSA
{
    class Program
    {
        static void Main(string[] args)
        {
            /*I would much rather have a file explorer so the user could choose the file instead of typing in the path.
            *Since it wasn't specified what platform (Windows,Mac,Linux) so I decided that .NET Core and cross platform is
            *best. .NET Core doesn't support OpenFileDialog and I could find any packages that work as a file explorer. Thus
            *the user has to type in the file path*/

            //Asks the user to tpye in the file path. Keeps asking until it's a valid filepath
            StreamReader fileStream;
            while (true)
            {
                try
                {

                    Console.WriteLine("Please enter the full file path of the file you would like to scan.");
                    string filename = Console.ReadLine();
                    fileStream = new StreamReader(filename);
                    break;
                }

                catch (FileNotFoundException e)
                {
                    Console.WriteLine("File Not Found. Please try again with a valid path.");
                }
            }

            try
            { 
                //List to store valied entries
                List<Entry> processedEntries = new List<Entry>();

                //Begin scanning through the file
                using (fileStream)
                {
                    string line;
                    //Keeping reading until end of the file
                    while (fileStream.EndOfStream == false)
                    {
                        line = fileStream.ReadLine();
                        //Split the line of the file at the commas
                        string[] split = line.Split(',');
                        //If its formatted correctly there should be 5 elements. Otherwise throw an expcetion
                        if (split.Length != 5)
                            throw new Exception();
                        else
                        {
                            //Create a new entry with the split line. Add it to the list of processed entries
                            Entry entry = new Entry(split[0], split[1], split[2], split[3], split[4]);
                            processedEntries.Add(entry);
                        }
                    }
                }
                //For each entry in the List print it out to the console
                foreach (Entry entry in processedEntries)
                {
                    Console.WriteLine(entry.ToString());
                }
            }
           
            //If anything goes wrong. Processing stops and we print this message. We can easily make more specfic expception messages
            //and tailor it to exactly what went wrong but this was the requirement.
            catch (Exception e)
            {
                Console.WriteLine("A record in the file failed validation. Processing has stopped.");
            }
        }
    }
}
