using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentApl
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("1. Add new students");
            Console.WriteLine("2. Generate new points");
            Console.WriteLine("3. Query students");
            Console.WriteLine("4. Grade and display students");
            var input = Console.ReadLine();

            if (input == "1")
            {
                Console.WriteLine("How many new students would you like to add?");
                input = Console.ReadLine();
                var inpOut = Convert.ToInt32(input);

                var students = new List<Student>();

                for (int i = 0; i < inpOut; i++)
                {
                    Console.WriteLine("Upisite ime studenta: ");
                    var name = Console.ReadLine();

                    Console.WriteLine("Upisite godine studenta: ");

                    var age = Console.ReadLine();
                    var ageout = Int32.Parse(age);
                    bool studentFound = students.Any(x => x.FirstName == name);
                    bool validAge = ValidateAge(ageout);

                    if (studentFound && !validAge)
                    {
                        Console.WriteLine("unable to register student!");

                        i--;
                    }
                    else if (!studentFound && validAge)
                    {
                        students.Add(new Student { FirstName = name, Age = ageout });
                    }
                }

                using (var context = new StudentContext())
                {
                    context.AddRange(students);
                    context.SaveChanges();
                }

                students = GetAllStudents();

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
            else if (input == "2")
            {
                Console.WriteLine("assigning points...");

                var students = GetAllStudents();

                using (var ctx = new StudentContext())
                {
                    foreach (var student in students)
                    {
                        var rnd = new Random();
                        var points = rnd.Next(1, 101);
                        student.Points = points;
                        ctx.Student.Attach(student);
                        ctx.Entry(student).Property(x => x.Points).IsModified = true;
                    }

                    ctx.SaveChanges();
                }

                Console.WriteLine("done!");
            }
            else if (input == "3")
            {
                var students = GetAllStudents();
                IEnumerable<Student> studentQuery = students.Where(x => x.Points < 60 || x.Age > 24);

                foreach (var student in studentQuery)
                {
                    Console.WriteLine(student.FirstName);
                }

                Console.WriteLine("students with less than 50 points");
                Console.WriteLine();

                IEnumerable<Student> negativeStudentQuery = students.Where(x => x.Points < 50);

                foreach (var student in negativeStudentQuery)
                {
                    Console.WriteLine(student.FirstName);
                }
            }
            else
            {
                var students = GetAllStudents();

                using (var ctx = new StudentContext())
                {
                    foreach (var student in students)
                    {
                        GradeStudents(student);

                        ctx.Student.Attach(student);
                        ctx.Entry(student).Property(x => x.Grade).IsModified = true;
                    }

                    ctx.SaveChanges();
                }

                foreach (var student in students)
                {
                    Console.WriteLine("{0}, {1}", student.FirstName, student.Grade);
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

        public static List<Student> GetAllStudents()
        {
            var context = new StudentContext();

            var students = context.Student.ToList<Student>();

            return students;
        }

        public static void GradeStudents(Student student)
        {
            if (student.Points < 50)
            {
                student.Grade = 1;
            }
            else if (student.Points > 50 && student.Points < 60)
            {
                student.Grade = 2;
            }
            else if (student.Points > 61 && student.Points < 70)
            {
                student.Grade = 3;
            }
            else if (student.Points > 70 && student.Points < 85)
            {
                student.Grade = 4;
            }
            else
            {
                student.Grade = 5;
            }
        }
    }
}