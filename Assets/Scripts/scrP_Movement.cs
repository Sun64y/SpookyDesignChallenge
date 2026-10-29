using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class scrP_Movement : MonoBehaviour
{
    public static scrP_Movement single;

    //movement
    public bool canWalk = true, canLook = true;
    Rigidbody rb;
    Vector3 direction;
    //looking
    public float speed, sensitivity;
    float tiltCamera;
    GameObject headJoint;


    //sound
    AudioSource soundFootsteps;
    float fullVolume;


    //dialogue
    TMP_Text txtDialogue;
    public float dialogueType;

    private void Awake()
    {
        if (single == null) { single = this; }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        //find things---------------------------------------------------------------
        headJoint = transform.Find("headJoint").gameObject;

        txtDialogue = GameObject.Find("txtDialogue").GetComponent<TMP_Text>();
        txtDialogue.text = "";

        soundFootsteps = GetComponent<AudioSource>();
        fullVolume = soundFootsteps.volume;
    }


    private void Update()
    {


        #region MOVEMENT===========================================================================
        if (canWalk)
        {
            //strafing
            direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

            //walk animation and sound
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) 
            { 
                soundFootsteps.volume = fullVolume;
            }
            else 
            {
                soundFootsteps.volume = 0;
            }
        }
        //if can't walk, stop moving
        else { direction = Vector3.zero; }
        rb.velocity = transform.TransformDirection(direction * speed * Time.fixedDeltaTime);

        //looking - can only look when screen not out
        if (canLook)
        {
            //look left/right
            transform.localEulerAngles += new Vector3(0, Input.GetAxis("Mouse X") * sensitivity, 0);

            //look up/down
            tiltCamera += Input.GetAxis("Mouse Y") * sensitivity;
            //lock vertical look (55 -80)
            if (-tiltCamera > 90) { tiltCamera = -90; }
            if (-tiltCamera < -90) { tiltCamera = 90; }

            headJoint.transform.localEulerAngles = new Vector3(-tiltCamera, 0, 0);
        }
        #endregion

    }

    private void OnTriggerEnter(Collider other)
    {
        //final win zone
        if (other.gameObject.tag == "finalExit")
        {
            Debug.Log("WIN");
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("scnClose");
        }

        //enemy collision
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("LOSE");
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("scnLose");
        }

        //dialogue trigger boxes
        if (other.gameObject.tag == "Dialogue")
        {
            //get info to display
            int message = other.GetComponent<DialogueHolder>().message;
            float duration = other.GetComponent<DialogueHolder>().duration;

            //display text
            StopAllCoroutines();
            StartCoroutine(Dialogue(message, duration));
            
            //remove dialogue box to prevent subsequent text
            other.gameObject.SetActive(false);
        }
    }

    public IEnumerator Dialogue(int message, float duration)
    {
        //pick message to show
        txtDialogue.text = "";
        string newDialogue = "";
        switch (message)
        {
            case 1:
                newDialogue = $"I'm a demo dialogue box.\nYou can change my text in the movement script.";
                break;
            case 2:
                newDialogue = $"([L Click] to interact)";
                break;
            case 3:
                newDialogue = $"You can pick things up ([L Click] to pick up, [R Click] to drop)\nUse the pickup prefab and add a custom mesh as a child.";
                break;
            case 4:
                newDialogue = $"What's happening? What is that? I have to hide!";
                break;
            case 5:
                newDialogue = $"That's everything! Come on Teddy, we have to get out of here!";
                break;
            default:
                newDialogue = "error";
                break;
        }

        //type out message
        foreach (char c in newDialogue) 
        {
            txtDialogue.text += c;
            yield return new WaitForSeconds(dialogueType);
        }


        //wait then delete message
        yield return new WaitForSeconds(duration);
        txtDialogue.text = "";
    }
}
