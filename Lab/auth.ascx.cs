﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab
{
    public partial class auth : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["InstructorID"] == null)
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
}