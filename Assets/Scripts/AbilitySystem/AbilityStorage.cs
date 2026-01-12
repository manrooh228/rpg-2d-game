using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

namespace Assets.Scripts.AbilitySystem
{
    public class AbilityStorage : MonoBehaviour
    {
        [SerializeField] private AbilityConfig[] _abilityConfigs;
        [SerializeField] private Entity _owner;


        private List<Ability> _abilities = new();

        public void Init()
        {
            for (int i = 0; i < _abilityConfigs.Length; ++i)
            {
                var builder = _abilityConfigs[i].GetBuilder();

                builder.Make();
                var ability = builder.GetResult();

                ability.Added(_owner); //25
                _abilities.Add(ability);
            }
        }

        public Ability[] GetAbilities() => _abilities.ToArray();

    }
}