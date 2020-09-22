using System;
using System.Collections.Generic;
using System.Text;

namespace Garage2
{
    public interface UserInterface
    {
        
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


       

    }
}

