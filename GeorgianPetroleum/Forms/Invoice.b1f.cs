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
using SAPbobsCOM;

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
            Button0 = ((Button)(GetItem("1").Specific));
            Button0.PressedBefore += new _IButtonEvents_PressedBeforeEventHandler(Button0_PressedBefore);
            OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            DataAddAfter += new DataAddAfterHandler(Form_DataAddAfter);

        }

        private Button Button0;



        private void OnCustomInitialize()
        {

        }

        private void Button0_PressedBefore(object sboObject, SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
      
            var blanketAgreementNumber = ((EditText)(Application.SBO_Application.Forms.ActiveForm.Items.Item("1980002192")
                .Specific)).Value;

            if (string.IsNullOrWhiteSpace(blanketAgreementNumber))
            {
                return;
            } 

            var postingDateString = ((EditText)(Application.SBO_Application.Forms.ActiveForm.Items.Item("10")
                .Specific)).Value;

            Recordset recSet =
                (Recordset) DiManager.Company.GetBusinessObject(BoObjectTypes
                    .BoRecordset);
            recSet.DoQuery(DiManager.QueryHanaTransalte($"select U_01, U_02 from OOAT WHERE Number = '{blanketAgreementNumber}'"));

            string subContractorCode  = recSet.Fields.Item("U_01").Value.ToString();
            string subContractorName = recSet.Fields.Item("U_02").Value.ToString();

            DateTime postingDate = DateTime.ParseExact(postingDateString, "yyyyMMdd", CultureInfo.InvariantCulture);

            DiManager.Recordset.DoQuery(DiManager.QueryHanaTransalte($"SELECT U_AVG_PRICE, U_PROFIT_MARGIN FROM [@RSM_PRCE] WHERE U_ABS_NUMBER = '{blanketAgreementNumber}' AND '{postingDate:s}' BETWEEN U_S_DATE AND U_E_DATE"));

            decimal avgPrice = 0;
            decimal profitMargin = 0;

            if (DiManager.Recordset.EoF)
            {
                Application.SBO_Application.SetStatusBarMessage("საშუალო ფასი ვერ მოიძებნა",
                    BoMessageTime.bmt_Short, true);
                return;
            }
            else
            {
                avgPrice = decimal.Parse(DiManager.Recordset.Fields.Item("U_AVG_PRICE").Value.ToString());
                profitMargin = decimal.Parse(DiManager.Recordset.Fields.Item("U_PROFIT_MARGIN").Value.ToString());
            }

            Matrix invoiceMatrix = (Matrix)Application.SBO_Application.Forms.ActiveForm.Items.Item("38").Specific;

            for (int i = 1; i < invoiceMatrix.RowCount; i++)
            {
                EditText NetPrice = (EditText)invoiceMatrix.Columns.Item("14").Cells.Item(i).Specific;
                EditText GrossPrice = (EditText)invoiceMatrix.Columns.Item("234000377").Cells.Item(i).Specific;
                EditText GrossPriceAfterDisc = (EditText)invoiceMatrix.Columns.Item("20").Cells.Item(i).Specific;
                EditText SubContractorCode = (EditText)invoiceMatrix.Columns.Item("U_ContractorCode").Cells.Item(i).Specific;
                EditText SubContractorName = (EditText)invoiceMatrix.Columns.Item("U_Qcontractor").Cells.Item(i).Specific;

                try
                {
                    SubContractorCode.Value = subContractorCode;
                    SubContractorName.Value = subContractorName;
                }
                catch (Exception)
                {

                    
                }

                try
                {
                    GrossPriceAfterDisc.Value = ((avgPrice + profitMargin) / 1000).ToString(CultureInfo.InvariantCulture);
                }
                catch (Exception e)
                {
                    NetPrice.Value = (Math.Round((avgPrice + profitMargin) / 1.18m)/1000).ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        public static Action refresWaybill;

        private void Form_DataAddAfter(ref BusinessObjectInfo pVal)
        {
            if (!pVal.ActionSuccess)
            {
                return;
            }

            var wbId = Application.SBO_Application.Forms.ActiveForm.DataSources.UserDataSources.Item("wbid")
                .Value;
 


            if (string.IsNullOrWhiteSpace(wbId))
            {
                return;
            }

            int docEntry = 0;
            try
            {
                string xmlObjectKey = pVal.ObjectKey;
                XElement xmlnew = XElement.Parse(xmlObjectKey);
                XElement xElementx = xmlnew.Element("DocEntry");
                if (xElementx != null)
                {
                    docEntry = int.Parse(xElementx.Value);
                }

                Recordset recSet =
                    (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes
                        .BoRecordset);
                recSet.DoQuery(DiManager.QueryHanaTransalte($"UPDATE [@RSM_WBAR] SET U_INVOICE_DOCENTRY = N'{docEntry}' WHERE U_ID = N'{wbId}'"));

                refresWaybill.Invoke();

            }
            catch (Exception e)
            {
                // ignored
            }



            Documents invoice = (Documents)DiManager.Company.GetBusinessObject(BoObjectTypes.oInvoices);
            if (!invoice.GetByKey(docEntry))
            {
                return;
            }

            if (invoice.CancelStatus == CancelStatusEnum.csCancellation)
            {
                return;
            }  

            Matrix invoiceMatrix = (Matrix)Application.SBO_Application.Forms.ActiveForm.Items.Item("38").Specific;
            var model = DiManager.RsClient.GetWaybillModelFromId(wbId);


            EditText netPrice =
                (EditText)invoiceMatrix.Columns.Item("14").Cells.Item(1).Specific;
            EditText grossPrice =
                (EditText)invoiceMatrix.Columns.Item("234000377").Cells.Item(1).Specific;
            EditText grossPriceAfterDisc = (EditText)invoiceMatrix.Columns.Item("20").Cells.Item(1).Specific;
            EditText quantity = (EditText)invoiceMatrix.Columns.Item("11").Cells.Item(1).Specific;
            EditText grossTotal;
            try 
            {
                 grossTotal = (EditText)invoiceMatrix.Columns.Item("284").Cells.Item(1).Specific;
            }
            catch (Exception e)
            {
                Application.SBO_Application.SetStatusBarMessage("Gross Total (Doc) - გამოაჩინეთ Form Settings -დან",
                    BoMessageTime.bmt_Short, true);
                return;
            }


            foreach (GOOD good in model.GOODS_LIST)
            {
                var currency = grossPriceAfterDisc.Value.Split(' ')[1];
                EditText postingDateString =
                    (EditText) Application.SBO_Application.Forms.ActiveForm.Items.Item("10").Specific;
                DateTime postingDate = DateTime.ParseExact(postingDateString.Value, "yyyyMMdd",
                    CultureInfo.InvariantCulture);
                var rate = DiManager.GetCurrencyRate(currency, postingDate, DiManager.Company);

                if (currency == "GEL")
                {
                    good.PRICE = grossPriceAfterDisc.Value.Split(' ')[0];
                    good.QUANTITY = quantity.Value.ToString(CultureInfo.InvariantCulture);
                    good.AMOUNT = (double.Parse(grossTotal.Value.Split(' ')[0], CultureInfo.InvariantCulture).ToString());
                }
                else
                {
                    good.PRICE =
                        (double.Parse(grossPriceAfterDisc.Value.Split(' ')[0], CultureInfo.InvariantCulture) * rate).ToString(CultureInfo.InvariantCulture);
                    good.AMOUNT = (double.Parse(grossTotal.Value.Split(' ')[0], CultureInfo.InvariantCulture) * rate).ToString(CultureInfo.InvariantCulture);
                    good.QUANTITY = quantity.Value.ToString(CultureInfo.InvariantCulture);
                }
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
                    var error = errorNode?.Element("TEXT")?.Value;
                    Application.SBO_Application.SetStatusBarMessage(error,
                        BoMessageTime.bmt_Short);
                }
            }
        }
    }
}
