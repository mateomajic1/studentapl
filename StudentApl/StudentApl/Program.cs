using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentApl
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("1. Add students");
            Console.WriteLine("2. Add bodovi");
            var input = Console.ReadLine();

            if (input == "1.")
            {
                var students = new List<Student>();

                for (int i = 0; i < 8; i++)
                {
                    Console.WriteLine("Upisite ime studenta: ");
                    var name = Console.ReadLine();

                    Console.WriteLine("Upisite godine studenta: ");
                    var age = Console.ReadLine();
                    var intage = Int16.Parse(age);

                    bool studentFound = students.Any(x => x.FirstName == name);
                    bool validAge = ValidateAge(intage);

                    if (studentFound && !validAge)
                    {
                        Console.WriteLine("unable to register student!");
                        i--;
                    }
                    else if (!studentFound && validAge)
                    {
                        students.Add(new Student { FirstName = name, age = intage });
                    }
                }

                using (var context = new StudentContext())
                {
                    context.AddRange(students);
                    context.SaveChanges();
                }

                students = GetStudents();

                foreach (var student in students)
                {
                    var letter = student.FirstName.Substring(0, 1);
                    letter = letter.ToLower();

                    switch (letter)
                    {
                        case "m":
                            Console.WriteLine(student.FirstName);
                            break;

                        case "j":
                            Console.WriteLine(student.FirstName);
                            break;

                        case "d":
                            Console.WriteLine(student.FirstName);
                            break;

                        case "k":
                            Console.WriteLine(student.FirstName);
                            break;

                        case "f":
                            Console.WriteLine(student.FirstName);
                            break;

                        case "a":
                            Console.WriteLine(student.FirstName);
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("assigning points...");

                var students = GetStudents();

                using (var ctx = new StudentContext())
                {
                    foreach (var student in students)
                    {
                        var rnd = new Random();
                        var points = rnd.Next(1, 101);
                        student.points = points;
                    }

                    ctx.SaveChanges();
                }
            }
        }

        public static bool ValidateAge(int age)
        {
            bool validAge = false;

            if (age < 19 && age > 27)
            {
                validAge = false;
                return validAge;
            }
            else
            {
                validAge = true;
                return validAge;
            }
        }

        public static List<Student> GetStudents()
        {
            var context = new StudentContext();

            var students = context.students.ToList<Student>();

            return students;
        }
    }
}