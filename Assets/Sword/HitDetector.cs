using UnityEngine;

public class HitDetector : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    { 

        if (other.gameObject.CompareTag("Player2"))
        {
            Debug.Log("Hit!!");

        }
    }

}
