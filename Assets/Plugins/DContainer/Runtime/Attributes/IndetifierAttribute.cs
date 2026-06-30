using System;

namespace DContainer
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class IndetifierAttribute : Attribute
    {
        public readonly object Identifier;

        public IndetifierAttribute(object identifier)
        {
            Identifier = identifier;
        }
    }
}
