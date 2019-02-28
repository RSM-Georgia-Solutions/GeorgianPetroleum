using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;
using BoOrderType = SAPbouiCOM.BoOrderType;
using Grid = SAPbouiCOM.Grid;

namespace GeorgianPetroleum.Forms
{
    [FormAttribute("GeorgianPetroleum.Forms.MatchingTable", "Forms/MatchingTable.b1f")]
    class MatchingTable : UserFormBase
    {
        private readonly string _buyerCode;
        private readonly List<string> _itemNames;
        private readonly string _waybillId;

        public MatchingTable(string buyerCode, List<string> itemNames, string waybillId)
        {
            _buyerCode = buyerCode;
            _itemNames = itemNames;
            _waybillId = waybillId;
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_0").Specific));
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_1").Specific));
            this.Grid0.ClickAfter += new SAPbouiCOM._IGridEvents_ClickAfterEventHandler(this.Grid0_ClickAfter);
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_2").Specific));
            this.Grid1 = ((SAPbouiCOM.Grid)(this.GetItem("Item_3").Specific));
            this.Grid1.ClickAfter += new SAPbouiCOM._IGridEvents_ClickAfterEventHandler(this.Grid1_ClickAfter);
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_4").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("Item_5").Specific));
            this.Button1.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button1_PressedAfter);
            this.Button2 = ((SAPbouiCOM.Button)(this.GetItem("Item_6").Specific));
            this.Button2.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button2_PressedAfter);
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.VisibleAfter += new SAPbouiCOM.Framework.FormBase.VisibleAfterHandler(this.Form_VisibleAfter);
            this.ActivateAfter += new ActivateAfterHandler(this.Form_ActivateAfter);

        }

        private SAPbouiCOM.StaticText StaticText0;

        private void OnCustomInitialize()
        {
           
        }

        private void Refresh()
        {
            Grid0.Item.Enabled = false;
            Grid1.Item.Enabled = false;

            string itemCodes = _itemNames.Aggregate(string.Empty, (current, item) => current + $"N'{item}', ");
            itemCodes = itemCodes.Remove(itemCodes.Length - 2, 2);

            string queryForMatched = $"Select  U_RS_ITEM_ID as [RS-ის საქონელი], U_SAP_ITEM_ID as [Sap-ის საქონელი] from [@RSM_MTCH] WHERE U_BP_ID = '{_buyerCode}' And U_RS_ITEM_ID in ({itemCodes})";

            string queryForMatchedSelect = $"Select U_RS_ITEM_ID as [RS-ის საქონელი] from [@RSM_MTCH] WHERE U_BP_ID = '{_buyerCode}' And U_RS_ITEM_ID in ({itemCodes})";

            string queryForNotMatched = $"Select U_W_NAME as [RS-ის საქონელი] from [@RSM_SWBI] WHERE U_WB_CODE = '{_waybillId}' AND " +
                                        $"U_W_NAME NOT IN ({queryForMatchedSelect})";

            Grid0.DataTable.ExecuteQuery(DiManager.QueryHanaTransalte(queryForMatched));
            Grid1.DataTable.ExecuteQuery(DiManager.QueryHanaTransalte(queryForNotMatched));
        }

        private SAPbouiCOM.Grid Grid0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.Grid Grid1;
        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.Button Button1;

        private void Form_VisibleAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (Application.SBO_Application.Forms.ActiveForm.Title == "საქონლის შესაბამისობა")
            {
                Refresh();
            }
        }

        private void Grid1_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (pVal.Row == -1)
            {
                return;
            }

            Grid0.Rows.SelectedRows.Clear();

            if (Grid1.Rows.IsSelected(pVal.Row))
            {
                Grid1.Rows.SelectedRows.Remove(pVal.Row);
            }
            else
            {
                Grid1.Rows.SelectedRows.Add(pVal.Row);
            }
    
        
        }

        private void Button1_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            Grid1.Rows.SelectedRows.Clear();
            for (int i = 0; i < Grid1.DataTable.Rows.Count; i++)
            {
               Grid1.Rows.SelectedRows.Add(i);
            }
        }

        private SAPbouiCOM.Button Button2;

        private void Button2_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            List<string> codes = new List<string>();

            if (Grid1.Rows.SelectedRows.Count == 0)
            {
                for (int i = 0; i < Grid0.Rows.SelectedRows.Count; i++)
                {
                    int x = Grid0.Rows.SelectedRows.Item(i, BoOrderType.ot_RowOrder);
                    string code = Grid0.DataTable.GetValue(0, x).ToString();
                    codes.Add(code);
                }
                ItemsList itemsLists = new ItemsList(codes, _buyerCode, false);
                itemsLists.Show();
            }
            else if(Grid0.Rows.SelectedRows.Count == 0)
            {
                for (int i = 0; i < Grid1.Rows.SelectedRows.Count; i++)
                {
                    int x = Grid1.Rows.SelectedRows.Item(i, BoOrderType.ot_RowOrder);
                    string code = Grid1.DataTable.GetValue(0, x).ToString();
                    codes.Add(code);
                }
                ItemsList itemsLists = new ItemsList(codes, _buyerCode, true);
                itemsLists.Show();
            }

           

          
        }

        private void Grid0_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (pVal.Row == -1)
            {
                return;
            }

            Grid1.Rows.SelectedRows.Clear();
            if (Grid0.Rows.IsSelected(pVal.Row))
            {
                Grid0.Rows.SelectedRows.Remove(pVal.Row);
            }
            else
            {
                Grid0.Rows.SelectedRows.Add(pVal.Row);
            }
            
        }

        private void Form_ActivateAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            Refresh();
        }

        private void Button0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Close();
        }
    }
}
