using System;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorProject_A3
{
    public class Elevator
    {
        public int CurrentFloor { get; set; }
        public int Capacity { get; set; }
        // SelectedFloor is highest or lowest floor the elevator will go to.
        public int SelectedFloor { get; set; }
        public int TotalTimeTaken { get; set; }
        public int ElevatorStuck { get; set; }
        public List<Person> Passengers { get; set; }
        public Elevator()
        {
            Passengers = new List<Person>();
            CurrentFloor = 0;
            Capacity = 10;
            TotalTimeTaken = 0;
            ElevatorStuck = 0;
        }

        //Decides what direction elevator should be going. If CurrentFloor is smaller than SelectedFloor elevator should go up, if not it should go down.
        public Direction Direction
        {
            get
            {
                if (CurrentFloor == 0 || CurrentFloor < SelectedFloor)
                {
                    return Direction.Up;
                }
                else if(CurrentFloor == 9 || CurrentFloor > SelectedFloor)
                {
                    return Direction.Down;
                }
                else
                {
                    return Direction.Neutral;
                }
            }
        }
    }
}
