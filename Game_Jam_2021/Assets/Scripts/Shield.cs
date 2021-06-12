using UnityEngine;

public class Shield : MonoBehaviour {

    public Player player;
    public float turnSpeed = 1.0f;
    public float maxPotency = 100.0f;
    public float hitDegradation = 20.0f;
    public float potency;
    public float respawnRate = 5.0f;
    private float _previousPlayerAngle;

    private void Awake() {
        this._previousPlayerAngle = player.transform.eulerAngles.z;
    }

    private void Start(){
        this.potency = this.maxPotency;
    }

    private void Update() {
        this.transform.position = this.player.transform.position;
    }
    
    private void FixedUpdate() {

        float playerAngle = player.transform.eulerAngles.z;
        float deltaAngle = Mathf.DeltaAngle(this._previousPlayerAngle, playerAngle);

        this.transform.Rotate(new Vector3(0, 0, deltaAngle * this.turnSpeed));

        this._previousPlayerAngle = playerAngle;

        if(!this.player.gameObject.activeSelf){
            this.DespawnShield();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){

        tag = collision.gameObject.tag;

        if (tag == "Enemy" || tag == "EnemyBullet"){

            this.potency -= this.hitDegradation;
            
            if(this.potency < 0.0f){
                this.DespawnShield();
                Invoke(nameof(RespawnShield), this.respawnRate);
            }
        }
    }

    public void RespawnShield(){

        if(LayerMask.LayerToName(this.gameObject.layer) != "Shield"){
            this.potency = this.maxPotency;
        }

        this.gameObject.layer = LayerMask.NameToLayer("Shield");
        Renderer renderer = this.gameObject.GetComponent<Renderer>();
        renderer.enabled = true;

    }

    private void DespawnShield(){

        this.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        Renderer renderer = this.gameObject.GetComponent<Renderer>();
        renderer.enabled = false;
    }
}
