using System.Data;
using System.Threading.Tasks;
using DVLD_DAL.Applications.ManageTestTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DVLD_BLL.Applications.ManageTestTypes
{
    public static class clsTestType
    {
        #region Aync Methods
        //************** R **************\\
        public static DataTable GetTestTypes() => clsTestTypeData.GetTestTypes();

        public static int FInd(string Name) => clsTestTypeData.GetTestType(Name);

        public static string FInd(int ID) => clsTestTypeData.GetTestType(ID);


        //************** U **************\\
        public static bool Update(int ID, float Fees) => clsTestTypeData.Update(ID, Fees);
        
        public static bool Update(string Title, float Fees) => clsTestTypeData.Update(Title, Fees);

        public static bool Update(int ID, string Title, string Description, float Fees) => clsTestTypeData.Update(ID, Title, Description, Fees);
        #endregion


        #region Async Methods
        //************** R **************\\
        public static async Task<DataTable> GetTestTypesAsync() => await clsTestTypeData.GetTestTypesAsync();

        public static async Task<int> FindAsync(string Name) => await clsTestTypeData.GetTestTypeAsync(Name);

        public static async Task<string> FindAsync(int ID) => await clsTestTypeData.GetTestTypeAsync(ID);


        //************** U **************\\
        public static async Task<bool> UpdateAsync(int ID, float Fees) => await clsTestTypeData.UpdateAsync(ID, Fees);

        public static async Task<bool> UpdateAsync(string Title, float Fees) => await clsTestTypeData.UpdateAsync(Title, Fees);
        public static async Task<bool> UpdateAsync(int ID, string Title, string Description, float Fees) => await clsTestTypeData.UpdateAsync(ID, Title, Description, Fees);
        #endregion
    }
}
