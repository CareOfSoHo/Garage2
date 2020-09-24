using System;
using System.Collections;
using System.Collections.Generic;


namespace Garage2
{
    class Garage<T> : IEnumerable<T> where T : Vehicle
    {
        protected int buff; // buffert
        protected T[] vehicleArray;
        protected int noOfSpaces; // tillgängliga platser
        protected int noUsedSpaces; // tagna platser
        private Vehicle[] vehiclesArr;

        public string RegNo { get; set; }
        public string Color { get; set; }
        public int NoOfWheels { get; set; }
        public string VehicleType { get; set; }


        public Garage()
        {
            buff = 1;
            noUsedSpaces = 0; // garaget är tomt
            noOfSpaces = 0; // tillgängliga platser 3st, ToDo: dynamisk
            vehicleArray = new T[noOfSpaces];


        }
        //Add Vehicles 
        public bool AddV(T vehicle)
        {
            //kolla plats kvar i arrayen
            if (noUsedSpaces + 1 > noOfSpaces)
            {
                //lägg till plats i arrayen
                ExpandArray(1 + buff);
            }
            // om nu.ll return false

            //kolla index, för att lägga rätt i arrayen
            vehicleArray[noUsedSpaces++] = vehicle;
            return true;

        }
        public void AddV(Garage<T> vehicles)
        {
            if (noUsedSpaces + vehicles.vehicleArray.Length > noOfSpaces)
            {
                ExpandArray(vehicles.vehicleArray.Length + 1);
            }
            for (int i = 0; i < vehicles.vehicleArray.Length; i++)
            {
                vehicleArray[noUsedSpaces++] = vehicles.vehicleArray[i];
            }

        }
        public T RemoveV(int index)
        {
            T tempArr = vehicleArray[index];

            for (int i = index; i < noOfSpaces - 1; i++)
            {
                vehicleArray[i] = vehicleArray[i + 1];
            }
            //minskar antal använda platser i arrayen
            noOfSpaces--;

            //krymp arrayen
            if (noOfSpaces - noUsedSpaces > buff)
            {
                ReduceArraySize();
            }
            return tempArr;
        }

        private void ReduceArraySize()
        {
            T[] tempArr = new T[noUsedSpaces];
            for (int i = 0; i < noUsedSpaces; i++)
            {
                tempArr[i] = vehicleArray[i];
            }
            vehicleArray = tempArr;
            noOfSpaces = noUsedSpaces;
        }

        private void ExpandArray(int size)
        {
            if (size < 1) return;

            T[] tempArr = new T[noOfSpaces + size];

            for (int i = 0; i < noUsedSpaces; i++)
            {
                tempArr[i] = vehicleArray[i];
            }

            vehicleArray = tempArr;
            noOfSpaces = size;
        }



        #region Properties
        public int NoOfSpaces
        {
            get
            {
                return noOfSpaces;
            }
        }

        public T this[int index]
        {
            get
            {
                return vehicleArray[index];
            }
        }

        #endregion



        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            Vehicle compareVehicle = obj as Vehicle;
            if (compareVehicle != null && this.RegNo == compareVehicle.RegNo)
            {
                return true;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public void AddVehicle(T regNo)
        {
            vehicleArray[noOfSpaces++] = regNo;

        }

        public IEnumerator<T> GetEnumerator()
        {
            //iterera arrayen och returnera vehicle
            for (int i = 0; i < vehicleArray.Length-1; i++)
            {
                Console.WriteLine(vehicleArray[i].RegNo);
                Console.WriteLine(vehicleArray[i].Color);
                Console.WriteLine(vehicleArray[i].NoOfWheels);
                Console.WriteLine(vehicleArray[i].Stats());
                Console.WriteLine(vehicleArray[i].GetType().Name);
                i++;
            }
            //ToDo
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void RetriveVehicles()
        {
            vehiclesArr = new Vehicle[4]
            {
                new AirPlane("SAS987", "Grey", 16, 4, "AirBus") as Vehicle,
                new AirPlane("AIR643", "Gold", 30, 8, "727") as Vehicle,
                new Boat("kak234", "Blue", 0, 35) as Vehicle,
                new Boat("sos124", "white", 0, 305) as Vehicle
            };

            foreach (var item in vehiclesArr)
            {
                Console.WriteLine(item.GetType().ToString() + " Color: " + item.Color + "RegNo: " + item.RegNo);
            }
            return;
        }
    }
}
