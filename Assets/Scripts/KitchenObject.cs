using UnityEngine;

public class KitchenObject : MonoBehaviour {
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;

    public KitchenObjectsSO GetKitchenObjectsSO() {
        return kitchenObjectsSO;
    }
}
