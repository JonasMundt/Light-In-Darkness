using UnityEngine;
using System.Collections;

//Tick Tack Level 5 Logik mit Schritten

public class ClockRhythmController : MonoBehaviour
{
    [System.Serializable]
    public struct ClockStepTransform
    {
        public Vector2 localPosition;
        public float localZRotation;
    }

    [Header("Clock")]
    public Transform clockHand;

    [Header("Timing")]
    public float tickInterval = 1f;
    public int currentStep = 0;

    [Header("State")]
    public bool platformsVisible = true;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip tickClip;
    public AudioClip tackClip;

    [Header("Manual Step Transforms (0 = 12 Uhr, 1 = 1 Uhr, ... 11 = 11 Uhr)")]
    //Hardcoded Positions von dem Uhrzeiger
    public ClockStepTransform[] stepTransforms = new ClockStepTransform[12]
    {
        new ClockStepTransform { localPosition = new Vector2( 0f,        -5.136f),     localZRotation =   0f      }, // 12
        new ClockStepTransform { localPosition = new Vector2(-2.833f,    -4.375f),     localZRotation = 329.997f }, // 1
        new ClockStepTransform { localPosition = new Vector2(-4.790367f, -2.510616f),  localZRotation = 301.96f  }, // 2
        new ClockStepTransform { localPosition = new Vector2(-5.72f,      0.3565331f), localZRotation = 272.299f }, // 3
        new ClockStepTransform { localPosition = new Vector2(-5.173f,     3.012768f),  localZRotation = 244.416f }, // 4
        new ClockStepTransform { localPosition = new Vector2(-3.23354f,   5.306782f),  localZRotation = 214.071f }, // 5
        new ClockStepTransform { localPosition = new Vector2(-0.1309357f, 6.324619f),  localZRotation = 181.161f }, // 6
        new ClockStepTransform { localPosition = new Vector2( 2.598f,     5.69062f),   localZRotation = 152.53f  }, // 7
        new ClockStepTransform { localPosition = new Vector2( 4.926f,     3.466f),     localZRotation = 120.181f }, // 8
        new ClockStepTransform { localPosition = new Vector2( 5.69342f,   0.7347908f), localZRotation =  91.009f }, // 9
        new ClockStepTransform { localPosition = new Vector2( 5.00515f,  -2.092487f),  localZRotation =  61.436f }, // 10
        new ClockStepTransform { localPosition = new Vector2( 2.79f,     -4.368f),     localZRotation =  29.802f }  // 11
    };

    private Coroutine tickCoroutine;

    private void Start()
    {
        currentStep = Mathf.Clamp(currentStep, 0, stepTransforms.Length - 1);
        ApplyStep(currentStep);
        tickCoroutine = StartCoroutine(TickLoop());
    }

    private IEnumerator TickLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(tickInterval);

            currentStep++;
            if (currentStep >= stepTransforms.Length)
                currentStep = 0;

            ApplyStep(currentStep);
            PlayTickTack();
        }
    }

    private void ApplyStep(int stepIndex)
    {
        if (clockHand != null && stepIndex >= 0 && stepIndex < stepTransforms.Length)
        {
            ClockStepTransform step = stepTransforms[stepIndex];

            clockHand.localPosition = new Vector3(step.localPosition.x, step.localPosition.y, clockHand.localPosition.z);
            clockHand.localRotation = Quaternion.Euler(0f, 0f, step.localZRotation);
        }

        platformsVisible = (stepIndex % 2 == 0);
    }

    private void PlayTickTack()
    {
        if (audioSource == null)
            return;

        AudioClip clipToPlay = (currentStep % 2 == 0) ? tickClip : tackClip;

        if (clipToPlay != null)
        {
            audioSource.PlayOneShot(clipToPlay);
        }
    }

    public bool ArePlatformsVisible()
    {
        return platformsVisible;
    }

    public void ResetToTwelve()
    {
        currentStep = 0;
        ApplyStep(currentStep);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (stepTransforms == null || stepTransforms.Length != 12)
            return;

        currentStep = Mathf.Clamp(currentStep, 0, stepTransforms.Length - 1);

        if (clockHand != null)
        {
            ClockStepTransform step = stepTransforms[currentStep];
            clockHand.localPosition = new Vector3(step.localPosition.x, step.localPosition.y, clockHand.localPosition.z);
            clockHand.localRotation = Quaternion.Euler(0f, 0f, step.localZRotation);
        }

        platformsVisible = (currentStep % 2 == 0);
    }
#endif
}