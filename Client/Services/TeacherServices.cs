using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;

namespace Client.Services;

public class TeacherServices : ITeacherService
{
  private readonly IHttpService<TeacherRestDb> _httpService;

  public TeacherServices(IHttpService<TeacherRestDb> httpService)
  {
    _httpService = httpService;
  }

  public async Task<List<TeacherRestDb>> List()
  {
    return await _httpService.Get("teachers");
  }

  public async Task Add(TeacherRestDb teacher)
  {
    await _httpService.Post("teachers", teacher);
  }

  public async Task Update(TeacherRestDb teacher, string id)
  {
    await _httpService.Put("teachers", teacher, id);
  }
}
