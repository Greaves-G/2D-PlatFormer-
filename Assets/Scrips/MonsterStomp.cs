using UnityEngine;

public class MonsterStomp : MonoBehaviour
{
    [SerializeField] private AudioClip StompFX;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Weak Point")
        {
            SoundFX.instance.PlaySoundFXClip(StompFX, transform, 1f);

            Destroy(collision.gameObject);
        }
    }
}
