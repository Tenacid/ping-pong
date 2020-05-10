using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutZone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        if (Game.getInstance().isBall(other.gameObject)) {
            Game.getInstance().restartGame();
        }
    }
}
