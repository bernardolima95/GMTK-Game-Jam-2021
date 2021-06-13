using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour{

    private BoxCollider2D _backgroundCollider;
    private float _backgroundSize;

    void Start(){
        _backgroundCollider = GetComponent<BoxCollider2D>();
        _backgroundSize = _backgroundCollider.size.y;
    }

    void Update(){
        if(transform.position.y < -_backgroundSize){
            RepeatBackground();
        }
    }

    void RepeatBackground(){
        Vector2 BGOffset = new Vector2(0, _backgroundSize * 2.0f);
        transform.position = (Vector2)transform.position + BGOffset;
    }
}
