using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public struct ValueOption<T> : IEquatable<ValueOption<T>> where T : struct
    {
        private readonly T? _content;
        private static readonly ValueOption<T> _noneInstance = new ValueOption<T>();

        private ValueOption(T content) => _content = content;

        public static ValueOption<T> Some(T obj) => new ValueOption<T>(obj);
        public static ValueOption<T> None() => _noneInstance; // Avoid unnecessary allocations


        public Option<TResult> Map<TResult>(Func<T, TResult> map) where TResult : class =>
            _content.HasValue ? Option<TResult>.Some(map(_content.Value)) : Option<TResult>.None();
        public ValueOption<TResult> MapValue_ai<TResult>(Func<T, TResult> map) where TResult : struct =>
            _content.HasValue ? ValueOption<TResult>.Some(map(_content.Value)) : ValueOption<TResult>.None();

        public Option<TResult> MapOptional<TResult>(Func<T, Option<TResult>> map) where TResult : class =>
            _content.HasValue ? map(_content.Value) : Option<TResult>.None();
        public ValueOption<TResult> MapOptionalValue<TResult>(Func<T, ValueOption<TResult>> map) where TResult : struct =>
            _content.HasValue ? map(_content.Value) : ValueOption<TResult>.None();


        public T Reduce(T orElse) => _content ?? orElse;
        public T Reduce(Func<T> orElse) => _content ?? orElse();

        public ValueOption<T> Where(Func<T, bool> predicate) => 
            _content.HasValue && predicate(_content.Value) ? this : None();
        public ValueOption<T> WhereNot(Func<T, bool> predicate) => 
            _content.HasValue && !predicate(_content.Value) ? this : None();


        public static implicit operator ValueOption<T>(T value) => Some(value);
        public static implicit operator ValueOption<T>(T? value) => value.HasValue ? Some(value.Value) : None();


        public bool IsEmpty() => this.Equals(None());
        public bool IsNotEmpty() => this.NotEquals(None());

        public static bool IsEmpty<TVariable>(ValueOption<TVariable> valueOption) where TVariable : struct => valueOption.Equals(other: ValueOption<TVariable>.None());
        public static bool IsNotEmpty<TVariable>(ValueOption<TVariable> valueOption) where TVariable : struct => !IsEmpty(valueOption);



        public bool NotEquals(ValueOption<T> other) =>
            _content.HasValue ? other._content.HasValue && !_content.Value.Equals(other._content.Value) : other._content.HasValue;

        public bool Equals(ValueOption<T> other) =>
            _content.HasValue ? other._content.HasValue && _content.Value.Equals(other._content.Value) : !other._content.HasValue;


        public override bool Equals(object obj) => obj is ValueOption<T> option && Equals(option);
        public override string ToString() => _content.HasValue ? $"Some({_content.Value})" : "None";
        public override int GetHashCode() => _content?.GetHashCode() ?? 0;

        public static bool operator ==(ValueOption<T> a, ValueOption<T> b) => a.Equals(b);
        public static bool operator !=(ValueOption<T> a, ValueOption<T> b) => !(a == b);
    };

}
