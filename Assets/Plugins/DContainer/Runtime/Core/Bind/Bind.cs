using System;
using System.Collections.Generic;
using DContainer.Utility;

namespace DContainer.Core
{
    public sealed class Bind
    {
        public readonly Type ContractType;
        public readonly Type ImplementationType;
        public readonly Func<IDependencyResolver, object> Factory;
        public readonly IReadOnlyDictionary<short, TypeValuePair> Arguments;
        public readonly object Instance;
        public readonly object Identifier;
        public readonly ScopeType ScopeType;

        public Bind(
            Type contractType, 
            Type implementationType, 
            Func<IDependencyResolver, object> factory, 
            IReadOnlyDictionary<short, TypeValuePair> arguments,
            object instance,
            object identifier, 
            ScopeType scopeType)
        {
            ContractType = contractType;
            ImplementationType = implementationType;
            Factory = factory;
            Arguments = arguments;
            Instance = instance;
            Identifier = identifier;
            ScopeType = scopeType;
        }
    }
}
