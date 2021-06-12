using UnityEngine;

public class Enemy : MonoBehaviour{
    
    public Sprite[] sprites;
    public Bullet bulletPrefab;

    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float speed = 50.0f;
    public float maxLifetime = 30.0f;
    public float firingRate = 1.0f;
    public float turnSpeed = 1.0f;

    private Transform _target;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;


    public void SetTrajectory(Vector2 direction){
        _rigidbody.AddForce(direction * this.speed);
    }

    private void Awake(){
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start(){
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        _target = GameObject.FindWithTag("Player").transform;

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;

        _rigidbody.mass = this.size;
        
        InvokeRepeating(nameof(Shoot), 2.0f, this.firingRate);

        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
        if(collision.gameObject.tag == "Bullet") {

            if(this.size >= 2*this.minSize) {
                CreateSplit();
                CreateSplit();

            }
            FindObjectOfType<GameManager>().EnemyDestroyed(this);
            Destroy(this.gameObject);
        }
        if(collision.gameObject.tag == "AggroCircle"){
            if(_target){
                this.transform.LookAt(_target);
            }
        }
    }

    private void Update(){
        if (Vector3.Distance(transform.position, _target.position) > 10f){
            MoveTowards(_target.position);
            RotateTowards(_target.position);
        }        
    }

    private void MoveTowards(Vector2 _target){
        transform.position = Vector2.MoveTowards(transform.position, _target, speed * Time.deltaTime);
    }
    
    private void RotateTowards(Vector2 _target){
        var offset = 90f;
        Vector2 direction = _target - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    private void Shoot(){
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(-this.transform.up);
    }

    private void CreateSplit() {
        
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Enemy half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;

        half.SetTrajectory(Random.insideUnitCircle.normalized * this.speed);
    }
}
