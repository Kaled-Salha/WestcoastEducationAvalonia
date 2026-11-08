using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;

namespace Client.Services;

public class CourseServices : ICourseService
{
  private readonly IHttpService<CourseRestDb> _httpService;

  public CourseServices(IHttpService<CourseRestDb> httpService)
  {
    _httpService = httpService;
  }

  public async Task<List<CourseRestDb>> List()
  {
    return await _httpService.Get("courses");
  }

  public async Task Add(CourseRestDb course)
  {
    await _httpService.Post("courses", course);
  }

  public async Task Delete(string id)
  {
    await _httpService.Delete("courses", id);
  }

  public async Task Update(CourseRestDb course, string id)
  {
    await _httpService.Put("courses", course, id);
  }
}