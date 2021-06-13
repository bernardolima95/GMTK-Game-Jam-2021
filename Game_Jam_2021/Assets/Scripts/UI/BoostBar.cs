using UnityEngine;
using UnityEngine.UI;

public class BoostBar : MonoBehaviour {

    public Player player;

    private void Update() {
        this.transform.localScale = new Vector3(player.boostMeter/player.maxBoostMeter, 1, 1);
    }
}
