using UnityEngine;
using System.Collections;

public class SwordMovement : MonoBehaviour
{
    public float speed = 4.0f; //移動速度
    public float jumpPower = 5.0f; //ジャンプ力


    //攻撃に関する設定
    public GameObject swordHitbox; //攻撃判定の四角を入れる
    public float attackDuration = 0.3f; //攻撃判定の出ている時間
    private bool isAttacking = false; //攻撃中かどうか

    private Rigidbody2D rb; //重力
    private bool isGrounded = true; //着地判定

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if(swordHitbox != null)
        {
            swordHitbox.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(isAttacking)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }


        //キーボードの左右入力を取得
        float moveInput = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);


        //スペースキー＆地面にいる時にジャンプ可
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            isGrounded = false;
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(AttackRoutine());
        }

    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;// 攻撃中
        swordHitbox.SetActive(true);// 攻撃判定を出現

        //時間を止める
        yield return new WaitForSeconds(attackDuration);

        swordHitbox.SetActive(false); //時間が来たら攻撃判定を消す
        isAttacking = false; //攻撃終わり
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true; //地面に着地
    }
    
}
