using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM;
using SAPbouiCOM.Framework;
using Application = SAPbouiCOM.Framework.Application;

namespace GeorgianPetroleum.Forms
{
    [FormAttribute("GeorgianPetroleum.Forms.ItemsList", "Forms/ItemsList.b1f")]
    class ItemsList : UserFormBase
    {
        private List<string> _RscodesList;
        private readonly string _cardCode;
        private readonly bool _insert;

        public ItemsList(List<string> rscodesList, string cardCode, bool insert)
        {
            _RscodesList = rscodesList;
            _cardCode = cardCode;
            _insert = insert;
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_0").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_1").Specific));
            this.EditText0.KeyDownAfter += new SAPbouiCOM._IEditTextEvents_KeyDownAfterEventHandler(this.EditText0_KeyDownAfter);
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_2").Specific));
            this.Grid0.ClickAfter += new SAPbouiCOM._IGridEvents_ClickAfterEventHandler(this.Grid0_ClickAfter);
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_3").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("Item_4").Specific));
            this.Button1.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button1_PressedAfter);
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.VisibleAfter += new VisibleAfterHandler(this.Form_VisibleAfter);

        }

        private SAPbouiCOM.StaticText StaticText0;

        private void OnCustomInitialize()
        {
            Grid0.Item.Enabled = false;
        }

        private void Refresh()
        {
            string query = $"Select ItemCode as [საქონლის კოდი], ItemName as [საქონლის დასახელბა] from OITM WHERE ItemType != 'F'";
            Grid0.DataTable.ExecuteQuery(DiManager.QueryHanaTransalte(query));
        }

        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.Grid Grid0;
        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.Button Button1;

        private void Grid0_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (pVal.Row == -1)
            {
                return;
            }
            Grid0.Rows.SelectedRows.Clear();
            Grid0.Rows.SelectedRows.Add(pVal.Row);
        }

        private void EditText0_KeyDownAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            string query =
                $"Select ItemCode as [საქონლის კოდი], ItemName as [საქონლის დასახელბა] from OITM WHERE ItemType != 'F' AND (ItemCode LIKE N'%" +
                EditText0.Value + "%' OR  ItemName LIKE N'%" + EditText0.Value + "%')";
            Grid0.DataTable.ExecuteQuery(DiManager.QueryHanaTransalte(query));
        }

        private static void OpenItemMasterData(string ItemName)
        {
            Application.SBO_Application.ActivateMenuItem("3073");
            SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Mode = BoFormMode.fm_ADD_MODE;
            ((EditText)SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Items.Item("7").Specific).Value = ItemName;
        }

        private void Button0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            OpenItemMasterData(_RscodesList[0]);
        }

        private void Form_VisibleAfter(SBOItemEventArg pVal)
        {
            if (SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Title == "საქონლის სია")
            {
                Refresh();
            }
        }

        private void Button1_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            int x = Grid0.Rows.SelectedRows.Item(0, BoOrderType.ot_RowOrder);
            string sapItemCode = Grid0.DataTable.GetValue(0, x).ToString();

            if (_insert)
            {
                foreach (var rsCode in _RscodesList)
                {
                    string query = $"insert into  [dbo].[@RSM_MTCH] (U_BP_ID, U_RS_ITEM_ID, U_SAP_ITEM_ID) values (N'{_cardCode}', N'{rsCode}', N'{sapItemCode}')";
                    DiManager.Recordset.DoQuery(DiManager.QueryHanaTransalte(query));
                }
            }
            else
            {
                foreach (var rsCode in _RscodesList)
                {
                    string query = $"UPDATE  [dbo].[@RSM_MTCH] SET U_BP_ID = N'{_cardCode}', U_RS_ITEM_ID = N'{rsCode}', U_SAP_ITEM_ID = N'{sapItemCode}' WHERE U_RS_ITEM_ID = N'{rsCode}' AND U_BP_ID = N'{_cardCode}'";
                    DiManager.Recordset.DoQuery(DiManager.QueryHanaTransalte(query));
                }
            }

            SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Close();
        }
    }
}
