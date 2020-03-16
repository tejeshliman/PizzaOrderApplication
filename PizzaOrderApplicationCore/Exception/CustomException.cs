namespace PizzaOrderApplication.Core.Exception
{
    public class CustomException : System.Exception
    {
        public string ExMessage { get; set; }
        public CustomException(string msg, System.Exception originalException) : base("A service exception occurred, check inner exception", originalException)
        {
            ExMessage = msg;
        }
    }
}
