using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperClasses.Extensions
{
    public static class OptionalExtensions
    {
        public static Option<T> ToOption<T>(this T obj) where T : class =>
            obj is null ? Option<T>.None() : Option<T>.Some(obj);

        public static Option<T> Where<T>(this T obj, Func<T, bool> predicate) where T : class =>
            obj is null && !predicate(obj) ? Option<T>.None() : Option<T>.Some(obj);

        public static Option<T> WhereNot<T>(this T obj, Func<T, bool> predicate) where T : class =>
            obj is null && predicate(obj) ? Option<T>.None() : Option<T>.Some(obj);

        public static IEnumerable<T> AsEnumerable<T>(this Option<T> option) where T : class
        {
            if (!option.Equals(Option<T>.None())) yield return option.Reduce(() => default(T));
        }

        public static IEnumerable<T> AsEnumerable<T>(this Option<T> option, Func<T> func) where T : class
        {
            if (!option.Equals(Option<T>.None())) yield return option.Reduce(func);
        }
    };

}
