using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour{
    
    public AudioSource shotEnemy;
    public AudioSource shotCharlie;

    public AudioSource shotOnShield;
    public AudioSource shotOnShip;

    public AudioSource shieldDespawn;
    public AudioSource lowHealth;
    public AudioSource deathEnemy;
    public AudioSource deathCharlie;

    public AudioSource cockpitAmbience;
    public AudioSource mainMusic;
    public AudioSource boost;

    static AudioClip clipShotCharlie, clipShotEnemy, 
                    clipShotOnShield, clipShotOnShip,
                    clipLowHealth, clipShieldDespawn, clipDeathCharlie, clipDeathEnemy,
                    clipCockpitAmbience, clipMainMusic, clipBoost;


    public void Start(){
        clipShotCharlie = Resources.Load<AudioClip>("SFX/shotCharlie");
        clipShotEnemy = Resources.Load<AudioClip>("SFX/shotEnemy");

        clipShotOnShield = Resources.Load<AudioClip>("SFX/shotOnShield");
        clipShotOnShip = Resources.Load<AudioClip>("SFX/shotOnShip");

        clipShieldDespawn = Resources.Load<AudioClip>("SFX/shieldDespawn");
        clipDeathCharlie = Resources.Load<AudioClip>("SFX/shotEnemy");
        clipDeathEnemy = Resources.Load<AudioClip>("SFX/deathEnemy");
        
        clipCockpitAmbience = Resources.Load<AudioClip>("SFX/cockpitAmbience");
        clipLowHealth = Resources.Load<AudioClip>("SFX/lowHealth");

        clipMainMusic = Resources.Load<AudioClip>("SFX/mainMusic");
        clipBoost = Resources.Load<AudioClip>("SFX/boost");
    }

    public void PlayAmbient(){

        cockpitAmbience.Play();
        lowHealth.Play();
        mainMusic.Play();
        AmbientControl("normal");
    }

    public void AmbientControl(string state){ 
        switch(state){
            
            case "normal":
                cockpitAmbience.volume = 0.2f;
                lowHealth.volume = 0.0f;
                mainMusic.volume = 0.2f;
                break;
            
            case "lowHealth":
                cockpitAmbience.volume = 0.1f;
                lowHealth.volume = 0.3f;
                mainMusic.volume = 0.1f;
                break;
            
            case "death":
                cockpitAmbience.volume = 0.0f;
                lowHealth.volume = 0.0f;
                mainMusic.volume = 0.1f;
                break;

        }
    }

    public void PlaySound(string clipname){
        switch(clipname){
        
            case "shieldDespawn":
                shieldDespawn.PlayOneShot(clipShieldDespawn);
                break;

            case "shotCharlie":
                shotCharlie.PlayOneShot(clipShotCharlie);
                break;

            case "shotEnemy":
                shotEnemy.PlayOneShot(clipShotEnemy);
                break;

            case "shotOnShield":
                shotOnShield.PlayOneShot(clipShotOnShield); 
                break;

            case "shotOnShip":
                shotOnShip.PlayOneShot(clipShotOnShield);
                break;

            case "deathEnemy":
                deathEnemy.PlayOneShot(clipDeathEnemy);
                break;

            case "deathCharlie":
                deathCharlie.PlayOneShot(clipDeathCharlie);
                break;

            case "boost":
                boost.PlayOneShot(clipBoost);
                break;   
        }

    }



}
