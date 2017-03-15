using System.Web.Mvc;

namespace WomenMarket.App.Extensions
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString EmailTextBox(this HtmlHelper helper, string email)
        {
            TagBuilder builder = new TagBuilder("a");
            builder.MergeAttribute("href", $"mailto: {email}");
            builder.SetInnerText(email);
            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString Image(this HtmlHelper helper, string imgUrl, string alt, string style)
        {
            TagBuilder builder = new TagBuilder("img");
            builder.AddCssClass("img-responsive");
            builder.MergeAttribute("src", imgUrl);
            builder.MergeAttribute("alt", alt);
            builder.MergeAttribute("style", style);
            return new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString HyperLink(this HtmlHelper helper, string link, string icon)
        {
            TagBuilder builder = new TagBuilder("a");
            builder.MergeAttribute("href", link);
            builder.MergeAttribute("target", "_blank");
            TagBuilder builderIcon = new TagBuilder("i");
            builderIcon.AddCssClass(icon);
            builder.SetInnerText(builderIcon.ToString());
            return new MvcHtmlString(builder.ToString());
        }
    }
}