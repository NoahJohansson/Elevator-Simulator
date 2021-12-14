using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ElevatorProject_A3
{
    public class ElevatorControl
    {
        public int count = 0;
        public static bool IsPaused = false;
        public Elevator elevator { get; set; }
        public List<Person> WaitingPersons { get; set; }
        public List<Person> FinishedPersons { get; set; }
        public int row = 0;

        public ElevatorControl()
        {
            elevator = new Elevator();
            WaitingPersons = new List<Person>();
            FinishedPersons = new List<Person>();
        }

        //First updates elevator.SelectedFloor by getting the highest value out of all persons currently in elevator using FindHighestDestinationFloor
        //Then unloads and loads elevator using respective methods
        //Writes the current floor number, what direction elevator is moving in, and how many people are currently in elevator. Pauses the program for .5 seconds to make console readable
        //Increments current floor of elevator
        //Increments total time taken for elevator as well as calling method for incrementing time for passengers
        public void MoveElevatorUp(bool skip)
        {
            FindHighestDestinationFloor();
            UnloadElevator();
            LoadElevator();
            if (skip == false)
            {
                Console.WriteLine("Current floor is: " + elevator.CurrentFloor + ". Elevator is moving: " + elevator.Direction + ". There are currently " + elevator.Passengers.Count + " people in elevator.");
                for (int i = 0; i < 10; i++)
                {
                    foreach (Person p in WaitingPersons)
                    {
                        if (p.OriginalFloor == i)
                        {
                            count++;
                        }
                    }
                    Console.WriteLine("There are currently waiting: " + count + " people in line on this floor: " + i);
                    count = 0;
                }
                System.Threading.Thread.Sleep(150);
            }
            elevator.CurrentFloor++;
            elevator.TotalTimeTaken++;
            IncrementTimeOfPassengers();
        }

        //First updates elevator.SelectedFloor by getting the lowest value out of all persons currently in elevator using FindLowestDestinationFloor
        //Then unloads and loads elevator using respective methods
        //Writes the current floor number, what direction elevator is moving in, and how many people are currently in elevator. Pauses the program for .5 seconds to make console readable
        //Decrements current floor of elevator
        //Increments total time taken for elevator as well as calling method for incrementing time for passengers
        public void MoveElevatorDown(bool skip)
        {
            FindLowestDestinationFloor();
            UnloadElevator();
            LoadElevator();
            if (skip == false)
            {
                Console.WriteLine("Current floor is: " + elevator.CurrentFloor + ". Elevator is moving: " + elevator.Direction + ". There are currently: " + elevator.Passengers.Count + " people in elevator.");
                for (int i = 0; i < 10; i++)
                {
                    foreach (Person p in WaitingPersons)
                    {
                        if(p.OriginalFloor == i)
                        {
                            count++;
                        }
                    }
                    Console.WriteLine("There are currently waiting: " + count + " people in line on this floor: " + i);
                    count = 0;
                 }
                
                System.Threading.Thread.Sleep(150);
            }
            elevator.CurrentFloor--;
            elevator.TotalTimeTaken++;
            IncrementTimeOfPassengers();
        }
        //Loops through all persons that are currently in WaitingPersons, then adds person to elevator if following conditions are met: elevator isn't full,
        //persons direction is the same as elevators direction, persons original floor is the same as elevators current floor.
        //Method also removes that person from WaitingPersons list. Finally the method returns list of people currently in elevator.
        public List<Person> LoadElevator()
        {
            List<Person> templist = new List<Person>(WaitingPersons);
            foreach (Person person in templist)
            {
                if (elevator.Passengers.Count < elevator.Capacity && person.Direction == elevator.Direction && person.OriginalFloor == elevator.CurrentFloor)
                {
                    elevator.Passengers.Add(person);
                    WaitingPersons.Remove(person);
                }
            }
            return elevator.Passengers;
        }

        //Loops through all persons currently in elevator and with an if statement checks if their destinationfloor is the same as current floor of elevator. 
        //if true, person is removed from the elevator passenger list.
        //Person is also added to list of persons FinishedPersons. Finally method returns Passengers.
        public List<Person> UnloadElevator()
        {
            List<Person> templist = new List<Person>(elevator.Passengers);
            foreach (Person person in templist)
            {
                if (person.DestinationFloor == elevator.CurrentFloor)
                {
                    elevator.Passengers.Remove(person);
                    FinishedPersons.Add(person);
                }
            }
            return elevator.Passengers;
        }

        //The actual algorithm for deciding what direction elevator should be moving.
        //This method is called on from Main with a while loop and will run as long as there are people waiting for elevator or elevator is not empty.

        //ElevatorStuck is an int used for checking if elevator gets stuck. If MoveElevatorRightDirection() is looped twice without moving ElevatorStuck will be == 2.
        //If this happens it means elevator is stuck because the people left are all on one floor and they have opposite direction of elevator. Therefore they are not being picked up.
        //To counteract this, in the end of the method an if statement changes selected floor of elevator if ElevatorStuck == 2. This prevents elevator from being stuck in infinite loop.

        //Method starts off by unloading and loading elevator with people on current floor. This is followed by finding lowest destination floor.
        //A while loop is run checking whether there is anyone in elevator currently with a destination floor higher than current floor. If so, elevator moves up.
        //If the elevator is at top floor, aka floor 9, method for finding lowest destination floor is called.
        //If the elevator is empty, elevators selected floor will be given a new value and it will, using Math.Min, find person with lowest original floor.

        //Another while loop is run, this time elevator will go down if selected floor is smaller than current floor.
        //Elevator will then be loaded off and on, and find highest destination floor.
        //If elevator is at bottom floor, aka floor 0, method for finding highest destination floor is run.
        //If the elevator is empty, elevators selected floor will be given a new value and it will, using Math.Max, find person with highest original floor.
        //If elevator is stuck the final if statement will fix that by changing selected floor.
        //The loop is now completed.

        //RunProgram() method makes it possible to see wether the program is paused or wether it is running again.
        public static bool RunProgram()
        {
            if (!Console.KeyAvailable)
            {

            }
            else if (Console.ReadKey(true).Key == ConsoleKey.P)
            {


                if (IsPaused == false)
                {
                    Console.WriteLine("The program is paused.");
                    Console.WriteLine("Press 'P again to resume'.");
                    IsPaused = true;
                }
                else
                {
                    Console.WriteLine("The program is now running");
                    IsPaused = false;
                }
            }
            return IsPaused;
        }
        

        public void MoveElevatorRightDirection(bool skip)
        {
            elevator.ElevatorStuck++;
            UnloadElevator();
            LoadElevator();
            FindLowestDestinationFloor();

            do
            {
                
                    MoveElevatorUp(skip);
                    elevator.ElevatorStuck = 0;
                if (skip == false)
                {
                    Thread.Sleep(500);
                }
                    Console.Clear();
                

                RunProgram();

            } while (IsPaused == false && AnyOutstandingPickups() == true && elevator.CurrentFloor < elevator.SelectedFloor);


            while (RunProgram() == true && AnyOutstandingPickups())
            {
                RunProgram();
            }

            

            if (elevator.CurrentFloor == 9)
            {
                FindLowestDestinationFloor();
            }

            if (elevator.Passengers.Count == 0)
            {
                foreach (Person person in WaitingPersons)
                {
                    elevator.SelectedFloor = Math.Min(elevator.SelectedFloor, person.OriginalFloor);
                }
            }

            do
            {
                
                    MoveElevatorDown(skip);
                    elevator.ElevatorStuck = 0;
                if (skip == false)
                {
                    Thread.Sleep(500);
                }
                    Console.Clear();
               

                RunProgram();
            } while (IsPaused == false && AnyOutstandingPickups() == true && elevator.CurrentFloor > elevator.SelectedFloor);

            while (RunProgram() == true && AnyOutstandingPickups())
            {
                RunProgram();
            }
           

            UnloadElevator();
            LoadElevator();
            FindHighestDestinationFloor();

            if (elevator.CurrentFloor == 0)
            {
                FindHighestDestinationFloor();
            }
            
                

             if (elevator.Passengers.Count == 0)
            {
                foreach (Person person in WaitingPersons)
                {
                    elevator.SelectedFloor = Math.Max(elevator.SelectedFloor, person.OriginalFloor);
                }
            }

            if (elevator.ElevatorStuck == 2)
            {
                if (elevator.Direction.ToString() == "Up")
                {
                    elevator.SelectedFloor--;
                }
                else
                {
                    elevator.SelectedFloor++;
                }
            }
            
        }

        //checks if there are any persons currently waiting for elevator or if there are any persons currently in elevator.
        public bool AnyOutstandingPickups()
        {
            return (WaitingPersons.Any() || elevator.Passengers.Any());
        }

        //Method for incrementing time of both people currently in elevator and people that are currently waiting on elevator
        public void IncrementTimeOfPassengers()
        {
            foreach (Person person in elevator.Passengers)
            {
                person.CompletionTime++;
            }
            foreach (Person person in WaitingPersons)
            {
                person.CompletionTime++;
                person.WaitingTime++;
            }
        }

        //Method for identifying lowest destinationfloor in elevator
        public void FindLowestDestinationFloor()
        {
            foreach (Person person in elevator.Passengers)
            {
                elevator.SelectedFloor = Math.Min(elevator.SelectedFloor, person.DestinationFloor);
            }
        }

        //Method for identifying highest destinationfloor in elevator
        public void FindHighestDestinationFloor()
        {
            foreach (Person person in elevator.Passengers)
            {
                elevator.SelectedFloor = Math.Max(elevator.SelectedFloor, person.DestinationFloor);
            }
        }
    }


}
