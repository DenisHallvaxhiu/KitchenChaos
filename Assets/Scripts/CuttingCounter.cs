using UnityEngine;

public class CuttingCounter : BaseCounter {

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public override void Interact(Player player) {
        if(!HasKitchenObject()) {
            //There is no kitchen object
            if(player.HasKitchenObject()) {
                //Player is carrying smth
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectsSO())) {
                    //Player carrying object that can be cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
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
    public override void InteractAlternate(Player player) {
        if(HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectsSO())) {
            //There is a object AND it can be cut
            KitchenObjectsSO outputKitchenbjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectsSO());

            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(outputKitchenbjectSO,this);
        }
    }

    private bool HasRecipeWithInput(KitchenObjectsSO inputKitchenObjectSO) {
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if(cuttingRecipeSO.input == inputKitchenObjectSO) {
                return true;
            }
        }
        return false;
    }

    private KitchenObjectsSO GetOutputForInput(KitchenObjectsSO inputKitchenObjectSO) {
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if(cuttingRecipeSO.input == inputKitchenObjectSO) {
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }

}
