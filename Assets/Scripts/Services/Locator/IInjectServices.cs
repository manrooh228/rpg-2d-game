using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Services.Locator
{
    public interface IInjectServices
    {
        void Inject(IServiceLocator locator);
    }

}