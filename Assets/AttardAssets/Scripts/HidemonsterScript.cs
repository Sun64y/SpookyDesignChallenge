using UnityEngine;

public class HidemonsterScript : MonoBehaviour
{
    //animation
    public Animator hideMonster;

    //audio clips
    public AudioSource audioSource;
    public AudioClip hide;


    private void OnTriggerEnter(Collider trigger)
    {
        //checks if it touched a projectile or a collectable
        if (trigger.tag == "GoAway")
        {
            hideMonster.SetBool("HidingTime", true);
            audioSource.clip = hide;
            audioSource.Play();
        }
    }
    private void OnTriggerExit(Collider trigger)
    {
        Invoke("Reset", 5f);
    }

    private void Reset()
    {
        hideMonster.SetBool("HidingTime", false);
    }
}
