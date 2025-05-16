using DVLD_BLL.Applications.DrivingLicenseServices;
using Helper.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BLL
{
    public sealed class clsLazySingleton
    {
        #region Private Static Fields
        private static readonly Lazy<clsLazySingleton> _instance = new Lazy<clsLazySingleton>(() => new clsLazySingleton());
        #endregion

        #region Private Fields
        private List<clsCountry> _countries;
        private List<clsLicenseClass> _classLicenses;
        #endregion

        #region Constructor
        private clsLazySingleton()
        {
            _countries = new List<clsCountry>();
            _classLicenses = new List<clsLicenseClass>();
        }
        #endregion

        #region Public Static Property
        public static clsLazySingleton Instance => _instance.Value;
        #endregion

        #region Public Methods
        public async Task<List<clsCountry>> GetCountriesAsync()
        {
            if (_countries is null || _countries.Count is 0)
            {
                _countries = (await clsCountry.GetCountriesAsIEnumerableAsync()).ToList();
            }
            return _countries;
        }

        public async Task<List<clsLicenseClass>> GetClassLicensesAsync()
        {
            if (_classLicenses is null || _classLicenses.Count is 0)
            {
                _classLicenses = await clsLicenseClass.GetAllAsListAsync();
            }
            return _classLicenses;
        }

        public async Task RefreshDataAsync()
        {
            _countries = (await clsCountry.GetCountriesAsIEnumerableAsync()).ToList();
            _classLicenses = (await clsLicenseClass.GetAllAsIEnumerableAsync()).ToList();
        }
        #endregion
    }

}
