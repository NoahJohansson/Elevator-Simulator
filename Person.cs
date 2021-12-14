using System.Collections.Generic;
using System;
using System.Linq;
namespace ElevatorProject_A3
{
    public class Person
    {
        public int PersonID { get; set; }
        public int OriginalFloor { get; set; }
        public int DestinationFloor { get; set; }
        public int CompletionTime { get; set; }
        public int WaitingTime { get; set; }

        public Person(int ID, int OF, int DF)
        {
            PersonID = ID;
            OriginalFloor = OF;
            DestinationFloor = DF;
            CompletionTime = 0;
            WaitingTime = 0;
        }

        //Decides what Direction Person is going. If Persons starting floor is lesser than their destination floor they should be going up and if not Person should go down.
        public Direction Direction
        {
            get
            {
                if (OriginalFloor < DestinationFloor) return Direction.Up;
                else return Direction.Down;
            }
        }
    }
}

