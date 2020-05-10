using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private float speedMod;

    private PlayerID playerID;

    private float maxPos;
    private float moveToX = 0;

    public void setPlayerID(int id) {
        playerID = (PlayerID)id;
        Debug.Log("ID:" + id.ToString() + " - " + playerID);

        returnToBase();
    }


    // Start is called before the first frame update
    public void returnToBase()
    {
        Rect rect = Game.getInstance().getGameRect();

        Debug.Log(rect);

        transform.localScale = new Vector3(rect.height * 0.05f, rect.width * 0.15f, rect.height * 0.05f);

        if (playerID == PlayerID.ONE) {
            transform.position = new Vector3(0, -rect.height / 2, 0);
        } else if (playerID == PlayerID.TWO) {
            transform.position = new Vector3(0, rect.height / 2, 0);
        }

        maxPos = rect.width / 2 - rect.width * 0.15f / 2f;
    }

    public void move(float newX) {
        moveToX = newX;

        if (moveToX > maxPos) {
            moveToX = maxPos;
        }

        if (moveToX < -maxPos) {
            moveToX = -maxPos;
        }
    }

    private void OnCollisionEnter(Collision collision) {

        if (Game.getInstance().isBall(collision.gameObject)) {
            Game.getInstance().incResult();
        }


    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        if (Game.getInstance().getGameState() == GameState.PLAY) {

            if (Mathf.Abs(moveToX - transform.position.x) > Constants.mathError) {
                float step = Mathf.Min(dt * speedMod, Mathf.Abs(moveToX - transform.position.x));

                if (moveToX > transform.position.x) {
                    transform.Translate(new Vector3(step, 0, 0), Space.World);
                } else {
                    transform.Translate(new Vector3(-step, 0, 0), Space.World);
                }
            }
        }
    }
}

public enum PlayerID {
    ONE,
    TWO
}
