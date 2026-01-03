using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;

namespace Assets.Scripts.AbilitySystem
{
    public class AbilityBuilder
    {
        private AbilityConfig _config;
        protected Ability _ability;

        public AbilityBuilder(AbilityConfig config)
        {
            _config = config;
        }

        public virtual void Make()
        {
            if (_ability != null)
            {
                _ability.SetDescription(_config.Title, _config.Description, _config.DisplayImage);
                _ability.SetCooldownTime(_config.CooldownTime);
                _ability.SetKey(_config.HotKey);
                _ability.ChangeStatus(EAbilityStatus.Ready);
            }
        }

        public virtual Ability GetResult() => _ability;
    }

}