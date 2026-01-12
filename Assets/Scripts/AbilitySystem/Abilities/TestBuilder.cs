using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AbilitySystem.Abilities
{
    public class TestBuilder : AbilityBuilder
    {
        private readonly TestConfig _config;

        public TestBuilder(TestConfig config) : base(config)
        {
            _config = config;
        }

        public override void Make()
        {
            _ability = new TestAbility(_config.TestText);

            base.Make();
        }
    }
}