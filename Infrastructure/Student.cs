namespace SE_training.Infrastructure;

public class Student : User
{
        public Student(string name, string email) : base(name, email) { }
        public override string TypeOfToString() => "Student";

}