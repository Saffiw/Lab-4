using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//model references for EF
using Lab.Models;
using System.Web.ModelBinding;

namespace comp2007_lesson6_wed
{
    public partial class student : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if save wasn't clicked AND we have a StudentID in the url
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                GetStudent();
            }
        }

        protected void GetStudent()
        {
            //populate form with existing student record
            Int32 StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

            //connect to db via EF
            using (DefaultConnection db = new DefaultConnection())
            {
                //populate a student instance with the StudentID from the URL parameter
                Student s = (from objS in db.Students
                             where objS.StudentID == StudentID
                             select objS).FirstOrDefault();

                //map the student properties to the form controls if we found a match
                if (s != null)
                {
                    txtLastName.Text = s.LastName;
                    txtFirstMidName.Text = s.FirstMidName;
                    txtEnrollmentDate.Text = s.EnrollmentDate.ToString("yyyy-MM-dd");
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //use EF to connect to SQL Server
            using (DefaultConnection db = new DefaultConnection())
            {

                //use the Student model to save the new record
                Student s = new Student();
                Int32 StudentID = 0;

                //check the querystring for an id so we can determine add / update
                if (Request.QueryString["StudentID"] != null)
                {
                    //get the id from the url
                    StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

                    //get the current student from EF
                    s = (from objS in db.Students
                         where objS.StudentID == StudentID
                         select objS).FirstOrDefault();
                }

                s.LastName = txtLastName.Text;
                s.FirstMidName = txtFirstMidName.Text;
                s.EnrollmentDate = Convert.ToDateTime(txtEnrollmentDate.Text);

                //call add only if we have no student ID
                if (StudentID == 0)
                {
                    db.Students.Add(s);
                }

                //run the update or insert
                db.SaveChanges();

                //redirect to the updated students page
                Response.Redirect("students.aspx");
            }
        }
    }
}