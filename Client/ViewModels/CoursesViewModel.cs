using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Client.Enums;

namespace Client.ViewModels;

public partial class CoursesViewModel : ViewModelBase
{
  private readonly ICourseService _courseService;
  private string? _editingCourseId;

  [ObservableProperty]
  public partial bool IsEditing { get; set; } = false;

  [ObservableProperty]
  public partial ObservableCollection<CourseRestDb> Courses { get; set; } = [];

  [ObservableProperty]
  public partial string StatusMessage { get; set; } = "";

  [ObservableProperty]
  public partial string NewCourseNumber { get; set; } = "";

  [ObservableProperty]
  public partial string NewCourseName { get; set; } = "";

  [ObservableProperty]
  public partial string NewDuration { get; set; } = "";

  [ObservableProperty]
  public partial string NewStartDate { get; set; } = "";

  [ObservableProperty]
  public partial string NewEndDate { get; set; } = "";

  [ObservableProperty]
  public partial string NewCourseType { get; set; } = "1";

  public CoursesViewModel(ICourseService courseService)
  {
    _courseService = courseService;
  }

  [RelayCommand]
  private async Task LoadCourses()
  {
    try
    {
      Courses = new ObservableCollection<CourseRestDb>(
          await _courseService.List());

      StatusMessage = $"Loaded {Courses.Count} courses.";
    }
    catch (Exception ex)
    {
      StatusMessage = $"Could not load courses: {ex.Message}";
    }
  }



  [RelayCommand]
  private async Task AddCourse()
  {
    if (_editingCourseId is not null)
    {
      StatusMessage = "Finish editing before adding a new course.";
      return;
    }

    if (string.IsNullOrWhiteSpace(NewCourseNumber) ||
        string.IsNullOrWhiteSpace(NewCourseName))
    {
      StatusMessage = "Course number and name are required.";
      return;
    }

    if (!int.TryParse(NewDuration, out int duration) || duration <= 0 ||
        !DateTime.TryParse(NewStartDate, out DateTime startDate) ||
        !DateTime.TryParse(NewEndDate, out DateTime endDate) ||
        endDate < startDate)
    {
      StatusMessage = "Invalid duration or dates.";
      return;
    }

    if (NewCourseType != "1" && NewCourseType != "2")
    {
      StatusMessage = "Invalid course type.";
      return;
    }

    var course = new CourseRestDb
    {
      CourseNumber = NewCourseNumber,
      CourseName = NewCourseName,
      DurationInWeeks = duration,
      StartDate = startDate,
      EndDate = endDate,
      CourseType = NewCourseType == "2"
            ? CourseTypeEnum.Distance
            : CourseTypeEnum.Classroom
    };

    try
    {
      await _courseService.Add(course);

      ClearCourseForm();

      await LoadCourses();
      StatusMessage = "Course added successfully.";
    }
    catch (Exception ex)
    {
      StatusMessage = $"Could not add course: {ex.Message}";
    }
  }



  [RelayCommand]
  private async Task DeleteCourse(string? id)
  {
    if (string.IsNullOrWhiteSpace(id))
    {
      StatusMessage = "Course ID not found.";
      return;
    }

    try
    {
      await _courseService.Delete(id);
      await LoadCourses();
      StatusMessage = "Course deleted.";
    }
    catch (Exception ex)
    {
      StatusMessage = $"Could not delete course: {ex.Message}";
    }
  }

  [RelayCommand]
  private void EditCourse(CourseRestDb? course)
  {
    if (course is null || string.IsNullOrWhiteSpace(course.Id))
    {
      StatusMessage = "Course ID not found.";
      return;
    }

    _editingCourseId = course.Id;
    IsEditing = true;

    NewCourseNumber = course.CourseNumber;
    NewCourseName = course.CourseName;
    NewDuration = course.DurationInWeeks.ToString();
    NewStartDate = course.StartDate.ToString("yyyy-MM-dd");
    NewEndDate = course.EndDate.ToString("yyyy-MM-dd");

    NewCourseType = course.CourseType == CourseTypeEnum.Distance ? "2" : "1";

    StatusMessage = $"Editing course {course.CourseNumber}.";
  }


  [RelayCommand]
  private async Task UpdateCourse()
  {
    if (string.IsNullOrWhiteSpace(_editingCourseId))
    {
      StatusMessage = "Select a course to edit first.";
      return;
    }

    if (string.IsNullOrWhiteSpace(NewCourseNumber) ||
        string.IsNullOrWhiteSpace(NewCourseName) ||
        !int.TryParse(NewDuration, out int duration) ||
        duration <= 0 ||
        !DateTime.TryParse(NewStartDate, out DateTime startDate) ||
        !DateTime.TryParse(NewEndDate, out DateTime endDate) ||
        endDate < startDate ||
        (NewCourseType != "1" && NewCourseType != "2"))
    {
      StatusMessage = "Invalid course information.";
      return;
    }

    var course = new CourseRestDb
    {
      Id = _editingCourseId,
      CourseNumber = NewCourseNumber,
      CourseName = NewCourseName,
      DurationInWeeks = duration,
      StartDate = startDate,
      EndDate = endDate,
      CourseType = NewCourseType == "2"
            ? CourseTypeEnum.Distance
            : CourseTypeEnum.Classroom
    };


    try
    {
      await _courseService.Update(course, _editingCourseId);

      _editingCourseId = null;
      IsEditing = false;

      ClearCourseForm();

      await LoadCourses();
      StatusMessage = "Course updated successfully.";
    }
    catch (Exception ex)
    {
      StatusMessage = $"Could not update course: {ex.Message}";
    }

  }



  [RelayCommand]
  private void CancelEdit()
  {
    _editingCourseId = null;
    IsEditing = false;

    ClearCourseForm();

    StatusMessage = "Editing cancelled.";
  }



  private void ClearCourseForm()
  {
    NewCourseNumber = "";
    NewCourseName = "";
    NewDuration = "";
    NewStartDate = "";
    NewEndDate = "";
    NewCourseType = "1";
  }


}
