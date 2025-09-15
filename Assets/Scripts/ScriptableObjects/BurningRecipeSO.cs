using UnityEngine;

[CreateAssetMenu()]
public class BurningRecepiSO : ScriptableObject {
    public KitchenObjectsSO input;
    public KitchenObjectsSO output;
    public float burningTimerMax;
}
