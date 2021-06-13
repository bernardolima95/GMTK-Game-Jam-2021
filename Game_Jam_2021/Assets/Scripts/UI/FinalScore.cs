using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour {

    private Text _scoreText;
    private int _finalScore;

    private void Awake() {
        _scoreText = GetComponent<Text>();
        _finalScore = PlayerPrefs.GetInt("score", -1);

        if(_finalScore != -1){
            _scoreText.text = "Final score:\n" + _finalScore;
        }

        PlayerPrefs.DeleteKey("score");
    }
}
