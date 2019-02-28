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
    class CreateTables : IRunable
    {
        public void Run(DiManager diManager)
        {
            DiManager.Company.StartTransaction();
            if (diManager.CreateTable("RSM_USRS", BoUTBTableType.bott_NoObjectAutoIncrement) &&
                diManager.CreateTable("RSM_CPRM", BoUTBTableType.bott_NoObjectAutoIncrement) &&
                diManager.CreateTable("RSM_SWBI", BoUTBTableType.bott_NoObjectAutoIncrement) &&
                diManager.CreateTable("RSM_WBAR", BoUTBTableType.bott_NoObjectAutoIncrement) &&
                diManager.CreateTable("RSM_UOMS", BoUTBTableType.bott_NoObjectAutoIncrement) &&
                diManager.CreateTable("RSM_MTCH", BoUTBTableType.bott_NoObjectAutoIncrement) &&
                diManager.CreateTable("RSM_PRCE", BoUTBTableType.bott_NoObjectAutoIncrement) 
                ) 
            {
                Application.SBO_Application.SetStatusBarMessage("ცხირლები წარმატებით შეიქმნა",
                    BoMessageTime.bmt_Short, false);
                DiManager.Company.EndTransaction(BoWfTransOpt.wf_Commit);
            }
            else
            {
                Application.SBO_Application.SetStatusBarMessage("პრობლემა მოხდა ცხირლების შეიქმნისას",
                    BoMessageTime.bmt_Short, true);
                DiManager.Company.EndTransaction(BoWfTransOpt.wf_RollBack);
            }
        }
    }
}
