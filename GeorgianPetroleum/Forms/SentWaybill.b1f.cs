using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;

namespace GeorgianPetroleum.Forms
{
    [FormAttribute("GeorgianPetroleum.Forms.SentWaybill", "Forms/SentWaybill.b1f")]
    class SentWaybill : UserFormBase
    {
        private string _waybillId;

        public SentWaybill(string waybillId)
        {
            _waybillId = waybillId;
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_0").Specific));
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

        private void Form_VisibleAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Title == "გაგზავნილი ზედნადები")
            {
                string query = $"SELECT U_W_NAME as [საქონლის დასახელება], U_UNIT_ID as [საზომი ერთეული], U_QUANTITY as [რაოდენობა], U_PRICE as [ერთეულის ფასი], U_AMOUNT as [ფასი]  FROM [@RSM_SWBI] WHERE U_WB_CODE = ${_waybillId}";
                Grid0.DataTable.ExecuteQuery(query);
            }
        }

        private void OnCustomInitialize()
        {

        }
    }
}
