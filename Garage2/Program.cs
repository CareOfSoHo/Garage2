using System;
using System.Collections.Generic;
using System.Linq;

namespace Garage2
{
    class Program
    {
        static void Main(string[] args)
        {
            GarageManager manager = new GarageManager();
            manager.Run();
            //manager.Menu();

            Vehicle vehicles; // = GetVehicles.FirstOrDefault(vehicle => vehicle.RegNo == "FZK678");

        }

       //public IEnumerable<Vehicle> GetVehicles
       // {
       //     //get { return $"RegNo: {RegNo}, Color: {Color}, NoWheels: {NoOfWheels}"; }
       //    throw new NotImplementedException();
       // }
    }
}
