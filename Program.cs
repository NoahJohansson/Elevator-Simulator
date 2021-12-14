using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

//Elevator simulation project by group A3
//Made by:
//Noah Johansson
//Felix Nykvist
//Eric Remnebäck
//Johan Söderberg
//Nils Blomgren
//Simon Myrén
namespace ElevatorProject_A3
{
    class Program
    {
        public static bool skip = false;

        static void Main(string[] args)
        {
            
            Console.WriteLine("Press any key to start elevator simulation.");
            Console.WriteLine("Press 'P' to pause/play");
            Console.WriteLine("Press 'S' to skip to the end of the simulation.");
            Console.WriteLine("--------------------------------------------------------------------");
            string input = Console.ReadLine();
            if (input == "s" || input == "S")
            {
                skip = true;
            }
            Console.WriteLine();

            int row = 0;
            ElevatorControl controlpanel = new ElevatorControl();
            ReadTextFile.TextFileToPerson(controlpanel.WaitingPersons, row);


            while (controlpanel.AnyOutstandingPickups())
            {
                controlpanel.MoveElevatorRightDirection(skip);
                row += 10;
                ReadTextFile.TextFileToPerson(controlpanel.WaitingPersons, row);
            }

            

          

            int TotalTime = controlpanel.elevator.TotalTimeTaken;
            float AverageWaitingTime = 0;
            float AverageCompletionTime = 0;
            int LeastTime = 10;
            int MostTime = 0;
            int NumberOfPersonsWithMostTime = 0;
            int NumberOfPersonsWithLeastTime = 0;
            foreach (Person person in controlpanel.FinishedPersons)
            {
                AverageWaitingTime += person.WaitingTime;
                AverageCompletionTime += person.CompletionTime;
                LeastTime = Math.Min(LeastTime, person.CompletionTime);
                MostTime = Math.Max(MostTime, person.CompletionTime);
            }
            AverageWaitingTime = AverageWaitingTime / controlpanel.FinishedPersons.Count;
            AverageCompletionTime = AverageCompletionTime / controlpanel.FinishedPersons.Count;
            foreach (Person person in controlpanel.FinishedPersons)
            {
                if (person.CompletionTime == MostTime)
                {
                    NumberOfPersonsWithMostTime++;
                }
                if (person.CompletionTime == LeastTime)
                {
                    NumberOfPersonsWithLeastTime++;
                }
            }
            if (MostTime == 0)
            {
                LeastTime = 0;
                AverageCompletionTime = 0;
                AverageWaitingTime = 0;
            }

            int finishedpersons = controlpanel.FinishedPersons.Count - 1;

            //Following code is used to create a text file with output and save it. Code was taken from link below.
            //https://stackoverflow.com/a/4470751
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;

            //Replace (C:\\Users...) with local path where you wish to save the .txt file
            ostrm = new FileStream("/Users/ericremneback/Desktop/Output.txt", FileMode.OpenOrCreate, FileAccess.Write);
            writer = new StreamWriter(ostrm);


            Console.SetOut(writer);
            Console.WriteLine("Number of passengers: " + finishedpersons);
            Console.WriteLine("Total time for elevator: " + TotalTime * 10 + " ms.");
            Console.WriteLine("Average waiting time: " + AverageWaitingTime * 10 + " ms.");
            Console.WriteLine("Average completion time: " + AverageCompletionTime * 10 + " ms.");
            Console.WriteLine("Least total time taken: " + LeastTime * 10 + " ms. by " + NumberOfPersonsWithLeastTime + " persons.");
            Console.WriteLine("Most total time taken: " + MostTime * 10 + " ms. by " + NumberOfPersonsWithMostTime + " persons.");
            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();


            Console.WriteLine("Simulation complete");
            Console.WriteLine();
            Console.WriteLine("Number of passengers: " + finishedpersons);
            Console.WriteLine("Total time for elevator: " + TotalTime * 10 + " ms.");
            Console.WriteLine("Average waiting time: " + AverageWaitingTime * 10 + " ms.");
            Console.WriteLine("Average completion time: " + AverageCompletionTime * 10 + " ms.");
            Console.WriteLine("Least total time taken: " + LeastTime * 10 + " ms. by " + NumberOfPersonsWithLeastTime + " persons.");
            Console.WriteLine("Most total time taken: " + MostTime * 10 + " ms. by " + NumberOfPersonsWithMostTime + " persons.");

        }


    }
}