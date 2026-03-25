using Castle.Core.Resource;
using E_Commerce_Application.Data;
using E_Commerce_Application.Models;
using E_Commerce_Application.Seeding;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_Commerce_Application
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new AppDbContext();
            //US-003 
            var seeder = new DbSeeder(context);
            await seeder.SeedAsync();

            Console.WriteLine("Seeding Done");



        }
    }
}