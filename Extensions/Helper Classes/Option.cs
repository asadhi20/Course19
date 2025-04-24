using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperClasses
{
    public struct Option<T> : IEquatable<Option<T>> where T : class
    {
        private readonly T _content;
        private static readonly Option<T> _noneInstance = new Option<T>();

        private Option(T content) => _content = content;

        public static Option<T> Some(T obj) => new Option<T>(obj);
        public static Option<T> None() => _noneInstance; // Always return the same instance


        public Option<TResult> Map_ai<TResult>(Func<T, TResult> map) where TResult : class =>
            _content is null ? Option<TResult>.None() : Option<TResult>.Some(map(_content));
        public ValueOption<TResult> MapValue<TResult>(Func<T, TResult> map) where TResult : struct =>
            _content is null ? ValueOption<TResult>.None() : ValueOption<TResult>.Some(map(_content));

        public Option<TResult> MapOptional<TResult>(Func<T, Option<TResult>> map) where TResult : class =>
            _content is null ? Option<TResult>.None() : map(_content);
        public ValueOption<TResult> MapOptionalValue<TResult>(Func<T, ValueOption<TResult>> map) where TResult : struct =>
            _content is null ? ValueOption<TResult>.None() : map(_content);


        public T Reduce(T orElse) => _content ?? orElse;
        public T Reduce(Func<T> orElse) => _content ?? orElse();

        public Option<T> Where(Func<T, bool> predicate) => 
            _content is null && !predicate(_content) ? None() : this;
        public Option<T> WhereNot(Func<T, bool> predicate) => 
            _content is null && predicate(_content) ? None() : this;

        
        public static implicit operator Option<T>(T obj) => obj is null ? None() : Some(obj);


        public bool Equals(Option<T> other) => _content is null ? other._content is null : _content.Equals(other._content);

        public override bool Equals(object obj) => obj is Option<T> option && Equals(option);
        public override int GetHashCode() => _content?.GetHashCode() ?? 0;
        public override string ToString() => _content is null ? "None" : $"Some({_content})";

        public static bool operator ==(Option<T> a, Option<T> b) => a.Equals(b);
        public static bool operator !=(Option<T> a, Option<T> b) => !(a == b);
    }

}
