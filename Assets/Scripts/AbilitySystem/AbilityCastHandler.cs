using Assets.Scripts.Services.Locator;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.AbilitySystem
{
    public class AbilityCastHandler : MonoBehaviour
    {
        [SerializeField] private AbilityStorage _abilityStorage;
        [SerializeField] private Entity _ownerActor;
        [SerializeField] private LayerMask _targetsLayer;

        private List<Ability> _abilities = new();
        private Ability _currentAbility;

        private Camera _camera;

        public void Awake()
        {
            _camera = Camera.main;
            
            _abilityStorage.Init();
            _abilities.AddRange(_abilityStorage.GetAbilities());

            //_abilitydisplaying.init(_abilitystorage.getabilities());
            //_abilitydisplaying.onclickability += onclickabilitybutton;
        }
            

        public void Update()
        {
            for (int i = 0; i < _abilities.Count; ++i)
            {
                _abilities[i].EventTick(Time.deltaTime);

                if (Input.GetKeyDown(_abilities[i].HotKey))
                {
                    OnClickAbilityButton(i);
                }
            }
        }

        public void OnClickAbilityButton(int abilityIndex)
        {
            _currentAbility?.CancelCast();

            switch (_abilities[abilityIndex].Status)
            {
                case EAbilityStatus.Ready:

                    _currentAbility = _abilities[abilityIndex];
                    _currentAbility.StartCast();

                    break;
                case EAbilityStatus.Cooldown:
                    break;
                case EAbilityStatus.NeedMana:
                    break;
            }
        }

        
    }

}