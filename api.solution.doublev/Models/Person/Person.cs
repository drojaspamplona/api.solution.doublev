using System.ComponentModel.DataAnnotations.Schema;

namespace api.solution.doublev.Models.Person;

public class Person
{
    public int IdPerson { get; set; }  

    public string FirstName { get; set; } 

    public string LastName { get; set; }  

    public string IdentificationNumber { get; set; } 

    public string Email { get; set; } 

    public string IdentificationType { get; set; }
    [NotMapped]
    public DateTime CreationDate { get; set; } 
    [NotMapped]
    public string FullIdentification { get; set; }
    [NotMapped]
    public string FullName { get; set; }
}

public class VwPerson
{
    public int IdPerson { get; set; }  

    public string FirstName { get; set; } 

    public string LastName { get; set; }  

    public string IdentificationNumber { get; set; } 

    public string Email { get; set; } 

    public string IdentificationType { get; set; }
    public DateTime CreationDate { get; set; } 
    public string FullIdentification { get; set; }
    public string FullName { get; set; }
}

