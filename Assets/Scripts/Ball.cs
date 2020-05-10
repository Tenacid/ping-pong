using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speedMod;
    Rigidbody rigidbody;
    
    private float magnitude;

    private int zeroSpeedCounter = 0;
    

    public Rigidbody getRigidbody() {
        if (rigidbody == null) {
            rigidbody = GetComponent<Rigidbody>();
        }

        return rigidbody;
    }

    

    public void addForce() {
        Vector3 rndVec = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-1f, 1f));

        getRigidbody().AddForce(rndVec.normalized * speedMod, ForceMode.Impulse);

        magnitude = (rndVec.normalized * speedMod).magnitude;
    }

    public void stop() {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    public void pause() {
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void unpause() {
        rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
    }
    
}
