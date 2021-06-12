using UnityEngine;

public class Shield : MonoBehaviour {

    public Player player;
    public float turnSpeed = 1.0f;
    public float shieldPotency = 3.0f;
    private float _previousPlayerAngle;

    private void Awake() {
        this._previousPlayerAngle = player.transform.eulerAngles.z;
    }

    private void Update() {
        this.transform.position = this.player.transform.position;
    }
    
    private void FixedUpdate() {

        float playerAngle = player.transform.eulerAngles.z;
        float deltaAngle = Mathf.DeltaAngle(this._previousPlayerAngle, playerAngle);

        this.transform.Rotate(new Vector3(0, 0, deltaAngle * this.turnSpeed));

        this._previousPlayerAngle = playerAngle;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        tag = collision.gameObject.tag;
        if (tag == "Enemy" || tag == "EnemyBullet"){
            shieldPotency -= 1;
            
            if(this.shieldPotency < 1){
                this.gameObject.SetActive(false);
            }
        }
    }
}
