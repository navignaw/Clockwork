using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {
    public FollowPath[] paths;

    private bool canInteract = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Action")) {
            for (int i = 0; i < paths.Length; i++) {
                paths[i].Play(1f);
            }
        } else if (canInteract && Input.GetButton("Action")) {
            for (int i = 0; i < paths.Length; i++) {
                paths[i].Rewind(3f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            canInteract = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            canInteract = false;
            for (int i = 0; i < paths.Length; i++) {
                paths[i].Play(1f);
            }
        }
    }
}