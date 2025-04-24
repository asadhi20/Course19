using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperClasses.Extensions
{
    public static class ValueOptionExtensions
    {
        public static ValueOption<T> ToValueOption<T>(this T value) where T : struct =>
            ValueOption<T>.Some(value);
        public static ValueOption<T> ToValueOption<T>(this T? value) where T : struct =>
            value.HasValue ? ValueOption<T>.Some(value.Value) : ValueOption<T>.None();


        public static ValueOption<T> Where<T>(this ValueOption<T> option, Func<T, bool> predicate) where T : struct =>
            option.Where(predicate);
        public static ValueOption<T> WhereNot<T>(this ValueOption<T> option, Func<T, bool> predicate) where T : struct =>
            option.WhereNot(predicate);

        public static ValueOption<T> Where<T>(this T? value, Func<T, bool> predicate) where T : struct =>
            value.HasValue && predicate(value.Value) ? ValueOption<T>.Some(value.Value) : ValueOption<T>.None();
        public static ValueOption<T> WhereNot<T>(this T? value, Func<T, bool> predicate) where T : struct =>
            value.HasValue && !predicate(value.Value) ? ValueOption<T>.Some(value.Value) : ValueOption<T>.None();


        public static IEnumerable<T> AsEnumerable<T>(this ValueOption<T> option) where T : struct
        {
            if (!option.Equals(ValueOption<T>.None())) yield return option.Reduce(() => default(T));
        }
        
        public static IEnumerable<T> AsEnumerable<T>(this ValueOption<T> option, Func<T> orElse) where T : struct
        {
            if (!option.Equals(ValueOption<T>.None())) yield return option.Reduce(orElse);
        }
        
        public static IEnumerable<T> AsEnumerable<T>(this ValueOption<T> option, T orElse) where T : struct
        {
            if (!option.Equals(ValueOption<T>.None())) yield return option.Reduce(orElse);
        }

    };

}
