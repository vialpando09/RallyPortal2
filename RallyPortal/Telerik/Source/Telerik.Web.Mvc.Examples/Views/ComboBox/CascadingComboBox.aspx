<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<List<Category>>" %>

<asp:content contentPlaceHolderID="MainContent" runat="server">
    <div class="panel">
        <p>
            <label for="DropDownList_Categories-input">Categories:</label>
            <%= Html.Telerik().DropDownList()
                    .Name("DropDownList_Categories")
                    .BindTo(new SelectList(Model, "CategoryID", "CategoryName"))
                    .Placeholder("Select Category...")
                    .CascadeTo("DropDownList_Products")
            %>
        </p>

        <p>
            <label for="DropDownList_Products-input">Products:</label>
            <%= Html.Telerik().DropDownList()
                    .Name("DropDownList_Products")
                    .DataBinding(binding => binding.Ajax().Select("_GetDropDownListProducts", "ComboBox"))
                    .Placeholder("Select Product...")
                    .CascadeTo("DropDownList_Orders")
            %>
        </p>

        <p>
            <label for="DropDownList_Orders-input">Orders:</label>
            <%= Html.Telerik().DropDownList()
                    .Name("DropDownList_Orders")
                    .DataBinding(binding => binding.Ajax().Select("_GetDropDownListOrders", "ComboBox"))
                    .Placeholder("Select Order...")
            %>
        </p>
    </div>
     <div class="panel">
        <p>
            <label for="ComboBox_Categories-input">Categories:</label>
            <%= Html.Telerik().ComboBox()
                    .Name("ComboBox_Categories")
                    .BindTo(new SelectList(Model, "CategoryID", "CategoryName"))
                    .Placeholder("Select Category...")
                    .CascadeTo("ComboBox_Products")
                    .SelectedIndex(0)
            %>
        </p>

        <p>
            <label for="ComboBox_Products-input">Products:</label>
            <%= Html.Telerik().ComboBox()
                    .Name("ComboBox_Products")
                    .DataBinding(binding => binding.Ajax().Select("_GetComboBoxProducts", "ComboBox"))
                    .Placeholder("Select Product...")
                    .CascadeTo("ComboBox_Orders")
                    .SelectedIndex(0)
            %>
        </p>

        <p>
            <label for="ComboBox_Orders-input">Orders:</label>
            <%= Html.Telerik().ComboBox()
                    .Name("ComboBox_Orders")
                    .DataBinding(binding => binding.Ajax().Select("_GetComboBoxOrders", "ComboBox"))
                    .Placeholder("Select Order...")
                    .SelectedIndex(0)
            %>
        </p>
    </div>
</asp:content>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">

    <style type="text/css">
        .panel 
        {
             float:left;
             width:30%;
             padding-bottom: 3em;
        }
        
        .panel label
        {
            clear: both;
        }
    </style>
</asp:Content>