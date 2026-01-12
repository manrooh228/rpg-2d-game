using Assets.Scripts.Services.Locator;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.FilePathAttribute;

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

            if(_currentAbility != null)
            {
                // смещение на 1.5 единицы в сторону взгляда
                float offset = 1.5f;
                float distance = 2.0f;
                Vector2 direction = Vector2.right * _ownerActor.GetFacingDir();
                Vector3 castLocation = _ownerActor.transform.position + new Vector3(_ownerActor.GetFacingDir() * offset, 0, 0); ;

                // Рисуем луч в окне Scene
                Debug.DrawRay(castLocation, direction * distance, Color.red);

                // Твой реальный рейкаст
                RaycastHit2D hit = Physics2D.Raycast(castLocation, direction, distance, _targetsLayer);
                Entity target = hit.collider != null ? 
                    hit.collider.GetComponent<Entity>() : 
                    null;
                

                // Проверяем условия (хватает ли дистанции и т.д.)
                if (_currentAbility.CheckCondition(_ownerActor, target, castLocation))
                {   
                    if(target != null)
                    {
                        Debug.Log("target not found");
                    }
                    // Если игрок нажимает "подтверждение" (например, ЛКМ или повторно HotKey)
                    if (Input.GetMouseButtonDown(0))
                    {
                        _currentAbility.ApplyCast();
                        _currentAbility = null;
                    }
                }

                if (Input.GetMouseButtonDown(1))
                {
                    _currentAbility.CancelCast();
                    _currentAbility = null;
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