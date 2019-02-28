using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM;
using SAPbouiCOM.Framework;

namespace GeorgianPetroleum.Forms
{
    [FormAttribute("133", "Forms/Invoice.b1f")]
    class Invoice : SystemFormBase
    {
        public Invoice()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.Button0.PressedBefore += new SAPbouiCOM._IButtonEvents_PressedBeforeEventHandler(this.Button0_PressedBefore);
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.Button Button0;

        private void Button0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
       
        }

        private void OnCustomInitialize()
        {

        }

        private void Button0_PressedBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
          var  blanketAgreementNumber = ((EditText) (SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Items.Item("1980002192")
                .Specific)).Value;

            Matrix invoiceMatrix = (Matrix)SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Items.Item("38").Specific;

            for (int i = 1; i < invoiceMatrix.RowCount; i++)
            {
                SAPbouiCOM.EditText NetPrice = (SAPbouiCOM.EditText)invoiceMatrix.Columns.Item("14").Cells.Item(i).Specific;
                SAPbouiCOM.EditText GrossPrice = (SAPbouiCOM.EditText)invoiceMatrix.Columns.Item("234000377").Cells.Item(i).Specific;
                try
                {
                    NetPrice.Value = "99";
                }
                catch (Exception e)
                {
                    GrossPrice.Value = "99";
                }
            }
        }
    }
}
