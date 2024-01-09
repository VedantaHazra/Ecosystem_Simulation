using UnityEngine;

public class MoveCamera : MonoBehaviour {

    public Transform player;

    void Update() {
        this.transform.position = player.transform.position;
    }
}
