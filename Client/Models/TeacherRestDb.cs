using System.Text.Json.Serialization;

namespace Client.Models;

public class TeacherRestDb : Teacher
{
  [JsonPropertyName("_id")]
  public string? Id { get; set; }
}