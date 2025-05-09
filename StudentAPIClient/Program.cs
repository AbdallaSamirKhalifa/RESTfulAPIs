using System.Net.Http.Json;

class Program
{
    static readonly HttpClient httpClient = new HttpClient();

    static async Task Main(string[] args)
    {
        httpClient.BaseAddress = new Uri("http://localhost:5221/api/Students");
        await GetAllStudents();
    }

    static async Task GetAllStudents()
    {
        try
        {  
            Console.WriteLine("\n---------------------------------");
            Console.WriteLine("\nFetching all students....\n");

            var students =  await 
                httpClient.GetFromJsonAsync<List<Students>>("Students/GetAllStudents");
            if (students != null)
            {
                foreach (var student in students)
                {
                    Console.WriteLine(student);
                }
            } 
        }catch(Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }       
    }
}

