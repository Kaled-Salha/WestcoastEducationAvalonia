using System;

namespace Client.Models;

public class Person
{
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Phone { get; set; } = string.Empty;
  public string PersonalNumber { get; set; } = string.Empty;


  public Address Address { get; set; } = new();

  public override string ToString()
  {
    return $"{FirstName} {LastName}, Phone: {Phone}, Personal number: {PersonalNumber}, Address: {Address}";
  }

}
