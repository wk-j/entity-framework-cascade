using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MyWeb.Controllers {
    [Route("api/[controller]/[action]")]
    public class StudentController : ControllerBase {

        readonly MyContext _db;

        public StudentController(MyContext context) {
            _db = context;
        }

        [HttpGet]
        public IActionResult Insert() {
            var student = new Student {
                Name = "wk"
            };

            var book = new Book {
                Name = "C++"
            };

            var sb = new StudentBook {
                Student = student,
                Book = book
            };

            _db.StudentBooks.Add(sb);
            _db.SaveChanges();

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult RemoveStudent(int id) {
            var student = _db.Students.First(x => x.Id == id);
            _db.Remove(student);
            _db.SaveChanges();
            return Ok();
        }
    }
}