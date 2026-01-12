using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.AbilitySystem
{
    public class AbilityConfig : ScriptableObject
    {
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite DisplayImage { get; private set; }
        [field: SerializeField] public float CooldownTime { get; private set; }
        //[field: SerializeField] public float ManaCost { get; private set; }
        [field: SerializeField] public KeyCode HotKey { get; private set; }

        public virtual AbilityBuilder GetBuilder() => new AbilityBuilder(this);
    }

}