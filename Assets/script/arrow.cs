using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public float speed;

    private int toward;
    private Rigidbody2D rg;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        if(toward == -1)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
        rg.velocity = new Vector2(toward * speed, 0);
    }

    public void setToward(int setToward)
    {
        toward = setToward;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
