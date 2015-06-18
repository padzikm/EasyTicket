using EasyTicketLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WcfService
{
    public class MessageCreator
    {
        public IEnumerable<Offer> OfferList { get; set; }

        public MessageCreator(IEnumerable<Offer> offers)
        {
            OfferList = offers;
        }

        public string GetMessage()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<h1>New offers are available</h1>");
            foreach (var elem in OfferList)
            {
                sb.Append(elem.ToString());
                sb.AppendFormat("<br/><br/>");
            }

            return sb.ToString();

        }
    }
}