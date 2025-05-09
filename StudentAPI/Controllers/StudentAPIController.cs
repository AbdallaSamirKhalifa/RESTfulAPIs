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

        [HttpGet("GetAllStudents", Name = "GetAllStudents")]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            return Ok(StudendsDataSimulation.StudentsList);
        }
    }
}
