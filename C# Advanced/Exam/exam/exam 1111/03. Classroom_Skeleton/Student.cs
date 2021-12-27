using System;
using System.Collections.Generic;
using System.Text;

namespace ClassroomProject
{
    class Student
    {
        public Student(string firstName, string lastName, string subject)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Subject = subject;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Subject { get; set; }

        public override string ToString()
        {

            return base.ToString();
        }
    }
}
//•	FirstName: string
//•	LastName: string
//•	Subject: string
