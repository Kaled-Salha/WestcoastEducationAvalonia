using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;

namespace Client.Services;

public class StudentServices : IStudentService
{
  private readonly IHttpService<Student> _httpService;

  public StudentServices(IHttpService<Student> httpService)
  {
    _httpService = httpService;
  }

  public async Task<List<Student>> List()
  {
    return await _httpService.Get("students");
  }

  public async Task Add(Student student)
  {
    await _httpService.Post("students", student);
  }
}
