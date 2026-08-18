using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    // call by animation trigger
    private void DisableMovementAndJump()
    {
        // call method from "Player" script
        // that method should stop movement of the player object
        player.EnableMovementAndJump(false);
    }

    // call by animation trigger
    private void EnableMovementAndJump()
    {
        player.EnableMovementAndJump(true);
    }

    public void DamageEnemies()
    {
        player.DamageEnemies();
    }
}
