using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace ElevatorProject_A3
{
    public class ReadTextFile
    {
        //Method for converting textfile to List<Person> using StreamReader and system.IO. The two links below were used as help.
        //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-read-a-text-file-one-line-at-a-time
        //https://stackoverflow.com/a/18887270
        //Method takes a list of Persons and will fill it with people from the .txt file.
        public static void TextFileToPerson(List<Person> listofPersons, int row)
        {
            int id = 1;
            int floor = 0;
            string line;
            int count = 0;
            //Replace ("C:\\Users... with path to .txt on your device.
            StreamReader file = new StreamReader("/Users/ericremneback/Desktop/TestDataAlgoDS.txt");
            for (int i = 0; i < row; i++)
            {
                file.ReadLine();
            }

            if (floor == 10)
            {
                floor = 0; 
            }
            while ((line = file.ReadLine()) != null && floor < 10)
            {
                try
                {
                    string[] words = line.Split(',');
                    int[] possiblefloors = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                    if (words[0] == "-1") 
                    {

                    }
                    
                    else
                    {
                        foreach (string s in words)
                        {

                            Int32.TryParse(s, out int x);
                            if (x != -1 && x != floor && possiblefloors.Contains(x))
                            {
                                foreach(Person p in listofPersons)
                                {
                                    if(p.OriginalFloor == floor)
                                    {
                                        count++;
                                    }
                                }
                                if (count < 50)
                                {
                                    listofPersons.Add(new Person(id, floor, x));
                                    id++;
                                }
                                count = 0;
                            }
                            
                        }
                    }
                    floor++;
                    
                }

                catch
                {
                    Console.WriteLine("Something went wrong with reading input text file");
                    Environment.Exit(0);
                }
            }
            file.Close();
        }
    }
}