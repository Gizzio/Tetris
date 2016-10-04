using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    private ParticleSystem ps;


    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        if (ps)
        {
            if (!ps.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}

