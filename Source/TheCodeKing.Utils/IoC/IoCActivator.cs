﻿using System;
using System.Linq;
using System.Reflection;
using TheCodeKing.Utils.Contract;

namespace TheCodeKing.Utils.IoC
{
    public sealed class IoCActivator
    {
        #region Constants and Fields

        private const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic |
                                                  BindingFlags.Instance;

        private readonly IoCContainer container;

        #endregion

        #region Constructors and Destructors

        public IoCActivator(IoCContainer container)
        {
            Validate.That(container).IsNotNull();

            this.container = container;
        }

        #endregion

        #region Public Methods

        public object CreateInstance(Type type)
        {
            var args = GetConstructorParameters(type);
            return Activator.CreateInstance(type,
                                            bindingFlags,
                                            null, args, null);
        }

        #endregion

        #region Methods

        private ConstructorInfo FindConstructorWithLeastParameters(Type type)
        {
            return type.GetConstructors(bindingFlags)
                .Where(o => !o.GetParameters().Any(p => !container.IsRegistered(p.ParameterType)))
                .OrderBy(o => o.GetParameters().Length).ElementAtOrDefault(0);
        }

        private object[] GetConstructorParameters(Type type)
        {
            var constructor = FindConstructorWithLeastParameters(type);
            if (constructor == null)
            {
                return null;
            }
            return constructor.GetParameters().Select(param => container.Resolve(param.ParameterType)).ToArray();
        }

        #endregion
    }
}