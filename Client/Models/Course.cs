using System;
using Client.Enums;

namespace Client.Models;

public class Course
{
  public string CourseNumber { get; set; } = string.Empty;
  public string CourseName { get; set; } = string.Empty;
  public int DurationInWeeks { get; set; }
  public DateTime StartDate { get; set; }
  public DateTime EndDate { get; set; }
  public CourseTypeEnum CourseType { get; set; }

  public override string ToString()
  {
    return $"{CourseNumber} - {CourseName}, {DurationInWeeks} weeks, " +
           $"{StartDate:yyyy-MM-dd} to {EndDate:yyyy-MM-dd}, {CourseType}";
  }

}
