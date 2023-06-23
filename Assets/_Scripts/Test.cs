using UnityEngine;


public class Test : MonoBehaviour
{
    [SerializeField] private AudioClip[] damageSoundClips;
    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.G))
        // {
        //     EventManager.GameOver?.Invoke();
        // }
        
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     print("here");
        //     EventManager.GameOver?.Invoke();
        // }

        // if (Input.GetKeyDown(KeyCode.C))
        // {
        //     CreateCard();
        // }

        //     if (Input.GetKeyDown(KeyCode.Space))
        //     {
        //         SetCardsPosition();
        //     }

        // if (Input.GetKeyDown(KeyCode.L))
        // {
        //     SoundFXManager.Instance.PlayRandomSoundFXClip(damageSoundClips,transform);
        // }
    }
}