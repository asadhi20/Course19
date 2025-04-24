using DVLD_BLL.Applications.ManageApplications.LocalDrivingLicenseApplications;
using DVLD_BLL.Applications.ManageApplications;
using DVLD_BLL.Applications.ManageApplicationTypes;
using DVLD_BLL.People;
using DVLD_BLL.Users;
using DVLD_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DVLD_BLL.Applications.ManageApplications.clsApplication;

namespace DVLD_BLL
{
    public struct CountryID : IEquatable<CountryID>, IComparable<CountryID>, IComparer<CountryID>
    {
        #region Constructors
        private CountryID(int value) => Value = value;
        #endregion

        #region Public Properties
        public int Value { get; }
        public static CountryID Empty => new CountryID(0);
        #endregion


        #region Public Static Methods
        public static CountryID CreateNew(int id) => new CountryID(id);
        #endregion

        #region Public Methods
        public bool IsNotEmpty() => this.Value > 0;
        public bool IsEmpty() => this.Value < 1; //it equivalent to { this.Value <= 0; }

        public bool NotEquals(CountryID other) => this.Value != other.Value;
        public bool NotEquals(int @int) => this.Value != @int;
        #endregion


        #region Interface Methods Implementation
        public bool Equals(CountryID other) => this.Value == other.Value;
        public bool Equals(int @int) => this.Value == @int;

        public int CompareTo(CountryID other) => this.Value.CompareTo(other.Value);
        public int CompareTo(int @int) => this.Value.CompareTo(@int);

        public int Compare(CountryID x, CountryID y) => x.Equals(y) ? 0 : x.Value < y.Value ? -1 : 1;
        public int Compare(CountryID x, int @int) => x.Equals(@int) ? 0 : x.Value < @int ? -1 : 1;
        #endregion


        #region Overriden Methods
        public override bool Equals(object obj) => obj is CountryID other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
        #endregion


        #region Overloaded Operators
        public static bool operator ==(CountryID left, CountryID right) => left.Equals(right);
        public static bool operator ==(CountryID left, int @int) => left.Equals(@int);

        public static bool operator !=(CountryID left, CountryID right) => !(left == right);
        public static bool operator !=(CountryID left, int @int) => !(left == @int);


        public static bool operator >(CountryID left, CountryID right) => left.Value > right.Value;
        public static bool operator >(CountryID left, int @int) => left.Value > @int;

        public static bool operator <(CountryID left, CountryID right) => left.Value < right.Value;
        public static bool operator <(CountryID left, int @int) => left.Value < @int;


        public static bool operator <=(CountryID left, CountryID right) => left.Value <= right.Value;
        public static bool operator <=(CountryID left, int @int) => left.Value <= @int;

        public static bool operator >=(CountryID left, CountryID right) => left.Value >= right.Value;
        public static bool operator >=(CountryID left, int @int) => left.Value >= @int;
        #endregion
    }

    public sealed class clsCountry
    {
        #region Priivate Constructors
        private clsCountry() : this(ID: CountryID.Empty, CountryName: null) { }

        private clsCountry(CountryID ID, string CountryName) => (this.ID, this.CountryName) = (ID, CountryName);
        #endregion

        #region Internal Static Creation Methods
        internal static clsCountry CreateFromDB(CountryID ID, string CountryName) => new clsCountry(ID: ID, CountryName: CountryName);
        #endregion

        #region Public Properties
        public CountryID ID;
        public string CountryName;

        public static clsCountry Empty => new clsCountry();
        #endregion


        #region Aync Methods
        public static int GetCountry(string Name) => clsCountryData.GetCountry(Name);

        public static string GetCountry(CountryID ID) => clsCountryData.GetCountry(ID.Value);
        
        public static string[] GetCountriesNames() => clsCountryData.GetCountriesNames();

        public static IEnumerable<clsCountry> GetCountriesAsIEnumerable() => clsCountryData.GetCountries().ToIEnumerable();
        public static List<clsCountry> GetCountriesAsList() => clsCountryData.GetCountries().ToList();
        #endregion


        #region Async Methods
        public static async Task<int> GetCountryAsync(string Name) => await clsCountryData.GetCountryAsync(Name);

        public static async Task<string> GetCountryAsync(CountryID ID) => await clsCountryData.GetCountryAsync(ID.Value);
        
        public static async Task<string[]> GetCountriesNamesAsync() => await clsCountryData.GetCountriesNamesAsync();

        public static async Task<IEnumerable<clsCountry>> GetCountriesAsIEnumerableAsync() => (await clsCountryData.GetCountriesAsync()).ToIEnumerable();

        public static async Task<List<clsCountry>> GetCountriesAsListAsync() => (await clsCountryData.GetCountriesAsync()).ToList();
        #endregion
    }

    public static class CountryExtensions
    {
        public static IEnumerable<clsCountry> ToIEnumerable(this IEnumerable<(int ID, string CountryName)> data)
        {
            foreach ((int ID, string CountryName) item in data)
            {
                yield return clsCountry.CreateFromDB(ID: CountryID.CreateNew(item.ID), CountryName: item.CountryName);
            }
        }

        public static List<clsCountry> ToList(this IEnumerable<(int ID, string CountryName)> data)
        {
            const int numberOfCountries = 196;
            List<clsCountry> result = new List<clsCountry>(numberOfCountries);
            foreach ((int ID, string CountryName) item in data)
            {
                result.Add(clsCountry.CreateFromDB(ID: CountryID.CreateNew(item.ID), CountryName: item.CountryName));
            }

            return result;
        }
    }
}
