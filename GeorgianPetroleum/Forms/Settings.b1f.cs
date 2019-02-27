using System;
using System.Collections.Generic;
using System.Xml;
using SAPbouiCOM.Framework;

namespace GeorgianPetroleum
{
    [FormAttribute("GeorgianPetroleum.Settings", "Forms/Settings.b1f")]
    class Settings : UserFormBase
    {
        public Settings()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_0").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_13").Specific));
            this.StaticText5 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_14").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("Item_15").Specific));
            this.EditText5 = ((SAPbouiCOM.EditText)(this.GetItem("Item_16").Specific));
            this.Folder3 = ((SAPbouiCOM.Folder)(this.GetItem("Item_18").Specific));
            this.Folder4 = ((SAPbouiCOM.Folder)(this.GetItem("Item_19").Specific));
            this.Grid1 = ((SAPbouiCOM.Grid)(this.GetItem("Item_20").Specific));
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("Item_21").Specific));
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

        private SAPbouiCOM.Button Button0;
        SAPbouiCOM.EditTextColumn oEditCol;

        private void OnCustomInitialize()
        {
            DiManager.Recordset.DoQuery(DiManager.QueryHanaTransalte("SELECT * FROM [@RSM_CPRM]"));
            if (!DiManager.Recordset.EoF)
            {
                EditText4.Value = DiManager.Recordset.Fields.Item("U_username").Value.ToString();
                EditText5.Value = DiManager.Recordset.Fields.Item("U_password").Value.ToString();
            }
            Grid1.DataTable.ExecuteQuery(DiManager.QueryHanaTransalte("select  sapus.USERID as [ID], sapus.USER_CODE as [მომხმარებლის კოდი], sapus.U_NAME as [დასახელება],  U_RS_USER_NAME as [რს-ის მომხმარებელი], case when   us.U_RS_PASSWORD = ' ' or  us.U_RS_PASSWORD is null then '...'  else  '***                        ' end as     [პაროლი] from [@RSM_USRS] us right join  OUSR sapus on sapus.USER_CODE = us.U_USERID"));
            oEditCol = (SAPbouiCOM.EditTextColumn)(Grid1.Columns.Item("ID"));
            oEditCol.LinkedObjectType = "12";
            (Grid1.Columns.Item("მომხმარებლის კოდი")).Editable = false;
            (Grid1.Columns.Item("დასახელება")).Editable = false;
        }

        private void Button0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            string rsUser = EditText4.Value;
            string rsUserPass = EditText5.Value;
            DiManager.Recordset.DoQuery(DiManager.QueryHanaTransalte("SELECT * FROM [@RSM_CPRM]"));
            if (DiManager.Recordset.EoF)
            {
                DiManager.Recordset.DoQuery(DiManager.QueryHanaTransalte($"INSERT INTO [@RSM_CPRM] (U_username, U_password) VALUES ('{rsUser}', '{rsUserPass}')"));
            }
            else
            {
                var Code = DiManager.Recordset.Fields.Item("code").Value;
                DiManager.Recordset.DoQuery(DiManager.QueryHanaTransalte($"UPDATE [@RSM_CPRM] SET U_username = '{rsUser}', U_password = '{rsUserPass}'  WHERE Code = '{Code}'"));
            }

            DiManager.RsUserName = rsUser;
            DiManager.RsUserPass = rsUserPass;

            for (int i = 0; i < Grid1.DataTable.Rows.Count; i++)
            {
                string sapUserId = Grid1.DataTable.GetValue("მომხმარებლის კოდი", i).ToString();
                string rsUserGridName = Grid1.DataTable.GetValue("რს-ის მომხმარებელი", i).ToString();
                string rsUserGridPass = Grid1.DataTable.GetValue("პაროლი", i).ToString();
                if (rsUserGridPass == "***" || rsUserGridPass == "...") continue;

                DiManager.Recordset.DoQuery(DiManager.QueryHanaTransalte($"SELECT * FROM [@RSM_USRS] WhERE U_USERID = '{sapUserId}'"));
                if (DiManager.Recordset.EoF)
                {
                    DiManager.Recordset.DoQuery(DiManager.QueryHanaTransalte($"INSERT INTO [@RSM_USRS] (U_USERID,U_RS_USER_NAME,U_RS_PASSWORD) VALUES ('{sapUserId}', '{rsUserGridName}', '{rsUserGridPass}')"));
                }
                else
                {
                    DiManager.Recordset.DoQuery(DiManager.QueryHanaTransalte($"UPDATE [@RSM_USRS] Set U_USERID ='{sapUserId}',U_RS_USER_NAME = '{rsUserGridName}' ,U_RS_PASSWORD =  '{rsUserGridPass}' WHERE U_USERID = '{sapUserId}'"));
                }
            }

            DiManager.Recordset.DoQuery(DiManager.QueryHanaTransalte($"SELECT * FROM [@RSM_USRS] WhERE U_USERID = '{DiManager.Company.UserName}'"));

            DiManager.RsServiceUser = DiManager.Recordset.Fields.Item("U_RS_USER_NAME").Value.ToString();
            DiManager.RsServiceUserPass = DiManager.Recordset.Fields.Item("U_RS_PASSWORD").Value.ToString();

            Application.SBO_Application.StatusBar.SetSystemMessage("პარამეტრები წარმატებით განახლდა",
                SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
 
        }

        private SAPbouiCOM.StaticText StaticText4;
        private SAPbouiCOM.StaticText StaticText5;
        private SAPbouiCOM.EditText EditText4;
        private SAPbouiCOM.EditText EditText5;
        private SAPbouiCOM.Folder Folder3;
        private SAPbouiCOM.Folder Folder4;
        private SAPbouiCOM.Grid Grid1;
        private SAPbouiCOM.Button Button1;

        private void Button1_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Close();
        }

        private void Form_VisibleAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Title == "პარამეტრები")
            {
                Folder3.Select();
                Folder3.Item.Width = 300;
                Folder4.Item.Width = 300;
            }
        }
    }
}