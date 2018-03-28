using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pinsetter : MonoBehaviour
{
    public GameObject pinSet;
    public ParticleSystem explosion;

    private Animator animator;
    private PinCounter pinCounter;
    private AudioSource endGameCheer;
    private Canvas canvasUI;
    private Transform child;
    private Text congratulations;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        endGameCheer = GetComponent<AudioSource>();
        pinCounter = FindObjectOfType<PinCounter>();
        canvasUI = FindObjectOfType<Canvas>();

        child = canvasUI.transform.Find("Congratulations");
        congratulations = child.GetComponent<Text>();
        congratulations.enabled = false;
    }

    //Calls animation to be triggered based off of roll results from Game Manager
    public void PerformAction(ActionMaster.Action action) {

        if (action == ActionMaster.Action.Tidy)
        {
            animator.SetTrigger("tidyTrigger");
        }

        else if (action == ActionMaster.Action.Reset)
        {
            animator.SetTrigger("resetTrigger");
            pinCounter.Reset();
        }

        else if (action == ActionMaster.Action.EndTurn)
        {
            animator.SetTrigger("resetTrigger");
            pinCounter.Reset();
        }

        else if (action == ActionMaster.Action.EndGame)
        {
            congratulations.enabled = true;
            endGameCheer.Play();
        }
    }

    // Raises pins during tidy/reset animations
    public void RaisePins()
    {
        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            pin.RaiseIfStanding();
        }
    }

    // Lowers pins during tidy/reset animations
    public void LowerPins() {

        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            pin.Lower();
        }
    }

    // Renews pins during reset
    public void RenewPins() {
        Debug.Log("Renew Pins");
        Instantiate(pinSet, new Vector3(0, 20, 1829), Quaternion.identity);
    }

    // Destroys pins that are knocked off the lane
	private void OnTriggerExit(Collider other)
	{
        GameObject pinLeaving = other.gameObject;
        Vector3 pinLocation;

        if (pinLeaving.GetComponent<Pin>()) {
            pinLocation = pinLeaving.transform.position;
            Explosion(pinLocation);
            Destroy(pinLeaving);
        }
	}

    // Instantiates Particle Effect on Pin Destroy
    private void Explosion (Vector3 position) {

        float duration = explosion.main.duration;
        ParticleSystem explosionEmit = Instantiate(explosion, position, Quaternion.identity);
        Destroy(explosionEmit, duration);
    }

}