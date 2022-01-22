//Basic trayectory evaluator using state machines
//and coroutines.

// Comment this line to use coroutines instead of a state machine to drive the playback process
#define VFX_INTERPOLATION_STATE_MACHINE 
#if !VFX_INTERPOLATION_STATE_MACHINE
#define VFX_INTERPOLATION_COROUTINE
#endif
using UnityEngine;

public class TrajectoryCurve : MonoBehaviour
{
    const float DURATION_EPSILON = 0f;
    const float SPEED_EPSILON = 0f;

    const float MIN_DURATION = 0.1f;
    const float MIN_SPEED = 0.1f;

#if VFX_INTERPOLATION_STATE_MACHINE
    enum PlayState
    {
        Paused,
        Playing
    }

    float timer = 0;
    PlayState state = PlayState.Paused;
#endif

#if VFX_INTERPOLATION_COROUTINE
    Coroutine currentPlayback;
#endif


    [SerializeField] float duration;
    [SerializeField] float speed;
    [SerializeField] AnimationCurve curve;
    [SerializeField] float maxDistance;

    void OnValidate()
    {
        //Prevent Zero divisions by restricting user ranges
        if(duration <= DURATION_EPSILON)
        {
            duration = MIN_DURATION;
        }

        if(speed <= SPEED_EPSILON)
        {
            speed = MIN_SPEED;
        }
    }

    void Update()
    {
#if VFX_INTERPOLATION_STATE_MACHINE

        //State machine state controls
        if (Input.GetButtonDown("Jump") && state != PlayState.Playing)
        {
            state = PlayState.Playing;
        }

        //State machine evaluation
        if (state == PlayState.Playing)
        {
            timer += Time.deltaTime * speed;
            float x = Mathf.Lerp(0, maxDistance, timer / duration);
            transform.localPosition = new Vector3(x, curve.Evaluate(timer / duration) * maxDistance, 0);
            if (timer > duration)
            {
                state = PlayState.Paused;
                timer = 0;
            }
        }
#endif

#if VFX_INTERPOLATION_COROUTINE

        //Query Input and whether or not playback exists to start playback
        if (currentPlayback == null && Input.GetButtonDown("Jump"))
        {
            currentPlayback = StartCoroutine(Playback());
        }
#endif
    }

#if VFX_INTERPOLATION_COROUTINE
    IEnumerator Playback()
    {
        for(float t = 0; t < duration; t += Time.deltaTime * speed)
        {
            float x = Mathf.Lerp(0, maxDistance, t / duration);
            transform.position = new Vector3(x, curve.Evaluate(t / duration) * maxDistance, 0);
            yield return null;
        }
        currentPlayback = null;
    }
#endif
}
