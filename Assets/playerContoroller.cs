using UnityEngine;
using System.Collections;

public class playerContoroller : MonoBehaviour
{
    public float speed = 5f; //横移動速度
    public float jumpPower = 7f; //ジャンプ力

    private Rigidbody2D rb;
    private bool isGrounded = true; //キャラがステージ上にいるか判定するフラグ

    public GameObject attackHitbox; //攻撃判定の四角を入れる
    public float attackDuration = 0.3f; //攻撃判定の出ている時間
    private bool isAttacking = false; //攻撃中かどうか

    private bool isCrouching = false; //しゃがみ判定

    public float dodgeDistance = 3f; //回避距離
    public float dodgeTime = 0.2f; //回避時間

    private bool isDodging = false; //回避判定のフラグ

    
//------------------------------------------------------------------------------

    void Start() //ゲーム開始時に1回だけ実行
    {
        rb = GetComponent<Rigidbody2D>(); //このキャラについているRigidbody2Dを探す

        if (attackHitbox != null)
        {
            attackHitbox.SetActive(false);
        }

    }

    void Update() //毎フレーム実行
    {
        if (isAttacking)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }
        //キーボードの左右入力を取得
        float moveInput = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) //「SPACE」キーでジャンプ
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            isGrounded = false;
        }

     

        if (Input.GetKey(KeyCode.S) && isGrounded) //「S」キーでしゃがみ
        {
            isCrouching = true;
            transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        }
        else 
        {
            isCrouching = false;
            transform.localScale = new Vector3(1.5f, 2.5f, 1f);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(AttackRoutine());
        }


        //「LeftShift」キーで横回避
        if (Input.GetKeyDown(KeyCode.LeftShift)
        && isGrounded
        && !isCrouching
        && !isAttacking
        && !isDodging   )

        {
            if (Input.GetKey(KeyCode.A)) //「A」キーで左回避
            {
                StartCoroutine(Dodge(-1));
            }
            
            if (Input.GetKey(KeyCode.D)) //「D」キーで右回避
            {
                StartCoroutine(Dodge(1));
            }
            
        }
    }

//---------------------------------------------------------------------------------------
    
    //衝突判定
    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }


    IEnumerator AttackRoutine()
    {
        isAttacking = true;// 攻撃中
        attackHitbox.SetActive(true);// 攻撃判定を出現

        //時間を止める
        yield return new WaitForSeconds(attackDuration);

        attackHitbox.SetActive(false); //時間が来たら攻撃判定を消す
        isAttacking = false; //攻撃終わり
    }

    //横回避
    System.Collections.IEnumerator Dodge(float direction)
    {
        isDodging = true;
        
        transform.position += new Vector3(
            direction * dodgeDistance,
            0,
            0
        );

        yield return new WaitForSeconds(dodgeTime);

        isDodging = false;
    }
}