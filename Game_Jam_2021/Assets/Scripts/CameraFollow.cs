using UnityEngine;

public class CameraFollow : MonoBehaviour{

    public Transform pilot;

    void FixedUpdate(){
        transform.position = new Vector3(pilot.position.x, pilot.position.y, transform.position.z);
    }

}
