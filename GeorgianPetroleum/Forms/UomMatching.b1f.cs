using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;

namespace GeorgianPetroleum.Forms
{
    [FormAttribute("GeorgianPetroleum.Forms.UomMatching", "Forms/UomMatching.b1f")]
    class UomMatching : UserFormBase
    {
        public UomMatching()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_0").Specific));
            this.Grid0.DoubleClickAfter += new SAPbouiCOM._IGridEvents_DoubleClickAfterEventHandler(this.Grid0_DoubleClickAfter);
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_1").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.ActivateAfter += new ActivateAfterHandler(this.Form_ActivateAfter);

        }

        private SAPbouiCOM.Grid Grid0;

        private void Refresh()
        {
            string query =$"select [@RSM_UOMS].U_UOM_RS as 'რს-ის საზომი ერთეული', [@RSM_UOMS].U_UOM_SAP as 'SAP-ის საზომი ერთეული' from [@RSM_UOMS]";
            Grid0.DataTable.ExecuteQuery(DiManager.QueryHanaTransalte(query));
        }

        private void OnCustomInitialize()
        {
            Grid0.Item.Enabled = false;
            Refresh();
        }

        private SAPbouiCOM.Button Button0;

        private void Grid0_DoubleClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (pVal.Row == -1) return;
            Grid0.Rows.SelectedRows.Clear();
            DiManager.Recordset.DoQuery(DiManager.QueryHanaTransalte($"SELECT * FROM OUOM  where UomCode  not in  (select distinct U_UOM_SAP from  [@RSM_UOMS] where U_UOM_SAP IS NOT NULL )"));
            var RsUomName = Grid0.DataTable.GetValue(0, pVal.Row).ToString();
            UomList form3 = new UomList(RsUomName);
            form3.Show();
            Grid0.Rows.SelectedRows.Add(pVal.Row);
        }

        private void Form_ActivateAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            Refresh();
        }
    }
}
