using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Entity player;
    private void Awake()
    {
        player = GetComponentInParent<Entity>();
    }

    public void DamageEnemies() => player.DamageTargets();

    private void DisableJumpAndMovement() => player.EnableJumpAndMove(false);
    private void EnableJumpAndMovement() => player.EnableJumpAndMove(true);
}
