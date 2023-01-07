using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public void OnBallHit()
    {
        onBallHit();
    }

    protected void onBallHit()
    {
        BallManager.instance.OnBallHit();
        Destroy(gameObject);
    }
}
