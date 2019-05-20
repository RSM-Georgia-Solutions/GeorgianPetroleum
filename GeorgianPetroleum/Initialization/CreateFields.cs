using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;
using SAPbouiCOM;
using Application = SAPbouiCOM.Framework.Application;
using GeorgianPetroleum.Initialization;

namespace GeorgianPetroleum.Initialization
{
    class CreateFields : IRunable
    {
        public void Run(DiManager diManager)
        {
            DiManager.Company.StartTransaction();
            if (
                 //diManager.AddField("OINV", "WbNumber", "ზედნადების ნომერი", BoFieldTypes.db_Alpha, 20, false, true) &&
                 //diManager.AddField("OOAT", "ProfitMargin", "მარჟა", BoFieldTypes.db_Alpha, 20, false, true) &&
                 diManager.AddField("OINV", "WbId", "ზედნადების ID", BoFieldTypes.db_Alpha, 20, false, true) &&
                 //diManager.AddField("OINV", "VatNumber", "ანგარიშ-ფაქტურის ნომერი", BoFieldTypes.db_Alpha, 20, false, true) &&


                diManager.AddField("RSM_CPRM", "username", "username", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, true) &&
                diManager.AddField("RSM_CPRM", "password", "password", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, true) &&

                diManager.AddField("RSM_USRS", "USERID", "USERID", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, true) &&
                diManager.AddField("RSM_USRS", "RS_USER_NAME", "RS_USER_NAME", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, true) &&
                diManager.AddField("RSM_USRS", "RS_PASSWORD", "RS_PASSWORD", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, true) &&

                diManager.AddField("RSM_SWBI", "ID", "ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                diManager.AddField("RSM_SWBI", "W_NAME", "W_NAME", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                diManager.AddField("RSM_SWBI", "UNIT_ID", "UNIT_ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                diManager.AddField("RSM_SWBI", "UNIT_TXT", "UNIT_TXT", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                diManager.AddField("RSM_SWBI", "QUANTITY", "QUANTITY", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                diManager.AddField("RSM_SWBI", "PRICE", "PRICE", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                diManager.AddField("RSM_SWBI", "STATUS", "STATUS", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                diManager.AddField("RSM_SWBI", "AMOUNT", "AMOUNT", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                diManager.AddField("RSM_SWBI", "BAR_CODE", "BAR_CODE", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                diManager.AddField("RSM_SWBI", "A_ID", "A_ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                diManager.AddField("RSM_SWBI", "VAT_TYPE", "VAT_TYPE", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                diManager.AddField("RSM_SWBI", "QUANTITY_EXT", "QUANTITY_EXT", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                diManager.AddField("RSM_SWBI", "WB_CODE", "WB_CODE", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                diManager.AddField("RSM_SWBI", "WAYBILL_NUMBER", "WAYBILL_NUMBER", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&

                diManager.AddField("RSM_WBAR", "ID", "ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                diManager.AddField("RSM_WBAR", "TYPE", "TYPE", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "BUYER_TIN", "BUYER_TIN", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "CHEK_BUYER_TIN", "CHEK_BUYER_TIN", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "BUYER_NAME", "BUYER_NAME", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "START_ADDRESS", "START_ADDRESS", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "END_ADDRESS", "END_ADDRESS", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "DRIVER_TIN", "DRIVER_TIN", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "CHEK_DRIVER_TIN", "CHEK_DRIVER_TIN", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "DRIVER_NAME", "DRIVER_NAME", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "TRANSPORT_COAST", "TRANSPORT_COAST", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "RECEPTION_INFO", "RECEPTION_INFO", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "RECEIVER_INFO", "RECEIVER_INFO", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "DELIVERY_DATE", "DELIVERY_DATE", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "STATUS", "STATUS", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "SELER_UN_ID", "SELER_UN_ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "PAR_ID", "PAR_ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "FULL_AMOUNT", "FULL_AMOUNT", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "CAR_NUMBER", "CAR_NUMBER", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "WAYBILL_NUMBER", "WAYBILL_NUMBER", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "S_USER_ID", "S_USER_ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "BEGIN_DATE", "BEGIN_DATE", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "TRAN_COST_PAYER", "TRAN_COST_PAYER", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "TRANS_ID", "TRANS_ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "TRANS_TXT", "TRANS_TXT", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "COMMENT", "COMMENT", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "CATEGORY", "CATEGORY", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "WOOD_LABELS", "WOOD_LABELS", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "CREATE_DATE", "CREATE_DATE", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "ACTIVATE_DATE", "ACTIVATE_DATE", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "FULL_AMOUNT_TXT", "FULL_AMOUNT_TXT", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "CLOSE_DATE", "CLOSE_DATE", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "IS_CONFIRMED", "IS_CONFIRMED", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "INVOICE_ID", "INVOICE_ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "CONFIRMATION_DATE", "CONFIRMATION_DATE", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "SELLER_TIN", "SELLER_TIN", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "SELLER_NAME", "SELLER_NAME", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "ORIGIN_TYPE", "ORIGIN_TYPE", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "ORIGIN_TEXT", "ORIGIN_TEXT", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "BUYER_S_USER_ID", "BUYER_S_USER_ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "TOTAL_QUANTITY", "TOTAL_QUANTITY", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "TRANSPORTER_TIN", "TRANSPORTER_TIN", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "CUST_STATUS", "CUST_STATUS", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "CUST_NAME", "CUST_NAME", SAPbobsCOM.BoFieldTypes.db_Alpha, 200, false) &&
                 diManager.AddField("RSM_WBAR", "INVOICE_DOCENTRY", "INVOICE_DOCENTRY", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, false) &&

                 diManager.AddField("RSM_UOMS", "UOM_SAP", "UOM SAP", SAPbobsCOM.BoFieldTypes.db_Alpha, 250, false) &&
                 diManager.AddField("RSM_UOMS", "UOM_RS", "UOM RS", SAPbobsCOM.BoFieldTypes.db_Alpha, 250, false) &&
                 diManager.AddField("RSM_UOMS", "ID", "OUM RS ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 250, false) &&


                 diManager.AddField("RSM_MTCH", "BP_ID", "Business Partner ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 250, false) &&
                 diManager.AddField("RSM_MTCH", "RS_ITEM_ID", "RS Item ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 250, false) &&
                 diManager.AddField("RSM_MTCH", "SAP_ITEM_ID", "SAP Item ID", SAPbobsCOM.BoFieldTypes.db_Alpha, 250, false) &&


                 diManager.AddField("RSM_PRCE", "S_DATE", "Starting Date", SAPbobsCOM.BoFieldTypes.db_Date, 250, false) &&
                 diManager.AddField("RSM_PRCE", "E_DATE", "Ending Date", SAPbobsCOM.BoFieldTypes.db_Date, 250, false) &&
                 diManager.AddField("RSM_PRCE", "ABS_NUMBER", "Agreement Number", SAPbobsCOM.BoFieldTypes.db_Alpha, 250, false) &&
                 diManager.AddField("RSM_PRCE", "PROFIT_MARGIN", "Profit Margin", SAPbobsCOM.BoFieldTypes.db_Float, 250, false) &&
                 diManager.AddField("RSM_PRCE", "AVG_PRICE", "Avarage Price", SAPbobsCOM.BoFieldTypes.db_Float, 250, false) &&
                 diManager.AddField("RSM_PRCE", "FreeText", "Free Text", SAPbobsCOM.BoFieldTypes.db_Alpha, 250, false) 




                )
            {
                Application.SBO_Application.SetStatusBarMessage("ველები წარმატებით შეიქმნა",
                    BoMessageTime.bmt_Short, false);
                DiManager.Company.EndTransaction(BoWfTransOpt.wf_Commit);
            }
            else
            {
                Application.SBO_Application.SetStatusBarMessage("პრობლემა მოხდა ველების შეიქმნისას",
                    BoMessageTime.bmt_Short, true);
                DiManager.Company.EndTransaction(BoWfTransOpt.wf_RollBack);
            }

        }
    }
}
