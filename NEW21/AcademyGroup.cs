﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp10
{
    
    
        class Academy_Group
        {
            ArrayList students;
            int count;

            public Academy_Group()
            {
                students = new ArrayList();
                count = 0;
            }

            
            public void Add(Student student)
            {
                students.Add(student);
                count++;
                Console.WriteLine("Student " + student.Name + " " + student.Surname + " added");

            }

            
            public void Remove(string student_surname)
            {
                for (int i = 0; i < students.Count; i++)
                {
                    Student student = (Student)students[i];
                    if (student.Surname.Equals(student_surname, StringComparison.OrdinalIgnoreCase))
                    {
                        students.RemoveAt(i);
                        count--;
                        Console.WriteLine("Student " + student_surname + " removed from the group)");
                        return;
                    }
                }

                Console.WriteLine("Student " + student_surname + " not found in group.");
            }

            public void Print()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No students in the group.");
                return;
            }

            foreach (Student student in students)
            {
                student.Print();
                Console.WriteLine();
            }
        }

        public void Sort(int sorting_сriteria)
        {
            if (sorting_сriteria == 1)
            {
                Console.WriteLine("Sort by name:");
                students.Sort();
                foreach (Student student in students)
                    student.Print();
            }
            else if (sorting_сriteria == 2)
            {
                Console.WriteLine("\nSort by last name:");
                students.Sort(new Student.SortBySurname());
                foreach (Student student in students)
                    student.Print();
            }
            else if (sorting_сriteria == 3)
            {
                Console.WriteLine("\nSort by age:");
                students.Sort(new Student.SortByAge());
                foreach (Student student in students)
                    student.Print();
            }
            else
            {
                Console.WriteLine("There is no such criterion!");
            }
            Print();
        }

        public void Save()
        {
            using (StreamWriter file = new StreamWriter("Group.txt"))
            {
                foreach (Student student in students)
                {
                    file.WriteLine($"{student.Name},{student.Surname},{student.Age},{student.Phone},{student.Average},{student.NumberOfGroup}");
                }
            }
            Console.WriteLine("Data saved to file.");
        }

        public void Load()
        {
            students.Clear();
            using (StreamReader file = new StreamReader("Group.txt"))
            {
                while (!file.EndOfStream)
                {
                    string[] fields = file.ReadLine().Split(',');
                    students.Add(new Student(fields[0], fields[1], int.Parse(fields[2]), fields[3], double.Parse(fields[4]), int.Parse(fields[5])));
                }
            }
            Console.WriteLine("Data loaded from file.");
        }

        public void Search(string searchValue, int criterion)
        {
            bool found = false;
            foreach (Student student in students)
            {
                bool matches = criterion switch
                {
                    1 => student.Name.Equals(searchValue, StringComparison.OrdinalIgnoreCase),
                    2 => student.Surname.Equals(searchValue, StringComparison.OrdinalIgnoreCase),
                    3 => student.Phone.Equals(searchValue, StringComparison.OrdinalIgnoreCase),
                    4 => student.Age.ToString() == searchValue,
                    5 => student.Average.ToString() == searchValue,
                    6 => student.NumberOfGroup.ToString() == searchValue,
                    _ => false
                };

                if (matches)
                {
                    student.Print();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No matching student found.");
            }
        }

        public object Clone()
        {
            Academy_Group clonedGroup = new Academy_Group();
            foreach (Student student in this.students)
            {
                clonedGroup.Add(student);
            }
            return clonedGroup;
        }

        internal void Search(int criterion)
        {
            throw new NotImplementedException();
        }

        internal void Edit(string? surname, Student newStudent)
        {
            throw new NotImplementedException();
        }
    }
}
