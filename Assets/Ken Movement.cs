using UnityEngine;

public class KenMovement : MonoBehaviour
{
    public float speed = 5f; //横移動速度
    public float jumpPower = 7f; //ジャンプ力

    private Rigidbody2D rb;
    private bool isGrounded = true; //キャラがステージ上にいるか判定するフラグ

    public float attackTime = 0.2f; //攻撃硬直(攻撃が発生してから動けるまでの時間)
    public float specialTime  = 0.5f; //攻撃硬直
    private bool isAttacking = false; //攻撃判定のフラグ

    public GameObject shield; //シールド追加


    


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

        if (Input.GetKeyDown(KeyCode.J) && !isAttacking) //「J」キーで通常技
        {
            StartCoroutine(NormalAttack());
        }

        if (Input.GetKeyDown(KeyCode.K) && !isAttacking) //「K」キーで必殺技
        {
            StartCoroutine(SpecialAttack());
        }

        if (Input.GetKey(KeyCode.S)) //「S」キーでしゃがみ
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        }
        else 
        {
            transform.localScale = new Vector3(1.5f, 2.5f, 1f);
        }

        if (Input.GetKey(KeyCode.L))
        {
            shield.SetActive(true);
        }
        else
        {
            shield.SetActive(false);
        }
    }


    //衝突判定
    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }


    //通常技
    System.Collections.IEnumerator NormalAttack()
    {
        isAttacking = true; 
        Debug.Log("通常技"); //メッセージを表示

        yield return new WaitForSeconds(attackTime); //攻撃硬直

        isAttacking = false; 
    }

    //必殺技
    System.Collections.IEnumerator SpecialAttack()
    {
        isAttacking = true;
        Debug.Log("必殺技"); //メッセージ表示

        yield return new WaitForSeconds(specialTime); //攻撃硬直

        isAttacking = false;
    }
}