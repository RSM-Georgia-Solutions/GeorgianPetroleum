using System;
using System.Collections.Generic;
using GeorgianPetroleum.Initialization;
using SAPbouiCOM.Framework;
using SAPbobsCOM;

namespace GeorgianPetroleum
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application oApp = null;
                oApp = args.Length < 1 ? new Application() : new Application(args[0]);
                Menu MyMenu = new Menu();
                MyMenu.AddMenuItems();
                Initial initial = new Initial();
                DiManager diManager = new DiManager();

                var recordset = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
                recordset.DoQuery("SELECT * FROM [@RSM_CPRM]");
                if (!recordset.EoF)
                {
                    DiManager.RsUserName = recordset.Fields.Item("U_username").Value.ToString();
                    DiManager.RsUserPass = recordset.Fields.Item("U_password").Value.ToString();
                }
                recordset.DoQuery($"SELECT * FROM [@RSM_USRS] WHERE U_USERID = '{DiManager.Company.UserName}'");
                if (!recordset.EoF)
                {
                    DiManager.RsServiceUser = recordset.Fields.Item("U_RS_USER_NAME").Value.ToString();
                    DiManager.RsServiceUserPass = recordset.Fields.Item("U_RS_PASSWORD").Value.ToString();
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(recordset);
                recordset = null;
                GC.Collect();

                initial.Run(diManager);
                oApp.RegisterMenuEventHandler(MyMenu.SBO_Application_MenuEvent);
                Application.SBO_Application.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler(SBO_Application_AppEvent);
                oApp.Run();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        static void SBO_Application_AppEvent(SAPbouiCOM.BoAppEventTypes EventType)
        {
            switch (EventType)
            {
                case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                    //Exit Add-On
                    System.Windows.Forms.Application.Exit();
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_FontChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_LanguageChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition:
                    break;
                default:
                    break;
            }
        }
    }
}
