using System;
using System.Collections.Generic;
using System.Text;
using GeorgianPetroleum.Forms;
using SAPbouiCOM.Framework;

namespace GeorgianPetroleum
{
    class Menu
    {
        public void AddMenuItems()
        {
            SAPbouiCOM.Menus oMenus = null;
            SAPbouiCOM.MenuItem oMenuItem = null;

            oMenus = Application.SBO_Application.Menus;

            SAPbouiCOM.MenuCreationParams oCreationPackage = null;
            oCreationPackage = ((SAPbouiCOM.MenuCreationParams)(Application.SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)));


            oMenuItem = Application.SBO_Application.Menus.Item("43520"); // moudles'

            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP;
            oCreationPackage.UniqueID = "GeorgianPetroleum";
            oCreationPackage.String = "GeorgianPetroleum";
            oCreationPackage.Enabled = true;
            oCreationPackage.Position = -1;

            oMenus = oMenuItem.SubMenus;

            try
            {
                //  If the manu already exists this code will fail
                oMenus.AddEx(oCreationPackage);
            }
            catch (Exception e)
            {

            }

            try
            {
                oMenuItem = Application.SBO_Application.Menus.Item("GeorgianPetroleum");
                oMenus = oMenuItem.SubMenus;

                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "GeorgianPetroleum.Forms.GrossProffitMargin";
                oCreationPackage.String = "ფასები";
                oMenus.AddEx(oCreationPackage);

                // Create s sub menu
                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "GeorgianPetroleum.Forms.UomMatching";
                oCreationPackage.String = "საზომი ერთეულები";
                oMenus.AddEx(oCreationPackage);

                // Create s sub menu
                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "GeorgianPetroleum.SentWaybill";
                oCreationPackage.String = "გაგზავნილი ზედნადებები";
                oMenus.AddEx(oCreationPackage);
                // Get the menu collection of the newly added pop-up item
                oMenuItem = Application.SBO_Application.Menus.Item("GeorgianPetroleum");
                oMenus = oMenuItem.SubMenus;

                // Create s sub menu
                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "GeorgianPetroleum.Settings";
                oCreationPackage.String = "პარამეტრები";
                oMenus.AddEx(oCreationPackage);
            }
            catch (Exception er)
            { //  Menu already exists
                Application.SBO_Application.SetStatusBarMessage("Menu Already Exists", SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }

        public void SBO_Application_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {
                if (pVal.BeforeAction && pVal.MenuUID == "GeorgianPetroleum.Settings")
                {
                    Settings activeForm = new Settings();
                    activeForm.Show();
                }
                else if (pVal.BeforeAction && pVal.MenuUID == "GeorgianPetroleum.SentWaybill")
                {
                    SentWaybills activeForm = new SentWaybills();
                    activeForm.Show();
                }
                else if (pVal.BeforeAction && pVal.MenuUID == "GeorgianPetroleum.Forms.UomMatching")
                {
                    UomMatching activeForm = new UomMatching();
                    activeForm.Show();
                }
                else if (pVal.BeforeAction && pVal.MenuUID == "GeorgianPetroleum.Forms.GrossProffitMargin")
                {
                    GrossProffitMargin activeForm = new GrossProffitMargin();
                    activeForm.Show();
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.ToString(), 1, "Ok", "", "");
            }
        }

    }
}
