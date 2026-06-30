using System;
using System.Collections.Generic;
using System.Linq;

namespace DContainer.Exceptions
{
    public sealed class ResolutionDependencyException : Exception
    {
        public readonly Type FailedType;
        public readonly List<Type> ResolutionStack;

        public ResolutionDependencyException(string message, Type failedType, IEnumerable<Type> resolutionStack, Exception innerException) : base(message, innerException)
        {
            FailedType = failedType;
            ResolutionStack = resolutionStack?.Reverse()?.ToList();
        }

        public override string Message
        {
            get
            {
                string formatStack = string.Join("-->", ResolutionStack?.Select(x => x.ToString()));
                return $"{base.Message}\n FailedTYpe: {FailedType}\n ResolutionStack: {formatStack}";
            }
        }
    }
}
