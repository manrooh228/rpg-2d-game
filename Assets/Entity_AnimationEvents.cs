using UnityEngine;

public class Entity_AnimationEvents : MonoBehaviour
{
    private Entity entity;
    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    public void DamageTargets() => entity.DamageTargets();

    private void DisableJumpAndMovement() => entity.EnableJumpAndMove(false);
    private void EnableJumpAndMovement() => entity.EnableJumpAndMove(true);
}
