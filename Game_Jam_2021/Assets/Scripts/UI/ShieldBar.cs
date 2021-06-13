using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour {
    
    public Shield shield;

    private void Update(){
        this.transform.localScale = new Vector3(shield.potency/shield.maxPotency, 1, 1);
    }
}
