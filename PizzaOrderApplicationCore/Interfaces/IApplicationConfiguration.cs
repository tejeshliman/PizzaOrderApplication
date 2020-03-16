namespace PizzaOrderApplication.Core.Kernel
{
    public interface IApplicationConfiguration
    {
        string DefaultConnectionString { get; set; }
    }
}
