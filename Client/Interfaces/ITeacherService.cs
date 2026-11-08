using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Models;

namespace Client.Interfaces;

public interface ITeacherService
{
  Task<List<TeacherRestDb>> List();
  Task Add(TeacherRestDb teacher);
  Task Update(TeacherRestDb teacher, string id);
}
