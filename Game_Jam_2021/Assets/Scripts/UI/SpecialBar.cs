using UnityEngine;
using UnityEngine.UI;

public class SpecialBar : MonoBehaviour {

    public Player player;

    private void Update() {
        this.transform.localScale = new Vector3(player.specialMeter/player.maxSpecialMeter, 1, 1);
    }
}
