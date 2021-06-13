using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour{ //achei mais legal com cada som sendo sua própria audiosource, permite edição mais legal de pitch e etc
    
    public static AudioClip shotEnemy;
    public static AudioClip shotCharlie;
    public static AudioClip shotOnShield;
    public static AudioClip shotOnShip;
    public static AudioClip shotOnEnemy;
    public static AudioClip boost;
    public static AudioClip deathEnemy;
    public static AudioClip deathCharlie;
    public static AudioClip cockpitAmbience;
    public static AudioClip lowHealth;
    public static AudioClip mainMusic;
    static AudioSource audioSource;

    void Start(){

        shotCharlie = Resources.Load<AudioClip>("SFX/shotCharlie");

        audioSource = GetComponent<AudioSource>();

    }

    public static void PlaySound(string clipname){
        switch(clipname){
        
        case "shotCharlie":
            audioSource.PlayOneShot(shotCharlie, 0.3f);
            break;
        }
    }



}
