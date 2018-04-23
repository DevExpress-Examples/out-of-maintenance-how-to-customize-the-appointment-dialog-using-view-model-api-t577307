using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.Web.ASPxScheduler.Dialogs;
using DevExpress.XtraScheduler;

namespace WebApplication1 {
    public partial class Default : System.Web.UI.Page {
        protected void Page_Init(object sender, EventArgs e) {
            ASPxScheduler1.OptionsForms.DialogLayoutSettings.AppointmentDialog.ViewModel.PrepareControl((ASPxCheckBox cb) => {
                cb.ToggleSwitchDisplayMode = ToggleSwitchDisplayMode.Never;
            });

            ASPxScheduler1.OptionsForms.DialogLayoutSettings.AppointmentDialog.ViewModel.PrepareControlFor(m => m.StartTime, (ASPxDateEdit de) => {
                de.EditFormat = EditFormat.Custom;
                de.EditFormatString = "dd - MM - yyyy";
            });

            ASPxScheduler1.OptionsForms.DialogLayoutSettings.AppointmentDialog.ViewModel.PrepareControlFor(m => m.EndTime, (ASPxDateEdit de) => {
                de.EditFormat = EditFormat.Custom;
                de.EditFormatString = "dd - MM - yyyy";
            });

            ASPxScheduler1.OptionsForms.DialogLayoutSettings.AppointmentDialog.ViewModel.PrepareControlFor(m => m.ResourceIds, (ASPxEdit de) => {
                de.Caption = "Employee";
            });

            ASPxScheduler1.OptionsForms.DialogLayoutSettings.AppointmentDialog.ViewModel.PrepareControlFor(m => m.Subject, (ASPxTextBox de) => {
                de.CaptionStyle.ForeColor = Color.Red;
                de.CaptionStyle.Font.Bold = true;
            });

            ASPxScheduler1.OptionsForms.DialogLayoutSettings.AppointmentDialog.ViewModel.PrepareControlFor(m => m.Description, (ASPxMemo me) => {
                me.ForeColor = Color.Blue;
                me.Font.Bold = true;
                me.Font.Italic = true;
            });

            ASPxScheduler1.OptionsForms.DialogLayoutSettings.AppointmentDialog.ViewModel.SetItemVisibilityCondition("Location", false);
            ASPxScheduler1.OptionsForms.DialogLayoutSettings.AppointmentDialog.ViewModel.SetItemVisibilityCondition(vm => vm.IsAllDay, false);


        }


        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void ObjectDataSourceResources_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
            if(Session["CustomResourceDataSource"] == null) {
                Session["CustomResourceDataSource"] = new CustomResourceDataSource(GetCustomResources());
            }
            e.ObjectInstance = Session["CustomResourceDataSource"];
        }

        BindingList<CustomResource> GetCustomResources() {
            BindingList<CustomResource> resources = new BindingList<CustomResource>();
            resources.Add(CreateCustomResource(1, "Max Fowler"));
            resources.Add(CreateCustomResource(2, "Nancy Drewmore"));
            resources.Add(CreateCustomResource(3, "Pak Jang"));
            return resources;
        }

        private CustomResource CreateCustomResource(int res_id, string caption) {
            CustomResource cr = new CustomResource();
            cr.ResID = res_id;
            cr.Name = caption;
            return cr;
        }

        public Random RandomInstance = new Random();
        private CustomAppointment CreateCustomAppointment(string subject, object resourceId, int status, int label) {
            CustomAppointment apt = new CustomAppointment();
            apt.Subject = subject;
            apt.OwnerId = resourceId;
            Random rnd = RandomInstance;
            int rangeInMinutes = 60 * 24;
            apt.StartTime = DateTime.Today + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes));
            apt.EndTime = apt.StartTime + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes / 4));
            apt.Status = status;
            apt.Label = label;
            apt.Description = "Some detailed information about " + apt.Subject;
            return apt;
        }

        protected void ObjectDataSourceAppointment_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
            if(Session["CustomAppointmentDataSource"] == null) {
                Session["CustomAppointmentDataSource"] = new CustomAppointmentDataSource(GetCustomAppointments());
            }
            e.ObjectInstance = Session["CustomAppointmentDataSource"];
        }

        BindingList<CustomAppointment> GetCustomAppointments() {
            BindingList<CustomAppointment> appointments = new BindingList<CustomAppointment>(); ;
            CustomResourceDataSource resources = Session["CustomResourceDataSource"] as CustomResourceDataSource;
            if(resources != null) {
                foreach(CustomResource item in resources.Resources) {
                    string subjPrefix = item.Name + "'s ";
                    appointments.Add(CreateCustomAppointment(subjPrefix + "meeting", item.ResID, 2, 5));
                    appointments.Add(CreateCustomAppointment(subjPrefix + "travel", item.ResID, 3, 6));
                    appointments.Add(CreateCustomAppointment(subjPrefix + "phone call", item.ResID, 0, 10));
                }
            }
            return appointments;
        }
    } 
}