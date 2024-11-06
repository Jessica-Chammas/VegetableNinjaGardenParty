using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public AudioSource startGameSound;
    public AudioSource sliceSound;
    public AudioSource lifeLostSound;
    public AudioSource goldenCarrotSound;

    public void PlayStartGameSound()
    {
        startGameSound.Play();
    }

    public void PlaySliceSound()
    {
        sliceSound.Play();
    }

    public void PlayLifeLostSound()
    {
        lifeLostSound.Play();
    }

    public void PlayGoldenCarrotSound()
    {
        goldenCarrotSound.Play();
    }
}
