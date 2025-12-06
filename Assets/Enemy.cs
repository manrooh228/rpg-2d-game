using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private float redColorDuration = 1;

    public float currentTimeInGame;
    public float lastTimeDamaged;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        currentTimeInGame = Time.time;

        if (currentTimeInGame > lastTimeDamaged + redColorDuration) 
            if (sr.color != Color.white)
                TurnWhite();

    }

    public void TakeDamage ()
    {
        sr.color = Color.red;
        lastTimeDamaged = currentTimeInGame;

        
    }

    private void TurnWhite ()
    {
        sr.color = Color.white;
    }
}
