using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rigid;

    private bool isJump = false;
    private bool isDoubleJump = false;


    void Awake()
    {
         anim.speed = 2;

        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameState == EGameState.End)
        {
            return;
        }

        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            // jump
            if (isJump == false)
            {
                isJump = true;

                rigid.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
                anim.SetBool("IsJump", true);
                return;
            }

            if (isDoubleJump == false)
            {
                isDoubleJump = true;

                rigid.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
                anim.SetTrigger("IsDoubleJump");
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isJump = false;
            isDoubleJump = false;

            anim.SetBool("IsJump", false);
        }
        else if (col.gameObject.CompareTag("Enemy"))
        {
            isJump = false;
            isDoubleJump = false;

            GameManager.Instance.OnChangeStatEvent.Invoke(EGameState.End);
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Item"))
        {
            GameManager.Instance.AddScore(10);
            GameObject.Destroy(col.gameObject);
        }
        else if (col.gameObject.CompareTag("Finish"))
        {
            GameManager.Instance.OnChangeStatEvent.Invoke(EGameState.NextStage);
        }
    }
}
