using System;

namespace Client.Models;

public class Address
{

  public string Street { get; set; } = string.Empty;
  public string PostalCode { get; set; } = string.Empty;
  public string City { get; set; } = string.Empty;

  public override string ToString()
  {
    return $"{Street}, {PostalCode} {City}";
  }

}
