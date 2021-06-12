using UnityEngine;

public class Shield : MonoBehaviour {

    public Player player;
    public float turnSpeed = 1.0f;
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
}
