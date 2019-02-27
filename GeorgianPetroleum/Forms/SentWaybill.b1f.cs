using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeorgianPetroleum.RsClasses;
using SAPbouiCOM.Framework;
using System.Xml.Linq;
using System.Xml.XPath;
using SAPbouiCOM;
using Application = SAPbouiCOM.Framework.Application;

namespace GeorgianPetroleum.Forms
{
    [FormAttribute("GeorgianPetroleum.Forms.SentWaybill", "Forms/SentWaybill.b1f")]
    class SentWaybill : UserFormBase
    {
        private WaybillModel _waybillModel;

        public SentWaybill(WaybillModel waybillModel)
        {
            _waybillModel = waybillModel;
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_0").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_32").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("Item_35").Specific));
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("Item_37").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("Item_17").Specific));
            this.EditText5 = ((SAPbouiCOM.EditText)(this.GetItem("Item_19").Specific));
            this.EditText6 = ((SAPbouiCOM.EditText)(this.GetItem("Item_21").Specific));
            this.EditText7 = ((SAPbouiCOM.EditText)(this.GetItem("Item_23").Specific));
            this.EditText8 = ((SAPbouiCOM.EditText)(this.GetItem("Item_24").Specific));
            this.ComboBox0 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_27").Specific));
            this.ComboBox1 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_4").Specific));
            this.ComboBox2 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_15").Specific));
            this.EditText9 = ((SAPbouiCOM.EditText)(this.GetItem("Item_9").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_11").Specific));
            this.EditText10 = ((SAPbouiCOM.EditText)(this.GetItem("Item_13").Specific));
            this.EditText11 = ((SAPbouiCOM.EditText)(this.GetItem("Item_6").Specific));
            this.EditText12 = ((SAPbouiCOM.EditText)(this.GetItem("Item_8").Specific));
            this.EditText13 = ((SAPbouiCOM.EditText)(this.GetItem("Item_25").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_29").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.EditText14 = ((SAPbouiCOM.EditText)(this.GetItem("Item_33").Specific));

            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_39").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            VisibleAfter += new VisibleAfterHandler(Form_VisibleAfter);
        }

        private SAPbouiCOM.Grid Grid0;

        private void FillFormFromModel()
        {
            string query = $"SELECT U_W_NAME as [საქონლის დასახელება], U_UNIT_ID as [საზომი ერთეული], U_QUANTITY as [რაოდენობა], U_PRICE as [ერთეულის ფასი], U_AMOUNT as [ფასი]  FROM [@RSM_SWBI] WHERE U_WB_CODE = ${_waybillModel.ID}";
            Grid0.DataTable.ExecuteQuery(query);


            if (!string.IsNullOrWhiteSpace(_waybillModel.TRAN_COST_PAYER))
            {
                ComboBox0.Select(_waybillModel.TRAN_COST_PAYER);
            }
            ComboBox1.Select(_waybillModel.TYPE);
            if (!string.IsNullOrWhiteSpace(_waybillModel.TRANS_ID))
            {
                ComboBox2.Select(_waybillModel.TRANS_ID);
            }

            EditText1.Value = _waybillModel.BUYER_TIN;
            EditText2.Value = _waybillModel.BUYER_NAME;
            EditText3.Value = _waybillModel.END_ADDRESS;
            EditText4.Value = _waybillModel.DRIVER_TIN;
            EditText5.Value = _waybillModel.DRIVER_NAME;
            EditText6.Value = _waybillModel.CAR_NUMBER;
            EditText7.Value = _waybillModel.TRANS_TXT;
            EditText8.Value = _waybillModel.TRANSPORT_COAST;
            EditText9.Value = _waybillModel.SELLER_TIN;
            EditText0.Value = _waybillModel.SELLER_NAME;
            EditText10.Value = _waybillModel.START_ADDRESS;
            EditText11.Value = _waybillModel.WAYBILL_NUMBER;
            EditText14.Value = _waybillModel.COMMENT;
            DateTime dt = DateTime.Parse(_waybillModel.ACTIVATE_DATE);
            EditText12.Value = dt.ToString("g");

            EditText1.Item.Enabled = false;
            EditText5.Item.Enabled = false;
            EditText9.Item.Enabled = false;
            EditText0.Item.Enabled = false;
            EditText11.Item.Enabled = false;
            ComboBox1.Item.Enabled = false;
            EditText13.Active = true;
            EditText12.Item.Enabled = false;
        }

        private void FillModelFromForm()
        {
            _waybillModel.BUYER_TIN = EditText1.Value;
            _waybillModel.BUYER_NAME = EditText2.Value;
            _waybillModel.END_ADDRESS = EditText3.Value;
            _waybillModel.DRIVER_TIN = EditText4.Value;
            _waybillModel.DRIVER_NAME = EditText5.Value;
            _waybillModel.CAR_NUMBER = EditText6.Value;
            _waybillModel.TRANS_TXT = EditText7.Value;
            _waybillModel.TRANSPORT_COAST = EditText7.Value;
            _waybillModel.SELLER_TIN = EditText9.Value;
            _waybillModel.SELLER_NAME = EditText0.Value;
            _waybillModel.START_ADDRESS = EditText10.Value;
            _waybillModel.WAYBILL_NUMBER = EditText11.Value;
            _waybillModel.COMMENT = EditText14.Value;
            _waybillModel.TRANS_ID = ComboBox2.Value;
            DateTime dt = DateTime.Parse(_waybillModel.ACTIVATE_DATE);
            _waybillModel.ACTIVATE_DATE = dt.ToString("s");

            _waybillModel.TRAN_COST_PAYER = ComboBox0.Selected.Value;

            _waybillModel.TRANS_ID = ComboBox2.Selected.Value;

            for (int i = 0; i < Grid0.DataTable.Rows.Count; i++)
            {
                GOOD good = _waybillModel.GOODS_LIST.FirstOrDefault(g => g.W_NAME == (string)Grid0.DataTable.GetValue("საქონლის დასახელება", i));
                good.PRICE = Grid0.DataTable.GetValue("ერთეულის ფასი", i).ToString();
                good.AMOUNT = Grid0.DataTable.GetValue("ფასი", i).ToString();
                good.QUANTITY = Grid0.DataTable.GetValue("რაოდენობა", i).ToString();
                //good.QUANTITY_EXT = Grid0.DataTable.GetValue("საქონლის დასახელება", i).ToString();

            }



            var modelToXml = _waybillModel.ToXml();
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

        private void Form_VisibleAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Title == "გაგზავნილი ზედნადები")
            {
                FillFormFromModel();
            }
        }

        private void OnCustomInitialize()
        {
            ComboBox1.ValidValues.Add("1", "შიდა გადაზიდვა");
            ComboBox1.ValidValues.Add("2", "ტრანსპორტირებით");
            ComboBox1.ValidValues.Add("3", "ტრანსპორტირების გარეშე");
            ComboBox1.ValidValues.Add("4", "დისტრიბუცია");
            ComboBox1.ValidValues.Add("5", "უკან დაბრუნება");
            ComboBox1.ValidValues.Add("6", "ქვეზედნადები");

            ComboBox0.ValidValues.Add("1", "მყიდველი");
            ComboBox0.ValidValues.Add("2", "გამყიდველი");

            ComboBox2.ValidValues.Add("1", "საავტომობილო");
            ComboBox2.ValidValues.Add("2", "სარკინიგზო");
            ComboBox2.ValidValues.Add("3", "საავიაციო");
            ComboBox2.ValidValues.Add("4", "სხვა");
            ComboBox2.ValidValues.Add("6", "საავტომობილო - უცხო ქვეყნის");
            ComboBox2.ValidValues.Add("7", "გადამზიდავი - საავტომობილო");

            GetItem("Item_4").DisplayDesc = true;
            GetItem("Item_27").DisplayDesc = true;
            GetItem("Item_15").DisplayDesc = true;
        }

        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.EditText EditText3;
        private SAPbouiCOM.EditText EditText4;
        private SAPbouiCOM.EditText EditText5;
        private SAPbouiCOM.EditText EditText6;
        private SAPbouiCOM.EditText EditText7;
        private SAPbouiCOM.EditText EditText8;
        private SAPbouiCOM.ComboBox ComboBox0;
        private SAPbouiCOM.ComboBox ComboBox1;
        private SAPbouiCOM.ComboBox ComboBox2;
        private SAPbouiCOM.EditText EditText9;
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.EditText EditText10;
        private SAPbouiCOM.EditText EditText11;
        private SAPbouiCOM.EditText EditText12;
        private SAPbouiCOM.EditText EditText13;
        private SAPbouiCOM.Button Button0;



        private void Button0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            FillModelFromForm();
        }

        private SAPbouiCOM.EditText EditText14;
        private SAPbouiCOM.StaticText StaticText0;


    }
}
