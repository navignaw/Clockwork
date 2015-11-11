using UnityEngine;
using System.Collections;

public class FollowPath : MonoBehaviour {
    public Transform[] waypoints;
    public float maxTime = 5f;
    public float currentTime = 5f;
    public float timeScale = 0f;

    private Animator anim;

    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Mathf.Clamp(currentTime + timeScale * Time.deltaTime, 0f, maxTime);
        if (currentTime == 0f || currentTime == maxTime) {
            Pause();
        }

        float pathPercent = currentTime / maxTime;
        iTween.PutOnPath(gameObject, waypoints, pathPercent);

        anim.speed = Mathf.Abs(timeScale);
    }

    public void Rewind(float speed)
    {
        timeScale = -1f * speed;
    }

    public void Play(float speed)
    {
        timeScale = 1f * speed;
    }

    public void Pause()
    {
        timeScale = 0f;
    }

    void OnDrawGizmos()
    {
        // For debugging
        iTween.DrawPathGizmos(waypoints);
    }

}