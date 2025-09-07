using UnityEngine;

public class KitchenObject : MonoBehaviour {
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;

    private ClearCounter clearCounter;

    public KitchenObjectsSO GetKitchenObjectsSO() {
        return kitchenObjectsSO;
    }
    public void SetClearCounter(ClearCounter clearCounter) {
        if(this.clearCounter != null) {
            this.clearCounter.ClearKitchenObject();
        }
        this.clearCounter = clearCounter;

        if(clearCounter.HasKitchenObject()) {
            Debug.Log("Counter has kitchenobject");
        }
        clearCounter.SetKitchenObject(this);
        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter() {
        return clearCounter;
    }
}
