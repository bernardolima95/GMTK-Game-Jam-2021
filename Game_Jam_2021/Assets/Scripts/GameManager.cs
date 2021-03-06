using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Shield shield;
    public ParticleSystem explosionPrefab;
    
    public int lives = 3;
    public int score = 0;
    public float respawnTime = 3;
    public float invulnerabilityTime = 3;

    public void EnemyDestroyed(Enemy enemy){

        ParticleSystem explosion = Instantiate(this.explosionPrefab, enemy.transform.position, enemy.transform.rotation);
        explosion.Play();

        int scoreGained = (int)(1/enemy.size*10);

        this.score += scoreGained;
        this.player.AccumulateBoost(scoreGained);
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

    private void Respawn(){

        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions"); //Better practice: OnEnable() on Player.cs
        this.player.gameObject.SetActive(true);

        this.player.health = this.player.maxHealth;
        this.player.boostMeter = this.player.maxBoostMeter;
        this.player.specialMeter = 0.0f;
        this.player.specialOn = false;
        
        Invoke(nameof(TurnOnCollisions), this.invulnerabilityTime);

        this.shield.Respawn();
    }

    private void TurnOnCollisions(){
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver(){

        PlayerPrefs.SetInt("score", this.score);
        Invoke(nameof(GoToMenu), 1.5f);
    }

    private void GoToMenu(){
        SceneManager.LoadScene("Menu");
    }
}
