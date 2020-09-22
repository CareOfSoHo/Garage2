using System;
using System.Collections.Generic;
using System.Collections;
using System.Numerics;
using System.Linq;

namespace Garage2
{
    public class GarageManager
    {
        private UserInterface IUi;
        private List<Vehicle> vehicles;

        //private Garage<T> garage = new Garage<T>();
        private Vehicle[] vehiclesArr;

        public UserInterface IUi1 { get => IUi; set => IUi = value; }

        public void Run()
        {
            vehicles = new List<Vehicle>();
            Menu();
            

        }

        public void Menu()
        {
            while (true)
            {
                char choise = GetMenuChoise(); 
                switch (choise)
                {
                    case '1':
                        ParkVehicle();
                        break;
                    case '2':
                        ListAllVehicles();
                        break;
                    case '3':
                        SearchVehicle();
                        break;
                    case '4':
                       GetVehicle();
                        break;
                    case '0':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please enter some valid input (0, 1, 2, 3, 4)");
                        break;
                }
            }
        }

        public char GetMenuChoise()
        {
            Console.WriteLine("Please navigate through the menu by inputting the number \n(1, 2, 3 ,4, 0) of your choice"
                             + "\n1. Park your vehicle in the garage"
                             + "\n2. Examine the vehicles in the garage"
                             + "\n3. Search for your vehicle"
                             + "\n4. Get your vechicle"
                             + "\n0. Exit the application");
            char menuVal;
            try
            {
                menuVal = Console.ReadLine()[0];
                return menuVal;
            }
            catch (IndexOutOfRangeException) //If the input line is empty, we ask the users for some input.
            {

                Console.Clear();
                Console.WriteLine("Please enter some input!");
                try
                {
                    menuVal = Console.ReadLine()[0];
                    return menuVal;
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }

        public void GetVehicle()
        {

            Console.WriteLine("PICK UP YOUR VEHICLES\n");
            Console.WriteLine("Provide your registration number: ");
            //sök på regnr
            string searchWord = Console.ReadLine().ToUpper();
            if (searchWord != "")
            {
                if (vehicles.Count > 0)
                {
                    int noOfPosts = vehicles.Count - 1; //antal poster i listan. -1 för att jämföra med i i for-loopen
                    int counter = 0; // finns det träff på regnr(sökordet), räknare

                    //loopar igenom vehicles listan
                    for (int i = 0; i < vehicles.Count; i++)
                    {
                        if (vehicles[i].RegNo.ToUpper().Contains(searchWord))
                        {
                            Console.WriteLine("The vehicle is in the garage.");
                            Console.WriteLine("It is " + vehicles[i].Color + " and it is a " + vehicles[i].GetType() + "\n\n");
                            Console.WriteLine("Do you want to check out your vehicle? type Y/N");
                            string checkoutVehicle = Console.ReadLine().ToUpper();

                            if (checkoutVehicle == "Y")
                            {
                                vehicles.RemoveAt(i);
                                counter--; //en sökträff  mindre
                                Console.WriteLine("WELCOME BACK\n");
                            }
                            else
                            {
                                Console.WriteLine("We are keeping your vehicle, thank you\n");
                            }
                            counter++; //om sökträff
                        }
                        else
                        {
                            if ((i == noOfPosts) && (counter == 0)) //loopat till sista posten och räknare för träff på sökordet är 0
                                Console.WriteLine("Your vehicle is not here");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("The garage is empty");
                }
            }
            else
            {
                Console.WriteLine("You did not provide your registration number");
            }

        }

        public void SearchVehicle()
        {
            Console.WriteLine("Ange ditt registreringsnummer: ");
            //sök på regnr
            string searchWord = Console.ReadLine().ToUpper();
            if (searchWord != "")
            {
                if (vehicles.Count > 0)
                {
                    int noOfPosts = vehicles.Count - 1; //antal poster i listan. -1 för att jämföra med i i for-loopen
                    int counter = 0; // finns det träff på regnr(sökordet), räknare

                    //loopar igenom vehicles listan
                    for (int i = 0; i < vehicles.Count; i++)
                    {
                        if (vehicles[i].RegNo.ToUpper().Contains(searchWord))
                        {
                            Console.WriteLine("The vehicle is in the garage.");
                            Console.WriteLine("It is " + vehicles[i].Color + " and it is a " + vehicles[i].GetType() + "\n\n");
                            counter++; //om sökträff
                        }
                        else
                        {
                            if ((i == noOfPosts) && (counter == 0)) //loopat till sista posten och räknare för träff på sökordet är 0
                                Console.WriteLine("Your vehicle is not here");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("The garage is empty");
                }
            }
            else
            {
                Console.WriteLine("You did not provide your registration number");
            }

        }

        public void ListAllVehicles()
        {
            if (vehicles.Count <= 0)
            {
                Console.WriteLine("The garage is empty");
            }
            else
            {
                Console.WriteLine("LISTED VEHICLES IN THE GARAGE:");
                foreach (var item in vehicles)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("**********");
            }

        }

        public void ParkVehicle()
        {
            bool run = true;
            string regNo, color, model;
            int noOfWheels, numberofEngines, cylVol, noOfSeats, hrsPwr;
            char vehicleType;



            //method for user input Vehicle Base
            UserInputforBase(out regNo, out color, out noOfWheels, out vehicleType);

            while (run)
            {
                switch (vehicleType)
                {
                    case '1':
                        {
                            Console.WriteLine("Please, provide the model of your Airplane");
                            model = Console.ReadLine();
                            Console.WriteLine("Please, provide the number of Engines on your Airplane");

                            int.TryParse(Console.ReadLine(), out numberofEngines);
                            Vehicle v = new AirPlane(regNo, color, noOfWheels, numberofEngines, model);
                            vehicles.Add(v);

                            Console.WriteLine("This is what you checked in to the garage: " + v);

                            run = false;
                            break;
                        }
                    case '2':
                        {
                            Console.WriteLine("Please, provide the Cylindervolume on your Boat");

                            int.TryParse(Console.ReadLine(), out cylVol);
                            Vehicle v = new Boat(regNo, color, noOfWheels, cylVol);
                            vehicles.Add(v);
                            Console.WriteLine("This is what you checked in to the garage: " + v);

                            run = false;
                            break;
                        }
                    case '3':
                        {
                            Console.WriteLine("Please, provide the number of seats on your Bus");

                            int.TryParse(Console.ReadLine(), out noOfSeats);
                            Vehicle v = new Bus(regNo, color, noOfWheels, noOfSeats);
                            vehicles.Add(v);
                            Console.WriteLine("This is what you checked in to the garage: " + v);

                            run = false;

                            break;
                        }
                    case '4':
                        {
                            Console.WriteLine("Please, provide the horsepowers of your car");

                            int.TryParse(Console.ReadLine(), out hrsPwr);
                            Vehicle v = new Car(regNo, color, noOfWheels, hrsPwr);
                            vehicles.Add(v);

                            Console.WriteLine("This is what you checked in to the garage: " + v);

                            run = false;

                            break;
                        }
                    case '5':
                        {
                            Console.WriteLine("Please, provide the horsepowers of your MC");

                            int.TryParse(Console.ReadLine(), out hrsPwr);
                            Vehicle v = new Motorcycle(regNo, color, noOfWheels, hrsPwr);
                            vehicles.Add(v);

                            Console.WriteLine("This is what you checked in to the garage: " + v);

                            run = false;

                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Please enter some valid input (1, 2, 3, 4, 5)");
                            run = false;

                            break;
                        }

                }
            }
        }

        public virtual void UserInputforBase(out string regNo, out string color, out int noOfWheels, out char vehicleType)
        {
            Console.WriteLine("Please, provide the registration number");
            regNo = Console.ReadLine();

            //för att kolla att regnr anges ABC123
            regNo = Vehicle.CheckRegNo(regNo);

            foreach (var item in vehicles)
            {
                if (regNo == item.RegNo)
                {
                    Console.WriteLine("The regNo already exist in the garage");

                    //gör om
                    Console.WriteLine("Please, provide the registration number in the format of ABC123");
                    regNo = Console.ReadLine();
                }
            }
            Console.WriteLine("Please, provide the color of your vehicle");
            color = Console.ReadLine();
            Console.WriteLine("Please, provide the number of wheels on your vehicle");

            int.TryParse(Console.ReadLine(), out noOfWheels);

            Console.WriteLine("What kind of vehicle is it?"
                             + "\n[1]. AirPlane"
                             + "\n[2]. Boat"
                             + "\n[3]. Bus"
                             + "\n[4]. Car"
                             + "\n[5]. Motorcycle");
            vehicleType = Console.ReadLine()[0];
        }

    }
}