using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using GeorgianPetroleum.RS.GE;
using GeorgianPetroleum.RsClasses;
using SAPbouiCOM.Framework;
using SAPbouiCOM;
using Application = SAPbouiCOM.Framework.Application;

namespace GeorgianPetroleum
{
    public class RsClient
    {
        private WayBillsSoapClient _client;
        private string UserName { get; set; }
        private string Password { get; set; }
        private string ServiceUser { get; set; }
        private int ServiceUserId { get; set; }
        private string ServiceUserPassword { get; set; }
        private int UN_ID { get; set; }
        public RsClient(string username, string password, string serviceUSer, string serviceUserPassword)
        {
            _client = new WayBillsSoapClient();
            Password = password;
            UserName = username;
            ServiceUser = serviceUSer;
            ServiceUserPassword = serviceUserPassword;
            var authenticated = Login();
            if (!authenticated)
            {
                SAPbouiCOM.Framework.Application.SBO_Application.MessageBox(
                    "ვერ ხერხდება შემოსავლების სამსახურთან დაკავშირება");
            }
        }

        private bool Login()
        {
            int unId = 0;
            int serviceUserId = 0;
            var authenticated = false;
            try
            {
                authenticated = _client.chek_service_user(ServiceUser, ServiceUserPassword, out unId,
                    out serviceUserId);
            }
            catch (Exception)
            {
                Application.SBO_Application.SetStatusBarMessage("ვერ ხერხდება RS-თან დაკავშირება",
                    BoMessageTime.bmt_Short, true);
            }

            return authenticated;
        }

        public void GetWaybills(WayBilsRequest req)
        {
            XElement sentWaybillsXml = _client.get_waybills(ServiceUser, ServiceUserPassword, req.itypes, req.buyerTin, req.statuses,
                req.carNumber, req.beginDateS, req.beginDateE, req.beginDateS, req.beginDateE, req.driverTin,
                req.beginDateS, req.beginDateE, req.fullAmount, req.waybillNumber, req.beginDateS, req.beginDateE,
                req.sUserIds, req.comment);

            List<string> waybillIds = new List<string>();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(sentWaybillsXml.ToString());
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodeList = root.SelectNodes("*");
            foreach (XmlNode isbn in nodeList)
            {
                foreach (XmlNode item in isbn)
                {
                    if (item.Name == "ID")
                    {
                        string waybillNumber = item.InnerText;
                        waybillIds.Add(waybillNumber);
                        break;
                    }
                }
            }

            int increment = 0;

            ProgressBar progressBar = null;
            try
            {
                 progressBar = SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.CreateProgressBar("ზედნადებები იტვირთება", waybillIds.Count, true);
            }
            catch (Exception)
            {
                // ignored
            }

            foreach (var wbId in waybillIds)
            {
                var model = GetWaybillModelFromId(wbId);
                model.InsertOrUpdateIntoDatabase();
                try
                {
                    if (progressBar != null) progressBar.Value += 1;
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            progressBar?.Stop();
        }

       

        public  WaybillModel GetWaybillModelFromId(string wbId)
        {
            XElement sentWaybillsXml = GetWaybill(wbId);
            IEnumerable<XElement> elements = sentWaybillsXml.Elements();
            IEnumerable<XElement> goodsXml = elements.Elements("GOODS");

            WaybillModel model;
            XmlSerializer ser = new XmlSerializer(typeof(WaybillModel));
            using (TextReader reader = new StringReader(sentWaybillsXml.ToString()))
            {
                model = (WaybillModel)ser.Deserialize(reader);
                model.GOODS_LIST = new List<GOOD>();
            }
            foreach (var VARIABLE in goodsXml)
            {
                VARIABLE.ToString();
                GOOD good1;
                XmlSerializer ser1 = new XmlSerializer(typeof(GOOD));
                using (TextReader reader = new StringReader(VARIABLE.ToString()))
                {
                    good1 = (GOOD)ser1.Deserialize(reader);
                }
                model.GOODS_LIST.Add(good1);
            }

            return model;
        }

        public List<WayBilsRequest> GetRequest(string StartDate, string EndDate, string WbTypes = "")
        {
            DateTime now = DateTime.Now;
            DateTime s_dt;
            DateTime e_dt;
            try
            {
                s_dt = DateTime.ParseExact(StartDate, "yyyyMMdd", CultureInfo.InvariantCulture);
                e_dt = DateTime.ParseExact(EndDate, "yyyyMMdd", CultureInfo.InvariantCulture);
                e_dt.AddDays(1);
            }
            catch (Exception e)
            {
                s_dt = Convert.ToDateTime(StartDate).AddMinutes(-1);
                e_dt = Convert.ToDateTime(EndDate).AddMinutes(1);
            }



            TimeSpan myDateResult = e_dt - s_dt;


            List<WayBilsRequest> wayBilsRequests = new List<WayBilsRequest>();
            int extraDays = myDateResult.Days % 3;

            int divisor = myDateResult.Days / 3;

            if (divisor != 0)
            {

                for (int i = 0; i < divisor; i++)
                {
                    wayBilsRequests.Add(new WayBilsRequest(s_dt.AddDays(i * 3), s_dt.AddDays((i + 1) * 3), WbTypes));
                }


                wayBilsRequests.Add(new WayBilsRequest(s_dt.AddDays(divisor * 3), s_dt.AddDays(divisor * 3 + extraDays), WbTypes));
            }
            else
            {
                wayBilsRequests.Add(new WayBilsRequest(s_dt, e_dt, WbTypes));
            }



            // DateTime s_dt_Processing = DateTime.ParseExact(EditText9.Value, "yyyyMMdd", CultureInfo.InvariantCulture);
            //  DateTime e_dt_Processing = DateTime.ParseExact(EditText2.Value, "yyyyMMdd", CultureInfo.InvariantCulture);


            return wayBilsRequests;


        }

        internal XElement SaveWaybill(XElement modelToXml)
        {
            return _client.save_waybill(ServiceUser, ServiceUserPassword, modelToXml);
        }

        public XElement GetErrorCodes()
        {
            var errCodes = _client.get_error_codes(ServiceUser, ServiceUserPassword);
            return errCodes;
        }

        public XElement GetWaybill(string waybillId)
        {
           return  _client.get_waybill(ServiceUser, ServiceUserPassword, int.Parse(waybillId));
        }
    }
}
