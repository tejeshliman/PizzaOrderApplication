using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PizzaOrderApplicationClient.Models
{
    public enum MessageSeverity
    {
        None,
        Info,
        Success,
        Warning,
        Danger
    }
    public interface INotifier
    {
        IList<Message> Messages { get; }
        void AddMessage(MessageSeverity severity, string text, params object[] format);
    }

   
    public class Message
    {
        public MessageSeverity Severity { get; set; }

        public string Text { get; set; }

        public string Generate()
        {
            var isDismissable = Severity != MessageSeverity.Danger;
            if (Severity == MessageSeverity.None) Severity = MessageSeverity.Info;
            var sb = new StringBuilder();
            var divTag = new TagBuilder("div");
            divTag.AddCssClass("alert");
            divTag.AddCssClass("alert-" + Severity.ToString().ToLower());


            var spanTag = new TagBuilder("span");
            spanTag.MergeAttribute("id", "MessageContent");

            if (isDismissable)
            {
                divTag.AddCssClass("alert-dismissable");
            }

            _ = sb.Append(divTag.ToString(TagRenderMode.StartTag));

            if (isDismissable)
            {
                var buttonTag = new TagBuilder("button");
                buttonTag.MergeAttribute("class", "close");
                buttonTag.MergeAttribute("data-dismiss", "alert");
                buttonTag.MergeAttribute("aria-hidden", "true");
                buttonTag.InnerHtml = "×";
                sb.Append(buttonTag.ToString(TagRenderMode.Normal));
            }

            sb.Append(spanTag.ToString(TagRenderMode.StartTag));
            sb.Append(Text);
            sb.Append(spanTag.ToString(TagRenderMode.EndTag));
            sb.Append(divTag.ToString(TagRenderMode.EndTag));

            return sb.ToString();
        }
    }


    public static class Constants
    {
        public const string TempDataKey = "Messages";
    }

    public class NotifierFilterAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public INotifier Notifier { get; set; }

        public override void OnActionExecuted(System.Web.Mvc.ActionExecutedContext filterContext)
        {
            var messages = Notifier.Messages;
            if (messages.Any())
            {
                filterContext.Controller.TempData[Constants.TempDataKey] = messages;
            }
        }
    }

    
}
