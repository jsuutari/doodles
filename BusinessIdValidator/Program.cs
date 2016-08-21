using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessIdValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type the businessId:");
            BusinessIdSpecification businessIdSpecification = new BusinessIdSpecification();
            if (businessIdSpecification.IsSatisfiedBy(Console.ReadLine()))
            {
                Console.WriteLine("The typed businessId is valid.");
            }
            else
            {
                Console.WriteLine("The typed businessId is NOT valid. It has following issues:");
                foreach(var s in businessIdSpecification.ReasonsForDissatisfaction)
                    Console.WriteLine(s);
            }
            Console.ReadLine();
        }
    }
}
