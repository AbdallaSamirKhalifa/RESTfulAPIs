public class Students
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int Grade { get; set; }


    public override string ToString() => $"ID: {Id}, Name: {Name}, Age: {Age}, Grade: {Grade}";
    
}

