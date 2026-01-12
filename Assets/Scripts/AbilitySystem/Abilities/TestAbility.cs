using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.AbilitySystem.Abilities
{
    public class TestAbility : Ability
    {
        public string TestText { get; private set; }

        public TestAbility(string testText)
        {
            TestText = testText;
        }

        public override bool CheckCondition(Entity owner, Entity target, Vector3 location = default)
        {
            return true;
        }

        public override void ApplyCast() 
        {
            Debug.Log(TestText);

            ChangeCooldownTimer(CooldownTime);
            ChangeStatus(EAbilityStatus.Cooldown);
        }

        public override void EventTick(float deltaTick)
        {
            if(Status == EAbilityStatus.Cooldown) { 
                ChangeCooldownTimer(CooldownTime -  deltaTick);

                if (CooldownTimer <= 0f)
                    ChangeStatus(EAbilityStatus.Ready);
            }
        }
    }
}