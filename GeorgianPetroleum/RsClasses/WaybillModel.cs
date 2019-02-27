using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using SAPbobsCOM;

namespace GeorgianPetroleum.RsClasses
{
    [XmlRoot("WAYBILL")]
    public class WaybillModel
    {
        public WaybillModel()
        {
            GOODS_LIST = new List<GOOD>();
            WOOD_DOC_LIST = new List<WOODDOCUMENT>();

            ID = "";
            TYPE = "";
            BUYER_TIN = "";
            CHEK_BUYER_TIN = "";
            BUYER_NAME = "";
            START_ADDRESS = "";
            END_ADDRESS = "";
            DRIVER_TIN = "";
            CHEK_DRIVER_TIN = "";
            DRIVER_NAME = "";
            TRANSPORT_COAST = "";
            RECEPTION_INFO = "";
            RECEIVER_INFO = "";
            DELIVERY_DATE = "";
            STATUS = "";
            SELER_UN_ID = "";
            PAR_ID = "";
            FULL_AMOUNT = "";
            CAR_NUMBER = "";
            WAYBILL_NUMBER = "";
            S_USER_ID = "";
            BEGIN_DATE = "";
            TRAN_COST_PAYER = "";
            TRANS_ID = "";
            TRANS_TXT = "";
            COMMENT = "";
            CATEGORY = "";
            WOOD_LABELS = "";
            FULL_AMOUNT_TXT = "";
            SELLER_TIN = "";
            SELLER_NAME = "";
            TOTAL_QUANTITY = "";
            BUYER_S_USER_ID = "";
            IS_CONFIRMED = "";
            INVOICE_DOCENTRY = "";
            Name = "";
            IS_CORRECTED = "";
            CREATE_DATE = "";
            ACTIVATE_DATE = "";
            INVOICE_ID = "";
        }



        [XmlElement("GOODS_LIST")]
        public List<GOOD> GOODS_LIST { get; set; }
        [XmlElement("WOOD_DOC_LIST")]
        public List<WOODDOCUMENT> WOOD_DOC_LIST { get; set; }


        [XmlElement("SELLER_TIN")]
        public string SELLER_TIN { get; set; }

        public string INVOICE_DOCENTRY { get; set; }
        public string Name { get; set; }

        [XmlElement("IS_CONFIRMED")]
        public string IS_CONFIRMED { get; set; }
        [XmlElement("IS_CORRECTED")]
        public string IS_CORRECTED { get; set; }

        [XmlElement("SELLER_NAME")]
        public string SELLER_NAME { get; set; }

        [XmlElement("BUYER_S_USER_ID")]
        public string BUYER_S_USER_ID { get; set; }

        [XmlElement("TOTAL_QUANTITY")]
        public string TOTAL_QUANTITY { get; set; }

        [XmlElement("ID")]
        public string ID { get; set; } //WB_CODE
        [XmlElement("TYPE")]
        public string TYPE { get; set; }
        [XmlElement("BUYER_TIN")]
        public string BUYER_TIN { get; set; }
        [XmlElement("CHEK_BUYER_TIN")]
        public string CHEK_BUYER_TIN { get; set; }
        [XmlElement("BUYER_NAME")]
        public string BUYER_NAME { get; set; }
        [XmlElement("START_ADDRESS")]
        public string START_ADDRESS { get; set; }
        [XmlElement("END_ADDRESS")]
        public string END_ADDRESS { get; set; }
        [XmlElement("DRIVER_TIN")]
        public string DRIVER_TIN { get; set; }
        [XmlElement("CHEK_DRIVER_TIN")]
        public string CHEK_DRIVER_TIN { get; set; }
        [XmlElement("DRIVER_NAME")]
        public string DRIVER_NAME { get; set; }
        [XmlElement("TRANSPORT_COAST")]
        public string TRANSPORT_COAST { get; set; }
        [XmlElement("RECEPTION_INFO")]
        public string RECEPTION_INFO { get; set; }
        [XmlElement("RECEIVER_INFO")]
        public string RECEIVER_INFO { get; set; }
        [XmlElement("DELIVERY_DATE")]
        public string DELIVERY_DATE { get; set; }
        [XmlElement("STATUS")]
        public string STATUS { get; set; }
        [XmlElement("SELER_UN_ID")]
        public string SELER_UN_ID { get; set; }
        [XmlElement("ACTIVATE_DATE")]
        public string ACTIVATE_DATE { get; set; }
        [XmlElement("INVOICE_ID")]
        public string INVOICE_ID { get; set; }

        [XmlElement("PAR_ID")]
        public string PAR_ID { get; set; }
        [XmlElement("FULL_AMOUNT")]
        public string FULL_AMOUNT { get; set; }
        [XmlElement("FULL_AMOUNT_TXT")]
        public string FULL_AMOUNT_TXT { get; set; }
        [XmlElement("CAR_NUMBER")]
        public string CAR_NUMBER { get; set; }
        [XmlElement("WAYBILL_NUMBER")]
        public string WAYBILL_NUMBER { get; set; }
        [XmlElement("CLOSE_DATE")]
        public string CLOSE_DATE { get; set; }
        [XmlElement("S_USER_ID")]
        public string S_USER_ID { get; set; }
        [XmlElement("BEGIN_DATE")]
        public string BEGIN_DATE { get; set; }
        [XmlElement("TRAN_COST_PAYER")]
        public string TRAN_COST_PAYER { get; set; }
        [XmlElement("TRANS_ID")]
        public string TRANS_ID { get; set; }
        [XmlElement("TRANS_TXT")]
        public string TRANS_TXT { get; set; }
        [XmlElement("COMMENT")]
        public string COMMENT { get; set; }
        [XmlElement("CUST_STATUS")]
        public string CUST_STATUS { get; set; }
        [XmlElement("CUST_NAME")]
        public string CUST_NAME { get; set; }
        [XmlElement("CATEGORY")]
        public string CATEGORY { get; set; }
        [XmlElement("WOOD_LABELS")]
        public string WOOD_LABELS { get; set; }
        [XmlElement("CREATE_DATE")]
        public string CREATE_DATE { get; set; }

        public XElement ToXml()
        {
            XElement elem = new XElement("WAYBILL");
            elem.Add(new XElement("SUB_WAYBILLS", ""));

            XElement goodList = new XElement("GOODS_LIST");
            foreach (GOOD item in GOODS_LIST)
            {
                XElement good = new XElement("GOODS");
                good.Add(new XElement("ID", item.ID));
                good.Add(new XElement("W_NAME", item.W_NAME));
                good.Add(new XElement("UNIT_ID", item.UNIT_ID));
                good.Add(new XElement("UNIT_TXT", item.UNIT_TXT));
                good.Add(new XElement("QUANTITY", item.QUANTITY));
                good.Add(new XElement("PRICE", item.PRICE));
                good.Add(new XElement("STATUS", item.STATUS));
                good.Add(new XElement("AMOUNT", item.AMOUNT));
                good.Add(new XElement("BAR_CODE", item.BAR_CODE));
                good.Add(new XElement("A_ID", item.A_ID));
                good.Add(new XElement("VAT_TYPE", item.VAT_TYPE));
                good.Add(new XElement("QUANTITY_EXT", item.QUANTITY_EXT));
                goodList.Add(good);
            }
            elem.Add(goodList);

            XElement woodDocuments = new XElement("WOOD_DOC_LIST");
            foreach (WOODDOCUMENT item in WOOD_DOC_LIST)
            {
                XElement wood = new XElement("WOODDOCUMENT");
                wood.Add(new XElement("ID", item.ID));
                wood.Add(new XElement("DOC_N", item.DOC_N));
                wood.Add(new XElement("DOC_DATE", item.DOC_DATE));
                wood.Add(new XElement("DOC_DESC", item.DOC_DESC));
                wood.Add(new XElement("STATUS", item.STATUS));
                woodDocuments.Add(wood);

            }
            elem.Add(woodDocuments);

            elem.Add(new XElement("ID", ID));
            elem.Add(new XElement("TYPE", TYPE));
            elem.Add(new XElement("BUYER_TIN", BUYER_TIN));
            elem.Add(new XElement("CHEK_BUYER_TIN", CHEK_BUYER_TIN));
            elem.Add(new XElement("BUYER_NAME", BUYER_NAME));
            elem.Add(new XElement("START_ADDRESS", START_ADDRESS));
            elem.Add(new XElement("END_ADDRESS", END_ADDRESS));
            elem.Add(new XElement("DRIVER_TIN", DRIVER_TIN));
            elem.Add(new XElement("CHEK_DRIVER_TIN", CHEK_DRIVER_TIN));
            elem.Add(new XElement("DRIVER_NAME", DRIVER_NAME));
            elem.Add(new XElement("TRANSPORT_COAST", TRANSPORT_COAST));
            elem.Add(new XElement("RECEPTION_INFO", RECEPTION_INFO));
            elem.Add(new XElement("RECEIVER_INFO", RECEIVER_INFO));
            elem.Add(new XElement("DELIVERY_DATE", DELIVERY_DATE));
            elem.Add(new XElement("STATUS", STATUS));
            elem.Add(new XElement("SELER_UN_ID", SELER_UN_ID));
            elem.Add(new XElement("PAR_ID", PAR_ID));
            elem.Add(new XElement("FULL_AMOUNT", FULL_AMOUNT));
            elem.Add(new XElement("CAR_NUMBER", CAR_NUMBER));
            elem.Add(new XElement("WAYBILL_NUMBER", WAYBILL_NUMBER));
            elem.Add(new XElement("S_USER_ID", S_USER_ID));
            elem.Add(new XElement("BEGIN_DATE", BEGIN_DATE));
            elem.Add(new XElement("TRAN_COST_PAYER", TRAN_COST_PAYER));
            elem.Add(new XElement("TRANS_ID", TRANS_ID));
            elem.Add(new XElement("TRANS_TXT", TRANS_TXT));
            elem.Add(new XElement("COMMENT", COMMENT));
            elem.Add(new XElement("CATEGORY", CATEGORY));
            elem.Add(new XElement("WOOD_LABELS", WOOD_LABELS));
            elem.Add(new XElement("ACTIVATE_DATE", ACTIVATE_DATE));
            elem.Add(new XElement("CLOSE_DATE", CLOSE_DATE));
            elem.Add(new XElement("CUST_STATUS", CUST_STATUS));
            elem.Add(new XElement("CUST_NAME", CUST_NAME));
            elem.Add(new XElement("CREATE_DATE", CREATE_DATE));


            return elem;
        }

        public void InsertIntoDatabase()
        {

            DiManager.Company.StartTransaction();
            UserTable SWBITable = DiManager.Company.UserTables.Item("RSM_SWBI");

            foreach (var field in GOODS_LIST)
            {
                SWBITable.UserFields.Fields.Item("U_" + nameof(field.AMOUNT)).Value = field.AMOUNT ?? "";
                SWBITable.UserFields.Fields.Item("U_" + nameof(field.UNIT_TXT)).Value = field.UNIT_TXT ?? "";
                SWBITable.UserFields.Fields.Item("U_" + nameof(field.A_ID)).Value = field.A_ID ?? "";
                SWBITable.UserFields.Fields.Item("U_" + nameof(field.PRICE)).Value = field.PRICE ?? "";
                SWBITable.UserFields.Fields.Item("U_" + nameof(field.BAR_CODE)).Value = field.BAR_CODE ?? "";
                SWBITable.UserFields.Fields.Item("U_" + nameof(field.ID)).Value = field.ID ?? "";
                SWBITable.UserFields.Fields.Item("U_" + nameof(field.QUANTITY)).Value = field.QUANTITY ?? "";
                SWBITable.UserFields.Fields.Item("U_" + nameof(field.QUANTITY_EXT)).Value = field.QUANTITY_EXT ?? "";
                SWBITable.UserFields.Fields.Item("U_" + nameof(field.STATUS)).Value = field.STATUS ?? "";
                SWBITable.UserFields.Fields.Item("U_" + nameof(field.UNIT_ID)).Value = field.UNIT_ID ?? "";
                SWBITable.UserFields.Fields.Item("U_" + nameof(field.VAT_TYPE)).Value = field.VAT_TYPE ?? "";
                SWBITable.UserFields.Fields.Item("U_" + nameof(field.W_NAME)).Value = field.W_NAME ?? "";
                SWBITable.UserFields.Fields.Item("U_" + nameof(WAYBILL_NUMBER)).Value = WAYBILL_NUMBER ?? "";
                SWBITable.UserFields.Fields.Item("U_WB_CODE").Value = ID ?? "";
                int Ret = SWBITable.Add();
                if (Ret != 0)
                {
                    SAPbouiCOM.Framework.Application.SBO_Application.SetStatusBarMessage("Error : " + DiManager.Company.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Short);
                    return;
                }
            }

            UserTable WBARTable = DiManager.Company.UserTables.Item("RSM_WBAR");
            WBARTable.UserFields.Fields.Item("U_" + nameof(ACTIVATE_DATE)).Value = ACTIVATE_DATE ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(BEGIN_DATE)).Value = BEGIN_DATE ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(BUYER_NAME)).Value = BUYER_NAME ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(BUYER_TIN)).Value = BUYER_TIN ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(CAR_NUMBER)).Value = CAR_NUMBER ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(CATEGORY)).Value = CATEGORY ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(CHEK_BUYER_TIN)).Value = CHEK_BUYER_TIN ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(CHEK_DRIVER_TIN)).Value = CHEK_DRIVER_TIN ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(CLOSE_DATE)).Value = CLOSE_DATE ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(COMMENT)).Value = COMMENT ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(ID)).Value = ID ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(CREATE_DATE)).Value = CREATE_DATE ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(CUST_NAME)).Value = CUST_NAME ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(CUST_STATUS)).Value = CUST_STATUS ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(DELIVERY_DATE)).Value = DELIVERY_DATE ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(DRIVER_NAME)).Value = DRIVER_NAME ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(DRIVER_TIN)).Value = DRIVER_TIN ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(END_ADDRESS)).Value = END_ADDRESS ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(FULL_AMOUNT)).Value = FULL_AMOUNT ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(PAR_ID)).Value = PAR_ID ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(RECEIVER_INFO)).Value = RECEIVER_INFO ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(RECEPTION_INFO)).Value = RECEPTION_INFO ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(SELER_UN_ID)).Value = SELER_UN_ID ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(START_ADDRESS)).Value = START_ADDRESS ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(STATUS)).Value = STATUS ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(S_USER_ID)).Value = S_USER_ID ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(TRANSPORT_COAST)).Value = TRANSPORT_COAST ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(TRANS_ID)).Value = TRANS_ID ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(TRANS_TXT)).Value = TRANS_TXT ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(TRAN_COST_PAYER)).Value = TRAN_COST_PAYER ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(TYPE)).Value = TYPE ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(WAYBILL_NUMBER)).Value = WAYBILL_NUMBER ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(FULL_AMOUNT_TXT)).Value = FULL_AMOUNT_TXT ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(SELLER_TIN)).Value = SELLER_TIN ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(SELLER_NAME)).Value = SELLER_NAME ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(TOTAL_QUANTITY)).Value = TOTAL_QUANTITY ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(BUYER_S_USER_ID)).Value = BUYER_S_USER_ID ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(IS_CONFIRMED)).Value = IS_CONFIRMED ?? "";
            WBARTable.UserFields.Fields.Item("U_" + nameof(INVOICE_DOCENTRY)).Value = INVOICE_DOCENTRY ?? "";
            // WBARTable.UserFields.Fields.Item("U_" + nameof(IS_CORRECTED)).Value = IS_CORRECTED ?? "";
            WBARTable.Name = Name ?? "";


            int Ret1 = WBARTable.Add();
            if (Ret1 != 0 && Ret1 != -2035)
            {
                SAPbouiCOM.Framework.Application.SBO_Application.SetStatusBarMessage("Error : " + DiManager.Company.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Short);
                return;
            }

            DiManager.Company.EndTransaction(BoWfTransOpt.wf_Commit);
        }
    }

    [XmlRoot("GOODS")]
    public class GOOD
    {
        [XmlElement("ID")]
        public string ID { get; set; }
        [XmlElement("W_NAME")]
        public string W_NAME { get; set; }
        [XmlElement("UNIT_ID")]
        public string UNIT_ID { get; set; }
        [XmlElement("UNIT_TXT")]
        public string UNIT_TXT { get; set; }
        [XmlElement("QUANTITY")]
        public string QUANTITY { get; set; }
        [XmlElement("PRICE")]
        public string PRICE { get; set; }
        [XmlElement("STATUS")]
        public string STATUS { get; set; }
        [XmlElement("AMOUNT")]
        public string AMOUNT { get; set; }
        [XmlElement("BAR_CODE")]
        public string BAR_CODE { get; set; }
        [XmlElement("A_ID")]
        public string A_ID { get; set; }
        [XmlElement("VAT_TYPE")]
        public string VAT_TYPE { get; set; }
        [XmlElement("QUANTITY_EXT")]
        public string QUANTITY_EXT { get; set; }
    }

    public class WOODDOCUMENT
    {
        public string ID { get; set; }
        public string DOC_N { get; set; }
        public string DOC_DATE { get; set; }
        public string DOC_DESC { get; set; }
        public string STATUS { get; set; }
    }


}
