<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="WebApplication1.Default" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.17.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v17.2, Version=17.2.17.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v17.2.Core, Version=17.2.17.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraScheduler" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body> 
    <form id="form1" runat="server">
        <div>
            <dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" AppointmentDataSourceID="ObjectDataSourceAppointment"
                ClientIDMode="AutoID" Start='<%#Date.Now%>' GroupType="Date" ClientInstanceName="clientScheduler"
                ResourceDataSourceID="ObjectDataSourceResources">
                <Storage>
                    <Appointments AutoRetrieveId="True">
                        <Mappings
                            AllDay="AllDay"
                            AppointmentId="Id"
                            Description="Description"
                            End="EndTime"
                            Label="Label"
                            Location="Location"
                            RecurrenceInfo="RecurrenceInfo"
                            ReminderInfo="ReminderInfo"
                            ResourceId="OwnerId"
                            Start="StartTime"
                            Status="Status"
                            Subject="Subject"
                            Type="EventType" />
                        <CustomFieldMappings>
                            <dxwschs:ASPxAppointmentCustomFieldMapping Name="AppointmentCompany" Member="CompanyID" ValueType="Integer" />
                            <dxwschs:ASPxAppointmentCustomFieldMapping Name="AppointmentContact" Member="ContactID" ValueType="Integer" />
                        </CustomFieldMappings>
                    </Appointments>
                    <Resources>
                        <Mappings
                            Caption="Name"
                            ResourceId="ResID" />
                    </Resources>
                </Storage>

                <Views>
                    <DayView>
                        <TimeRulers>
                            <cc1:TimeRuler AlwaysShowTimeDesignator="False" ShowCurrentTime="Never"></cc1:TimeRuler>
                        </TimeRulers>
                        <DayViewStyles ScrollAreaHeight="600px">
                        </DayViewStyles>
                    </DayView>

                    <WorkWeekView>
                        <TimeRulers>
                            <cc1:TimeRuler></cc1:TimeRuler>
                        </TimeRulers>
                    </WorkWeekView>
                    <TimelineView>
                        <CellAutoHeightOptions Mode="FitToContent" />
                    </TimelineView>

                    <FullWeekView>
                        <TimeRulers>
                            <cc1:TimeRuler></cc1:TimeRuler>
                        </TimeRulers>
                    </FullWeekView>
                </Views>
            </dxwschs:ASPxScheduler>
            <br />
            <br />

            <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px" Caption="test" Text="content" ForeColor="#993399">
                <CaptionStyle ForeColor="#0099FF">
                </CaptionStyle>
            </dx:ASPxTextBox>
            <asp:Button ID="ButtonPostBack" runat="server" Text="Post Back" />

            <asp:ObjectDataSource ID="ObjectDataSourceResources" runat="server" OnObjectCreated="ObjectDataSourceResources_ObjectCreated" SelectMethod="SelectMethodHandler" TypeName="WebApplication1.CustomResourceDataSource"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceAppointment" runat="server" DataObjectTypeName="WebApplication1.CustomAppointment" DeleteMethod="DeleteMethodHandler" InsertMethod="InsertMethodHandler" SelectMethod="SelectMethodHandler" TypeName="WebApplication1.CustomAppointmentDataSource" UpdateMethod="UpdateMethodHandler" OnObjectCreated="ObjectDataSourceAppointment_ObjectCreated"></asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>