using System.Collections.Generic;

namespace PizzaOrderApplicationClient.Models
{
    public class Notifier : INotifier
    {
        public IList<Message> Messages { get; private set; }

        public Notifier()
        {
            Messages = new List<Message>();
        }

        public void AddMessage(MessageSeverity severity, string text, params object[] format)
        {
            Messages.Add(new Message { Severity = severity, Text = string.Format(text, format) });
        }
    }
}
