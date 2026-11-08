using System.Text.Json.Serialization;

namespace Client.Models;

public class CourseRestDb : Course
{
  [JsonPropertyName("_id")]
  public string? Id { get; set; }
}