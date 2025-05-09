using Microsoft.AspNetCore.Mvc;
using StudentAPI.Model;
using StudentAPI.DataSimulation;

namespace StudentAPI.Controllers
{
    //[Route("api/[controller]")]//sets the route for this controller to "Students" based on the controller

    [ApiController]//marks the class as a web api controller with enhanced features

    [Route("api/Students")]//not using the controller for (if a client is using the endpoint and
                           //the class name changed it want affect the endpoint's name)
    public class StudentsAPIController : ControllerBase
    {

        [HttpGet("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            if (StudendsDataSimulation.StudentsList.Count is 0)
                return NotFound("No students found.");

            return Ok(StudendsDataSimulation.StudentsList);
        }

        [HttpGet("Passed", Name = "GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<Student>> GetPassedStudents()
        {
            var passedStudents = StudendsDataSimulation.
                StudentsList.Where(student => student.Grade > 50);

            if (passedStudents is null)
                return NotFound("No passed students found.");
            else
                return Ok(passedStudents);
        }


        [HttpGet("AverageGrade", Name = "GetAverageGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<double> GetAverageGrade()
        {
            if (StudendsDataSimulation.StudentsList.Count is 0)
                return NotFound("No students found");

            var avgGrade = StudendsDataSimulation.StudentsList.Average(student => student.Grade);
            return Ok(avgGrade);
        }

        [HttpGet("{id}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Student> GetStudentById(int id)
        {
            if (id < 0)
                return BadRequest($"ID {id} Is Not Accepted ");

            var student = StudendsDataSimulation.StudentsList.FirstOrDefault(student => student.Id == id);
            if (student is null)
                return NotFound($"Student With ID: {id} Not Found.");

            return Ok(student);
        }

        [HttpPost(Name = "AddStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<string> AddStudent(Student NewStudent)
        {
            if (NewStudent is null || string.IsNullOrWhiteSpace(NewStudent.Name) || NewStudent.Age < 18 || NewStudent.Grade < 0)
                return BadRequest("Invalid Student Data.");

            NewStudent.Id = StudendsDataSimulation.StudentsList.Count > 0 ?
                StudendsDataSimulation.StudentsList.Max(s => s.Id) + 1 : 1;
            StudendsDataSimulation.StudentsList.Add(NewStudent);

            return CreatedAtRoute("GetStudentById", new { id = NewStudent.Id }, NewStudent);

        }

        [HttpDelete("{id}",Name ="DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult DeleteStudent(int id)
        {
            if (id < 1)
                return BadRequest($"ID: {id} Not Accepted");
            var student = StudendsDataSimulation.StudentsList.FirstOrDefault(s => s.Id == id);
            if(student is null)
                return NotFound($"Student With ID {id} Not Found.");

            StudendsDataSimulation.StudentsList.Remove(student);
            return Ok($"Student with ID {id} has been deleted");
        }

        [HttpPut("{id}",Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateStudent(int id,Student UpdatedStudent)
        {
            if (id <= 0 ||UpdatedStudent is null || string.IsNullOrWhiteSpace(UpdatedStudent.Name) || 
                UpdatedStudent.Age < 18 || UpdatedStudent.Grade < 0)
                return BadRequest("Invalid student data.");

            var student = StudendsDataSimulation.StudentsList.FirstOrDefault(s => s.Id == id);

            if(student is null)
                return NotFound($"Student with ID {id} not found.");

            student.Name = UpdatedStudent.Name;
            student.Age = UpdatedStudent.Age;
            student.Grade = UpdatedStudent.Grade;

            return Ok(student);

        }
    }
}
