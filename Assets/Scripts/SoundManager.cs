using UnityEngine;

public class SoundManager : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    [SerializeField] private AudioClipRefSO audioClipRefSO;
    public static SoundManager Instance {
        get; private set;
    }

    private float volume = 1f;

    private void Awake() {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME,1f);
    }

    private void Start() {
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        DeliveryManager.Instance.OnRecipeSuccess += DelivereyManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DelivereyManager_OnRecipeFailed;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender,System.EventArgs e) {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefSO.trash,trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender,System.EventArgs e) {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefSO.objectDrop,baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender,System.EventArgs e) {
        PlaySound(audioClipRefSO.objectPickUp,Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender,System.EventArgs e) {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefSO.chop,cuttingCounter.transform.position);
    }

    private void DelivereyManager_OnRecipeFailed(object sender,System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefSO.deliveryFail,deliveryCounter.transform.position);
    }

    private void DelivereyManager_OnRecipeSuccess(object sender,System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefSO.deliverySuccess,deliveryCounter.transform.position);
    }
    private void PlaySound(AudioClip[] audioClipArray,Vector3 position,float volume = 1f) {
        PlaySound(audioClipArray[Random.Range(0,audioClipArray.Length)],position,volume);
    }
    private void PlaySound(AudioClip audioClip,Vector3 position,float volumeMultiplier = 1f) {
        AudioSource.PlayClipAtPoint(audioClip,position,volumeMultiplier * volume);
    }

    public void PlayFootstepSound(Vector3 position,float volume) {
        PlaySound(audioClipRefSO.footstep,position,volume);
    }

    public void ChangeVolume() {
        volume += .1f;
        if(volume > 1f) {
            volume = 0f;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME,volume);
        PlayerPrefs.Save();
    }
    public float GetVolume() {
        return volume;
    }
}
