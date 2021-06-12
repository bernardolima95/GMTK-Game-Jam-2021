using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    public GameManager gameManager;
    private Text _scoreText;

    private void Awake() {
        _scoreText = GetComponent<Text>();
    }

    private void Update() {
        _scoreText.text = "Score: " + gameManager.score;
    }
}
