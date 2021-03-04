namespace WorkflowFramework.Domain.UnitTests
{
    public class TestClass
    {
        public string Name { get; set; }

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