using UnityEngine;

public class StoveCounterSound : MonoBehaviour {
    private AudioSource audioSource;
    [SerializeField] private StoveCounter stoveCounter;
    bool playWarningSound;
    private float warningSoundTimer;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender,IHasProgress.OnProgressChangedEventArgs e) {

        float burnShowProgressAmount = .5f;
        playWarningSound = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;


    }

    private void StoveCounter_OnStateChange(object sender,StoveCounter.OnStateChangedEventArgs e) {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if(playSound) {
            audioSource.Play();
        }
        else {
            audioSource.Pause();
        }

    }

    private void Update() {
        if(playWarningSound) {
            warningSoundTimer -= Time.deltaTime;
            if(warningSoundTimer <= 0) {
                float warningSoundTimerMax = .2f;
                warningSoundTimer = warningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }
}
