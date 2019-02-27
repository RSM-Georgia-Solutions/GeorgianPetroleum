using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using GeorgianPetroleum.RsClasses;
using SAPbouiCOM;
using SAPbouiCOM.Framework;
using Application = SAPbouiCOM.Framework.Application;

namespace GeorgianPetroleum.Forms
{
    [FormAttribute("GeorgianPetroleum.Forms.SentWaybills", "Forms/SentWaybills.b1f")]
    class SentWaybills : UserFormBase
    {
        public SentWaybills()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_0").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_1").Specific));
            this.Grid0.DoubleClickAfter += new SAPbouiCOM._IGridEvents_DoubleClickAfterEventHandler(this.Grid0_DoubleClickAfter);
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_2").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_3").Specific));
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_5").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_6").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.Button Button0;

        private void OnCustomInitialize()
        {
            Refresh();
        }

        private void Refresh()
        {
            string query = $"SELECT U_ID as [ზედნადების ID], U_WAYBILL_NUMBER as [ზედნადების ნომერი], U_TOTAL_QUANTITY as [რაოდენობა]," + $"U_FULL_AMOUNT as [ღირებულება],  U_BUYER_NAME as [მყიდველი], U_SELLER_NAME as [გამყიდველი], U_START_ADDRESS as [დაწყების ადგილი]," + $"U_END_ADDRESS as [დასტრულების ადგილი], U_DRIVER_TIN as [მძღოლის პ/ნ], U_DRIVER_NAME as [მძღოლის სახელი], U_CAR_NUMBER as [ა/მ ნომერი]   FROM [@RSM_WBAR]";
            Grid0.DataTable.ExecuteQuery(query);
        }

        private void Button0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            string startDate = EditText0.Value;
            string endDate = EditText1.Value;
            if (string.IsNullOrWhiteSpace(startDate) || string.IsNullOrWhiteSpace(endDate))
            {
                Application.SBO_Application.SetStatusBarMessage("მიუთითეთ ჩამოტვირტვის თარიღი",
                    BoMessageTime.bmt_Short, true);
            }
            List<WayBilsRequest> wayBilsRequest = DiManager.RsClient.GetRequest(startDate, endDate);
            foreach (var req in wayBilsRequest)
            {
                DiManager.RsClient.GetWaybills(req);
            }
            Refresh();
        }

        private SAPbouiCOM.Grid Grid0;
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.StaticText StaticText1;

        private void Grid0_DoubleClickAfter(object sboObject, SBOItemEventArg pVal)
        {
            if (pVal.ColUID != "ანგარიშ-ფაქტურის ნომერი")
            {
               var clickedWb = Grid0.DataTable.GetValue("ზედნადების ID", pVal.Row).ToString();
                var model = DiManager.RsClient.GetWaybillModelFromId(clickedWb);
                model.InsertOrUpdateIntoDatabase();
                SentWaybill waybill = new SentWaybill(model);
                waybill.Show();
            }
        }
    }
}
