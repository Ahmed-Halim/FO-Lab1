using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FO_Revision
{
    class Program
    {
        static void Main(string[] args)
        {
            String cont = "y";
            while (cont == "y" || cont == "Y")
            {
                Student_Fixed s = new Student_Fixed();

                Console.Clear();

                Console.WriteLine("Please choose from the following:");
                Console.WriteLine("1. Insert new student");
                Console.WriteLine("2. Display all students");
                Console.WriteLine("3. Search Student by ID");
                Console.WriteLine("4. Update Student");
                Console.WriteLine("5. Delete Student");

                String choice = Console.ReadLine();
                if (choice == "1")
                {
                    s.addStudent();
                }
                else if (choice == "2")
                {
                    s.displayAll();
                }
                else if (choice == "3")
                {
                    s.searchByID();
                }
                else if (choice == "4")
                {
                    s.updateStudent();
                }
                else if (choice == "5")
                {
                    s.deleteStudent();
                }
                else
                {
                    Console.WriteLine("Invalid Choice.");
                }

                Console.WriteLine("Do you want to continue? (Y/N)");
                cont = Console.ReadLine();
            }
        }
    }
}
