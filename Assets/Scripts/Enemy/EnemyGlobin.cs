using UnityEngine;

public class EnemyGlobin : Enemy
{
    [ContextMenu("StealGold")]
    private void StealGold()
    {
        Debug.Log(displayName + " steal money from player");
    }
}
