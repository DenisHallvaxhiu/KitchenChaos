using UnityEngine;

[CreateAssetMenu()]
public class FryingRecepiSO : ScriptableObject {
    public KitchenObjectsSO input;
    public KitchenObjectsSO output;
    public float fryingTimerMax;
}
