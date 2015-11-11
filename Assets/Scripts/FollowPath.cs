using UnityEngine;
using System.Collections;

public class FollowPath : MonoBehaviour {
    public Transform[] waypoints;
    public float maxTime = 5f;
    public float currentTime = 5f;
    public float timeScale = 0f;

    private Vector2 lastPos = Vector2.zero;
    private bool facingRight = true;
    private Animator anim;
    private Rigidbody2D rb2d;

    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        lastPos = transform.position;
        Rewind();
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

    void FixedUpdate()
    {
        Debug.Log(rb2d.velocity);
        if ((lastPos.x < transform.position.x && !facingRight) || (lastPos.x > transform.position.x && facingRight)) {
            Flip();
        }
        lastPos = transform.position;
    }

    public void Rewind()
    {
        timeScale = -1f;
    }

    public void Play()
    {
        timeScale = 1f;
    }

    public void Pause()
    {
        timeScale = 0f;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

}