using System;

namespace Usage_Of_MongoDb
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Program started.");

            Client.ObjeOlustur("AdressBook").RunCRUD();

            Console.ReadLine();
        }
    }
}
