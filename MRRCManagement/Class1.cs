using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace MRRCManagement
{
    public class Vehicle
    {
        public enum FuelType { Petrol, Diesel, LPG, Electric }; //(Fuel Watch Customer Protraction, 2011)
        public enum VehicleClass { Economy, Family, Luxury, Commercial };
        public enum TransmissionType { Automatic, Manual };
        string vehicleRego, make, model, colour;
        int year, numSeats;
        bool GPS, sunRoof;
        double dailyRate;
        public string VehicleRego
        {
            get
            {
                return vehicleRego;
            }
        }
        public string Make
        {
            get
            {
                return make;
            }
            set
            {
                 make= value;
            }
        }
        public string Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
            }
        }
        public string Colour
        {
            get
            {
                return colour;
            }
            set
            {
                colour = value;
            }
        }
       public int Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
            }
        }
        public int NumSeats
        {
            get
            {
                return numSeats;
            }
            set
            {
                numSeats = value;
            }
        }
        public bool GPSstatus
        {
            get
            {
                return GPS;
            }
            set
            {
                GPS = value;
            }
        }
        public bool SunRoof
        {
            get
            {
                return sunRoof;
            }
            set
            {
                sunRoof = value;
            }
        }
      public double DailyRate
        {
            get
            {
                return dailyRate;
            }
            set
            {
                dailyRate = value;
            }
        }
        FuelType fuelType;
        public FuelType Fuel
        {
            get
            {
                return fuelType;
            }
            set
            {
                fuelType = value;
            }
        }
        VehicleClass vehicleClass;
        public VehicleClass Class
        {
            get
            {
                return vehicleClass;
            }
            set
            {
                vehicleClass = value;
            }
        }
        TransmissionType transmission;
        public TransmissionType Transmission
        {
            get
            {
                return transmission;
            }
            set
            {
                transmission = value;
            }
        }
        private List<string> paramters = new List<string>();
       const int n = 12; //number of paramters
        public Vehicle(string vehicleRego, string make, string model, int year, VehicleClass vehicleClass )
        {
            this.vehicleRego = vehicleRego;
            this.vehicleClass = vehicleClass;
            this.make = make;
            this.model = model;
            this.year = year;
            numSeats = 4;
            dailyRate = 50;
            fuelType = FuelType.Petrol;
            GPS = false;
            sunRoof = false;
            colour = "Black";
            if (vehicleClass == VehicleClass.Commercial)
            {
                transmission = TransmissionType.Automatic;
                dailyRate = 50;
            }
            else if (vehicleClass == VehicleClass.Family)
            {
                dailyRate = 80;
            }
            else if (vehicleClass == VehicleClass.Luxury)
            {
                GPS = true;
                sunRoof = true;
                dailyRate = 120;
                transmission = TransmissionType.Automatic;
            }
            else if (vehicleClass == VehicleClass.Commercial)
            {
                fuelType = FuelType.Diesel;
                dailyRate = 130;
                transmission = TransmissionType.Automatic;
            }
            else
            {
                transmission = TransmissionType.Automatic;

            }
           

        }
        public Vehicle(string vehicleRego, string make, string model, int year, VehicleClass vehicleClass, 
                       int numSeats, TransmissionType transmissionType,FuelType fuelType, bool GPS, bool sunRoof, 
                       string colour,double dailyRate)
        {
            this.vehicleRego = vehicleRego;
            this.vehicleClass = vehicleClass;
            this.make = make;
            this.model = model;
            this.year = year;
            this.numSeats = numSeats;
            transmission = transmissionType;
            this.fuelType = fuelType;
            this.GPS = GPS;
            this.sunRoof = sunRoof;
            this.dailyRate = dailyRate;
            this.colour = colour;
           

        }
        public string ToCSVString()
        {
            paramters.Add(vehicleRego);
            paramters.Add(make);
            paramters.Add(model);
            paramters.Add(year.ToString());
            paramters.Add(vehicleClass.ToString());
            paramters.Add(numSeats.ToString());
            paramters.Add(transmission.ToString());
            paramters.Add(fuelType.ToString());
            paramters.Add(GPS.ToString());
            paramters.Add(sunRoof.ToString());
            paramters.Add(colour);
            paramters.Add(dailyRate.ToString());
            string CSV = String.Join(",", paramters);
            return CSV;
        }
        public override string ToString()
        {

            List<string> altributes = new List<string>();
            int counter = 1;
            foreach (string alt in paramters)
            {
                if (int.TryParse(alt, out int num) && counter == 6)
                {
                    altributes.Add(num.ToString() + "-Seater");
                }
                else if (alt == "True" && counter == 9)
                {
                    altributes.Add("GPS");
                }         
                else if (alt == "True" && counter == 10)
                {
                    altributes.Add("sunRoof");
                }
                else
                {
                    altributes.Add(alt);
                }
                counter++;
            }
            altributes.Remove(vehicleRego);
            
            altributes.Remove(GPS.ToString());
            altributes.Remove(sunRoof.ToString());
            string String = String.Join(" ", altributes);
            return String+ " Dollars/day";
        }       
    }
    public class Customer
    {
        int customerID;
        public int ID { get { return customerID; } set { customerID = value; } }
        string title, firstName, lastName, dateOfBirth;
        public string Title  { get { return title;}set { title = value; }} 
        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
        public string DOB { get { return dateOfBirth; } set { dateOfBirth = value; } }
   
        public enum Gender { Female, Male, TransMale, TransFemale, NonBiary, other }
        Gender gender;
        public Gender GENDER { get { return gender; } set { gender = value; } }

        private List<string> paramters = new List<string>();
        public Customer(int customerID, string title, string firstName, string lastName, Gender gender, string dateOfBirth)
        {
            this.customerID = customerID;
            this.title = title;
            this.firstName = firstName;
            this.lastName = lastName;
            this.gender = gender;
            this.dateOfBirth = dateOfBirth;
            paramters.Add(customerID.ToString());
            paramters.Add(title);
            paramters.Add(firstName);
            paramters.Add(lastName);
            paramters.Add(gender.ToString());
            paramters.Add(dateOfBirth);
        }
        public string ToCSVString()
        {
            string CSV = String.Join(",", paramters);
            return CSV;
        }

        public override string  ToString()
        {
            string CSV = String.Join(" ", paramters);
            return CSV;
        }      
    }
    public class CRM
    {
        List<Customer> customers = new List<Customer>();
        public List<Customer> Customers
        {
            get
            {
                return customers;
            }
            set
            {
                customers = value;
            }
        }
        string crmFile = @"..\..\..\Data\customer.csv";
        public CRM()
        {
            if (File.Exists(crmFile))
            {
                loadFromFile();
            }
        }
        public bool AddCustomer(Customer customer)
        {            

            foreach(Customer cust in customers)
            {
                if (customer.ID ==cust.ID)
                {
                    return false;
                }
            }
            customers.Add(customer);
            saveToFile();
            return true;    
        }
        public bool RemoveCustomer(Customer customer, Fleet fleet)
        {
            bool exists = false;
            if (!fleet.IsRenting(customer.ID))
            {
                foreach (Customer cust in customers)
                {
                    if(customer.ID == cust.ID)
                    {
                        exists = true;
                    }
                }
                if (exists)
                {
                 customers.Remove(customer);
                saveToFile();
                return true;
                }
            }
            return false;
        }
        public List<Customer> GetCustomers()
        {
            return customers;
        }

        public void loadFromFile()
        {
            string[] filArray = File.ReadAllLines(crmFile);
            string[] input = filArray[0].Split(',');
            for (int i = 1; i < filArray.Length; i++)
            {
                input = filArray[i].Split(',');
                string gender = input[4];
                if (gender == "Female") customers.Add(new Customer(int.Parse(input[0]), input[1], input[2], input[3], Customer.Gender.Female, input[5]));
                else if (gender == "Male") customers.Add(new Customer(int.Parse(input[0]), input[1], input[2], input[3], Customer.Gender.Male, input[5]));
                else if (gender == "TransMale") customers.Add(new Customer(int.Parse(input[0]), input[1], input[2], input[3], Customer.Gender.TransMale, input[5]));
                else if (gender == "TransFemale") customers.Add(new Customer(int.Parse(input[0]), input[1], input[2], input[3], Customer.Gender.TransFemale, input[5]));
                else if (gender == "NonBiary") customers.Add(new Customer(int.Parse(input[0]), input[1], input[2], input[3], Customer.Gender.NonBiary, input[5]));
                else customers.Add(new Customer(int.Parse(input[0]), input[1], input[2], input[3], Customer.Gender.other, input[5]));                
            }
        }
        public void saveToFile()
        {
            string[] customerz = new string[customers.Count];
            string[] newcustomerz = new string[customers.Count + 1];
            string[] filArray = File.ReadAllLines(crmFile);
            int i = 0;
            foreach (Customer vec in customers)
            {
                customerz[i] = vec.ToCSVString();
                i++;
            }
            newcustomerz[0] = filArray[0];
            for (i = 0; i < customerz.Length; i++)
            {
                newcustomerz[i + 1] = customerz[i];
            }
            File.WriteAllLines(crmFile, newcustomerz);
        }
    
    }
    public class Fleet
    {
        List<Vehicle> vehicles = new List<Vehicle>();
        public List<Vehicle> Vehicles
        {
            get
            {
                return vehicles;
            }
            set
            {
                vehicles = value;
            }
        }
        Dictionary<string, int> rentals = new Dictionary<string, int>();
        string rentalFile = @"..\..\..\Data\rentals.csv";
        private String fleetFile = @"..\..\..\Data\fleet.csv";
        public Fleet()
        {
            if (File.Exists(fleetFile)| File.Exists(fleetFile))
            {
                loadFromFile();
            }

           

        }
        public bool addVehicle(Vehicle vehicle)
        {
            string rego = vehicle.VehicleRego;
            int counter = 0;
            bool regoValid = true;
            foreach(char letter in rego)
            {
                if (counter<3 && int.TryParse(letter.ToString(), out int result))
                {
                    regoValid &= true;
                }
                else if (counter<3 && !int.TryParse(letter.ToString(), out int result2))
                {
                    regoValid &= false;
                }
                
               else  if (counter >= 3 && counter < 6 && !int.TryParse(letter.ToString(), out int result1))
                {
                    regoValid &= true;
                }
                else if ((counter >= 3 && counter < 6 && int.TryParse(letter.ToString(), out int result3)))
                {
                    regoValid &= false;
                }
                counter++;
            }

            if (rego.Length == 6 && regoValid)
            {
                List<string> regos = new List<string>();
                foreach (Vehicle car in vehicles)
                {
                    regos.Add(car.VehicleRego);
                }
                foreach (string reg in regos)
                {

                    if (reg == vehicle.VehicleRego)
                    {
                        return false;
                    }
                }
                vehicles.Add(vehicle);
                saveToFile();
                return true;
            }
            else return false;

       
            

            

        }
        public bool removeVehicle(Vehicle vehicle)
        {
            string vehicleRego = vehicle.VehicleRego;
            bool b = false;
            List<string> regos = new List<string>();
            foreach (Vehicle car in vehicles)
            {
                regos.Add(car.VehicleRego);
            }
            int i = 0;
            foreach (string rego in regos)
            {

                if (rego == vehicleRego && vehicles.Count != 0)
                {
                    vehicles.RemoveAt(i);
                    b = true;
                }
                i++;
            }
            saveToFile();

            return b;
        }
        public bool removeVehicle(string vehicleRego)

        {
            bool b = false;
            List<string> regos = new List<string>();
            foreach (Vehicle car in vehicles)
            {
                regos.Add(car.VehicleRego);
            }
            int i = 0;
            foreach (string rego in regos)
            {

                if (rego == vehicleRego && vehicles.Count != 0)
                {
                    vehicles.RemoveAt(i);
                    b = true;
                }
                i++;
            }
            saveToFile();
            return b;
        }
        public List<Vehicle> GetFleet()
        {
            return vehicles;
        }
        public List<Vehicle> GetFleet(bool rented)
        {
            List<Vehicle> avilableFleet = new List<Vehicle>();
            if (rented == true)
            {
                foreach (Vehicle vec in vehicles)
                {
                    foreach (string rego in rentals.Keys)
                    {
                        if (vec.VehicleRego == rego)
                        {
                            avilableFleet.Add(vec);
                        }
                    }
                }
            }
            else
            {  
                foreach (Vehicle vec in vehicles)
                {
                    bool notRented = true;
                    foreach (string rego in rentals.Keys)
                    {
                        if (vec.VehicleRego== rego)
                        {
                            notRented = false;
                        }
                    }
                    if (notRented)
                    {
                        avilableFleet.Add(vec);
                    }                   
                }
            }            
            return avilableFleet;
        }
        public bool IsRented(string vehicalRego)
        {
            foreach( string rego in rentals.Keys)
            {
                if(vehicalRego == rego)
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsRenting(int customerID)
        {
            foreach(int ID in rentals.Values)
            {
                if (ID == customerID)
                {
                    return true;
                }
            }
            return false;
        }
        public int RentedBy(string vehicleRego)
        {
            int i = 0;
            foreach(string rego in rentals.Keys)
            {            
                if (rego == vehicleRego)
                {
                    return rentals.Values.ElementAt(i);
                }
                i++;
            }
            return -1;
        }
        public bool rentCar(string VehicleRego, int customerID)
        {
            if (!rentals.ContainsKey(VehicleRego))
            {
                rentals.Add(VehicleRego, customerID);
                saveToFile();
                return true;                
            }
            else return false;
        }
        public int RetrunCar(string vehicleRego)
        {
            int i = 0;
            foreach (string rego in rentals.Keys)
            {
                if (rego == vehicleRego)
                {
                  int id =  rentals.Values.ElementAt(i);
                    rentals.Remove(rentals.ElementAt(i).Key);
                    saveToFile();
                    return id;
                }
                i++;
            }
            return -1;
        }
        public List<Vehicle> Search()
        {
            return GetFleet(false);
        }
        public List<Vehicle> Search(double min, double max)
        {
            List<Vehicle> Availablevehicles = new List<Vehicle>();
            foreach (Vehicle vec in GetFleet(false))
            {
               double rate = double.Parse(vec.ToCSVString().Split(',')[11]);
                if((int) rate >= (int) min && (int)rate <= (int)max)
                {
                    Availablevehicles.Add(vec);
                }
            }
            return Availablevehicles;
        }
        public List<Vehicle> Search(double min, double max, string query)
        {
            List<Vehicle> Availablevehicles = Search(min, max);
            List<Vehicle> SearchResult = new List<Vehicle>();
            string[] queries = query.Split(' ');
            if (queries.Length == 1)
            {
                foreach (Vehicle vec in Availablevehicles)
                {
                    string[] vecAlt = vec.ToString().Split(' ');
                    for (int i = 0; i < vecAlt.Length; i++)
                    {
                        if (query.ToUpper() == vecAlt[i].ToUpper() && !SearchResult.Contains(vec))
                        {
                            SearchResult.Add(vec);
                        }
                    }
                }
                return SearchResult;
            }
            else if (queries.Length > 1 && queries[1].ToUpper() != "AND")
            {
                foreach (Vehicle vec in Availablevehicles)
                {
                    string[] vecAlt = vec.ToString().Split(' ');
                    for (int j = 0; j < queries.Length; j++)
                    {
                        for (int i = 0; i < vecAlt.Length; i++)
                        {
                            if (queries[j].ToUpper() == vecAlt[i].ToUpper() && !SearchResult.Contains(vec))
                            {
                                SearchResult.Add(vec);
                            }
                        }
                    }
                }
                return SearchResult;
            }           
            else return SearchResult;
        }
        public void loadFromFile()
        {
        string[] filArray = File.ReadAllLines(fleetFile);
        string[] input = filArray[0].Split(',');
        string[] rentedCars = File.ReadAllLines(rentalFile);
        string[] rentalinput = rentedCars[0].Split(',');
        for (int i = 1; i < filArray.Length; i++)
            {
                input = filArray[i].Split(',');
                string rego = input[0];
                string make = input[1];
                string model = input[2];
                int year = int.Parse(input[3]);
                string _class = input[4];
                int seats = int.Parse(input[5]);
                string TransmissionType = input[6];
                string fueltype = input[7];
                bool GPS = bool.Parse(input[8]);
                bool sunRoof = bool.Parse(input[9]);
                string colour = input[10];
                double rate = double.Parse(input[11]);
                if (TransmissionType == "Automatic")
                {
                    if (fueltype == "Petrol")
                    {
                        if (_class == "Economy")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Economy, seats, Vehicle.TransmissionType.Automatic, Vehicle.FuelType.Petrol, GPS, sunRoof, colour, rate));
                        }
                        else if (_class == "Family")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Family, seats, Vehicle.TransmissionType.Automatic, Vehicle.FuelType.Petrol, GPS, sunRoof, colour, rate));
                        }
                        else if (_class == "Luxury")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Luxury, seats, Vehicle.TransmissionType.Automatic, Vehicle.FuelType.Petrol, GPS, sunRoof, colour, rate));
                        }
                        else
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Commercial, seats, Vehicle.TransmissionType.Automatic, Vehicle.FuelType.Petrol, GPS, sunRoof, colour, rate));
                        }
                    }
                   else if (fueltype == "Diesel")
                    {
                        if (_class == "Economy")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Economy, seats, Vehicle.TransmissionType.Automatic, Vehicle.FuelType.Diesel, GPS, sunRoof, colour, rate));
                        }
                        else if (_class == "Family")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Family, seats, Vehicle.TransmissionType.Automatic, Vehicle.FuelType.Diesel, GPS, sunRoof, colour, rate));
                        }
                        else if (_class == "Luxury")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Luxury, seats, Vehicle.TransmissionType.Automatic, Vehicle.FuelType.Diesel, GPS, sunRoof, colour, rate));
                        }
                        else
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Commercial, seats, Vehicle.TransmissionType.Automatic, Vehicle.FuelType.Diesel, GPS, sunRoof, colour, rate));
                        }
                    }
                    else if (fueltype == "LPG")
                    {
                        if (_class == "Economy")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Economy, seats, Vehicle.TransmissionType.Automatic, Vehicle.FuelType.LPG, GPS, sunRoof, colour, rate));
                        }
                        else if (_class == "Family")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Family, seats, Vehicle.TransmissionType.Automatic, Vehicle.FuelType.LPG, GPS, sunRoof, colour, rate));
                        }
                        else if (_class == "Luxury")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Luxury, seats, Vehicle.TransmissionType.Automatic, Vehicle.FuelType.LPG, GPS, sunRoof, colour, rate));
                        }
                        else
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Commercial, seats, Vehicle.TransmissionType.Automatic, Vehicle.FuelType.LPG, GPS, sunRoof, colour, rate));
                        }
                    }
                    else
                    {
                        if (_class == "Economy")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Economy, seats, Vehicle.TransmissionType.Automatic, Vehicle.FuelType.Electric, GPS, sunRoof, colour, rate));
                        }
                        else if (_class == "Family")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Family, seats, Vehicle.TransmissionType.Automatic, Vehicle.FuelType.Electric, GPS, sunRoof, colour, rate));
                        }
                        else if (_class == "Luxury")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Luxury, seats, Vehicle.TransmissionType.Automatic, Vehicle.FuelType.Electric, GPS, sunRoof, colour, rate));
                        }
                        else
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Commercial, seats, Vehicle.TransmissionType.Automatic, Vehicle.FuelType.Electric, GPS, sunRoof, colour, rate));
                        }
                    }
                }
                else 
                {
                    if (fueltype == "Petrol")
                    {
                        if (_class == "Economy")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Economy, seats, Vehicle.TransmissionType.Manual, Vehicle.FuelType.Petrol, GPS, sunRoof, colour, rate));
                        }
                        else if (_class == "Family")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Family, seats, Vehicle.TransmissionType.Manual, Vehicle.FuelType.Petrol, GPS, sunRoof, colour, rate));
                        }
                        else if (_class == "Luxury")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Luxury, seats, Vehicle.TransmissionType.Manual, Vehicle.FuelType.Petrol, GPS, sunRoof, colour, rate));
                        }
                        else
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Commercial, seats, Vehicle.TransmissionType.Manual, Vehicle.FuelType.Petrol, GPS, sunRoof, colour, rate));
                        }
                    }
                    else if (fueltype == "Diesel")
                    {
                        if (_class == "Economy")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Economy, seats, Vehicle.TransmissionType.Manual, Vehicle.FuelType.Diesel, GPS, sunRoof, colour, rate));
                        }
                        else if (_class == "Family")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Family, seats, Vehicle.TransmissionType.Manual, Vehicle.FuelType.Diesel, GPS, sunRoof, colour, rate));
                        }
                        else if (_class == "Luxury")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Luxury, seats, Vehicle.TransmissionType.Manual, Vehicle.FuelType.Diesel, GPS, sunRoof, colour, rate));
                        }
                        else
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Commercial, seats, Vehicle.TransmissionType.Manual, Vehicle.FuelType.Diesel, GPS, sunRoof, colour, rate));
                        }
                    }
                    else if (fueltype == "LPG")
                    {
                        if (_class == "Economy")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Economy, seats, Vehicle.TransmissionType.Manual, Vehicle.FuelType.LPG, GPS, sunRoof, colour, rate));
                        }
                        else if (_class == "Family")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Family, seats, Vehicle.TransmissionType.Manual, Vehicle.FuelType.LPG, GPS, sunRoof, colour, rate));
                        }
                        else if (_class == "Luxury")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Luxury, seats, Vehicle.TransmissionType.Manual, Vehicle.FuelType.LPG, GPS, sunRoof, colour, rate));
                        }
                        else
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Commercial, seats, Vehicle.TransmissionType.Manual, Vehicle.FuelType.LPG, GPS, sunRoof, colour, rate));
                        }
                    }
                    else
                    {
                        if (_class == "Economy")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Economy, seats, Vehicle.TransmissionType.Manual, Vehicle.FuelType.Electric, GPS, sunRoof, colour, rate));
                        }
                        else if (_class == "Family")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Family, seats, Vehicle.TransmissionType.Manual, Vehicle.FuelType.Electric, GPS, sunRoof, colour, rate));
                        }
                        else if (_class == "Luxury")
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Luxury, seats, Vehicle.TransmissionType.Manual, Vehicle.FuelType.Electric, GPS, sunRoof, colour, rate));
                        }
                        else
                        {
                            vehicles.Add(new Vehicle(rego, make, model, year, Vehicle.VehicleClass.Commercial, seats, Vehicle.TransmissionType.Manual, Vehicle.FuelType.Electric, GPS, sunRoof, colour, rate));
                        }
                    }
                }
           }           
        for (int i = 1; i < rentedCars.Length; i++)
        {
                rentalinput = rentedCars[i].Split(',');
                string carRego = rentalinput[0];
                int custID = int.Parse(rentalinput[1]);
                rentals.Add(carRego,custID);
        }
    }
        public void saveToFile()
        {
            string[] cars = new string[vehicles.Count];
            string[] newcars = new string[vehicles.Count+1];
            string[] filArray = File.ReadAllLines(fleetFile);
           int i= 0;
            foreach (Vehicle vec in vehicles)
            {
                cars[i] = vec.ToCSVString();
                i++;
            }
            newcars[0] = filArray[0];
            for ( i = 0; i < cars.Length; i++)
            {
                newcars[i+1]= cars[i];
            }
            File.WriteAllLines(fleetFile, newcars);
            string[] rented = new string[rentals.Count + 1];            
            string[] fileArray = File.ReadAllLines(rentalFile);            
            rented[0] = fileArray[0];
            int j = 1;           
            foreach (string rent in rentals.Keys)
            {
                rented[j] = rent+ ",";
                j++;
            }
            j = 1;
            foreach (int val in rentals.Values)
            {
                rented[j] += val.ToString();
                j++;
            }
            File.WriteAllLines(rentalFile, rented);
        }
        public override string ToString()
        {
            string carlist= "";
            foreach(Vehicle vec in vehicles)
            {
                carlist += vec.ToString() +"\n";
            }
            carlist += "rented;\n";
            foreach (string rent in rentals.Keys)
            {
                carlist += rent + "\n";
            }
            carlist += "by;\n";
            foreach (int rent in rentals.Values)
            {
                carlist += rent + "\n";
            }
            return carlist;
        }
        
    } 
}