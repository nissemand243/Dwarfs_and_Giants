namespace SE_training.Infrastructure;

public class Teacher : User
{   
    public Teacher(string name, string email) : base(name, email){ }
    public override string TypeOfToString() => "Teacher";
}