namespace Demo.DurableFunction.Models
{
    public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}";
        }
    }
}
