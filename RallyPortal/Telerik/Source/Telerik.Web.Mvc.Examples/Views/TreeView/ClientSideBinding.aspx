<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<asp:content contentplaceholderid="MainContent" runat="server">
    
    <%= Html.Telerik().TreeView()
            .Name("TreeView")
            .ClientEvents(events => events
                .OnDataBinding("onDataBinding")
            )
    %>

    <script type="text/javascript">
        function onDataBinding(e) {
            var treeview = $('#TreeView').data('tTreeView');
            var jsonObject;

            if (e.item == treeview.element) {
                jsonObject = [
                    { Text: "Product 1", Expanded: true,
                        Items: [
                          { Text: "Product 1.1", Expanded: true,
                              Items: [
                                { Text: "Product 1.1.1", Value: 6 },
                                { Text: "Product 1.1.2" }
                            ]
                          },
                          { Text: "Abyss product", LoadOnDemand: true, Value: "abyss" }
                      ]
                    },
                    { Text: "Product 2 (unavailable)", Enabled: false },
                    { Text: "Product 3" }
                ];

                treeview.bindTo(jsonObject);
            } else {
                jsonObject = [{ Text: "Abyss subproduct", LoadOnDemand: true, Value: "abyss" }];
                
                treeview.dataBind(e.item, jsonObject);
            }
        }
    </script>
</asp:content>
