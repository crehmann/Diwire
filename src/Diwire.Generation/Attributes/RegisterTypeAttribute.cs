using System;

namespace Diwire.Generation.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RegisterTypeAttribute : Attribute
    {
        public RegisterTypeAttribute(Type fromType, Type toType, Lifetime lifetime = Lifetime.Singelton)
        {
            FromType = fromType ?? throw new ArgumentNullException(nameof(fromType));
            ToType = toType ?? throw new ArgumentNullException(nameof(toType));
            Lifetime = lifetime;
        }

        public Type FromType { get; }
        public Type ToType { get; }
        public Lifetime Lifetime { get; }
    }
}
