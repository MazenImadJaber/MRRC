using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRRCManagement;
using MRRC;
using System.IO;


namespace AppTester
{
    class Program
    {
        static void Main(string[] args)
        {

            Vehicle vehicletest = new Vehicle("MAZ997", "MAZEN", "CAR", 1997, Vehicle.VehicleClass.Family);
            Vehicle vehicletest1 = new Vehicle("MAZ997", "MAZEN", "CAR", 1997, Vehicle.VehicleClass.Commercial);
            Vehicle vehicletest2 = new Vehicle("MAZ996", "teze", "teez", 1997, Vehicle.VehicleClass.Economy);
            Vehicle vehicletest3 = new Vehicle("MAZ996","Toyota", "Camary", 2007, Vehicle.VehicleClass.Luxury,6,Vehicle.TransmissionType.Manual,Vehicle.FuelType.LPG,true,false,"Red",200);
            //Vehicle vehicletest1 = new Vehicle("MAZ997", Vehicle.VehicleClass.Economy, "Honey", "Cell", 2020, 2, Vehicle.TransmissionType.Automatic, Vehicle.FuelType.Electric, true, true, 200, "Yellow");
            Customer customer1 = new Customer(5, "Mr", "Mazen", "Jaber", Customer.Gender.Male, "07/May/1997");
            //Vehicle vehicletest2 = new Vehicle("MAZ888", Vehicle.VehicleClass.Luxury, "Honey", "Cell", 2020, 2, Vehicle.TransmissionType.Automatic, Vehicle.FuelType.Electric, true, true, 200, "Yellow");
            Console.WriteLine(vehicletest.ToString());
            Console.WriteLine(vehicletest1.ToString());
            Console.WriteLine(vehicletest2.ToString());
            Console.WriteLine(vehicletest3.ToString());
            
            Console.WriteLine(vehicletest1.VehicleRego);
            //string[] test = "Black AND white AND red AND blue".Split(' ');
            //string tes= null;
            //foreach(string alt in test)
            //{
            //    tes += alt;
            //}
            //Console.WriteLine(tes);

            //test = tes.Split("AND".ToCharArray());
            //int t =test.Length;
            //tes = null;
            //foreach (string alt in test)
            //{
            //    tes += alt;
            //}
            //Console.WriteLine(tes);
            //string[] test1 = tes.Split(' ');
            Console.WriteLine("{0}", vehicletest.ToString().Length);
            Console.ReadKey();
            Fleet newFleet = new Fleet();
            Console.WriteLine(newFleet.ToString());
            Console.ReadKey();
            Console.WriteLine("{0}", newFleet.addVehicle(vehicletest));
            Console.WriteLine(newFleet.ToString());
            Console.ReadKey();
            Console.WriteLine("{0}", newFleet.addVehicle(vehicletest1));
            Console.WriteLine(newFleet.ToString());
            Console.ReadKey();
            Console.WriteLine("{0}", newFleet.addVehicle(vehicletest2));
            Console.WriteLine(newFleet.ToString());
            Console.ReadKey();
            Console.WriteLine(newFleet.ToString());
            Console.ReadKey();
            Console.WriteLine("{0}", newFleet.removeVehicle(vehicletest));
            Console.WriteLine(newFleet.ToString());
            Console.ReadKey();

          Console.WriteLine("{0}", newFleet.rentCar("MAZ996", 3));
            Console.WriteLine(newFleet.ToString());
            Console.ReadKey();
            Console.WriteLine("rented cars:");
           foreach (Vehicle veh in newFleet.GetFleet(true))
            {
                Console.WriteLine(veh.ToString());
            }
            Console.ReadKey();
            Console.WriteLine(" not rented cars:");
            foreach (Vehicle veh in newFleet.GetFleet(false))
            {
                Console.WriteLine(veh.ToString());
            }
            Console.WriteLine("123HCB is rented by {0}\n897HOI is rented by {1}\nMAZ996 is rented by {2}\n",
                newFleet.RentedBy("123HCB").ToString(), newFleet.RentedBy("897HOI").ToString(),
                newFleet.RentedBy("MAZ996").ToString());
            Console.WriteLine("Return car MAZ996; {0}", newFleet.RetrunCar("MAZ996"));
            Console.WriteLine("Return car MAZ997; {0}", newFleet.RetrunCar("MAZ997"));
            Console.WriteLine("rented cars:");
            foreach (Vehicle veh in newFleet.GetFleet(true))
            {
                Console.WriteLine(veh.ToString());
            }
            Console.WriteLine(" not rented cars:");
            foreach (Vehicle veh in newFleet.GetFleet(false))
            {
                Console.WriteLine(veh.ToString());
            }
            Console.ReadKey();
            List<Vehicle> newvec = newFleet.Search(0, 130, "GPS or White or grey or red");
            
            Console.WriteLine("search result of GPS or white or grey in the price range of 0 to 130");
            foreach (Vehicle veh in newvec)
            {
                Console.WriteLine(veh.ToString());
            }
            Console.ReadKey();
            
        }
    }
}
