using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Models;

namespace Client.Interfaces;

public interface IStudentService
{
  Task<List<Student>> List();
  Task Add(Student student);
}
