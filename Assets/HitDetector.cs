using UnityEngine;

public class HitDetector : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("剣が何かに触れた 相手の名前:" + other.gameObject.name + "タグ" + other.gameObject.tag);

        if(other.gameObject.CompareTag("Player2"))
        {
            Debug.Log("Hit!!");

        }
    }

}
