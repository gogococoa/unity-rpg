using UnityEngine;

public class EnemyGlobin : Enemy
{
    protected override void Attack()
    {
        base.Attack();
        StealGold();
    }

    [ContextMenu("StealGold")]
    private void StealGold()
    {
        Debug.Log(displayName + " steal money from player");
    }

}
