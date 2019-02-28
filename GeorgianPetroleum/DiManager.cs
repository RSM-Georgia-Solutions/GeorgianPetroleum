using System;
using SAPbobsCOM;
using Translator;
using SAPbouiCOM.Framework;

namespace GeorgianPetroleum
{
    class DiManager
    {
        public static string RsUserName { get; set; }
        public static string RsUserPass { get; set; }
        public static string RsServiceUser { get; set; }
        public static string RsServiceUserPass { get; set; }
        public static Recordset Recordset { get { return recSet.Value; } }
        public static Company Company { get { return xCompany.Value; } }
        public static bool IsHana { get { return IsHanax.Value; } }

        public static RsClient RsClient => new RsClient(RsUserName, RsUserPass, RsServiceUser, RsServiceUserPass);

        private static readonly Lazy<bool> IsHanax =
            new Lazy<bool>(() => Company.DbServerType.ToString() == "dst_HANADB" ? true : false);

        private static readonly Lazy<Company> xCompany =
            new Lazy<Company>(() => (Company)SAPbouiCOM.Framework
                .Application
                .SBO_Application
                .Company.GetDICompany());

        private static readonly Lazy<Recordset> recSet =
            new Lazy<SAPbobsCOM.Recordset>(() => (Recordset)
                Company
                    .GetBusinessObject(BoObjectTypes.BoRecordset));
        public static string QueryHanaTransalte(string query)
        {
            if (IsHana)
            {
                int numOfStatements;
                int numOfErrors;
                TranslatorTool TranslateTool = new TranslatorTool();
                query = TranslateTool.TranslateQuery(query, out numOfStatements, out numOfErrors);
                return query;
            }
            else
            {
                return query;
            }
        }

        public bool CreateTable(string tableName, BoUTBTableType TableType)
        {
            GC.Collect();
            UserTablesMD oUTables;
            try
            {
                oUTables = (UserTablesMD)Company.GetBusinessObject(BoObjectTypes.oUserTables);

                if (oUTables.GetByKey(tableName) == false)
                {
                    GC.Collect();
                    oUTables.TableName = tableName;
                    oUTables.TableDescription = tableName;
                    oUTables.TableType = TableType;
                    int ret = oUTables.Add();

                    if (ret == 0)
                    {
                        //_application.StatusBar.SetText("UDT created:" + oUTables.TableName, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oUTables);
                        GC.Collect();
                        return true;
                    }
                    else
                    {
                        Application.SBO_Application.StatusBar.SetText("UDT failed: " + Company.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oUTables);
                        GC.Collect();
                        return false;
                    }
                }
                else
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUTables);
                    GC.Collect();
                    return true;
                }
            }
            catch (Exception)
            {
                //  _application.StatusBar.SetText("exception: " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            finally
            {
                GC.Collect();
            }

            return false;
        }

        public bool AddField(string tablename, string fieldname, string description, BoFieldTypes type, int size, bool isMandatory, bool isSapTable = false, string likedToTAble = "")
        {
            UserFieldsMD oUfield = (UserFieldsMD)Company.GetBusinessObject(BoObjectTypes.oUserFields);
            var recordset = (Recordset) Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            recordset.DoQuery(QueryHanaTransalte($"SELECT * FROM CUFD WHERE AliasID = '{fieldname}' AND TableID = '@{tablename}'"));
            if (!recordset.EoF)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oUfield);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(recordset);
                GC.Collect();
                return true;
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(recordset);
            GC.Collect();
            try
            {
                if (isSapTable)
                {
                    oUfield.TableName = tablename;
                }
                else
                {
                    oUfield.TableName = "@" + tablename;
                }
                oUfield.Name = fieldname;
                oUfield.Description = description;
                oUfield.Type = type;
                oUfield.Mandatory = isMandatory ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;

                if (type == BoFieldTypes.db_Float)
                {
                    oUfield.SubType = BoFldSubTypes.st_Price;
                }

                if (type == BoFieldTypes.db_Alpha || type == BoFieldTypes.db_Numeric)
                {
                    oUfield.EditSize = size;
                }
                oUfield.LinkedTable = likedToTAble;

                int ret = oUfield.Add();
                if (ret == 0 || ret == -2035)
                {
                    var x = Company.GetLastErrorDescription();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUfield);
                  
                    GC.Collect();
                    return true;
                }
                else
                {
                    var x = Company.GetLastErrorDescription();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUfield);
                  
                    GC.Collect();
                    return false;
                }
        
            }
            catch (Exception)
            {
           
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oUfield);
                GC.Collect();
                return false;
            }
            finally
            {
               
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oUfield);
                GC.Collect();
            }

        }

        public bool AddKey(string tablename, string keyname, string fieldAlias, BoYesNoEnum IsUnique, string secondKeyAlias = "", string thirdKeyAlias = "")
        {
            int result;
            UserKeysMD oUkey = (UserKeysMD)Company.GetBusinessObject(BoObjectTypes.oUserKeys);
            try
            {

                oUkey.TableName = "@" + tablename;
                oUkey.KeyName = keyname;
                oUkey.Elements.ColumnAlias = fieldAlias;
                oUkey.Unique = IsUnique;
                oUkey.Elements.Add();
                if (secondKeyAlias != "")
                {
                    oUkey.Elements.ColumnAlias = secondKeyAlias; 
                }
                if (thirdKeyAlias != "")
                {
                    oUkey.Elements.Add();
                    oUkey.Elements.ColumnAlias = thirdKeyAlias;
                }
                result = oUkey.Add();

                if (result == 0)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUkey);
                    GC.Collect();
                    return true;
                }
                if (result == -1)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUkey);
                    GC.Collect();
                    return true;
                }
                else
                {
                    string str = Company.GetLastErrorDescription();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUkey);
                    GC.Collect();
                    return false;
                }
            }
            catch (Exception ex)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oUkey);
                GC.Collect();
                return false;
            }
        }
    }
}
