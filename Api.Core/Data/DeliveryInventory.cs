using Api.Core.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Api.Core.Data
{
    public class DeliveryInventory
    {
        public DataSet GetDeliveryEmsData(DateTime fromdate, DateTime todate)
        {
            DataSet ds = new DataSet();
            string fdate = fromdate.ToString("yyyy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
            string tdate = todate.ToString("yyyy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
            string rdate = fromdate.ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
            string pdate = todate.ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
            if (((TimeSpan)(todate - fromdate)).Days < 2)
            {
                if (EMSCONNECTCENTER.VnpeConnection_CENTER.State != ConnectionState.Open)
                {
                    EMSCONNECTCENTER.VnpeConnection_CENTER.ConnectionString = EMSCONNECTCENTER.CONNECTION_STRING_CENTER;
                    EMSCONNECTCENTER.VnpeConnection_CENTER.Open();
                }

                {
                    string sql = "Select MaE1 as ItemCode,tuiso AS ToPOSCode,ems.convertnumber2date(NGAYNHAN,gionhan) as DELIVERYDATE,CASE WHEN max(MaBcTra)>0 THEN 1 ELSE 0 END AS ISDELIVERABLE,max(LDCHUAPHAT) AS CAUSECODE,max(HDTIEPTHEO) AS SOLUTIONCODE,max(NGNHAN) AS REALRECIVERNAME,max(GHI_CHU) AS DELIVERYNOTE,max(sendtime) as lastupdateems FROM E1NH_2015 Where NgayNhan between '" + rdate + "' and '" + pdate + "' and bccp not in(2) and createtime between  to_date('" + fdate + "','yyyy/MM/dd hh24:mi:ss') and to_date('" + tdate + "','yyyy/MM/dd hh24:mi:ss') And ABS(mabc_kt) in (select deliveryroute from bccp_emsdeliveryroute group by deliveryroute UNION select deliverypostcode from bccp_emsdeliveryroute group by deliverypostcode)   Group By MaE1,MaBcTra,NgayNhan,GioNhan,tuiso";
                    OracleCommand myCommand = new OracleCommand(sql, EMSCONNECTCENTER.VnpeConnection_CENTER);
                    myCommand.CommandType = CommandType.Text;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    mAdapter = new OracleDataAdapter(myCommand);

                    mAdapter.Fill(ds);
                }
            }
            return ds;
        }

        public string InsertDeliveryInventoryList(DataSet ds)
        {
            int json = 0;
            string Itemcode = "";
            int Topostcode = 0;

            {
                if (EMSCONNECTCENTER.VnpeConnection_CENTER.State != ConnectionState.Open)
                {
                    EMSCONNECTCENTER.VnpeConnection_CENTER.ConnectionString = EMSCONNECTCENTER.CONNECTION_STRING_CENTER;
                    EMSCONNECTCENTER.VnpeConnection_CENTER.Open();
                }

                try
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Itemcode = row.ItemArray[0].ToString();
                        Topostcode = Convert.ToInt32(row.ItemArray[1].ToString());
                        //string sql = "Insert Into DeliveryLost values('" + Itemcode + "','" + Topostcode + "',SYSDATE,0,0,0,'','')";

                        string sql = "Insert Into bccp_retranferdelivery values('" + Itemcode + "')";

                        OracleCommand myCommand = new OracleCommand(sql, EMSCONNECTCENTER.VnpeConnection_CENTER);
                        myCommand.CommandType = CommandType.Text;
                        myCommand.CommandTimeout = 20000;
                        myCommand.ExecuteNonQuery();


                        string sql1 = "Insert Into deliverylost values('" + Itemcode + "','" + Topostcode + "',sysdate,'','','','','')";
                        OracleCommand myCommand1 = new OracleCommand(sql1, EMSCONNECTCENTER.VnpeConnection_CENTER);
                        myCommand1.CommandType = CommandType.Text;
                        myCommand1.CommandTimeout = 20000;
                        myCommand1.ExecuteNonQuery();


                    }

                }
                catch (System.Exception e)
                {
                    ds = null;
                }

                return "";
            }
        }
    }
}