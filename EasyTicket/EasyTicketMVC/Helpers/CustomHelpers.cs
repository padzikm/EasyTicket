using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyTicketMVC.Helpers
{
    public static class CustomHelpers
    {
        /// <summary>
        /// Creates a link that will open a jQuery UI dialog form.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="linkText">The inner text of the anchor element</param>
        /// <param name="dialogContentUrl">The url that will return the content to be loaded into the dialog window</param>
        /// <param name="dialogId">The id of the div element used for the dialog window</param>
        /// <param name="dialogTitle">The title to be displayed in the dialog window</param>
        /// <param name="updateTargetId">The id of the div that should be updated after the form submission</param>
        /// <param name="updateUrl">The url that will return the content to be loaded into the traget div</param>
        /// <returns></returns>
        public static MvcHtmlString DialogFormLink(this HtmlHelper htmlHelper, string linkText, string dialogContentUrl,
             string dialogTitle, string updateTargetId, string updateUrl, string id)
        {
            TagBuilder builder = new TagBuilder("a");
            builder.SetInnerText(linkText);
            builder.Attributes.Add("id", id);   
            builder.Attributes.Add("href", dialogContentUrl);
            builder.Attributes.Add("data-dialog-title", dialogTitle);
            builder.Attributes.Add("data-update-target-id", updateTargetId);
            builder.Attributes.Add("data-update-url", updateUrl);
            builder.Attributes.Add("autoOpen", "false");            

            builder.AddCssClass("btn btn-primary");
            builder.AddCssClass("dialogLink");

            return new MvcHtmlString(builder.ToString());
        }
    }
}