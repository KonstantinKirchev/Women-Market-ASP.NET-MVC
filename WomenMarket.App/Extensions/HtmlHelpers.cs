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

        public static MvcHtmlString Image(this HtmlHelper helper, string imgUrl, string alt)
        {
            TagBuilder builder = new TagBuilder("img");
            builder.AddCssClass("img-responsive");
            builder.MergeAttribute("src", imgUrl);
            builder.MergeAttribute("alt", alt);
            return new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString CustomImage(this HtmlHelper helper, string imgUrl, string alt, string customClass)
        {
            TagBuilder builder = new TagBuilder("img");
            builder.AddCssClass(customClass);
            builder.MergeAttribute("src", imgUrl);
            builder.MergeAttribute("alt", alt);
            return new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));
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

        public static MvcHtmlString Submit(this HtmlHelper helper, string value)
        {
            TagBuilder builder = new TagBuilder("input");
            builder.AddCssClass("btn btn-primary");
            builder.MergeAttribute("type", "submit");
            builder.MergeAttribute("value", value);
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