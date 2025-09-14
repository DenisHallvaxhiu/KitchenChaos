using UnityEngine;

public class StoveCounter : BaseCounter {

    [SerializeField] private FryingRecepiSO[] fryingRecepiSOArray;

    private float fryingTimer;
    private FryingRecepiSO fryingRecepiSO;
    private void Update() {
        if(HasKitchenObject()) {
            fryingTimer += Time.deltaTime;
            if(fryingTimer > fryingRecepiSO.fryingTimerMax) {
                //Fried
                fryingTimer = 0;
                GetKitchenObject().DestroySelf();
                Debug.Log("Fried");
                KitchenObject.SpawnKitchenObject(fryingRecepiSO.output,this);
            }
            Debug.Log(fryingTimer);
        }
    }

    public override void Interact(Player player) {
        if(!HasKitchenObject()) {
            //There is no kitchen object
            if(player.HasKitchenObject()) {
                //Player is carrying smth
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectsSO())) {
                    //Player carrying object that can be fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    fryingRecepiSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectsSO());
                }
            }
            else {
                //Player not carrying anything
            }
        }
        else {
            if(player.HasKitchenObject()) {
                //Player is carrying smth
            }
            else {
                //Player not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectsSO inputKitchenObjectSO) {
        FryingRecepiSO fryingRecepiSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecepiSO != null;
    }

    private KitchenObjectsSO GetOutputForInput(KitchenObjectsSO inputKitchenObjectSO) {
        FryingRecepiSO fryingRecepiSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if(fryingRecepiSO != null) {
            return fryingRecepiSO.output;
        }
        else {
            return null;
        }
    }

    private FryingRecepiSO GetFryingRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectSO) {
        foreach(FryingRecepiSO fryingRecepiSO in fryingRecepiSOArray) {
            if(fryingRecepiSO.input == inputKitchenObjectSO) {
                return fryingRecepiSO;
            }
        }
        return null;
    }
}
