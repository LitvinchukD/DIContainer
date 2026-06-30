using System;
using System.Collections.Generic;
using DContainer.Utility;

namespace DContainer.Core
{
    public sealed class BindBuilder : IBindConfiguration, IBindScopeType, IBindImplementation, IBindContract
    {
        private readonly IDependencyRegistrator _registrator;
        private readonly BindBuilderContext _context;

        public BindBuilder(IDependencyRegistrator registrator)
        {
            if (registrator == null)
                throw new ArgumentNullException(nameof(registrator), "Dependency registrator cannot be null.");

            _registrator = registrator;
            _context = new BindBuilderContext();
        }

        public void Commit()
        {
            _registrator.Register(CreateBind());
            _context.Reset();
        }

        public IBindContract Setup()
        {
            _context.Reset();
            return this;
        }

        public IBindConfiguration AsSingleton()
        {
            _context.ScopeType = ScopeType.Singleton;
            return this;
        }

        public IBindConfiguration AsTransient()
        {
            _context.ScopeType = ScopeType.Transient;
            return this;
        }

        public IBindImplementation Bind<TContract>() where TContract : class
        {
            return Bind(typeof(TContract));
        }

        public IBindImplementation Bind(Type contractType)
        {
            if (contractType == null)
                throw new ArgumentNullException(nameof(contractType), "Contract type cannot be null.");

            ValidateContractType(contractType);
            _context.ContractType = contractType;

            return this;
        }

        public IBindScopeType FromFactory<TImplemenation>(Func<IDependencyResolver, TImplemenation> factory) where TImplemenation : class
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory), "Factory cannot be null.");

            Type impType = typeof(TImplemenation);
            ValidateImplementationType(impType);
            _context.Factory = factory;
            _context.ImplementationType = impType;

            return this;
        }

        public IBindScopeType To<TImplemenation>() where TImplemenation : class
        {
            return To(typeof(TImplemenation));
        }

        public IBindScopeType To(Type implementationType)
        {
            if (implementationType == null)
                throw new ArgumentNullException(nameof(implementationType), "Implementation type cannot be null.");

            ValidateImplementationType(implementationType);
            _context.ImplementationType = implementationType;

            return this;
        }

        public IBindScopeType ToInstance<TImplementation>(TImplementation instance) where TImplementation : class
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance),"Instance cannot be null.");

            Type impType = typeof(TImplementation);
            ValidateImplementationType(impType);
            _context.Instance = instance;
            _context.ImplementationType = impType;

            return this;
        }

        public IBindScopeType ToSlef()
        {
            ValidateImplementationType(_context.ContractType);
            _context.ImplementationType = _context.ContractType;

            return this;
        }

        public IBindConfiguration WithArgument(Type type, object value, short position)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type), "Type cannot be null.");

            if (position < 0)
                throw new ArgumentException("Position cannot be less than zero.", nameof(position));

            _context.Arguments.Add(position, new TypeValuePair(type, value));
            return this;
        }

        public IBindConfiguration WithArguments(params object[] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values), "Array values cannot be null");

            if (values.Length == 0)
                return this;

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == null)
                    continue;

                WithArgument(values[i].GetType(), values[i], (short)i);
            }

            return this;
        }

        public IBindConfiguration WithIdentifier(object identifier)
        {
            _context.Identifier = identifier;
            return this;
        }

        private Bind CreateBind()
        {
            if (_context.ContractType == null)
                throw new InvalidOperationException("Contract type in the context cannot be null.");

            if (_context.ImplementationType == null)
                throw new InvalidOperationException("Implementation type in the context cannot be null.");

            IReadOnlyDictionary<short,TypeValuePair> arguments = _context.Arguments.Count > 0 ? new Dictionary<short, TypeValuePair>(_context.Arguments) : null;

            return new Bind(_context.ContractType,
                            _context.ImplementationType, 
                            _context.Factory, arguments, 
                            _context.Instance, 
                            _context.Identifier, 
                            _context.ScopeType);
        }

        private void ValidateImplementationType(Type impType)
        {
            if (impType.IsValueType)
                throw new ArgumentException("Implementation type cannot be a value type.", nameof(impType));

            if (impType.IsAbstract || impType.IsInterface)
                throw new ArgumentException("Implementation type cannot be abstract or an interface.", nameof(impType));

            if (!_context.ContractType.IsAssignableFrom(impType))
                throw new ArgumentException("Implementation type must be assignable to the contract type.", nameof(impType));
        }

        private void ValidateContractType(Type contractType)
        {
            if (contractType.IsValueType)
                throw new ArgumentException("Contarct type cannot be a value type.", nameof(contractType));
        }
    }
}
