using System;
using System.Collections.Generic;
using DContainer.Utility;

namespace DContainer.Core
{
    public sealed class BindBuilderContext
    {
        public readonly Dictionary<short, TypeValuePair> Arguments;

        public Type ContractType;
        public Type ImplementationType;
        public Func<IDependencyResolver, object> Factory;
        public object Instance;
        public object Identifier;
        public ScopeType ScopeType;

        public BindBuilderContext()
        {
            Arguments = new Dictionary<short, TypeValuePair>(5);
        }

        public void Reset()
        {
            Arguments.Clear();

            ContractType = null;
            ImplementationType = null;
            Factory = null;
            Instance = null;
            Identifier = null;
            ScopeType = ScopeType.Transient;
        }
    }
}
