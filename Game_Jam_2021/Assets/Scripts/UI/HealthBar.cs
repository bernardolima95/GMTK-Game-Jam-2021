using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public Player player;

    private void Update() {
        this.transform.localScale = new Vector3((float)player.health/(float)player.maxHealth, 1, 1);
    }
}