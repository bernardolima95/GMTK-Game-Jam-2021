using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour {

    public Player player;
    public SFXPlayer SFXPlayer;
    public float turnSpeed = 1.0f;
    public float maxPotency = 100.0f;
    public float hitDegradation = 20.0f;
    public float rechargeFactor = 1.0f;
    public float potency;
    public float respawnRate = 5.0f;
    private float _previousPlayerAngle;
    private Vector3 _previousPlayerPosition;
    private Color originalColor;

    private void Awake() {
        this._previousPlayerAngle = player.transform.eulerAngles.z;
    }

    private void Start(){
        this.originalColor = GetComponent<Renderer>().material.color;
        this.potency = this.maxPotency;
    }

    private void Update() {
        this.transform.position = this.player.transform.position;
    }
    
    private void FixedUpdate() {

        float playerAngle = this.player.transform.eulerAngles.z;
        float deltaAngle = Mathf.DeltaAngle(this._previousPlayerAngle, playerAngle);

        this.transform.Rotate(new Vector3(0, 0, deltaAngle * this.turnSpeed));

        this._previousPlayerAngle = playerAngle;

        Vector3 playerPosition = this.player.transform.position;
        Vector3 deltaPosition = playerPosition - this._previousPlayerPosition;

        this._previousPlayerPosition = playerPosition;

        this.Recharge(deltaPosition.magnitude);

        if(LayerMask.LayerToName(this.gameObject.layer) == "Ignore Collisions"){
            
            this.Recharge(0.5f);
            
            if(this.potency >= this.maxPotency){
                this.Respawn();
            }
        }

        if(!this.player.gameObject.activeSelf){
            this.Despawn();
            this.potency = 0.0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){

        string colliderTag = collision.gameObject.tag;

        if (colliderTag == "Enemy" || colliderTag == "EnemyBullet"){

            this.potency -= this.hitDegradation;
            this.player.AccumulateSpecial();
            
            if(this.potency < 0.0f){
                this.potency = 0.0f;
                SFXPlayer.PlaySound("shieldDespawn");
                this.Despawn();
            } else {
                StartCoroutine("HitFlash");
            }
        }
    }

    public void Recharge(float recharge){

        this.potency += recharge * this.rechargeFactor;

        if(this.potency >= this.maxPotency){
            this.potency = this.maxPotency;
        }
    }

    public void Respawn(){

        if(LayerMask.LayerToName(this.gameObject.layer) != "Shield"){
            this.potency = this.maxPotency;
        }

        this.gameObject.layer = LayerMask.NameToLayer("Shield");
        Renderer renderer = this.gameObject.GetComponent<Renderer>();
        renderer.enabled = true;
    }

    private void Despawn(){

        this.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        Renderer renderer = this.gameObject.GetComponent<Renderer>();
        renderer.enabled = false;
    }

    private IEnumerator HitFlash(){
        SFXPlayer.PlaySound("shotOnShield");
        this.gameObject.GetComponent<Renderer>().material.color = new Color(160, 160, 160, 1);
        yield return new WaitForSeconds(0.1f);
        this.gameObject.GetComponent<Renderer>().material.color = originalColor;
        StopCoroutine("HitFlash");
    }
}
