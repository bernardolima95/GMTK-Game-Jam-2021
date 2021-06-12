using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroCircle : MonoBehaviour{

    public Transform pilot;

    void FixedUpdate(){
        transform.position = new Vector3(pilot.position.x, pilot.position.y, transform.position.z);
    }

}
