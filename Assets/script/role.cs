using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class role : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float jumpSpeed;
    public GameObject prefab_arrow;

    private int toward = 1;

    private Rigidbody2D rg;
    private SpriteRenderer sr;
    private Animator ani;
    private string aniName;
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentAniName();
        foreach ( string aniType in GetInput())
        {
            SetAni(aniType);
        }

    }

    void GetCurrentAniName()
    {
        AnimatorClipInfo[] aniInfo = ani.GetCurrentAnimatorClipInfo(0);
        if (aniInfo.Length == 0)
            aniName  = "";
        else
            aniName = aniInfo[0].clip.name;
    }
    List<string> GetInput()
    {
        
        float step = speed * Time.deltaTime;
        List<string> inputArr = new List<string>();
        if (isAni("role_shoot"))
        {
            return inputArr;
        }
        if (Input.GetKey(KeyCode.A))
        {
            sr.flipX = false;
            toward = -1;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(toward,0,0), step);
            inputArr.Add("left");
        }
        if (Input.GetKey(KeyCode.D))
        {
            sr.flipX = true;
            toward = 1;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(toward, 0, 0), step);
            inputArr.Add("right");

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inputArr.Add("jump");
            rg.velocity = new Vector2(0, jumpSpeed);
        }
        if (Input.GetKey(KeyCode.J))
        {
            inputArr.Add("shoot");
        }
        if (inputArr.Count == 0)
            inputArr.Add("idle");

        return inputArr;

    }
    
    void SetAni(string aniType)
    {
        if( (aniType == "left" || aniType == "right") && isAni("role_idle"))
        {
            ani.SetBool("idle", false);
            ani.SetBool("run", true);
        }
        if(aniType == "jump" && (isAni("role_idle") || isAni("role_run") || isAni("role_jump") || isAni("role_fall")) )
        {
            ani.SetBool("jump", true);
            ani.SetBool("idle", false);
            ani.SetBool("run", false);
        }
        if(aniType == "shoot")
        {
            ani.SetBool("idle", false);
            ani.SetBool("run", false);
            ani.SetBool("shoot", true);
        }
        if (aniType == "idle" && (isAni("role_run")) )
            BackToIdle();
    }

    bool isAni(string name)
    {
        if (name == aniName)
            return true;
        return false;
    }
    void BackToIdle()
    {
        ani.SetBool("idle", true);
        ani.SetBool("run", false);
        ani.SetBool("jump", false);
        ani.SetBool("jumpTop", false);
        ani.SetBool("shoot", false);
    }

    void CreateArrow()
    {
        GameObject arrow = Instantiate(prefab_arrow);
        arrow.transform.position = transform.position + new Vector3(toward * 0.2f, 0.1f, 0);
        arrow.GetComponent<arrow>().setToward(toward);
    }
    void JumpToTop()
    {
        ani.SetBool("jumpTop", true);
        ani.SetBool("jump", false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        BackToIdle();
        Debug.Log("aaa");
    }
}
