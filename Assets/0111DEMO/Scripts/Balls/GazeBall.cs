using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeBall : BaseBall
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // onHover≈‹§j1.4≠ø
    public void OnHoverEnter()
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }
    public void OnHoverLeave()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
    public override void OnBallHit()
    {
        Debug.Log("GazeBall OnBallHit");
        onBallHit();
    }
}
