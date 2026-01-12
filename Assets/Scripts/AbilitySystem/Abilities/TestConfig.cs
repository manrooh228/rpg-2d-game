using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AbilitySystem.Abilities
{
    [CreateAssetMenu(menuName = "Scripts/Ability/Test", fileName = "TestConfig")]
    public class TestConfig : AbilityConfig
    {
        [SerializeField] public string TestText { get; private set; }

        public override AbilityBuilder GetBuilder()
        {
            return new TestBuilder(this);
        }
    }
}