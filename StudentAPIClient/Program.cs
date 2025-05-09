using System.Net.Http.Json;

class Program
{
    static readonly HttpClient httpClient = new HttpClient();

    static async Task Main(string[] args)
    {
        httpClient.BaseAddress = new Uri("http://localhost:5221/api/Students/");

        //this will show all students 
        await GetAllStudents();

        //this will show passed students only
        await GetPassedStudents();

        //this will show the calculated average for students grades
        await GetAverageGrade();

        //this will show the info for student 1
        await GetStudentById(1); // Example: Get student with ID 1

        //this will show the info for student 20 and show not found because 20 is not there
        await GetStudentById(20); // Example: Get student with ID 20

        //this will add new student
        var newStudent = new Student { Name = "Mazen Abdullah", Age = 20, Grade = 85 };
        await AddStudent(newStudent); // Example: Add a new student

        //this will show all students after adding new one
        await GetAllStudents();

        //this will delete student 1
        await DeleteStudent(1); // Example: Delete student with ID 1

        //this will show all students after deleting student 1
        await GetAllStudents();

        //this will Update student 2
        await UpdateStudent(2, new Student { Name = "Salma", Age = 22, Grade = 90 }); // Example: Update student with ID 2

        await GetAllStudents();

        //this will show all students after Updating student 2
        await GetAllStudents();

        Console.ReadKey();
    }
    static async Task GetAllStudents()
    {
        Console.WriteLine("\n---------------------------------");
        Console.WriteLine("\nFetching all students....\n");
        try
        {

            var response = await httpClient.GetAsync("All");

            if (response.IsSuccessStatusCode)
            {
                var students = await response.Content.ReadFromJsonAsync<List<Student>>();

                if (students != null && students.Count > 0)
                {
                    foreach (var student in students)
                    {
                        Console.WriteLine(student);
                    }
                }
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                Console.WriteLine("No Students Found.");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }



    static async Task GetPassedStudents()
    {
        Console.WriteLine("\n---------------------------------");
        Console.WriteLine("\nFetching Passed students....\n");
        try
        {
            var response = await httpClient.GetAsync("Passed");

            if (response.IsSuccessStatusCode)
            {
                var passedStudents = await response.Content.ReadFromJsonAsync<List<Student>>();

                if (passedStudents != null && passedStudents.Count > 0)
                {
                    foreach (var student in passedStudents)
                    {
                        Console.WriteLine(student);
                    }
                }
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                Console.WriteLine("No Passed Students Found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }

    }

    static async Task GetAverageGrade()
    {
        Console.WriteLine("\n---------------------------------");
        Console.WriteLine("\nFetching Average Grade....\n");
        try
        {
            var response = await httpClient.GetAsync("AverageGrade");
            if (response.IsSuccessStatusCode)
            {

                var AverageGrade = await response.Content.ReadFromJsonAsync<float>();

                Console.WriteLine($"Average Grade: {AverageGrade}");
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                Console.WriteLine("No students found.");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }

    }
    static async Task GetStudentById(int id)
    {
        Console.WriteLine("\n---------------------------------");
        Console.WriteLine($"\nFetching Student With ID: {id}....\n");
        try
        {
            var response = await httpClient.GetAsync($"{id}");

            if (response.IsSuccessStatusCode)
            {
                var student = await httpClient.GetFromJsonAsync<Student>($"{id}");

                if (student != null)
                    Console.WriteLine($"{student}");

            }
            else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                Console.WriteLine($"Not Found: Student with ID {id} not found.");

            else if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                Console.WriteLine($"Bad Request: Not accepted ID {id}");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }

    }

    static async Task AddStudent(Student NewStudent)
    {
        Console.WriteLine("\n_____________________________");
        Console.WriteLine("\nAdding a new student...\n");
        try
        {
            var response = await httpClient.PostAsJsonAsync("",NewStudent);
            if (response.IsSuccessStatusCode)
            {
                var addedStudent = await response.Content.ReadFromJsonAsync<Student>();
                if (addedStudent != null)
                    Console.WriteLine("Added Student:\n" + addedStudent);

            }else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                Console.WriteLine("Bad Request: Invalid student data.");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

    }
    static async Task DeleteStudent(int id)
    {
        Console.WriteLine("\n---------------------------------");
        Console.WriteLine($"\nDeleting Student With ID: {id}....\n");
        try
        {
            var response = await httpClient.DeleteAsync($"{id}");

            if (response.IsSuccessStatusCode)
                Console.WriteLine($"Student with ID {id} has been deleted.");

            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                Console.WriteLine($"Not Found: Student with ID {id} not found.");

            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                Console.WriteLine($"Bad Request: Not accepted ID {id}");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }

    }

    static async Task UpdateStudent(int id,Student UpdatedStudent)
    {
        Console.WriteLine("\n---------------------------------");
        Console.WriteLine($"\nUpdating Student With ID: {id}....\n");
        try
        {
            var response = await httpClient.PutAsJsonAsync($"{id}", UpdatedStudent);
            if(response.IsSuccessStatusCode)
            {
                var student = await response.Content.ReadFromJsonAsync<Student>();
                if (student != null)
                    Console.WriteLine($"Updated Student: {student}");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                Console.WriteLine($"Student with ID {id} not found.");

            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                Console.WriteLine("Failed to update student: Invalid data.");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
}

