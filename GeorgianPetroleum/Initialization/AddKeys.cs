using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;
using SAPbouiCOM;
using Application = SAPbouiCOM.Framework.Application;

namespace GeorgianPetroleum.Initialization
{
    class AddKeys : IRunable
    {
        public void Run(DiManager diManager)
        {
            DiManager.Company.StartTransaction();
            if (diManager.AddKey("RSM_WBAR", "KeySWBItID", "ID", BoYesNoEnum.tYES) &&
                diManager.AddKey("RSM_SWBI", "KeySWBID", "W_NAME", BoYesNoEnum.tYES, "WB_CODE")  
                /*diManager.AddKey("RSM_MTCH", "KeySWBID", "RS_ITEM_ID", BoYesNoEnum.tYES, "SAP_ITEM_ID", "BP_ID")*/)
            {
                Application.SBO_Application.SetStatusBarMessage("Keyes შეიქმნა",
                    BoMessageTime.bmt_Short, false);
                try
                {
                    DiManager.Company.EndTransaction(BoWfTransOpt.wf_Commit);
                }
                catch (Exception ex)
                {
                    if (!ex.Message.Contains("already exists on table"))
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                Application.SBO_Application.SetStatusBarMessage("პრობლემა მოხდა Keyes შეიქმნისას",
                    BoMessageTime.bmt_Short, true);
                DiManager.Company.EndTransaction(BoWfTransOpt.wf_RollBack);
            }

        }
    }
}
