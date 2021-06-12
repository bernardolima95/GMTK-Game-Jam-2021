using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    public Bullet bulletPrefab;
    public ParticleSystem boostPrefab;

    public float thrustSpeed = 1.0f;
    public float turnSpeed = 1.0f;
    public float maxBoostMeter = 100.0f;
    public float boostCost = 20.0f;
    public float boostMeter;
    public int maxHealth = 5;
    public int health;

    private Rigidbody2D _rigidbody;
    private bool _thrusting;
    private float _turnDirection;
    private Color originalColor;


    private void Awake(){
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start(){

        this.originalColor = GetComponent<Renderer>().material.color;

        this.boostMeter = this.maxBoostMeter;
        this.health = this.maxHealth;
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

            this.boostMeter += .3f;
            if(this.boostMeter > this.maxBoostMeter){
                this.boostMeter = this.maxBoostMeter;
            }
        }

        if(_turnDirection != 0.0){
            _rigidbody.AddTorque(_turnDirection * this.turnSpeed);
        }
    }

    private void Shoot(){
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void Boost() {
       
        if(this.boostMeter >= this.boostCost){

            ParticleSystem boost = Instantiate(this.boostPrefab, this.transform.position, this.transform.rotation);
            boostMeter -= this.boostCost;
            boost.Play();
            _rigidbody.AddForce(this.transform.up * this.thrustSpeed * 20);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){

        string colliderTag = collision.gameObject.tag;

        if(colliderTag == "Enemy" || colliderTag == "EnemyBullet"){

            health -= 1;

            if(this.health < 1){
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = 0.0f;

                this.gameObject.SetActive(false);

                FindObjectOfType<GameManager>().PlayerDied(); //Bad practice, expensive function
            }
            else {
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
}
