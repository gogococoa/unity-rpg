using System.Threading;
using UnityEngine;

public class CooldownExample : MonoBehaviour
{
    private SpriteRenderer sr;
    private readonly float redColorDuration = 1;

    private float currentTimeInGame;
    private float lastTimeWasDamaged;

    private Color originColor;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originColor = sr.color;
    }

    private void Update()
    {
        currentTimeInGame = Time.time;

        ChangeColorToOriginColor();
    }

    public void TakeDamage()
    {
        sr.color = Color.red;

        lastTimeWasDamaged = Time.time;
    }

    private void ChangeColorToOriginColor()
    {
        if (currentTimeInGame > lastTimeWasDamaged + redColorDuration && sr.color != originColor)
        {
            TurnOriginColor();
        }
    }

    private void TurnOriginColor()
    {
        sr.color = originColor;
    }
}
