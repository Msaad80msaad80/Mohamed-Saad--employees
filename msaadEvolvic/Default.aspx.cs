using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace msaadEvolvic
{
    public partial class Default : System.Web.UI.Page
    {
        //Modal Class for initial List
        public class EmplsProjects
        {
            public int EmpID { get; set; }
            public int ProjectID { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
        }
        //Modal Class for final List
        public class Results
        {
            public int EmpID1 { get; set; }
            public int EmpID2 { get; set; }
            public int ProjectID { get; set; }
            public int TotalDays { get; set; }

        }
        //Function to Get days Count if interval found beteen dates, Taked 4 Dates and return int or 0
        public int daycount(DateTime datefrom1, DateTime dateto1, DateTime datefrom2, DateTime dateto2)
        {
            if (dateto1 >= datefrom2 && datefrom1 <= datefrom2)
            {
                return (dateto1 - datefrom2).Days + 1;
            }
            else if (datefrom1 <= dateto2 && dateto1 >= dateto2)
            {
                return (dateto2 - datefrom1).Days + 1;
            }
            else if (datefrom1 >= datefrom2 && dateto1 <= dateto2)
            {
                return (dateto1 - datefrom1).Days + 1;
            }
            else if (datefrom1 < datefrom2 && dateto1 > dateto2)
            {
                return (dateto2 - datefrom2).Days + 1;
            }
            else
            {
                return 0;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btncrt_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Server.MapPath("~/MyFolder/" + flupld.FileName);
                flupld.SaveAs(path);
                string text = System.IO.File.ReadAllText(path);//Upload text file sould  be .txt

                string[] records = text.Replace("\n", "").Split(Convert.ToChar(13));
                List<EmplsProjects> emplsprojects = new List<EmplsProjects>();// List textfile content
                List<Results> finalresult = new List<Results>();// list Final result content
                foreach (string record in records)
                {
                    if (record.ToString().Length > 16)//to avoid as soon as posible empty or wrong line format
                    {
                        string[] elemnts = record.Split(',');

                        DateTime dateto;
                        // to convert null to today
                        if (elemnts[3].ToString().Trim().ToUpper().Contains("NULL")) { dateto = DateTime.Today; } else { dateto = Convert.ToDateTime(elemnts[3]); }
                        emplsprojects.Add(new EmplsProjects() { EmpID = int.Parse(elemnts[0].Trim()), ProjectID = int.Parse(elemnts[1].Trim()), DateFrom = Convert.ToDateTime(elemnts[2].Trim()), DateTo = dateto });
                    }

                }


                var projectslist = emplsprojects.GroupBy(p => p.ProjectID)
                       .Select(grp => grp.First())
                       .ToList();//Initial List Distinct for projects

                var emplsslist = emplsprojects.GroupBy(p => p.EmpID)
                       .Select(emp => emp.First())
                       .ToList();//Initial List Distinct for employees

                // First loop for projects
                foreach (var projectitem in projectslist)
                {
                    int prjid = projectitem.ProjectID;

                    // Second loop (inner loop) for Employess
                    foreach (var empl in emplsslist)
                    {

                        int empid = empl.EmpID;
                        var cursorrecord = emplsprojects.Where(a => a.EmpID == empid && a.ProjectID == prjid).FirstOrDefault();//Initial  record to cursor rest of records

                        if (cursorrecord != null)
                        {
                            var projemplloop = emplsprojects.Where(a => a.EmpID != empid && a.ProjectID == prjid).ToList();

                            // loop for rest records
                            foreach (var itememp in projemplloop)
                            {

                                int checkinterval = daycount(cursorrecord.DateFrom, cursorrecord.DateTo, itememp.DateFrom, itememp.DateTo); //Implement interval fuction
                                var checkDoublicated = finalresult.Where(a => a.EmpID2 == empid && a.EmpID1 == itememp.EmpID && a.ProjectID == prjid).Count();// First check if paired Employees already added to final list
                                if (checkinterval > 0 && checkDoublicated == 0)
                                {
                                    finalresult.Add(new Results() { EmpID1 = empid, EmpID2 = itememp.EmpID, ProjectID = prjid, TotalDays = checkinterval }); // Add Paired record to final list

                                }

                            }
                        }

                    }

                }
                finalresult = finalresult.OrderByDescending(a => a.TotalDays).ToList(); // resort final list to sort descecnding by days
                rptpairs.DataSource = finalresult;// set datasource to repeater to final list
                rptpairs.DataBind();// bind repeater
                flupld.Dispose(); // Dispose text file
                lblmsg.Text = "Done Upload you can see Results";
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Kindy Retry and Confirm File Extension an Content Format";
            }
        }
    }
}