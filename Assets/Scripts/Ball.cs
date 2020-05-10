using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speedMod;
    Rigidbody rigidbody;
    
    private float magnitude;

    private int zeroSpeedCounter = 0;

    // Start is called before the first frame update
    /*void Start()
    {
        
        stop();
        
    }*/

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

    /*public Vector3 correctVelocity(Vector3 velocity) {
        //return velocity.normalized * speedMod;
    }*/

    public void Update() {
        if (Game.getInstance().getGameState() == GameState.PLAY) {

            /*Debug.Log("1 " + rigidbody.velocity);

            if (Mathf.Abs(rigidbody.velocity.y) < Constants.minYVelocity) {

                zeroSpeedCounter++;
            } else {
                zeroSpeedCounter = 0;
            }

            if (zeroSpeedCounter > 5) { 

                float sign = 1;
                if (Mathf.Abs(rigidbody.velocity.y) > 0) {
                    sign = rigidbody.velocity.y / Mathf.Abs(rigidbody.velocity.y);
                }

                Debug.Log("sign=" + sign);

                Vector3 newVelocity = rigidbody.velocity + new Vector3(0, rigidbody.velocity.y + Constants.minYVelocity * sign, 0);

                Debug.Log("newVelocity=" + newVelocity);
                Debug.Log("newVelocity.normalized=" + newVelocity.normalized);
                Debug.Log("magnitude=" + magnitude);

                rigidbody.velocity = newVelocity.normalized * magnitude;

                Debug.Log("2 " + rigidbody.velocity);
            }

            if (rigidbody.velocity.magnitude < magnitude) {
                rigidbody.velocity = rigidbody.velocity.normalized * magnitude;

                Debug.Log("3 " + rigidbody.velocity);
            }*/
        }
    }
}
