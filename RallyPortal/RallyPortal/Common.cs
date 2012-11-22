using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Security.Policy;
using System.Linq.Expressions;
using System.Web.Routing;
using System.Net;
using System.Web.Mail;
using System.ComponentModel;
using System.IO;
using System.Web;
using System.Globalization;
using Telerik.Web.Mvc.UI;
using System.Drawing;

namespace RallyPortal
{
    public enum MessageType { Success, Information, Error, Warning }
        
    public static class LinkExtensions
    {
        public static MvcHtmlString ActionLinkButton(this HtmlHelper helper, UrlHelper url, string action, string controller, object routeValues, string value)
        {
            string result = "<input type=\"button\" onclick=\"javascript:window.location.href='" + url.Action(action, controller, routeValues) + "'\" value=\"" + value + "\" />";
            return new MvcHtmlString(result);
        }

        public static MvcHtmlString ActionLinkButton(this HtmlHelper helper, string url, string value)
        {
            string result = "<input type=\"button\" onclick=\"javascript:window.location.href='" + url + "'\" value=\"" + value + "\" />";
            return new MvcHtmlString(result);
        }
    }


    
    public static class Common
    {
        private static string SiteAddress = "";

        public static string GetFirstParagraph(string htmltext)
        {
            Match m = Regex.Match(htmltext, @"<p>\s*(.+?)\s*</p>");
            if (m.Success)
            {
                return m.Groups[1].Value;
            }
            else
            {
                return htmltext.Split(new string[] { "<br /><br />" }, StringSplitOptions.RemoveEmptyEntries)[0];
            }
        }

        public static void ResizeImage(string originalFilename, string newFilename, int width)
        {
            System.Drawing.Image imgOriginal = System.Drawing.Image.FromFile(originalFilename);
            ScaleBySize(imgOriginal, width).Save(newFilename);            
        }

        public static System.Drawing.Image ScaleBySize(System.Drawing.Image imgPhoto, float width)
        {            
            float sourceWidth = imgPhoto.Width;
            float sourceHeight = imgPhoto.Height;
            float destHeight = 0;
            float destWidth = width;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            destHeight = (float)(sourceHeight * width / sourceWidth);

            // Width is greater than height, set Width = logoSize and resize height accordingly

            System.Drawing.Bitmap bmPhoto = new System.Drawing.Bitmap((int)destWidth, (int)destHeight,
                                        System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, (int)destWidth, (int)destHeight),
                new Rectangle(sourceX, sourceY, (int)sourceWidth, (int)sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;
        }

        public static HtmlString FullTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            string content = "";
            ModelMetadata data = ModelMetadata.FromLambdaExpression(expression, html.ViewData as ViewDataDictionary<TModel>);
            if (data.Model != null)
                content = data.Model.ToString();

            return new HtmlString(
                html.Telerik().Editor()
                    .Name(data.PropertyName)
                    .Value(HttpUtility.HtmlDecode(content))
                    .Tools(tools => tools.Clear()
                        .FormatBlock()
                        .FontName()
                        .FontSize()
                        .FontColor()
                        .BackColor()
                        .Separator()
                        .Bold()
                        .Italic()
                        .Underline()
                        .Separator()
                        .Indent()
                        .Outdent()
                        .Separator()
                        .InsertOrderedList()
                        .InsertUnorderedList()
                        .Break()
                        .JustifyLeft()
                        .JustifyCenter()
                        .JustifyRight()
                        .JustifyFull()
                        .Separator()
                        .Subscript()
                        .Superscript()
                        .Separator()
                        .CreateLink()
                        .Unlink()
                        .InsertImage()
                        .Styles(styles => styles.Add("Highlight", "highlight").Add("Code block", "code").Add("Keybinding", "keybinding"))
                        )
                    .StyleSheets(styleSheets => styleSheets.Add("~/Content/Admin.css"))
                    .FileBrowser(t => t.Browse("Browse", "EditorImageBrowser")
                                         .Thumbnail("Thumbnail", "EditorImageBrowser")
                                         .Upload("Upload", "EditorImageBrowser")
                                         .DeleteFile("DeleteFile", "EditorImageBrowser")
                                         .DeleteDirectory("DeleteDirectory", "EditorImageBrowser")
                                         .CreateDirectory("CreateDirectory", "EditorImageBrowser")).ToHtmlString());
        }
        public static HtmlString HalfSizeFullTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            string content = "";
            ModelMetadata data = ModelMetadata.FromLambdaExpression(expression, html.ViewData as ViewDataDictionary<TModel>);
            if (data.Model != null)
                content = data.Model.ToString();

            return new HtmlString( "<div style=\"float: left; text-align: left; width: 80%;\">" + 
                html.Telerik().Editor()
                    .Name(data.PropertyName)
                    .Value(HttpUtility.HtmlDecode(content))
                    .Tools(tools => tools.Clear()
                        .FormatBlock()
                        .FontName()
                        .FontSize()
                        .FontColor()
                        .BackColor()
                        .Separator()
                        .Bold()
                        .Italic()
                        .Underline()
                        .Separator()
                        .Indent()
                        .Outdent()
                        .Separator()
                        .InsertOrderedList()
                        .InsertUnorderedList()
                        .Break()
                        .JustifyLeft()
                        .JustifyCenter()
                        .JustifyRight()
                        .JustifyFull()
                        .Separator()
                        .Subscript()
                        .Superscript()
                        .Separator()
                        .CreateLink()
                        .Unlink()
                        .InsertImage()
                        .Styles(styles => styles.Add("Highlight", "highlight").Add("Code block", "code").Add("Keybinding", "keybinding"))
                        )
                    .HtmlAttributes(new { width = "50%" })
                    .StyleSheets(styleSheets => styleSheets.Add("~/Content/Admin.css"))
                    .FileBrowser(t => t.Browse("Browse", "EditorImageBrowser")
                                         .Thumbnail("Thumbnail", "EditorImageBrowser")
                                         .Upload("Upload", "EditorImageBrowser")
                                         .DeleteFile("DeleteFile", "EditorImageBrowser")
                                         .DeleteDirectory("DeleteDirectory", "EditorImageBrowser")
                                         .CreateDirectory("CreateDirectory", "EditorImageBrowser")).ToHtmlString() + "</div>");
        }
        public static HtmlString SimpleTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            string content = "";
            ModelMetadata data = ModelMetadata.FromLambdaExpression(expression, html.ViewData as ViewDataDictionary<TModel>);
            if (data.Model != null)
                content = data.Model.ToString();

            return new HtmlString(
                html.Telerik().Editor()
                    .Name("Editor")
                    .Value(content)
                    .Tools(tools => tools.Clear()
                        .Bold()
                        .Italic()
                        .Underline()
                        .Separator()
                        .Indent()
                        .Outdent()
                        .Separator()
                        .InsertOrderedList()
                        .InsertUnorderedList()
                        .JustifyLeft()
                        .JustifyCenter()
                        .JustifyRight()
                        .JustifyFull()
                        .Separator()
                        .Subscript()
                        .Superscript()
                        .Separator()
                        .CreateLink()
                        .Unlink()
                        )
                    .ToHtmlString());
        }
    

        public static MvcHtmlString LinkedInLink(string url)
        {
            string linkedIn = "<script src=\"//platform.linkedin.com/in.js\" type=\"text/javascript\"></script><script type=\"IN/Share\" data-url=\"" + url + "\"></script>";

            return new MvcHtmlString(linkedIn);
        }

        public static MvcHtmlString GooglePlusLink(string url)
        {
            string gplus = "<div style=\"float: left; width: 42px; overflow: hidden;\"><div class=\"g-plusone\" data-size=\"medium\" data-annotation=\"inline\" data-width=\"120\" data-href=\"" + url + "\"></div></div>";

            return new MvcHtmlString(gplus);
        }

        public static MvcHtmlString GooglePlusStatic()
        {
            return new MvcHtmlString( "<script type=\"text/javascript\">  (function() {    var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;    po.src = 'https://apis.google.com/js/plusone.js';    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);  })();</script>" );
        }

        public static MvcHtmlString FacebookLink(string url)
        {
            string fb = "<div style=\"float: left; width: 80px;\"><div class=\"fb-like\" data-href=\""+url+"\" data-send=\"false\" data-layout=\"button_count\" data-width=\"120\" data-show-faces=\"false\"></div></div>";
            return new MvcHtmlString(fb);
        }

        public static MvcHtmlString FacebookStatic()
        {
            string script = "<div id=\"fb-root\"></div><script>(function(d, s, id) {  var js, fjs = d.getElementsByTagName(s)[0];  if (d.getElementById(id)) return;  js = d.createElement(s); js.id = id;  js.src = \"//connect.facebook.net/en_GB/all.js#xfbml=1&appId=379528372129877\";  fjs.parentNode.insertBefore(js, fjs);}(document, 'script', 'facebook-jssdk'));</script>";
            return new MvcHtmlString(script);
        }

        public static MvcHtmlString TwitterLink(string url)
        {
            string twitter;
                twitter = "<div style=\"float: left; display: inline-table; margin-right: 10px;\"><a href=\"https://twitter.com/share\" class=\"twitter-share-button\" data-url=\"" + url + "\" data-count=\"none\" data-lang=\"en\">Tweet</a><script>!function(d,s,id){var js,fjs=d.getElementsByTagName(s)[0];if(!d.getElementById(id)){js=d.createElement(s);js.id=id;js.src=\"//platform.twitter.com/widgets.js\";fjs.parentNode.insertBefore(js,fjs);}}(document,\"script\",\"twitter-wjs\");</script></div>";


            return new MvcHtmlString(twitter);
        }
        
        public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes, string labelText)
        {
            return LabelFor(html, expression, new RouteValueDictionary(htmlAttributes), labelText);
        }

        public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes, string labelText)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            TagBuilder tag = new TagBuilder("label");
            tag.MergeAttributes(htmlAttributes);
            tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));
            tag.SetInnerText(labelText);
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        public static string Urlable(this string text)
        {
            text = text.ToLower();
            text = text.Replace('ö', 'o');
            text = text.Replace('ő', 'o');
            text = text.Replace('ó', 'o');
            text = text.Replace('ü', 'u');
            text = text.Replace('ú', 'u');
            text = text.Replace('ű', 'u');
            text = text.Replace('í', 'i');
            text = text.Replace('á', 'a');
            text = text.Replace('é', 'e');
            text = text.Replace(' ', '_');
            text = text.Replace('#', '_');
            text = text.Replace('$', '_');
            text = text.Replace('.', '_');
            text = text.Replace('&', '_');
            text = text.Replace("\"", "");

            return text;
        }        
    }
}