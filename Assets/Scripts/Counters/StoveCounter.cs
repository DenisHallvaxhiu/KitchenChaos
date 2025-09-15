using UnityEngine;

public class StoveCounter : BaseCounter {

    private enum State {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] private FryingRecepiSO[] fryingRecepiSOArray;
    [SerializeField] private BurningRecepiSO[] burningRecepiSOArray;
    private State state;

    private float fryingTimer;
    private FryingRecepiSO fryingRecepiSO;
    private float burningTimer;
    private BurningRecepiSO burningRecepiSO;

    private void Start() {
        state = State.Idle;
    }

    private void Update() {
        if(HasKitchenObject()) {
            switch(state) {
                case State.Idle:
                break;
                case State.Frying:
                fryingTimer += Time.deltaTime;
                if(fryingTimer > fryingRecepiSO.fryingTimerMax) {
                    //Fried
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(fryingRecepiSO.output,this);
                    Debug.Log("Object Fried");

                    burningRecepiSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectsSO());
                    state = State.Fried;
                    burningTimer = 0;
                }
                break;
                case State.Fried:
                burningTimer += Time.deltaTime;
                if(burningTimer > burningRecepiSO.burningTimerMax) {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(burningRecepiSO.output,this);
                    Debug.Log("Object Burned");
                    state = State.Burned;
                }
                break;
                case State.Burned:
                break;
            }
            Debug.Log(state);
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
                    state = State.Frying;
                    fryingTimer = 0f;
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

    private BurningRecepiSO GetBurningRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectSO) {
        foreach(BurningRecepiSO burningRecepiSO in burningRecepiSOArray) {
            if(burningRecepiSO.input == inputKitchenObjectSO) {
                return burningRecepiSO;
            }
        }
        return null;
    }
}
