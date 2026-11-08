using Client.Interfaces;
using Client.Services;
using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Helpers;

public static class ServiceCollectionExtensions
{
  public static void AddServices(this IServiceCollection collection)
  {
    collection.AddSingleton<MainViewModel>();

    collection.AddTransient<CoursesViewModel>();
    collection.AddTransient<ICourseService, CourseServices>();
    collection.AddTransient<StudentsViewModel>();
    collection.AddTransient<IStudentService, StudentServices>();
    collection.AddTransient<TeachersViewModel>();
    collection.AddTransient<ITeacherService, TeacherServices>();


    collection.AddTransient(typeof(IHttpService<>), typeof(HttpService<>));
  }
}