using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Player player;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public void DamageEnemies() => player.DamageEnemies();

    private void DisableJumpAndMovement() => player.EnableJumpAndMove(false);
    private void EnableJumpAndMovement() => player.EnableJumpAndMove(true);
}
