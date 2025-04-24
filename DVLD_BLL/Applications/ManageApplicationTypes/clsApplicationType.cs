using System.Collections.Generic;
using System;
using System.Data;
using System.Threading.Tasks;
using DVLD_DAL.Applications.ManageApplicationTypes;

namespace DVLD_BLL.Applications.ManageApplicationTypes
{
    public struct ApplicationTypeID : IEquatable<ApplicationTypeID>, IComparable<ApplicationTypeID>, IComparer<ApplicationTypeID>
    {
        #region Constructors
        private ApplicationTypeID(int value) => Value = value;
        #endregion

        #region Public Properties
        public int Value { get; }
        public static ApplicationTypeID Empty => new ApplicationTypeID(0);
        #endregion


        #region Public Static Methods
        public static ApplicationTypeID CreateNew(int id) => new ApplicationTypeID(id);
        #endregion

        #region Public Methods
        public bool IsNotEmpty() => this.Value > 0;
        public bool IsEmpty() => this.Value < 1; //it equivalent to { this.Value <= 0; }

        public bool NotEquals(ApplicationTypeID other) => this.Value != other.Value;
        public bool NotEquals(int @int) => this.Value != @int;
        #endregion


        #region Interface Methods Implementation
        public bool Equals(ApplicationTypeID other) => this.Value == other.Value;
        public bool Equals(int @int) => this.Value == @int;

        public int CompareTo(ApplicationTypeID other) => this.Value.CompareTo(other.Value);
        public int CompareTo(int @int) => this.Value.CompareTo(@int);

        public int Compare(ApplicationTypeID x, ApplicationTypeID y) => x.Equals(y) ? 0 : x.Value < y.Value ? -1 : 1;
        public int Compare(ApplicationTypeID x, int @int) => x.Equals(@int) ? 0 : x.Value < @int ? -1 : 1;
        #endregion


        #region Overriden Methods
        public override bool Equals(object obj) => obj is ApplicationTypeID other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
        #endregion


        #region Overloaded Operators
        public static bool operator ==(ApplicationTypeID left, ApplicationTypeID right) => left.Equals(right);
        public static bool operator ==(ApplicationTypeID left, int @int) => left.Equals(@int);

        public static bool operator !=(ApplicationTypeID left, ApplicationTypeID right) => !(left == right);
        public static bool operator !=(ApplicationTypeID left, int @int) => !(left == @int);


        public static bool operator >(ApplicationTypeID left, ApplicationTypeID right) => left.Value > right.Value;
        public static bool operator >(ApplicationTypeID left, int @int) => left.Value > @int;

        public static bool operator <(ApplicationTypeID left, ApplicationTypeID right) => left.Value < right.Value;
        public static bool operator <(ApplicationTypeID left, int @int) => left.Value < @int;


        public static bool operator <=(ApplicationTypeID left, ApplicationTypeID right) => left.Value <= right.Value;
        public static bool operator <=(ApplicationTypeID left, int @int) => left.Value <= @int;

        public static bool operator >=(ApplicationTypeID left, ApplicationTypeID right) => left.Value >= right.Value;
        public static bool operator >=(ApplicationTypeID left, int @int) => left.Value >= @int;
        #endregion
    }

    public sealed class clsApplicationType
    {
        #region Public Sync Methods
        //************** R **************\\
        public static DataTable GetApplicationTypes() => clsApplicationTypeData.GetApplicationTypes();

        public static ApplicationTypeID Find(string Title) => ApplicationTypeID.CreateNew(clsApplicationTypeData.Get(Title));

        public static string Find(ApplicationTypeID ID) => clsApplicationTypeData.Get(ID.Value);


        //************** U **************\\
        public static bool Update(ApplicationTypeID ID, float Fees) => clsApplicationTypeData.Update(ID.Value, Fees);
        
        public static bool Update(string Title, float Fees) => clsApplicationTypeData.Update(Title, Fees);

        public static bool Update(ApplicationTypeID ID, string Title, float Fees) => clsApplicationTypeData.Update(ID.Value, Title, Fees);
        #endregion


        #region Public Async Methods
        //************** R **************\\
        public static async Task<DataTable> GetApplicationTypesAsync() => await clsApplicationTypeData.GetApplicationTypesAsync();

        public static async Task<ApplicationTypeID> FindAsync(string Title) => ApplicationTypeID.CreateNew(await clsApplicationTypeData.GetAsync(Title));

        public static async Task<string> FindAsync(ApplicationTypeID ID) => await clsApplicationTypeData.GetAsync(ID.Value);


        //************** U **************\\
        public static async Task<bool> UpdateAsync(ApplicationTypeID ID, float Fees) => await clsApplicationTypeData.UpdateAsync(ID.Value, Fees);

        public static async Task<bool> UpdateAsync(string Title, float Fees) => await clsApplicationTypeData.UpdateAsync(Title, Fees);

        public static async Task<bool> UpdateAsync(ApplicationTypeID ID, string Title, float Fees) => 
            await clsApplicationTypeData.UpdateAsync(ID.Value, Title, Fees);
        #endregion
    }
}
