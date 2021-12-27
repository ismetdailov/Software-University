using System;
using System.Collections.Generic;
using System.Text;

namespace ClassroomProject
{
   public class Studentss
    {

        public class Classroom
        {
            private List<Studentss> students = new List<Studentss>();

            public Classroom(int capacity)
            {
                Capacity = capacity;
                students = new List<Studentss>();
            }

            public int Capacity { get; set; }
            public int Count { get { return students.Count; } }

            public string RegisterStudent(Studentss student)
            {
                if (students.Count < Capacity)
                {
                    students.Add(student);
                    return $"Added student {student.FirstName} {student.LastName}";
                }
                else
                {
                    return "No seats in the classroom";
                }
            }

            public string DismissStudent(string firstName, string lastName)
            {
                Studentss realStudent = students.FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName);
                if (realStudent == null)
                {
                    return "Student not found";
                }
                else
                {
                    students.Remove(realStudent);
                    return $"Dismissed student {realStudent.FirstName} {realStudent.LastName}";
                }
            }

            public string GetSubjectInfo(string subject)
            {
                List<Studentss> studentsSelect = students.Where(s => s.Subject == subject).ToList();
                if (studentsSelect.Count == 0)
                {
                    return "No students enrolled for the subject";
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Subject: {subject}");
                sb.AppendLine("Students:");
                foreach (Studentss student in studentsSelect)
                {
                    sb.AppendLine($"{student.FirstName} {student.LastName}");
                }
                return sb.ToString().TrimEnd();
            }

            public int GetStudentsCount()
            {
                return Count;
            }

            public Studentss GetStudent(string firstName, string lastName)
             => students.FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);
            //public Student( string FirstName, string LastName, string Subject)
            //{
            //         override.ToString()
            //}
            //public string FirstName { get; set; }
            //public string LastName { get; set; }
            //public string Subject { get; set; }


        }
}
