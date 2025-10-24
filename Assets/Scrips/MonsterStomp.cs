using UnityEngine;

public class MonsterStomp : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Weak Point")
        {
            Destroy(collision.gameObject);
        }
    }
}
