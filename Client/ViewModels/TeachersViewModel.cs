using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class TeachersViewModel : ViewModelBase
{
  private readonly ITeacherService _teacherService;
  private string? _editingTeacherId;

  [ObservableProperty]
  public partial bool IsEditing { get; set; } = false;


  [ObservableProperty]
  public partial ObservableCollection<TeacherRestDb> Teachers { get; set; } = [];

  [ObservableProperty]
  public partial TeacherRestDb NewTeacher { get; set; } = new();

  [ObservableProperty]
  public partial string StatusMessage { get; set; } = "";

  public TeachersViewModel(ITeacherService teacherService)
  {
    _teacherService = teacherService;

  }

  [RelayCommand]
  private async Task LoadTeachers()
  {
    try
    {
      Teachers = new ObservableCollection<TeacherRestDb>(
          await _teacherService.List());

      StatusMessage = $"Loaded {Teachers.Count} teachers.";
    }
    catch (Exception ex)
    {
      StatusMessage = $"Could not load teachers: {ex.Message}";
    }
  }

  [RelayCommand]
  private async Task AddTeacher()
  {
    if (string.IsNullOrWhiteSpace(NewTeacher.FirstName) ||
        string.IsNullOrWhiteSpace(NewTeacher.LastName) ||
        string.IsNullOrWhiteSpace(NewTeacher.PersonalNumber))
    {
      StatusMessage = "First name, last name and personal number are required.";
      return;
    }

    try
    {
      await _teacherService.Add(NewTeacher);

      NewTeacher = new TeacherRestDb();
      await LoadTeachers();

      StatusMessage = "Teacher added successfully.";
    }
    catch (Exception ex)
    {
      StatusMessage = $"Could not add teacher: {ex.Message}";
    }
  }


  [RelayCommand]
  private void EditTeacher(TeacherRestDb? teacher)
  {
    if (_editingTeacherId is not null)
    {
      StatusMessage = "Finish editing before adding a new teacher.";
      return;
    }

    if (teacher is null || string.IsNullOrWhiteSpace(teacher.Id))
    {
      StatusMessage = "Teacher ID not found.";
      return;
    }

    _editingTeacherId = teacher.Id;
    IsEditing = true;

    NewTeacher = new TeacherRestDb
    {
      Id = teacher.Id,
      FirstName = teacher.FirstName,
      LastName = teacher.LastName,
      Phone = teacher.Phone,
      PersonalNumber = teacher.PersonalNumber,
      KnowledgeArea = teacher.KnowledgeArea,

      Address = new Address
      {
        Street = teacher.Address.Street,
        PostalCode = teacher.Address.PostalCode,
        City = teacher.Address.City
      },

      ResponsibleCourses = teacher.ResponsibleCourses
    };

    StatusMessage = $"Editing teacher {teacher.FirstName} {teacher.LastName}.";
  }


  [RelayCommand]
  private async Task UpdateTeacher()
  {

    if (string.IsNullOrWhiteSpace(_editingTeacherId))
    {
      StatusMessage = "Select a teacher to edit first.";
      return;
    }

    if (string.IsNullOrWhiteSpace(NewTeacher.FirstName) ||
        string.IsNullOrWhiteSpace(NewTeacher.LastName) ||
        string.IsNullOrWhiteSpace(NewTeacher.PersonalNumber) ||
        string.IsNullOrWhiteSpace(NewTeacher.KnowledgeArea))
    {
      StatusMessage = "Please fill in all required fields.";
      return;
    }

    try
    {
      await _teacherService.Update(NewTeacher, _editingTeacherId);

      _editingTeacherId = null;
      IsEditing = false;
      NewTeacher = new TeacherRestDb();

      await LoadTeachers();

      StatusMessage = "Teacher updated successfully.";
    }
    catch (Exception ex)
    {
      StatusMessage = $"Could not update teacher: {ex.Message}";
    }
  }


  [RelayCommand]
  private void CancelEdit()
  {
    _editingTeacherId = null;
    IsEditing = false;

    NewTeacher = new TeacherRestDb();

    StatusMessage = "Editing cancelled.";
  }



}
