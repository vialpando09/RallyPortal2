<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SliderFirstLookModelView>" %>

<asp:content contentPlaceHolderID="MainContent" runat="server">

	<%= Html.Telerik().Slider<int>()
            .Name("Slider")
            .ShowButtons(false)
            .Value(5)
	%>

    <br />
    
    <%= Html.Telerik().RangeSlider<int>()
            .Name("RangeSlider")
            .Values(1, 9)
            .HtmlAttributes(new { style = "margin: 3em 0 2em;" })
	%>

    <noscript>
        <p>Your browsing experience on this page will be better if you visit it with a JavaScript-enabled
            browser / if you enable JavaScript.</p>
    </noscript>

    <% Html.RenderPartial("AccessibilityValidation"); %>

</asp:content>