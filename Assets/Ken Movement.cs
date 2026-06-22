using UnityEngine;

public class KenMovement : MonoBehaviour
{
    public float speed = 5f; //横移動速度
    public float jumpPower = 7f; //ジャンプ力

    private Rigidbody2D rb;
    private bool isGrounded = true; //キャラがステージ上にいるか判定するフラグ

    public float attackTime = 0.2f; //攻撃硬直(攻撃が発生してから動けるまでの時間)
    public float specialTime  = 0.5f; //攻撃の持続時間
    private bool isAttacking = false; //攻撃判定のフラグ
    public GameObject normalHitbox; //通常技の攻撃判定
    public GameObject specialHitbox; //必殺技の攻撃判定
    public float hitboxTime = 0.2f; 

    public GameObject shield; //シールド追加
    private bool isCrouching = false; //しゃがみ判定

    public float dodgeDistance = 3f; //回避距離
    public float dodgeTime = 0.2f; //回避時間

    private bool isDodging = false; //回避判定のフラグ

    
//------------------------------------------------------------------------------

    void Start() //ゲーム開始時に1回だけ実行
    {
        rb = GetComponent<Rigidbody2D>(); //このキャラについているRigidbody2Dを探す

        if (normalHitbox != null)
        {
            normalHitbox.SetActive(false);
        }

        if (specialHitbox !=null)
        {
            specialHitbox.SetActive(false);
        }
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

        //「L」キーでシールド
        if (Input.GetKey(KeyCode.L) && !isAttacking  && !isCrouching  && isGrounded)
        {
            shield.SetActive(true);
        }
        else
        {
            shield.SetActive(false);
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


    //通常技
    System.Collections.IEnumerator NormalAttack()
    {
        isAttacking = true; 
        Debug.Log("通常技"); //メッセージを表示

        normalHitbox.SetActive(true);
        yield return new WaitForSeconds(hitboxTime); //攻撃硬直
        normalHitbox.SetActive(false);
        yield return new WaitForSeconds(attackTime);
        isAttacking = false; 
    }

    //必殺技
    System.Collections.IEnumerator SpecialAttack()
    {
        isAttacking = true;
        Debug.Log("必殺技"); //メッセージ表示

        specialHitbox.SetActive(false);
        yield return new WaitForSeconds(hitboxTime); //攻撃硬直
        specialHitbox.SetActive(false);

        yield return new WaitForSeconds(specialTime);
        isAttacking = false;
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