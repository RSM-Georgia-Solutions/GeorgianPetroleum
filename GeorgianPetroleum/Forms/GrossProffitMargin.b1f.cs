﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SAPbouiCOM.Framework;
using System.Windows.Forms;
using SAPbouiCOM;
using Form = SAPbouiCOM.Form;
using System.Data.OleDb;
using DataColumn = System.Data.DataColumn;
using DataTable = System.Data.DataTable;
using System.Globalization;
using SAPbobsCOM;

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
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_1").Specific));
            this.EditText0.LostFocusAfter += new SAPbouiCOM._IEditTextEvents_LostFocusAfterEventHandler(this.EditText0_LostFocusAfter);
            this.EditText0.PressedAfter += new SAPbouiCOM._IEditTextEvents_PressedAfterEventHandler(this.EditText0_PressedAfter);
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_2").Specific));
            this.EditText1.LostFocusAfter += new SAPbouiCOM._IEditTextEvents_LostFocusAfterEventHandler(this.EditText1_LostFocusAfter);
            this.EditText1.PressedAfter += new SAPbouiCOM._IEditTextEvents_PressedAfterEventHandler(this.EditText1_PressedAfter);
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_3").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_4").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_5").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("Item_6").Specific));
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("Item_7").Specific));
            this.Button1.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button1_PressedAfter);
            this.Button2 = ((SAPbouiCOM.Button)(this.GetItem("Item_8").Specific));
            this.Button2.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button2_PressedAfter);
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("Item_9").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_10").Specific));
            this.Button3 = ((SAPbouiCOM.Button)(this.GetItem("Item_11").Specific));
            this.Button3.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button3_PressedAfter);
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
            var startDateString = EditText0.Value;
            var endDateString = EditText1.Value;
            if (!string.IsNullOrWhiteSpace(startDateString) && !string.IsNullOrWhiteSpace(endDateString))
            {

                DateTime sDate = DateTime.ParseExact(startDateString, "yyyyMMdd", CultureInfo.InvariantCulture);
                DateTime eDate = DateTime.ParseExact(endDateString, "yyyyMMdd", CultureInfo.InvariantCulture);
                Grid0.DataTable.ExecuteQuery(DiManager.QueryHanaTransalte($"SELECT U_S_DATE as [დაწყების თარიღი], U_E_DATE as [დასრულების თარიღი],   U_ABS_NUMBER as [ხელშეკრულების ნომერი], U_PROFIT_MARGIN as [მარჟა], U_AVG_PRICE as [საშუალო ფასი] FROM [@RSM_PRCE] WHERE U_S_DATE BETWEEN '{sDate:s}' AND '{eDate:s}' "));
                EditText3.Value = Grid0.DataTable.GetValue("საშუალო ფასი", 0).ToString();
            }

        }

        private void OnCustomInitialize()
        {
            Button0.Item.FontSize = 24;
            EditText1.Value = DateTime.Now.ToString("yyyyMMdd");
            EditText0.Value = DateTime.Now.ToString("yyyyMMdd");
            Refresh();
        }

        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.EditText EditText2;

        private delegate void XPerformer();
        static XPerformer _performer;

        private static void ShowFolderBrowser()
        {
            try
            {
                NativeWindow nws = new NativeWindow();
                OpenFileDialog fdb = new OpenFileDialog();
                Process.GetProcessesByName("SAP Business One");
                nws.AssignHandle(System.Diagnostics.Process.GetProcessesByName("SAP Business One")[0].MainWindowHandle);
                Form mSboForm = SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm; //GET ACTIVE FORM 
                if (fdb.ShowDialog(nws) != System.Windows.Forms.DialogResult.OK) return;
                FilePath = fdb.FileName;
                _performer();
            }
            catch (Exception ex)
            {
                SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText(ex.Message);

            }
        }

        private void OpenFile()
        {
            try
            {
                Thread showFolderBrowserThread = new Thread(ShowFolderBrowser);

                switch (showFolderBrowserThread.ThreadState)
                {
                    case System.Threading.ThreadState.Unstarted:
                        showFolderBrowserThread.SetApartmentState(ApartmentState.STA);

                        showFolderBrowserThread.Start();
                        break;
                    case System.Threading.ThreadState.Stopped:
                        showFolderBrowserThread.Start();

                        showFolderBrowserThread.Join();
                        break;
                }
            }

            catch (Exception)
            {
                // ignored
            }
        }

        private static string FilePath { get; set; }

        private void XPerform()
        {
            if (string.IsNullOrEmpty(FilePath)) return;
            ((EditText)SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Items
                .Item("Item_6").Specific).Value = Path.GetFileName(FilePath);
            GetItem("Item_5").Enabled = true;
        }

        private void GetFileName(XPerformer performer)
        {
            _performer += performer;
            OpenFile();
        }
        private void Button0_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            XPerformer filePathSetter = XPerform;
            Task task1 = Task.Factory.StartNew(() => GetFileName(filePathSetter));
            Task.WaitAll(task1);
        }

        private SAPbouiCOM.Button Button1;
        private SAPbouiCOM.Button Button2;

        private void Button2_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Close();
        }

        private void Button1_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            var sheets = ToExcelsSheetList(FilePath);
            DataTable dataTable = ReadExcelFile(sheets.First(), FilePath);

            List<Dictionary<string, string>> columnValueList = new List<Dictionary<string, string>>();

            var startDateString = EditText0.Value;
            var endDateString = EditText1.Value;

            DateTime sDate = DateTime.ParseExact(startDateString, "yyyyMMdd", CultureInfo.InvariantCulture);
            DateTime eDate = DateTime.ParseExact(endDateString, "yyyyMMdd", CultureInfo.InvariantCulture);

            var avgPrice = EditText3.Value;

            DiManager.Company.StartTransaction();
            foreach (DataRow dtRow in dataTable.Rows)
            {
                var absNumber = dtRow["ხელშეკრულების ნომერი"].ToString();
                var U_PROFIT_MARGIN = dtRow["მარჟა"].ToString();

                DiManager.Recordset.DoQuery(DiManager.QueryHanaTransalte($"INSERT INTO [@RSM_PRCE] (U_S_DATE, U_E_DATE, U_ABS_NUMBER, U_PROFIT_MARGIN, U_AVG_PRICE) VALUES (N'{sDate:s}', N'{eDate:s}', N'{absNumber}', N'{U_PROFIT_MARGIN}', N'{avgPrice}') "));
            }
            DiManager.Company.EndTransaction(BoWfTransOpt.wf_Commit);
        }

        private static IEnumerable<string> ToExcelsSheetList(string excelFilePath)
        {
            List<string> sheets = new List<string>();
            using (OleDbConnection connection =
                new OleDbConnection((excelFilePath.TrimEnd().ToLower().EndsWith("x"))
                    ? "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + excelFilePath + "';" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'"
                    : "provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + excelFilePath + "';Extended Properties=Excel 8.0;"))
            {
                connection.Open();
                DataTable dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                foreach (DataRow drSheet in dt.Rows)
                    if (drSheet["TABLE_NAME"].ToString().Contains("$"))
                    {
                        string s = drSheet["TABLE_NAME"].ToString();
                        sheets.Add(s.StartsWith("'") ? s.Substring(1, s.Length - 3) : s.Substring(0, s.Length - 1));
                    }
                connection.Close();
            }
            return sheets;
        }

        private DataTable ReadExcelFile(string sheetName, string path)
        {

            using (OleDbConnection conn = new OleDbConnection())
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                string importFileName = path;
                string fileExtension = Path.GetExtension(importFileName);
                if (fileExtension.ToLower() == ".xls")
                {
                    conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + importFileName + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
                }
                if (fileExtension.ToLower() == ".xlsx")
                {
                    conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + importFileName + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                }
                conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + importFileName + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'";

                using (OleDbCommand comm = new OleDbCommand())
                {
                    comm.CommandText = "Select * from [" + sheetName + "$]";

                    comm.Connection = conn;

                    using (OleDbDataAdapter da = new OleDbDataAdapter())
                    {
                        da.SelectCommand = comm;
                        da.Fill(dt);
                        return dt;
                    }

                }
            }
        }

        private EditText EditText3;
        private StaticText StaticText2;
        private SAPbouiCOM.Button Button3;

        private void Button3_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            var startDateString = EditText0.Value;
            var endDateString = EditText1.Value;
            DateTime sDate = DateTime.ParseExact(startDateString, "yyyyMMdd", CultureInfo.InvariantCulture);
            DateTime eDate = DateTime.ParseExact(endDateString, "yyyyMMdd", CultureInfo.InvariantCulture);
            string query = $"SELECT * FROM [@RSM_PRCE] WHERE U_S_DATE BETWEEN '{sDate:s}' AND '{eDate:s}'";
            DiManager.Recordset.DoQuery(DiManager.QueryHanaTransalte(query));

            DateTime startDate = DateTime.MaxValue;
            DateTime endtDate = DateTime.MaxValue;
            string absNumber = string.Empty;
            string U_PROFIT_MARGIN = string.Empty;
            string avgPrice = EditText3.Value;

            for (int i = 0; i < Grid0.DataTable.Rows.Count; i++)
            {
                startDate = DateTime.Parse(Grid0.DataTable.GetValue("დაწყების თარიღი", i).ToString());
                endtDate = DateTime.Parse(Grid0.DataTable.GetValue("დასრულების თარიღი", i).ToString());
                absNumber = Grid0.DataTable.GetValue("ხელშეკრულების ნომერი", i).ToString();
                U_PROFIT_MARGIN = Grid0.DataTable.GetValue("მარჟა", i).ToString();
            }

            if (DiManager.Recordset.EoF)
            {
                DiManager.Recordset.DoQuery(DiManager.QueryHanaTransalte($"INSERT INTO [@RSM_PRCE] (U_S_DATE, U_E_DATE, U_ABS_NUMBER, U_PROFIT_MARGIN, U_AVG_PRICE) VALUES (N'{sDate:s}', N'{eDate:s}', N'{absNumber}', N'{U_PROFIT_MARGIN}', N'{avgPrice}') "));
            }
            else
            {
                DiManager.Recordset.DoQuery(DiManager.QueryHanaTransalte($"UPDATE [@RSM_PRCE] SET U_S_DATE = '{startDate}',  U_E_DATE = '{endtDate}',  U_ABS_NUMBER = '{absNumber}',  U_PROFIT_MARGIN = '{U_PROFIT_MARGIN}',  U_AVG_PRICE = '{avgPrice}' WHERE U_S_DATE BETWEEN '{sDate:s}' AND '{eDate:s}'"));
            }

            Refresh();
        }

        private void EditText0_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            Refresh();
        }

        private void EditText1_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            Refresh();
        }

        private void EditText0_LostFocusAfter(object sboObject, SBOItemEventArg pVal)
        {
            Refresh();

        }

        private void EditText1_LostFocusAfter(object sboObject, SBOItemEventArg pVal)
        {
            Refresh();

        }
    }
}
