using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Models;

namespace Client.Interfaces;

public interface ICourseService
{
  Task<List<CourseRestDb>> List();
  Task Add(CourseRestDb course);
  Task Delete(string id);
  Task Update(CourseRestDb course, string id);
}