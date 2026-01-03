using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Services.Locator
{
    public interface IServiceLocator
    {
        T GetService<T>();
    }

}