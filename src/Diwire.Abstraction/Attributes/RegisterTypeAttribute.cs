using System;

namespace Diwire.Abstraction.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RegisterTypeAttribute : Attribute
    {
        public RegisterTypeAttribute(Type fromType, Type toType, Lifetime lifetime = Lifetime.Singleton)
        {
            FromType = fromType ?? throw new ArgumentNullException(nameof(fromType));
            ToType = toType ?? throw new ArgumentNullException(nameof(toType));
            Lifetime = lifetime;
        }

        public RegisterTypeAttribute(Type type, Lifetime lifetime = Lifetime.Singleton)
        {
            FromType = ToType = type ?? throw new ArgumentNullException(nameof(type));
            Lifetime = lifetime;
        }

        public Type FromType { get; }
        public Type ToType { get; }
        public Lifetime Lifetime { get; }
    }
}
