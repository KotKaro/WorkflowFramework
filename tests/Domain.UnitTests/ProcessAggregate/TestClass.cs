namespace Domain.UnitTests.ProcessAggregate
{
    public class TestClass
    {
        public string Name { get; init; } = string.Empty;

        public int Number { get; init; }

        public string GetHello(string name)
        {
            return "Hello " + name;
        }
        
        public string GetHello(string name, int number)
        {
            return "Hello " + name + " " + number;
        }
    }
}