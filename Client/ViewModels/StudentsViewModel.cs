
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class StudentsViewModel : ViewModelBase
{
  private readonly IStudentService _studentService;

  [ObservableProperty]
  public partial ObservableCollection<Student> Students { get; set; } = [];

  [ObservableProperty]
  public partial Student NewStudent { get; set; } = new();

  [ObservableProperty]
  public partial string StatusMessage { get; set; } = "";

  public StudentsViewModel(IStudentService studentService)
  {
    _studentService = studentService;
  }

  [RelayCommand]
  private async Task LoadStudents()
  {
    try
    {
      Students = new ObservableCollection<Student>(
          await _studentService.List());

      StatusMessage = $"Loaded {Students.Count} students.";
    }
    catch (Exception ex)
    {
      StatusMessage = $"Could not load students: {ex.Message}";
    }
  }

  [RelayCommand]
  private async Task AddStudent()
  {
    if (string.IsNullOrWhiteSpace(NewStudent.FirstName) ||
        string.IsNullOrWhiteSpace(NewStudent.LastName) ||
        string.IsNullOrWhiteSpace(NewStudent.PersonalNumber))
    {
      StatusMessage = "First name, last name and personal number are required.";
      return;
    }

    try
    {
      await _studentService.Add(NewStudent);

      NewStudent = new Student();
      await LoadStudents();

      StatusMessage = "Student added successfully.";
    }
    catch (Exception ex)
    {
      StatusMessage = $"Could not add student: {ex.Message}";
    }
  }
}
