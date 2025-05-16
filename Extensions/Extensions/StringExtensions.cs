using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Extensions
{
    
    public static class StringExtensions
    {
        #region Public Methods
        public static string RemoveWhiteSpaces(this string @this)
        {
            if (@this.IsNullOrEmpty()) return @this;

            StringBuilder stringBuilder = new StringBuilder(@this.Length);
            for (int i = 0; i < @this.Length; i++) if (!char.IsWhiteSpace(@this[i])) stringBuilder.Append(@this[i]);

            return stringBuilder.ToString();
        }


        public static bool NotEquals(this string @this, string other) => !@this.Equals(other);


        public static bool IsNullOrEmpty(this string @this) => @this is null || @this.Length == 0;
        public static bool NotIsNullOrEmpty(this string @this) => !@this.IsNullOrEmpty();

        public static bool IsNullOrWhiteSpace(this string @this) => @this is null || @this.IsWhiteSpace();
        public static bool NotIsNullOrWhiteSpace(this string @this) => !@this.IsNullOrWhiteSpace();

        public static bool IsNullOrEmptyOrWhiteSpace(this string @this) => @this is null || @this.Length == 0 || @this.IsWhiteSpace();
        public static bool NotIsNullOrEmptyOrWhiteSpace(this string @this) => !@this.IsNullOrEmptyOrWhiteSpace();
        #endregion


        #region Private Methods
        private static bool IsWhiteSpace(this string @this)
        {
            for (int i = 0; i < @this.Length; i++) if (!char.IsWhiteSpace(@this[i])) return false;
            return true;
        }
        #endregion
    }

}
