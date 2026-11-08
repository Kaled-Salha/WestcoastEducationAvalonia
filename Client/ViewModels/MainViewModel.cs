

namespace Client.ViewModels;

public partial class MainViewModel : ViewModelBase
{

    public CoursesViewModel CoursesPage { get; }
    public StudentsViewModel StudentsPage { get; }
    public TeachersViewModel TeachersPage { get; }

    public MainViewModel(
        CoursesViewModel coursesPage,
        StudentsViewModel studentsPage,
        TeachersViewModel teachersPage)
    {
        CoursesPage = coursesPage;
        StudentsPage = studentsPage;
        TeachersPage = teachersPage;
    }

}
