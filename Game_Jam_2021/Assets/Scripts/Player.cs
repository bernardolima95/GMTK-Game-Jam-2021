using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    public Bullet bulletPrefab;
    public ParticleSystem boostPrefab;
    public SFXPlayer SFXPlayer;

    public float thrustSpeed = 1.0f;
    public float turnSpeed = 1.0f;
    public float maxBoostMeter = 100.0f;
    public float boostCost = 20.0f;
    public float boostAccumulationFactor = 1.0f;
    public float boostMeter;
    public float specialMeter;
    public float maxSpecialMeter = 100.0f;
    public float specialMeterAccumulation = 10.0f;
    public float specialBulletCount = 3;
    public float bulletStrayFactor = 20;
    public float specialDecayRate = 0.5f;
    public int maxHealth = 5;
    public int health;

    private Rigidbody2D _rigidbody;
    private bool _thrusting;
    private float _turnDirection;
    private Color originalColor;
    private bool recentlyRespawned;
    public bool specialOn;


    private void Awake(){
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start(){

        this.originalColor = GetComponent<Renderer>().material.color;
        SFXPlayer.PlayAmbient();

        this.boostMeter = this.maxBoostMeter;
        this.health = this.maxHealth;
        this.specialMeter = 0.0f;
        this.specialOn = false;
    }

    private void Update(){
    
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            _turnDirection = 1.0f;
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            _turnDirection = -1.0f;
        }
        else {
            _turnDirection = 0.0f;
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            Shoot();
        }

        if(Input.GetKeyDown(KeyCode.B)){
            Boost();
        }
    }

    private void FixedUpdate() {

        if(_thrusting){
            _rigidbody.AddForce(this.transform.up * this.thrustSpeed);
        }

        if(this.health < 3){
            SFXPlayer.AmbientControl("lowHealth");
        } else if(recentlyRespawned){
            SFXPlayer.AmbientControl("normal");
            recentlyRespawned = false;
        }

        if(_turnDirection != 0.0){
            _rigidbody.AddTorque(_turnDirection * this.turnSpeed);
        }

        if(this.specialMeter >= this.maxSpecialMeter && !this.specialOn){
            this.specialOn = true;
        }

        if(this.specialOn){
            this.specialMeter -= this.specialDecayRate;
            StartCoroutine("SpecialFlash");
            if(this.specialMeter <= 0.0f){
                this.specialMeter = 0.0f;
                this.specialOn = false;
            }
        }
    }

    private void Shoot(){

        if(!this.specialOn){
            this.SFXPlayer.PlaySound("shotCharlie");
            InstantiateBullet(this.transform.position, this.transform.rotation);
        }
        else{
            for(int i=0; i<this.specialBulletCount; i++){
                this.SFXPlayer.PlaySound("shotCharlie");
                InstantiateBullet(this.transform.position, this.transform.rotation);
            }
        }
    }

    public void AccumulateBoost(float accumulation){

        this.boostMeter += accumulation * this.boostAccumulationFactor;

        if(this.boostMeter > this.maxBoostMeter){
            this.boostMeter = this.maxBoostMeter;
        }
    }

    public void AccumulateSpecial(){

        this.specialMeter += this.specialMeterAccumulation;

        if(this.specialMeter > this.maxSpecialMeter){
            this.specialMeter = this.maxSpecialMeter;
        }
    }

    private void InstantiateBullet(Vector3 bulletPosition, Quaternion bulletRotation){

            Bullet bullet = Instantiate(this.bulletPrefab, bulletPosition, bulletRotation);

            if(this.specialOn){
                
                float angle = Random.Range(-bulletStrayFactor, bulletStrayFactor);
                bullet.transform.Rotate(new Vector3(0, 0, angle));
            }

            bullet.Project(bullet.transform.up);
    }

    private void Boost() {

        if(this.boostMeter >= this.boostCost){
            SFXPlayer.PlaySound("boost");
            Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
            ParticleSystem boost = Instantiate(this.boostPrefab, this.transform.position, this.transform.rotation);
            boostMeter -= this.boostCost;
            boost.Play();
            _rigidbody.AddForce(this.transform.up * this.thrustSpeed * 40);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){

        string colliderTag = collision.gameObject.tag;

        if(colliderTag == "Enemy" || colliderTag == "EnemyBullet"){

            health -= 1;

            if(this.health <= 0){
                this.health = 0;

                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = 0.0f;
                SFXPlayer.PlaySound("deathCharlie");
                SFXPlayer.AmbientControl("death");
                recentlyRespawned = true;

                this.gameObject.SetActive(false);

                FindObjectOfType<GameManager>().PlayerDied(); //Bad practice, expensive function
            }
            else {
                SFXPlayer.PlaySound("shotOnShip");
                StartCoroutine("HitFlash");
            }
        }
    }

    public IEnumerator HitFlash(){
        this.gameObject.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.GetComponent<Renderer>().material.color = originalColor;
        StopCoroutine("HitFlash");
    }

    public IEnumerator SpecialFlash(){
        this.gameObject.GetComponent<Renderer>().material.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        this.gameObject.GetComponent<Renderer>().material.color = originalColor;        
    }
}
