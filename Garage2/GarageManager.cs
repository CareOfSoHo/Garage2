using System;
using System.Collections.Generic;
using System.Linq;

namespace Garage2
{
    public class GarageManager
    {
        private Garage<Vehicle> _garage;
        private List<Vehicle> vehicles;


        private Vehicle[] vehiclesArr;

        public UserInterface IUi { get => IUi; set => IUi = value; }

        
        public void Run()
        {
            vehicles = new List<Vehicle>();
            _garage = new Garage<Vehicle>();
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
                Console.WriteLine("Please enter some valid input, in the for of a number between 0-4!");
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
            Console.Clear();
            Console.WriteLine("PICK UP YOUR VEHICLE\n");
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
                            Console.WriteLine("It is " + vehicles[i].Color + " and it is a " + vehicles[i].GetType().Name + "\n\n");
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
            Console.Clear();
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
            Console.Clear();
            if(vehicles.Count() <= 0)
            {
                Console.WriteLine("The garage is empty");
            }
            else
            {
                Console.WriteLine("\n\nLISTED VEHICLES IN THE GARAGE:\n\n");
                vehiclesArr = vehicles.ToArray();
                string[] result = new string[vehiclesArr.Length];

                for (int i = 0; i < vehiclesArr.Length; i++)
                {
                    result[i] = string.Format("In spot: {0}, is REGNr: {1} parked. It is a {2}", i + 1, vehiclesArr[i].RegNo, vehiclesArr[i].GetType().Name);
                    Console.WriteLine(result[i]);
                }

                
                Console.WriteLine("\n\n**********\n\n");
            }

        }

        public void ParkVehicle()
        {
            bool run = true;
            string regNo, color, model;
            int noOfWheels, numberofEngines, cylVol, noOfSeats, hrsPwr;
            char vehicleType;

            
            try
            {
                //method for user input Vehicle Base
                UserInputforBase(out regNo, out color, out noOfWheels, out vehicleType);

                while (run)
                {
                    switch (vehicleType)
                    {
                        case '1':
                            {
                                Console.Clear();
                                Console.WriteLine("Please, provide the model of your Airplane");
                                model = Console.ReadLine();
                                Console.WriteLine("Please, provide the number of Engines on your Airplane");

                                int.TryParse(Console.ReadLine(), out numberofEngines);
                                Vehicle v = new AirPlane(regNo, color, noOfWheels, numberofEngines, model);
                                vehicles.Add(v);
                                Console.Clear();

                                //försök till att använda funktion i Garage.cs
                                _garage.AddV(v);

                                // i _garage finns vehicleArray med rätt värden, men jag lyckas inte få ut dem
                                for (int i = 0; i < _garage.NoOfSpaces-1; i++)
                                {
                                    Console.WriteLine("Type: " + _garage[i].GetType().Name);
                                    Console.WriteLine("RegNu: " + _garage[i].RegNo);
                                    Console.WriteLine("Color: " + _garage[i].Color);
                                    Console.WriteLine("Number of Wheels: " +_garage[i].NoOfWheels);
                                    Console.WriteLine("Stats: " + _garage[i].Stats());
                                }


                                //lähher till i en array i generealmanager
                                vehiclesArr = new Vehicle[] { new AirPlane(regNo, color, noOfWheels, numberofEngines, model)};
                                
                                Console.WriteLine("This is what you checked in to the garage: " + v);

                                run = false;
                                break;
                            }
                        case '2':
                            {
                                Console.Clear();
                                Console.WriteLine("Please, provide the Cylindervolume on your Boat");

                                int.TryParse(Console.ReadLine(), out cylVol);
                                Vehicle v = new Boat(regNo, color, noOfWheels, cylVol);
                                //lägger till i lista
                                vehicles.Add(v);

                                //lägger till i array
                                vehiclesArr = new Vehicle[] { new Boat(regNo, color, noOfWheels, cylVol) };

                                Console.WriteLine("This is what you checked in to the garage: " + v);

                                run = false;
                                break;
                            }
                        case '3':
                            {
                                Console.Clear();
                                Console.WriteLine("Please, provide the number of seats on your Bus");

                                int.TryParse(Console.ReadLine(), out noOfSeats);
                                Vehicle v = new Bus(regNo, color, noOfWheels, noOfSeats);
                                //lägger tll i lista
                                vehicles.Add(v);

                                //lägger till i arrray
                                vehiclesArr = new Vehicle[] { new Bus(regNo, color, noOfWheels, noOfSeats) };

                                Console.WriteLine("This is what you checked in to the garage: " + v);

                                run = false;

                                break;
                            }
                        case '4':
                            {
                                Console.Clear();
                                Console.WriteLine("Please, provide the horsepowers of your car");

                                int.TryParse(Console.ReadLine(), out hrsPwr);
                                Vehicle v = new Car(regNo, color, noOfWheels, hrsPwr);
                                //läggaer till i lista
                                vehicles.Add(v);

                                //lägger till i array
                                vehiclesArr = new Vehicle[] { new Car(regNo, color, noOfWheels, hrsPwr)};

                                //for (int i = 0; i < vehiclesArr.Length; i++)
                                //{
                                //    Console.WriteLine(vehiclesArr[i]);
                                //}

                                Console.WriteLine("This is what you checked in to the garage: " + v);

                                run = false;

                                break;
                            }
                        case '5':
                            {
                                Console.Clear();
                                Console.WriteLine("Please, provide the horsepowers of your MC");

                                int.TryParse(Console.ReadLine(), out hrsPwr);
                                Vehicle v = new Motorcycle(regNo, color, noOfWheels, hrsPwr);
                                //lägger till i lista
                                vehicles.Add(v);

                                //lägger till i array
                                vehiclesArr = new Vehicle[] { new Motorcycle(regNo, color, noOfWheels, hrsPwr) };

                                Console.WriteLine("This is what you checked in to the garage: " + v);

                                run = false;

                                break;
                            }
                        default:
                            {
                                Console.Clear();
                                Console.WriteLine("Please enter some valid input (1, 2, 3, 4, 0)");
                                run = false;

                                break;
                            }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong, developer error !!", ex);
            }


        }

        //prper för base
        public virtual void UserInputforBase(out string regNo, out string color, out int noOfWheels, out char vehicleType)
        {
            //bool regNrValid = false;
            Console.Clear();
            Console.WriteLine("Please, provide the registration number");

            regNo = Console.ReadLine();

            //för att kolla att regnr anges ABC123
            regNo = Vehicle.CheckRegNoFormat(regNo);

            //göra koll för att regnr är giltigt i formatet AAA111
            //regNrValid = ValidateRegNo(regNo);

            //lägger till listans element till Arrayen
            vehiclesArr = vehicles.ToArray();
            //loopa arrayen
            for (int i = 0; i < vehiclesArr.Length; i++)
            {
                if (regNo == vehiclesArr[i].RegNo)
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

            //ToDo: göra en funktion av denna
            Console.WriteLine("What kind of vehicle is it?"
                             + "\n[1]. AirPlane"
                             + "\n[2]. Boat"
                             + "\n[3]. Bus"
                             + "\n[4]. Car"
                             + "\n[5]. Motorcycle");
            vehicleType = Console.ReadLine()[0];
        }

        private bool ValidateRegNo(string regNo)
        {
            throw new NotImplementedException();
        }
    }
}