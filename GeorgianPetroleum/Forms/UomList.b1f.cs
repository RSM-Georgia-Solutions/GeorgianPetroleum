using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;

namespace GeorgianPetroleum.Forms
{
    [FormAttribute("GeorgianPetroleum.Forms.UomList", "Forms/UomList.b1f")]
    class UomList : UserFormBase
    {
        private string _rsUomName;
        private string _sapUomCode;
        private bool _isOther;


        public UomList(string rsUomName)
        {
            _rsUomName = rsUomName;
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_0").Specific));
            this.Grid0.ClickAfter += new SAPbouiCOM._IGridEvents_ClickAfterEventHandler(this.Grid0_ClickAfter);
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_1").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.Grid Grid0;

        private void OnCustomInitialize()
        {
            Grid0.Item.Enabled = false;
            string query = DiManager.QueryHanaTransalte("select OUOM.UomCode as [საზომი ერთეულის კოდი], OUOM.UomName as [საზომი ერთეულის დასახელება] from OUOM ");
            //WHERE OUOM.UomCode NOT IN  (select U_UOM_SAP from[@RSM_UOMS] where U_UOM_SAP is not null)
            Grid0.DataTable.ExecuteQuery(query);
        }

        private SAPbouiCOM.Button Button0;

        private void Grid0_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            _sapUomCode = string.Empty;
            Grid0.Rows.SelectedRows.Clear();
            if (pVal.Row == -1)
            {
                return;
            }
            Grid0.Rows.SelectedRows.Add(pVal.Row);
            var sapUomCode = Grid0.DataTable.GetValue(0, pVal.Row).ToString();
            _sapUomCode = sapUomCode;
            if (_rsUomName == "სხვა")
            {
                _isOther = true;
            }
        }

        private void Button0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (_isOther)
            {
                string query = $"INSERT INTO [@RSM_UOMS] (U_UOM_RS, U_UOM_SAP, U_ID) VALUES (N'{_rsUomName}', N'{_sapUomCode}', '99')";
                DiManager.Recordset.DoQuery(DiManager.QueryHanaTransalte(query));
            }
            else
            {
                string query = $"update [@RSM_UOMS] set U_UOM_SAP  = N'{_sapUomCode}' where  U_UOM_RS  = N'{_rsUomName}'";
                DiManager.Recordset.DoQuery(DiManager.QueryHanaTransalte(query));
            }

            SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Close();
            
        }
    }
}
