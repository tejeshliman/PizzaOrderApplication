using PizzaOrderApplicationClient.Models;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace PizzaOrderApplicationClient.Extensions
{
    public static class NotifierExtensions
    {
        public static void Error(this INotifier notifier, string text, params object[] format)
        {
            notifier.AddMessage(MessageSeverity.Danger, text, format);
        }

        public static void Info(this INotifier notifier, string text, params object[] format)
        {
            notifier.AddMessage(MessageSeverity.Info, text, format);
        }

        public static void Success(this INotifier notifier, string text, params object[] format)
        {
            notifier.AddMessage(MessageSeverity.Success, text, format);
        }

        public static void Warning(this INotifier notifier, string text, params object[] format)
        {
            notifier.AddMessage(MessageSeverity.Warning, text, format);
        }

        public static MvcHtmlString DisplayMessages(this System.Web.Mvc.ViewContext context)
        {
            if (!context.Controller.TempData.ContainsKey(Constants.TempDataKey))
            {
                return null;
            }

            var messages = (IEnumerable<Message>)context.Controller.TempData[Constants.TempDataKey];
            var builder = new StringBuilder();
            foreach (var message in messages)
            {
                builder.AppendLine(message.Generate());
            }

            return builder.ToHtmlString();
        }

        private static MvcHtmlString ToHtmlString(this StringBuilder input)
        {
            return new MvcHtmlString(input.ToString());
        }
    }
}
