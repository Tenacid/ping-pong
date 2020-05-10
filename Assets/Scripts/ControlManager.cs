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
        if (Game.getInstance().getGameState() == GameState.PLAY) {
            checkInput();
        }
    }
}
