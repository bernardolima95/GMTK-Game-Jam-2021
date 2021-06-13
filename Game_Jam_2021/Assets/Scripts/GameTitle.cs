using UnityEngine;
using UnityEngine.UI;

public class GameTitle : MonoBehaviour {

    private void Awake() {
        int score = PlayerPrefs.GetInt("score", -1);

        if(score != -1){
            this.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
}
