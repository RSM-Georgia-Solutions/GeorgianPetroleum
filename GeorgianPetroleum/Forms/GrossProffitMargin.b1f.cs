using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;

namespace GeorgianPetroleum.Forms
{
    [FormAttribute("GeorgianPetroleum.Forms.GrossProffitMargin", "Forms/GrossProffitMargin.b1f")]
    class GrossProffitMargin : UserFormBase
    {
        public GrossProffitMargin()
        {
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
        }

        private SAPbouiCOM.Grid Grid0;

        private void Refresh()
        {
            Grid0.DataTable.ExecuteQuery(DiManager.QueryHanaTransalte("SELECT U_S_DATE as [დაწყები თარიღი], U_E_DATE as [დასრულების თარიღი],   U_ABS_NUMBER as [ხელშეკრულების ნომერი], U_PROFIT_MARGIN as [მარჟა], U_AVG_PRICE as [საშუალო ფასი] FROM [@RSM_PRCE]"));
        }

        private void OnCustomInitialize()
        {
            Refresh();
        }
    }
}
