﻿using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.MvcFramework
{
  public  class ServiceCollection : IServiceCollection
    {
        private Dictionary<Type, Type> dependencyContainer = new Dictionary<Type, Type>();
        public void Add<TSource, TDestrination>()
        {
            this.dependencyContainer[typeof(TSource)] = typeof(TDestrination);

        }
        //Dependence injections
        public object CreateInstance(Type type)
        {
            if (this.dependencyContainer.ContainsKey(type))
            {
                type = this.dependencyContainer[type];
            }
            var constructor = type.GetConstructors().OrderBy(x => x.GetParameters().Count()).FirstOrDefault();
            var parameters = constructor.GetParameters();
            var parameterValues = new List<object>();
            foreach (var parameter in parameters)
            {
                var parameterValue = CreateInstance(parameter.ParameterType);
                parameterValues.Add(parameterValue);
            }
            var obj = constructor.Invoke(parameterValues.ToArray());
            return obj;
        }
    }
}