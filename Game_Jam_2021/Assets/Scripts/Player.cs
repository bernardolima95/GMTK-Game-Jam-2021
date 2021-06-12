using UnityEngine;

public class Player : MonoBehaviour {

    public Bullet bulletPrefab;
    public ParticleSystem boostPrefab;

    public float thrustSpeed = 1.0f;
    public float turnSpeed = 1.0f;
    public float boostMeter = 100.0f;
    public float health = 5.0f;

    private Rigidbody2D _rigidbody;
    private bool _thrusting;
    private float _turnDirection;


    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
    
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            _turnDirection = 1.0f;
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            _turnDirection = -1.0f;
        }
        else {
            _turnDirection = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.B)){
            Boost();
        }
    }

    private void FixedUpdate() {

        if(_thrusting) {
            _rigidbody.AddForce(this.transform.up * this.thrustSpeed);
            this.boostMeter += .3f;
        }

        if(_turnDirection != 0.0) {
            _rigidbody.AddTorque(_turnDirection * this.turnSpeed);
        }
    }

    private void Shoot(){
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void Boost(){
        //if(FindObjectOfType<GameManager>().boostMeter >= 20){
        //    FindObjectOfType<GameManager>().PlayerBoost(); // too far gone
        //    _rigidbody.AddForce(this.transform.up * this.thrustSpeed * 20);
        //}
        if(this.boostMeter >= 20){
            PlayerBoost();
            _rigidbody.AddForce(this.transform.up * this.thrustSpeed * 20);
        }
    }

    private void PlayerBoost(){ // this feels like a bad idea
        ParticleSystem boost = Instantiate(this.boostPrefab, this.transform.position, this.transform.rotation);
        boostMeter -= 20;
        boost.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision){
        tag = collision.gameObject.tag;
        if (tag == "Enemy" || tag == "EnemyBullet"){
            health -= 1;
            
            if(this.health < 1){
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = 0.0f;

                this.gameObject.SetActive(false);

                FindObjectOfType<GameManager>().PlayerDied(); //Bad practice, expensive function
            }
        }
    }
}
