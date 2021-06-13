using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour{

    public float parallax = 2.0f;

    void Update(){
        MeshRenderer mr = GetComponent<MeshRenderer>();

        Material mat = mr.materials[0];

        Vector2 offset = mat.mainTextureOffset; 

        offset.x = transform.position.x / transform.localScale.x / parallax;
        offset.y = transform.position.y / transform.localScale.y / parallax;

        mat.mainTextureOffset = offset;



    }
}
