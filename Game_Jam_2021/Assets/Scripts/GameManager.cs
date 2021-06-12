using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosionPrefab;
    public ParticleSystem boostPrefab;
    
    public int lives = 3;
    public int score = 0;
    public float boostMeter = 100;
    public float respawnTime = 3;
    public float shieldRespawnTime = 10;
    public float invulnerabilityTime = 3;



    public void EnemyDestroyed(Enemy enemy){
        ParticleSystem explosion = Instantiate(this.explosionPrefab, enemy.transform.position, enemy.transform.rotation);
        explosion.Play();

        this.score += (int)(1/enemy.size*10);           
    }

    public void PlayerDied(){
        
        ParticleSystem explosion = Instantiate(this.explosionPrefab, this.player.transform.position, this.player.transform.rotation);
        explosion.Play();
        
        this.lives--;
        
        if (this.lives <= 0){
            GameOver();
        } else{
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }

    // public void PlayerBoost(){ // this feels like a bad idea
    //     ParticleSystem boost = Instantiate(this.boostPrefab, this.player.transform.position, this.player.transform.rotation);
    //     boostMeter -= 20;
    //     boost.Play();
    // }

    private void Respawn(){
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions"); //Better practice: OnEnable() on Player.cs
        this.player.gameObject.SetActive(true);
        
        Invoke(nameof(TurnOnCollisions), this.invulnerabilityTime);
    }

    private void TurnOnCollisions(){
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver(){
        this.lives = 3;
        this.score = 0;

        Invoke(nameof(Respawn), 3.0f);
    }
}
