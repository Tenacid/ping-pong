using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public UnityEngine.UI.Text text;
    private static ControlManager instance;
    
    public static ControlManager getInstance() {
        if (instance == null) {
            instance = FindObjectOfType<ControlManager>();            
        }

        return instance;
    }

/*#if UNITY_EDITOR
    public void waitForStart() {
        if (Input.GetMouseButton(0)) {
            Game.getInstance().startGame();
        }
    }
#else
    public void waitForStart() {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {
                Game.getInstance().startGame();
            }
        }
    }
#endif
*/

        /*
#if UNITY_EDITOR
    public void waitForUnpause() {
        if (Input.GetMouseButton(0)) {
            Game.getInstance().unpause();
        }
    }
#else
    public void waitForUnpause() {
        if (Input.touchCount > 0) {

    Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {
                Game.getInstance().unpause();
            }
        }
    }
#endif
*/

#if UNITY_EDITOR
    public void checkInput() {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Game.getInstance().movePlatform(worldPos.x);
    }
#else
    public void checkInput() {
        if (Input.touchCount > 0) {

            Touch touch = Input.GetTouch(0);

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(touch.position);

            Game.getInstance().movePlatform(worldPos.x);
        }
    }
#endif

    void Update()
    {
        /*if (Game.getInstance().getGameState() == GameState.WAIT_FOR_START) {
            waitForStart();
        } else if (Game.getInstance().getGameState() == GameState.PAUSE) {
            waitForUnpause();
        } else*/ if (Game.getInstance().getGameState() == GameState.PLAY) {
            checkInput();
        }

        string curText = "";

        

        /*for (int i = 0; i < Input.touchCount; i++) {

            Touch touch = Input.GetTouch(i);

            curText += "Touch " + i.ToString()+" finger:"+touch.fingerId + " pos:" + touch.position + " dir:" + touch.deltaPosition + "/n";    
        }

        text.text = curText;*/
    }
}
