using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayBall : BaseBall
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnBallHit()
    {
        if (gameObject == null)
        {
            return;
        }
        onBallHit();
    }
}
