<%@ Page Title="Batch" Language="C#" MasterPageFile="~/MasterPage/Standard.Master" AutoEventWireup="true" CodeBehind="BatchNew.aspx.cs" Inherits="InstituteManagement.Admin.BatchNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <script type="text/javascript">
        function validate() {
            //if validation sucess return true otherwise return false.
            if (document.getElementById("txt_BatchCode").value != "") {
                return true;
                alert('Batch code is compulsory field.');
            }
            
            else if (document.getElementById("txt_BatchDesc").value != "") {
                return true;
                alert('Batch Description is compulsory field.');
            }

            return false;
        }
    </script>

    <div class="container-fluid">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <asp:Label ID="Lab_message" runat="server" ForeColor="Red"></asp:Label>
                </div>
        </div>
        <div class="row">
            <div class="col-xs-6 col-sm-12 col-md-6 col-lg-6">
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="LabelBatchCode" runat="server" Text="Batch Code:*"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-9">
                        <asp:TextBox ID="txt_BatchCode" runat="server" Width="100%"></asp:TextBox>
                        <asp:TextBox ID="txtBatchKeyForEditMode" runat="server" Visible="false"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="LabelBatchDesc" runat="server" Text="Batch Desc.:*"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-9">
                        <asp:TextBox ID="txt_BatchDesc" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="lblNoOfInstallment" runat="server" Text="No Of Installment:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-4">
                        <asp:DropDownList ID="ddlNoOfInstallment" runat="server" Width="100%">
                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                            <asp:ListItem Text="6" Value="6"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="lblStartDate" runat="server" Text="Start Date:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-4">
                        <asp:TextBox ID="txtBatchStartDate" runat="server" TextMode="Date" Width="100%"></asp:TextBox>
                    </div>                    
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="lblEndDate" runat="server" Text="End Date:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-4">
                        <asp:TextBox ID="txtBatchEndDate" runat="server" TextMode="Date" Width="100%"></asp:TextBox>
                    </div>
                </div>
                 <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="lblClosed" runat="server" ></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-4">
                        <asp:CheckBox ID="chkClosed" runat="server" Text="Batch Closed" Width="100%"></asp:CheckBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblCourse" runat="server" Text="Select Course:*"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-9">
                        <asp:RadioButtonList ID="rdoListCourse" runat="server" DataTextField="courseCode" DataValueField="courseKey" RepeatColumns="5" RepeatLayout="Table"
                            CssClass="test" OnSelectedIndexChanged="rdoListCourse_SelectedIndexChanged" AutoPostBack="true" BorderStyle="Solid" RepeatDirection="Horizontal" Width="100%">
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblSubCourse" runat="server" Text="Subcourse:*"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-9">
                        <asp:RadioButtonList ID="rdoSubCourseList" runat="server" DataTextField="subcourseCode" DataValueField="subcourseKey" RepeatColumns="4" RepeatLayout="Table"
                            CssClass="test" OnSelectedIndexChanged="rdoSubCourseList_SelectedIndexChanged" AutoPostBack="true" BorderStyle="Solid" RepeatDirection="Horizontal" Width="100%">
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row divBottomMargin" runat="server" id="divGroupList">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblGroup" runat="server" Text="Select Group:*"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-9">
                        <asp:RadioButtonList ID="rdoGroupList" runat="server" DataTextField="groupsubcourseCode" DataValueField="groupsubcourseKey" RepeatColumns="4" RepeatLayout="Table"
                            CssClass="test" BorderStyle="Solid" RepeatDirection="Horizontal" Width="100%">
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="lab_CreatedBy" runat="server" Text="Created By:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-9">
                        <asp:Label ID="lab_CreatedByText" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="label3" runat="server" Text="Modified By:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-9">
                        <asp:Label ID="lab_ModifiedByText" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="Label2" runat="server" Text="Created On:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-9">
                        <asp:Label ID="lab_CreatedOnText" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="Label4" runat="server" Text="Modified On:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-9">
                        <asp:Label ID="lab_ModifiedOnText" runat="server"></asp:Label>
                    </div>
                </div>


            </div>
            <div class="col-xs-6 col-sm-12 col-md-6 col-lg-6" style="overflow:auto">
                <asp:GridView ID="grdBatch" runat="server" ForeColor="Black" BorderStyle="solid" Width="100%"
                    CssClass="col" BorderColor="#999999" BackColor="White" GridLines="Both" CellPadding="4" Height="100px"
                    BorderWidth="1px" AutoGenerateColumns="false" AllowSorting="True" CellSpacing="0" AllowPaging="true" AutoGenerateSelectButton="true" 
                   
                    EnablePersistedSelection="True" DataKeyNames="batchKey" ViewStateMode="Enabled" OnSelectedIndexChanged="grdBatch_SelectedIndexChanged"
                    OnPageIndexChanging="grdBatch_PageIndexChanging" PageSize="10">

                    <FooterStyle BackColor="#CCCCCC"></FooterStyle>
                    <RowStyle BackColor="LightGray" VerticalAlign="Middle" HorizontalAlign="Center" />
                    <%-- <RowStyle HorizontalAlign="Center" BorderColor="#CCCCCC" />--%>
                    <Columns>
                        
                        <%--<asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />--%>
                        <asp:BoundField DataField="batchKey" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />

                        <asp:BoundField DataField="batchCode" HeaderText="Batch Code" />

                        <asp:BoundField DataField="batchDesc" HeaderText="Batch Description" />
                        <asp:BoundField DataField="CreatedOn" HeaderText="Created On" DataFormatString="{0:dd/MMM/yyyy}" />
                        <asp:BoundField DataField="CreatedByEmpName" HeaderText="Created Emp" />
                        <asp:BoundField DataField="ModifiedOn" HeaderText="Modified On" DataFormatString="{0:dd/MMM/yyyy}" />                        
                        <asp:BoundField DataField="ModifiedByEmpName" HeaderText="Modified Emp" />
                        <asp:BoundField DataField="NoOfInstallment" HeaderText="Installments" />
                        <asp:BoundField DataField="BatchStartDate" HeaderText="Start Date" DataFormatString="{0:dd/MMM/yyyy}" />
                        <asp:BoundField DataField="BatchEndDate" HeaderText="End Date" DataFormatString="{0:dd/MMM/yyyy}" />

                        <asp:BoundField DataField="courseKey" HeaderText="courseKey" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"/>
                        <asp:BoundField DataField="courseCode" HeaderText="Course Code" />
                        <asp:BoundField DataField="subCourseKey" HeaderText="subCourseKey" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"/>
                        <asp:BoundField DataField="subCourseCode" HeaderText="sub Course Code" />
                        <asp:BoundField DataField="groupKey" HeaderText="groupKey" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"/>
                        <asp:BoundField DataField="groupsubcourseCode" HeaderText="Group Code" />

                    </Columns>
                    <SelectedRowStyle BackColor="DodgerBlue" ForeColor="White" Font-Bold="True"></SelectedRowStyle>
                    <PagerStyle BackColor="White" ForeColor="Blue" HorizontalAlign="Left" 
                        Font-Size="8pt" VerticalAlign="Middle" Font-Names="Verdana"></PagerStyle>
                    <HeaderStyle HorizontalAlign="Center" BackColor="chocolate" ForeColor="White" Font-Bold="True"
                        CssClass="headerstyle"></HeaderStyle>
                    <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
                    <PagerSettings Mode="NextPreviousFirstLast" PageButtonCount="100" Position="TopAndBottom"
                      FirstPageText="First Page" LastPageText="Last Page" />
                    <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                </asp:GridView>
            </div>

        </div>
        <div class="row divBottomMargin divTopMargin">
            <div class="col-xs-6 col-sm-6 col-md-3 col-lg-2">
                <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add New" Width="100px"/>
            </div>
            <div class="col-xs-6 col-sm-6 col-md-3 col-lg-2">

                <asp:Button ID="btn_Save" runat="server" OnClick="btn_Save_Click" Text="Save" Width="100px" OnClientClick="return validate();" />
            </div>
              <div class="col-xs-6 col-sm-6 col-md-3 col-lg-2">
                        <asp:Button ID="btnEdit" runat="server" Text="Update" Width="100px" OnClick="btnEdit_Click" /><%--OnClick="btn_EnquirySave_Click"--%>
                    </div>
            <div class="col-xs-6 col-sm-6 col-md-3 col-lg-2">
                <asp:Button ID="btn_Delete" runat="server" Text="Delete" Width="100px" OnClick="btn_Delete_Click"/>
            </div>
        </div>
         
    </div>

    <div>

       
        <br />
    </div>
</asp:Content>
