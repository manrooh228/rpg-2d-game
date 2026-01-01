using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.AbilitySystem
{
    public class Hook : AbilityConfig
    {
        [SerializeField] private float hookRange = 10f;
        private void Awake()
        {
            Title = "Hook";
            Description = "Hooks players in the range of" + hookRange;

        }
    }
}