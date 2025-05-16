using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using Helper.Extensions;
using DVLD_DAL.Applications.DrivingLicenseServices;
using DVLD_BLL.Users;

namespace DVLD_BLL.Applications.DrivingLicenseServices
{
    public struct LicenseID : IEquatable<LicenseID>, IComparable<LicenseID>, IComparer<LicenseID>
    {
        #region Constructors
        private LicenseID(int value) => Value = value;
        #endregion

        #region Public Properties
        public int Value { get; }
        public static LicenseID Empty => new LicenseID(0);
        #endregion


        #region Public Static Methods
        public static LicenseID CreateNew(int id) => new LicenseID(id);
        #endregion

        #region Public Methods
        public bool IsNotEmpty() => this.Value > 0;
        public bool IsEmpty() => this.Value < 1; //it equivalent to { this.Value <= 0; }

        public bool NotEquals(LicenseID other) => this.Value != other.Value;
        public bool NotEquals(int @int) => this.Value != @int;
        #endregion


        #region Interface Methods Implementation
        public bool Equals(LicenseID other) => this.Value == other.Value;
        public bool Equals(int @int) => this.Value == @int;

        public int CompareTo(LicenseID other) => this.Value.CompareTo(other.Value);
        public int CompareTo(int @int) => this.Value.CompareTo(@int);

        public int Compare(LicenseID x, LicenseID y) => x.Equals(y) ? 0 : x.Value < y.Value ? -1 : 1;
        public int Compare(LicenseID x, int @int) => x.Equals(@int) ? 0 : x.Value < @int ? -1 : 1;
        #endregion


        #region Overriden Methods
        public override bool Equals(object obj) => obj is LicenseID other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
        #endregion


        #region Overloaded Operators
        public static bool operator ==(LicenseID left, LicenseID right) => left.Equals(right);
        public static bool operator ==(LicenseID left, int @int) => left.Equals(@int);

        public static bool operator !=(LicenseID left, LicenseID right) => !(left == right);
        public static bool operator !=(LicenseID left, int @int) => !(left == @int);


        public static bool operator >(LicenseID left, LicenseID right) => left.Value > right.Value;
        public static bool operator >(LicenseID left, int @int) => left.Value > @int;

        public static bool operator <(LicenseID left, LicenseID right) => left.Value < right.Value;
        public static bool operator <(LicenseID left, int @int) => left.Value < @int;


        public static bool operator <=(LicenseID left, LicenseID right) => left.Value <= right.Value;
        public static bool operator <=(LicenseID left, int @int) => left.Value <= @int;

        public static bool operator >=(LicenseID left, LicenseID right) => left.Value >= right.Value;
        public static bool operator >=(LicenseID left, int @int) => left.Value >= @int;
        #endregion
    }

    public sealed class clsLicenseClass : IEquatable<clsLicenseClass>, IComparable<clsLicenseClass>
    {
        #region Private Constructors
        private clsLicenseClass() : this(ID: LicenseID.Empty, ClassName: string.Empty, ClassDescription: string.Empty, MinimumAllowedAge: -1, DefaultValidityLength: -1, Fees: .0f) { }

        private clsLicenseClass(LicenseID ID, string ClassName, string ClassDescription, int MinimumAllowedAge, int DefaultValidityLength, float Fees) =>
            (this.LicenseID, this.ClassName, this.ClassDescription, this.MinimumAllowedAge, this.DefaultValidityLength, this.Fees) = 
                (ID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, Fees);
        #endregion

        #region Public Static Creation Methods
        public static clsLicenseClass CreateNew(string ClassName, string ClassDescription, int MinimumAllowedAge, int DefaultValidityLength, float Fees) =>
            new clsLicenseClass(ID: LicenseID.Empty, ClassName: ClassName, ClassDescription: ClassDescription, MinimumAllowedAge: MinimumAllowedAge,
                DefaultValidityLength: DefaultValidityLength, Fees: Fees);
        #endregion

        #region Public Internal Creation Methods
        internal static clsLicenseClass CreateFromDB(LicenseID LicenseID, string ClassName, string ClassDescription, int MinimumAllowedAge, int DefaultValidityLength, float Fees) =>
            new clsLicenseClass(ID: LicenseID, ClassName: ClassName, ClassDescription: ClassDescription, MinimumAllowedAge: MinimumAllowedAge,
                DefaultValidityLength: DefaultValidityLength, Fees: Fees);
        #endregion


        #region Public Properties
        public LicenseID LicenseID { get; private set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public int MinimumAllowedAge { get; set; }
        public int DefaultValidityLength { get; set; }
        public float Fees { get; set; }

        public static clsLicenseClass Empty => new clsLicenseClass();
        #endregion


        #region Overloaded Operators
        public static bool operator ==(clsLicenseClass left, clsLicenseClass right) => left.Equals(right);
        public static bool operator !=(clsLicenseClass left, clsLicenseClass right) => !(left == right);

        public static bool operator >(clsLicenseClass left, clsLicenseClass right) => left.CompareTo(right) > 0;
        public static bool operator <(clsLicenseClass left, clsLicenseClass right) => left.CompareTo(right) < 0;

        public static bool operator >=(clsLicenseClass left, clsLicenseClass right) => left.CompareTo(right) >= 0;
        public static bool operator <=(clsLicenseClass left, clsLicenseClass right) => left.CompareTo(right) <= 0;
        #endregion


        #region Overridden Methods
        public override bool Equals(object obj) => obj is clsUser other && this.Equals(other);

        public override int GetHashCode() => (LicenseID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, Fees).GetHashCode();
        #endregion


        #region Private Methods
        private bool _isMore(clsLicenseClass other) => this.IsEmpty() ? !other.IsEmpty() : LicenseID > other.LicenseID && ClassName.CompareTo(other.ClassName) > 0 && ClassDescription.CompareTo(other.ClassDescription) > 0 && MinimumAllowedAge > other.MinimumAllowedAge && DefaultValidityLength > other.DefaultValidityLength && Fees > other.Fees;
        #endregion


        #region Public Methods
        public int CompareTo(clsLicenseClass other) => this.Equals(other) ? 0 : this._isMore(other) ? 1 : -1;

        public bool NotEquals(clsLicenseClass other) => !this.Equals(other);

        public bool Equals(clsLicenseClass other) => this.IsEmpty() ? other.IsEmpty() : LicenseID == other.LicenseID && ClassName.CompareTo(other.ClassName) == 0 && ClassDescription.CompareTo(other.ClassDescription) == 0 && MinimumAllowedAge.Equals(other.MinimumAllowedAge) && DefaultValidityLength.Equals(other.DefaultValidityLength) && Fees.Equals(other.Fees);

        public static bool IsEmpty(clsLicenseClass user) => user is null || user.IsEmpty();

        public bool IsEmpty() => LicenseID.IsEmpty() || ClassName.IsNullOrEmptyOrWhiteSpace() || ClassDescription.IsNullOrEmptyOrWhiteSpace() || MinimumAllowedAge < 1 || DefaultValidityLength < 1 || Fees < 1;
        #endregion


        #region Public Sync Methods
        //************** R **************\\
        public static DataTable GetLicenseClasss() => clsLicenseClassData.GetLicenseClasses();

        public static clsLicenseClass Find(string ClassName)
        {
            int ID = -1, MinimumAllowedAge = -1, DefaultValidityLength = -1;
            string ClassDescription = "";
            float Fees = .0f;

            bool isFound = clsLicenseClassData.Get(ref ID, ClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref Fees);

            return isFound ? clsLicenseClass.CreateFromDB(LicenseID.CreateNew(ID), ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, Fees) : clsLicenseClass.Empty;
        }

        public static clsLicenseClass Find(int ID)
        {
            int MinimumAllowedAge = -1, DefaultValidityLength = -1;
            string ClassName = "", ClassDescription = "";
            float Fees = .0f;

            bool isFound = clsLicenseClassData.Get(ID, ref ClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref Fees);

            return isFound ? clsLicenseClass.CreateFromDB(LicenseID.CreateNew(ID), ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, Fees) : clsLicenseClass.Empty;
        }


        //************** U **************\\
        public static bool Update(LicenseID ID, string ClassName, string Description, int MinimumAllowedAge, int DefaultValidityLength, float Fees) =>
            clsLicenseClassData.Update(ID.Value, ClassName, Description, MinimumAllowedAge, DefaultValidityLength, Fees);

        public static bool Update(LicenseID ID, string ClassName, string Description, float Fees) =>
            clsLicenseClassData.Update(ID.Value, ClassName, Description, Fees);


        //************** D **************\\
        public static bool Delete(LicenseID ID) => clsLicenseClassData.Delete(ID.Value);

        public static bool Delete(string ClassName) => clsLicenseClassData.Delete(ClassName);
        #endregion


        #region Public Async Methods
        //************** R **************\\
        public static async Task<DataTable> GetLicenseClassesAsync() => await clsLicenseClassData.GetLicenseClassesAsync();


        public static async Task<IEnumerable<clsLicenseClass>> GetAllAsIEnumerableAsync() =>
            (await clsLicenseClassData.GetAllAsync()).ToIEnumerable();

        public static async Task<List<clsLicenseClass>> GetAllAsListAsync() => (await clsLicenseClassData.GetAllAsync()).ToList();


        public static async Task<clsLicenseClass> FindAsync(LicenseID ID)
        {
            (string className, string classDescription, int minimumAllowedAge, int defaultValidityLength, float fees, bool isFound) = await clsLicenseClassData.GetAsync(ID.Value);

            return isFound ? clsLicenseClass.CreateFromDB(ID, className, classDescription, minimumAllowedAge, defaultValidityLength, fees) : clsLicenseClass.Empty;
        }

        public static async Task<clsLicenseClass> FindAsync(string ClassName)
        {
            (int id, string classDescription, int minimumAllowedAge, int defaultValidityLength, float fees, bool isFound) = await clsLicenseClassData.GetAsync(ClassName);

            return isFound ? clsLicenseClass.CreateFromDB(LicenseID.CreateNew(id), ClassName, classDescription, minimumAllowedAge, defaultValidityLength, fees) : clsLicenseClass.Empty;
        }


        //************** U **************\\
        public static async Task<bool> UpdateAsync(LicenseID ID, string ClassName, string Description, int MinimumAllowedAge, int DefaultValidityLength, float Fees) =>
            await clsLicenseClassData.UpdateAsync(ID.Value, ClassName, Description, MinimumAllowedAge, DefaultValidityLength, Fees);

        public static async Task<bool> UpdateAsync(LicenseID ID, string ClassName, string Description, float Fees) =>
            await clsLicenseClassData.UpdateAsync(ID.Value, ClassName, Description, Fees);


        //************** D **************\\
        public static async Task<bool> DeleteAsync(LicenseID ID) => await clsLicenseClassData.DeleteAsync(ID.Value);

        public static async Task<bool> DeleteAsync(string ClassName) => await clsLicenseClassData.DeleteAsync(ClassName);
        #endregion
    }

    public static class LicenseClassExtensions
    {
        public static IEnumerable<clsLicenseClass> ToIEnumerable(this IEnumerable<(int ID, string ClassName, string ClassDescription,
            int MinimumAllowedAge, int DefaultValidityLength, float Fees)> data)
        {
            foreach ((int ID, string ClassName, string ClassDescription, int MinimumAllowedAge, int DefaultValidityLength, float Fees) item in data)
            {
                yield return clsLicenseClass.CreateFromDB(LicenseID: LicenseID.CreateNew(item.ID), item.ClassName, 
                    ClassDescription: item.ClassDescription, item.MinimumAllowedAge, item.DefaultValidityLength, item.Fees);
            }
        }

        public static List<clsLicenseClass> ToList(this IEnumerable<(int ID, string ClassName, string ClassDescription, int MinimumAllowedAge, int DefaultValidityLength, float Fees)> data)
        {
            List<clsLicenseClass> result = new List<clsLicenseClass>(data.Count());
            foreach ((int ID, string ClassName, string ClassDescription, int MinimumAllowedAge, int DefaultValidityLength, float Fees) item in data)
            {
                result.Add(clsLicenseClass.CreateFromDB(LicenseID.CreateNew(item.ID), item.ClassName, 
                    ClassDescription: item.ClassDescription, item.MinimumAllowedAge, item.DefaultValidityLength, item.Fees));
            }

            return result;
        }
    }
}
