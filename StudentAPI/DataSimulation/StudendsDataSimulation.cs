using Microsoft.AspNetCore.Mvc.Formatters;
using StudentAPI.Model;

namespace StudentAPI.DataSimulation
{
    public class StudendsDataSimulation
    {

        public static readonly List<Student> StudentsList = new List<Student>
        {
            new Student{Id = 1, Name = "Ali Ahmed", Age = 20, Grade = 88},
            new Student{Id = 2, Name = "Fadi Khalil", Age = 22, Grade = 77},
            new Student{Id = 3, Name = "Ola Jaber", Age = 21, Grade = 66},
            new Student{Id = 4, Name = "Alia Maher", Age = 19, Grade = 44}
        };

    }
}
