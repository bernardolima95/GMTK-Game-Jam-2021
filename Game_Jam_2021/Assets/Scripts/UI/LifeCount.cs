using UnityEngine;
using UnityEngine.UI;

public class LifeCount : MonoBehaviour {

    public GameManager gameManager;
    private Text _text;

    private void Awake() {
        _text = GetComponent<Text>();
    }

    private void Update() {
        _text.text = "x" + this.gameManager.lives;
    }
}