using UnityEngine;

public class KenMovement : MonoBehaviour
{
    public float speed = 5f; //横移動速度
    public float jumpPower = 7f; //ジャンプ力

    private Rigidbody2D rb;
    private bool isGrounded = true; //キャラがステージ上にいるか判定するフラグ

    public float attackTime = 0.2f; 
    private bool isAttacking = false; //攻撃判定のフラグ

    public float specialTime  = 0.5f;


//------------------------------------------------------------------------------

    void Start() //ゲーム開始時に1回だけ実行
    {
        rb = GetComponent<Rigidbody2D>(); //このキャラについているRigidbody2Dを探す
    }

    void Update() //毎フレーム実行
    {
        if (Input.GetKey(KeyCode.A)) //「A」キーで左に移動
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D)) //「D」キーで右に移動
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) //「SPACE」キーでジャンプ
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.J) && !isAttacking) //「J」キーで攻撃
        {
            StartCoroutine(NormalAttack());
        }

        if (Input.GetKeyDown(KeyCode.K) && !isAttacking)
        {
            StartCoroutine(SpecialAttack());
        }
    }


    //衝突判定
    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }


    //通常攻撃の関数
    System.Collections.IEnumerator NormalAttack()
    {
        isAttacking = true; 
        Debug.Log("通常技"); //メッセージを表示

        yield return new WaitForSeconds(attackTime); //攻撃硬直

        isAttacking = false; 
    }

    //必殺技の関数
    System.Collections.IEnumerator SpecialAttack()
    {
        isAttacking = true;
        Debug.Log("必殺技"); //メッセージ表示

        yield return new WaitForSeconds(specialTime); //攻撃硬直

        isAttacking = false;
    }
}