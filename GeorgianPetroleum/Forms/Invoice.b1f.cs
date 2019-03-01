using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using SAPbouiCOM;
using SAPbouiCOM.Framework;
using Application = SAPbouiCOM.Framework.Application;
using System.Xml.Linq;
using System.Xml.XPath;
using GeorgianPetroleum.RsClasses;

namespace GeorgianPetroleum.Forms
{
    [FormAttribute("133", "Forms/Invoice.b1f")]
    class Invoice : SystemFormBase
    {
        public Invoice()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.Button0.PressedBefore += new SAPbouiCOM._IButtonEvents_PressedBeforeEventHandler(this.Button0_PressedBefore);
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.DataAddAfter += new DataAddAfterHandler(this.Form_DataAddAfter);

        }

        private SAPbouiCOM.Button Button0;

     

        private void OnCustomInitialize()
        {

        }

        private void Button0_PressedBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            var blanketAgreementNumber = ((EditText)(SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Items.Item("1980002192")
                .Specific)).Value;
        
            var postingDateString = ((EditText)(SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Items.Item("10")
                .Specific)).Value;

            DateTime postingDate = DateTime.ParseExact(postingDateString, "yyyyMMdd", CultureInfo.InvariantCulture);

            DiManager.Recordset.DoQuery(DiManager.QueryHanaTransalte($"SELECT U_AVG_PRICE, U_PROFIT_MARGIN FROM [@RSM_PRCE] WHERE U_ABS_NUMBER = '{blanketAgreementNumber}' AND '{postingDate:s}' BETWEEN U_S_DATE AND U_E_DATE"));

            decimal avgPrice = 0;
            decimal profitMargin = 0;

            if (DiManager.Recordset.EoF)
            {
                Application.SBO_Application.SetStatusBarMessage("საშუალო ფასი ვერ მოიძებნა",
                    BoMessageTime.bmt_Short, true);
            }
            else
            {
                avgPrice = decimal.Parse(DiManager.Recordset.Fields.Item("U_AVG_PRICE").Value.ToString());
                profitMargin = decimal.Parse(DiManager.Recordset.Fields.Item("U_PROFIT_MARGIN").Value.ToString());
            }

            Matrix invoiceMatrix = (Matrix)SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Items.Item("38").Specific;

            for (int i = 1; i < invoiceMatrix.RowCount; i++)
            {
                SAPbouiCOM.EditText NetPrice = (SAPbouiCOM.EditText)invoiceMatrix.Columns.Item("14").Cells.Item(i).Specific;
                SAPbouiCOM.EditText GrossPrice = (SAPbouiCOM.EditText)invoiceMatrix.Columns.Item("234000377").Cells.Item(i).Specific;
                try
                {
                    NetPrice.Value = (avgPrice + profitMargin).ToString(CultureInfo.InvariantCulture);
                }
                catch (Exception e)
                {
                    GrossPrice.Value = (avgPrice + profitMargin).ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        private void Form_DataAddAfter(ref BusinessObjectInfo pVal)
        {
            var wbId = SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.DataSources.UserDataSources.Item("wbid")
                .Value;

            Matrix invoiceMatrix = (Matrix)SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Items.Item("38").Specific;
            var model = DiManager.RsClient.GetWaybillModelFromId(wbId);

         
                SAPbouiCOM.EditText NetPrice =
                    (SAPbouiCOM.EditText) invoiceMatrix.Columns.Item("14").Cells.Item(1).Specific;
                SAPbouiCOM.EditText GrossPrice =
                    (SAPbouiCOM.EditText) invoiceMatrix.Columns.Item("234000377").Cells.Item(1).Specific;
          

            foreach (GOOD good in model.GOODS_LIST)
            {
                good.PRICE = NetPrice.Value;
            }

            var modelToXml = model.ToXml();
            XElement res = DiManager.RsClient.SaveWaybill(modelToXml);
            XElement xElement = res.Element("STATUS");
            if (xElement != null)
            {
                string result = xElement.Value;
                if (result != "0")
                {
                    var errors = DiManager.RsClient.GetErrorCodes();
                    var errorNode = errors.XPathSelectElement($"./ERROR_CODE[ID = {result}]");
                    var error = errorNode.Element("TEXT").Value;
                    Application.SBO_Application.SetStatusBarMessage(error,
                        BoMessageTime.bmt_Short, true);
                }
            }
        }
    }
}
