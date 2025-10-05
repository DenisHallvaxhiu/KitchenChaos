using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChange;
    public class OnStateChangedEventArgs : EventArgs {
        public State state;
    }

    public enum State {
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
                OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = fryingTimer / fryingRecepiSO.fryingTimerMax
                });
                if(fryingTimer > fryingRecepiSO.fryingTimerMax) {
                    //Fried
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(fryingRecepiSO.output,this);

                    burningRecepiSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectsSO());
                    state = State.Fried;
                    OnStateChange?.Invoke(this,new OnStateChangedEventArgs
                    {
                        state = state,
                    });
                    burningTimer = 0;
                }
                break;
                case State.Fried:
                burningTimer += Time.deltaTime;
                OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = burningTimer / burningRecepiSO.burningTimerMax
                });
                if(burningTimer > burningRecepiSO.burningTimerMax) {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(burningRecepiSO.output,this);
                    state = State.Burned;
                    OnStateChange?.Invoke(this,new OnStateChangedEventArgs
                    {
                        state = state,
                    });
                    OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0f
                    });
                }
                break;
                case State.Burned:
                break;
            }
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
                    OnStateChange?.Invoke(this,new OnStateChangedEventArgs
                    {
                        state = state,
                    });
                    OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecepiSO.fryingTimerMax
                    });
                }
            }
            else {
                //Player not carrying anything
            }
        }
        else {
            if(player.HasKitchenObject()) {
                //Player is carrying smth
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    //Player holding a plate
                    if(plateKitchenObject.TryAddIngridient(GetKitchenObject().GetKitchenObjectsSO())) {
                        GetKitchenObject().DestroySelf();

                        state = State.Idle;
                        OnStateChange?.Invoke(this,new OnStateChangedEventArgs
                        {
                            state = state,
                        });
                        OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                }
            }
            else {
                //Player not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnStateChange?.Invoke(this,new OnStateChangedEventArgs
                {
                    state = state,
                });
                OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
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

    public bool IsFried() {
        return state == State.Fried;
    }
}
