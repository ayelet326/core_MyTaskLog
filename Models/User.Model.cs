using SharedModel.Models;
using enumType.Models;

namespace IUser.Models;
public class User:Shared
{
    public string? Password {get;set;}
    public TypeUser? TypeUser {get;set;}
}

