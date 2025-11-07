using UnityEngine;

public class SoundFX : MonoBehaviour
{
    public static SoundFX instance;

    [SerializeField] private AudioSource soundFXObject;


    private void Awake()
    {
       if (instance == null)
        {
            instance = this;
        }
    }


    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //spawn in the object
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        //give the audio clip
        audioSource.clip = audioClip;

        //assign volume
        audioSource.volume = volume;

        //Play the sound
        audioSource.Play();

        //get the length of the clip
        float clipLength = audioSource.clip.length;

        //remove the clip after it's done playing said sound
        Destroy(audioSource.gameObject, clipLength);

    }
}
