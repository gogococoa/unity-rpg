using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected string displayName;

    [ContextMenu("MoveAround")]
    private void MoveAround()
    {
        Debug.Log(displayName + " Move " + speed + " speed");
    }

    [ContextMenu("Attack")]
    private void Attack() {
        Debug.Log(displayName + " Attach " + "player");
    }

    [ContextMenu("TakeDamage")]
    public void TakeDamage() {
        Debug.Log(displayName + " take some damage");
    }
}
