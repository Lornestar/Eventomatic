using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Eventomatic.Admin
{
    public partial class Payout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Load_Page();
            }
        }

        protected void Load_Page()
        {
            Site sitetemp = new Site();
            long fbid = Convert.ToInt64(sitetemp.getfbid());
            DataSet dstemp = Eventomatic_DB.SPs.ViewListFBUserResources(fbid).GetDataSet();
            DataSet dseventsall = new DataSet();
            dseventsall.Tables.Add();


            if (dstemp.Tables[0].Rows.Count > 0)
            {
                dstemp.Tables[0].TableName = "Groups";
                dstemp.Tables[0].Columns.Add("Revenue", typeof(string));
                dstemp.Tables[0].Columns.Add("Paid_Out", typeof(string));
                dstemp.Tables[0].Columns.Add("Amount_Owing", typeof(string));

                foreach (DataRow rowtemp in dstemp.Tables[0].Rows)
                {
                    //Set Transactions grid
                    int Resource_Key = Convert.ToInt32(rowtemp["Resource_Key"]);
                    DataSet dsevents = Eventomatic_DB.SPs.ViewListAllEventsProfile(Resource_Key).GetDataSet();

                    if (!dsevents.Tables[0].Columns.Contains("Revenue"))
                    {                        
                        //dsevents.Tables[0].Columns.Add("Revenue", System.Type.GetType("System.String"));
                        //dsevents.Tables[0].Columns.Add("Paid_Out", System.Type.GetType("System.String"));
                        
                    }
                    dsevents.Tables[0].Columns.Add("Resource_Key", typeof(Int32));
                    dsevents.Tables[0].Columns.Add("Amount_Owing", System.Type.GetType("System.String"));

                    int counter = 0;
                    Hashtable hsCollectedTotal = sitetemp.GetRevenue_Hashtable_Empty();
                    Hashtable hsPaidTotal = sitetemp.GetRevenue_Hashtable_Empty();
                    Hashtable hsOwedTotal = sitetemp.GetRevenue_Hashtable_Empty();
                    foreach (DataRow rowevents in dsevents.Tables[0].Rows)
                    {
                        int event_key = Convert.ToInt32(rowevents["Event_Key"]);

                        Hashtable hsCollectedtemp = sitetemp.GetRevenue_Hashtable(event_key, 0);
                        Hashtable hsPaidtemp = sitetemp.GetRevenue_Hashtable(event_key, 1);
                        Hashtable hsOwedtemp = sitetemp.GetRevenue_Hashtable_Empty();
                        foreach (DictionaryEntry de in hsCollectedtemp)
                        {
                            hsOwedtemp[de.Key.ToString()] = Convert.ToDecimal(hsCollectedtemp[de.Key.ToString()]) - Convert.ToDecimal(hsPaidtemp[de.Key.ToString()]);
                            hsOwedTotal[de.Key.ToString()] = Convert.ToDecimal(hsOwedtemp[de.Key.ToString()]) + Convert.ToDecimal(hsOwedTotal[de.Key.ToString()]);
                            hsPaidTotal[de.Key.ToString()] = Convert.ToDecimal(hsPaidtemp[de.Key.ToString()]) + Convert.ToDecimal(hsPaidTotal[de.Key.ToString()]);
                            hsCollectedTotal[de.Key.ToString()] = Convert.ToDecimal(hsCollectedtemp[de.Key.ToString()]) + Convert.ToDecimal(hsCollectedTotal[de.Key.ToString()]);
                        }


                        //rowevents["Revenue"] = "tewst"; //"<a href='" + System.Configuration.ConfigurationSettings.AppSettings.Get("Root_URL").ToString() + "Attendee_List.aspx?Event_Key=" + event_key.ToString() + "&view=transaction' target='_top'>" + sitetemp.GetRevenue(hsCollectedtemp) + "</a>";
                        //rowevents["Paid_Out"] = sitetemp.GetRevenue(hsPaidtemp);
                        rowevents["Amount_Owing"] = sitetemp.GetRevenue(hsOwedtemp);
                        rowevents["Resource_Key"] = Resource_Key;

                        rowtemp["Revenue"] = "Total= " + sitetemp.GetRevenue(hsCollectedTotal);
                        rowtemp["Paid_Out"] = "Total= " + sitetemp.GetRevenue(hsPaidTotal);
                        rowtemp["Amount_Owing"] = "Total= " + sitetemp.GetRevenue(hsOwedTotal);                                                
                    }

                    dseventsall.Tables[0].Merge(dsevents.Tables[0]);
                    counter += 1;
                    int tempint = dseventsall.Tables[0].Rows.Count;
                }
                

                        
                
                dstemp.Tables.Add(dseventsall.Tables[0].Copy());
                dstemp.Tables[1].TableName = "Events";
                dstemp.Relations.Add("CurrentRelationship", dstemp.Tables["Groups"].Columns["Resource_Key"], dstemp.Tables["Events"].Columns["Resource_Key"]);
            }

            UltraWebGrid1.DataSource = dstemp.Tables[0];
            UltraWebGrid1.DataBind();
        }
    }    
}
