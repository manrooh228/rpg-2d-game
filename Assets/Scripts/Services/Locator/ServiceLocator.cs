using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Services.Locator
{
    public class ServiceLocator : IServiceLocator
    {
        private Dictionary<Type, object> _services;

        public ServiceLocator()
        {
            _services = new Dictionary<Type, object>();
        }

        public ServiceLocator RegisterService<T>(T service)
        {
            Type serviceType = typeof(T);

            _services.TryAdd(serviceType, service);

            return this;
        }

        public T GetService<T>()
        {
            if (_services.TryGetValue(typeof(T), out object service))
            {
                return (T)service;
            }
            else
            {
                throw new Exception("Service: " + typeof(T).Name + " is not found!");
            }
        }
    }

}