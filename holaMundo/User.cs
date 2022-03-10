public class User
{
    public string? name {get; init;}

    public string UpperName() => name.ToUpper();
}