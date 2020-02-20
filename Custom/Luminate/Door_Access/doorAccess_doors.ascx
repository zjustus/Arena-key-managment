<%@ Control Language="c#" Inherits="ArenaWeb.UserControls.custom.Luminate.DoorAccess.Doors" CodeFile="doorAccess_doors.ascx.cs" %>

<asp:Panel ID="pnlReports" Runat="server" CssClass="card card-accent-primary">
    <div class="card-block">
        <div class="table-responsive">
            <Arena:DataGrid id="dgDoors" DataKeyField="door_id" Runat="server" AllowSorting="true">
                <Columns>
                    <asp:boundcolumn datafield="door_id" Visible="true" ReadOnly="True" HeaderText="ID"  />
	                <%--<asp:TemplateColumn HeaderText="Report Name" ItemStyle-Wrap="false" ItemStyle-VerticalAlign="Top" SortExpression="door_name">
	                    <ItemTemplate>
	                        <asp:LinkButton CommandName="ViewReport" Text='<%# Eval("door_name")%>' runat="server" ID="linkbtnViewReport"></asp:LinkButton>
	                    </ItemTemplate>
	                </asp:TemplateColumn>--%>

                    <asp:TemplateColumn
                        HeaderText="Door Name"
                        SortExpression="door_name"
                        ItemStyle-VerticalAlign="Top"
                        ItemStyle-Wrap="false">
                        <ItemTemplate><asp:PlaceHolder id="phName" Runat="server"></asp:PlaceHolder></ItemTemplate>
                        <EditItemTemplate><asp:TextBox ID="tbName" Runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "door_name") %>'></asp:TextBox></EditItemTemplate>
                    </asp:TemplateColumn>

	                <asp:TemplateColumn>
	                    <ItemTemplate>
	                        <asp:LinkButton ID="imgbtnCopy" runat="server" CommandName="CopyReport" CommandArgument='<%# Eval("location_id") %>' ToolTip="Copy List">
                                <i class="fa fa-copy fa-lg"></i>
	                        </asp:LinkButton>
	                    </ItemTemplate>
	                </asp:TemplateColumn>
                </Columns>
            </Arena:DataGrid>
        </div>
        <asp:Panel ID="pnlAdvanceReport" runat="server">
            <div class="text-right clear-padding">
                <asp:LinkButton ID="imgBtnAddRpt" runat="server" ToolTip="Add new Location">
                    <i class="fa-stack">
                        <i class="fa fa-th fa-stack-1x fa-lg text-gray-dark"></i>
                        <i class="fa fa-plus fa-stack-1x text-success" style="padding-left: 15px; padding-top: 8px;"></i>
                    </i>
                </asp:LinkButton>
            </div>
        </asp:Panel>
    </div>
</asp:Panel>
