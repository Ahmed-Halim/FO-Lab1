using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FO_Revision
{
    class Student_Fixed
    {
        private char[] isDeleted;
        public const int isDeletedLen = 1;

        private char[] studentID;
        public const int studentIDLen = 6;

        private char[] studentName;
        public const int studentNameLen = 20;

        private char[] studentFaculty;
        public const int studentFacultyLen = 20;

        public const int recordLen = 47;

        public Student_Fixed()
        {
            // Initialize the char arrays
            isDeleted = new char[isDeletedLen];
            studentID = new char[studentIDLen];
            studentName = new char[studentNameLen];
            studentFaculty = new char[studentFacultyLen];
        }

        public bool addStudent()
        {
            String input;

            // Set isDeletedFlag to 0 as default value
            isDeleted[0] = '0';

            // 1) Read input
            Console.Write("Student ID: ");
            input = Console.ReadLine();

            // 2) Check input length
            if (input.Length > studentIDLen)
            {
                Console.WriteLine("Error - Length Mismatch");
                return false;
            }

            // 3) Fill in the array of characters
            input.CopyTo(0, studentID, 0, input.Length);


            // Repeat the above steps for student name
            Console.Write("Student Name: ");
            input = Console.ReadLine();
            if (input.Length > studentNameLen)
            {
                Console.WriteLine("Error - Length Mismatch");
                return false;
            }
            input.CopyTo(0, studentName, 0, input.Length);

            // Repeat the above steps for student Faculty
            Console.Write("Student Faculty: ");
            input = Console.ReadLine();
            if (input.Length > studentFacultyLen)
            {
                Console.WriteLine("Error - Length Mismatch");
                return false;
            }
            input.CopyTo(0, studentFaculty, 0, input.Length);

            // Arrange the data into the big array of characters
            char[] record = new char[recordLen];
            isDeleted.CopyTo(record, 0);
            studentID.CopyTo(record, isDeletedLen);
            studentName.CopyTo(record, isDeletedLen + studentIDLen);
            studentFaculty.CopyTo(record, isDeletedLen + studentIDLen + studentNameLen);

            // Insert the big array of characters as one big record into a file
            FileStream fs = new FileStream("students_fixed.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(record);
            sw.Close();
            fs.Close();

            return true;
        }

        public void searchByID()
        {
            // Read ID from user
            String input;
            Console.Write("Search by ID: ");
            input = Console.ReadLine();

            // Same logic as display all but with an IF statement
            FileStream fs = new FileStream("students_fixed.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            // While there are lines to be read
            while (sr.Peek() != -1)
            {
                // Create the big array of characters that will hold the whole record
                char[] record = new char[recordLen];

                // Write from the file 47 characters and put them into the array of characters
                sr.Read(record, 0, recordLen);


                // If the record is not deleted display
                if (record[0] == '0')
                {
                    // To extract an attribute, we need to convert char[] to string
                    // because we need to use the substring function
                    String recordString = new String(record);

                    // Extraction of attributes
                    String id = recordString.Substring(isDeletedLen, studentIDLen);
                    String name = recordString.Substring(isDeletedLen + studentIDLen, studentNameLen);
                    String faculty = recordString.Substring(isDeletedLen + studentIDLen + studentNameLen, studentFacultyLen);

                    // Check if ID matches
                    if (input == id)
                    {
                        Console.WriteLine("Student Found!");
                        // Display on console
                        Console.Write("ID: " + id);
                        Console.Write("\tName: " + name);
                        Console.Write("\tFaculty: " + faculty + "\n");
                        sr.Close();
                        fs.Close();
                        return;
                    }
                }
            }

            // Close SR and FS
            Console.WriteLine("ID: " + input + " was not found");
            sr.Close();
            fs.Close();
        }

        public void displayAll()
        {
            FileStream fs = new FileStream("students_fixed.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            // While there are lines to be read
            while (sr.Peek() != -1)
            {
                // Create the big array of characters that will hold the whole record
                char[] record = new char[recordLen];

                // Write from the file 47 characters and put them into the array of characters
                sr.Read(record, 0, recordLen);


                // If the record is not deleted display
                if (record[0] == '0')
                {
                    // To extract an attribute, we need to convert char[] to string
                    // because we need to use the substring function
                    String recordString = new String(record);

                    // Extraction of attributes
                    String id = recordString.Substring(isDeletedLen, studentIDLen);
                    String name = recordString.Substring(isDeletedLen + studentIDLen, studentNameLen);
                    String faculty = recordString.Substring(isDeletedLen + studentIDLen + studentNameLen, studentFacultyLen);

                    // Display on console
                    Console.Write("ID: " + id);
                    Console.Write("\tName: " + name);
                    Console.Write("\tFaculty: " + faculty + "\n");
                }
            }

            // Close SR and FS
            sr.Close();
            fs.Close();
        }

        public void deleteStudent()
        {
             String input;
            Console.Write("Delete Student ID: ");
            input = Console.ReadLine();
            int recordNumber = getStudentRecordNumber(input);

            // if record found
            if (recordNumber != -1)
            {
                // Open new FS and SW
                FileStream fs = new FileStream("students_fixed.txt", FileMode.Open);
                StreamWriter sw = new StreamWriter(fs);

                // Seek to the record we want to overwrite
                sw.BaseStream.Seek(recordLen * recordNumber, SeekOrigin.Begin);

                // Write '1' in the isDeleted attribute
                sw.Write('1');

                // Close FS and SW
                sw.Close();
                fs.Close();

                Console.WriteLine("Student Successfully deleted");
            }
            else
            {
                Console.WriteLine("ID " + input + " was not found.");
            }
        }

        public int getStudentRecordNumber(String input)
        {

            // Same logic as search by id but with a counter
            FileStream fs = new FileStream("students_fixed.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            // Record Counter
            int recordNumber = 0;

            // While there are lines to be read
            while (sr.Peek() != -1)
            {
                // Create the big array of characters that will hold the whole record
                char[] record = new char[recordLen];

                // Write from the file 47 characters and put them into the array of characters
                sr.Read(record, 0, recordLen);

                // If the record is not deleted
                if (record[0] == '0')
                {
                    // To extract an attribute, we need to convert char[] to string
                    // because we need to use the substring function
                    String recordString = new String(record);

                    // Extraction of ID
                    String id = recordString.Substring(isDeletedLen, studentIDLen);

                    // Check if ID matches
                    if (input == id)
                    {
                        sr.Close();
                        fs.Close();
                        return recordNumber; // this is the record number we want
                    }
                }
                recordNumber++; // increment record
            }
            sr.Close();
            fs.Close();
            return -1; // -1 means the record is not found
        }


        public void updateStudent()
        {
            String input;
            Console.Write("Update Student ID: ");
            input = Console.ReadLine();
            int recordNumber = getStudentRecordNumber(input);

            // if record found
            if (recordNumber != -1)
            {
                // Open new FS and SW
                FileStream fs = new FileStream("students_fixed.txt", FileMode.Open);
                StreamWriter sw = new StreamWriter(fs);

                // Seek to the record we want to overwrite
                sw.BaseStream.Seek(recordLen * recordNumber, SeekOrigin.Begin);


                // Take updated info from user and save to char arrays
                isDeleted[0] = '0';
                input.CopyTo(0, studentID, 0, input.Length);

                Console.Write("Updated Name: ");
                input = Console.ReadLine();
                if (input.Length > studentNameLen)
                {
                    Console.WriteLine("Error - Length Mismatch");
                    return;
                }
                input.CopyTo(0, studentName, 0, input.Length);

                Console.Write("Updated Faculty: ");
                input = Console.ReadLine();
                if (input.Length > studentFacultyLen)
                {
                    Console.WriteLine("Error - Length Mismatch");
                    return;
                }
                input.CopyTo(0, studentFaculty, 0, input.Length);

                // Arrange the data into the big array of characters
                char[] record = new char[recordLen];
                isDeleted.CopyTo(record, 0);
                studentID.CopyTo(record, isDeletedLen);
                studentName.CopyTo(record, isDeletedLen + studentIDLen);
                studentFaculty.CopyTo(record, isDeletedLen + studentIDLen + studentNameLen);

                sw.Write(record);

                sw.Close();
                fs.Close();

                Console.WriteLine("Student Successfully updated");
            }
            else
            {
                Console.WriteLine("ID " + input + " was not found.");
            }
        }
    }
}
