Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web
Imports DevExpress.Web.ASPxScheduler.Dialogs
Imports DevExpress.XtraScheduler

Namespace WebApplication1
    Partial Public Class [Default]
        Inherits System.Web.UI.Page

        Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
            ASPxScheduler1.OptionsForms.DialogLayoutSettings.AppointmentDialog.ViewModel.PrepareControl(Sub(cb As ASPxCheckBox)
                cb.ToggleSwitchDisplayMode = ToggleSwitchDisplayMode.Never
            End Sub)

            ASPxScheduler1.OptionsForms.DialogLayoutSettings.AppointmentDialog.ViewModel.PrepareControlFor(Function(m) m.StartTime, Sub(de As ASPxDateEdit)
                de.EditFormat = EditFormat.Custom
                de.EditFormatString = "dd - MM - yyyy"
            End Sub)

            ASPxScheduler1.OptionsForms.DialogLayoutSettings.AppointmentDialog.ViewModel.PrepareControlFor(Function(m) m.EndTime, Sub(de As ASPxDateEdit)
                de.EditFormat = EditFormat.Custom
                de.EditFormatString = "dd - MM - yyyy"
            End Sub)

            ASPxScheduler1.OptionsForms.DialogLayoutSettings.AppointmentDialog.ViewModel.PrepareControlFor(Function(m) m.ResourceIds, Sub(de As ASPxEdit)
                de.Caption = "Employee"
            End Sub)

            ASPxScheduler1.OptionsForms.DialogLayoutSettings.AppointmentDialog.ViewModel.PrepareControlFor(Function(m) m.Subject, Sub(de As ASPxTextBox)
                de.CaptionStyle.ForeColor = Color.Red
                de.CaptionStyle.Font.Bold = True
            End Sub)

            ASPxScheduler1.OptionsForms.DialogLayoutSettings.AppointmentDialog.ViewModel.PrepareControlFor(Function(m) m.Description, Sub([me] As ASPxMemo)
                [me].ForeColor = Color.Blue
                [me].Font.Bold = True
                [me].Font.Italic = True
            End Sub)

            ASPxScheduler1.OptionsForms.DialogLayoutSettings.AppointmentDialog.ViewModel.SetItemVisibilityCondition("Location", False)
            ASPxScheduler1.OptionsForms.DialogLayoutSettings.AppointmentDialog.ViewModel.SetItemVisibilityCondition(Function(vm) vm.IsAllDay, False)


        End Sub


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

        End Sub

        Protected Sub ObjectDataSourceResources_ObjectCreated(ByVal sender As Object, ByVal e As ObjectDataSourceEventArgs)
            If Session("CustomResourceDataSource") Is Nothing Then
                Session("CustomResourceDataSource") = New CustomResourceDataSource(GetCustomResources())
            End If
            e.ObjectInstance = Session("CustomResourceDataSource")
        End Sub

        Private Function GetCustomResources() As BindingList(Of CustomResource)
            Dim resources As New BindingList(Of CustomResource)()
            resources.Add(CreateCustomResource(1, "Max Fowler"))
            resources.Add(CreateCustomResource(2, "Nancy Drewmore"))
            resources.Add(CreateCustomResource(3, "Pak Jang"))
            Return resources
        End Function

        Private Function CreateCustomResource(ByVal res_id As Integer, ByVal caption As String) As CustomResource
            Dim cr As New CustomResource()
            cr.ResID = res_id
            cr.Name = caption
            Return cr
        End Function

        Public RandomInstance As New Random()
        Private Function CreateCustomAppointment(ByVal subject As String, ByVal resourceId As Object, ByVal status As Integer, ByVal label As Integer) As CustomAppointment
            Dim apt As New CustomAppointment()
            apt.Subject = subject
            apt.OwnerId = resourceId
            Dim rnd As Random = RandomInstance
            Dim rangeInMinutes As Integer = 60 * 24
            apt.StartTime = Date.Today + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes))
            apt.EndTime = apt.StartTime.Add(TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes \ 4)))
            apt.Status = status
            apt.Label = label
            apt.Description = "Some detailed information about " & apt.Subject
            Return apt
        End Function

        Protected Sub ObjectDataSourceAppointment_ObjectCreated(ByVal sender As Object, ByVal e As ObjectDataSourceEventArgs)
            If Session("CustomAppointmentDataSource") Is Nothing Then
                Session("CustomAppointmentDataSource") = New CustomAppointmentDataSource(GetCustomAppointments())
            End If
            e.ObjectInstance = Session("CustomAppointmentDataSource")
        End Sub

        Private Function GetCustomAppointments() As BindingList(Of CustomAppointment)
            Dim appointments As New BindingList(Of CustomAppointment)()

            Dim resources As CustomResourceDataSource = TryCast(Session("CustomResourceDataSource"), CustomResourceDataSource)
            If resources IsNot Nothing Then
                For Each item As CustomResource In resources.Resources
                    Dim subjPrefix As String = item.Name & "'s "
                    appointments.Add(CreateCustomAppointment(subjPrefix & "meeting", item.ResID, 2, 5))
                    appointments.Add(CreateCustomAppointment(subjPrefix & "travel", item.ResID, 3, 6))
                    appointments.Add(CreateCustomAppointment(subjPrefix & "phone call", item.ResID, 0, 10))
                Next item
            End If
            Return appointments
        End Function
    End Class
End Namespace