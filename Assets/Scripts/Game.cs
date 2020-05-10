using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    private static Game instance;
    private GameState state;
    private GameState prevState;

    private Rect gameRect;

    [Header("Level Assets")]
    [SerializeField]
    private List<Ball> ballAssets = new List<Ball>();
    [SerializeField]
    private Platform platformAsset;
    [SerializeField]
    private GameObject wallAsset;
    [SerializeField]
    private GameObject outZone;

    [Header("Interface")]
    [SerializeField]
    private UnityEngine.UI.Text uiScore;
    [SerializeField]
    private UnityEngine.UI.Text uiBestScore;

    private Ball ballInstance;
    private Platform[] platformInstances;

    private int bestScore;
    private int curScore;
    
    private Config config;

    public static Game getInstance() {
        if (instance == null) {
            instance = FindObjectOfType<Game>();
        }

        return instance;
    }

    public Color getBallColor() {
        if (config == null) {
            config = GameUtils.readConfig();
        }

        return config.BallColor;
    }

    public void setBallColor(Color color) {
        if (config == null) {
            config = GameUtils.readConfig();
        }

        config.BallColor = color;
    }

    public void resetScore() {
        curScore = 0;

        uiScore.text = curScore.ToString();

        uiBestScore.text = bestScore.ToString();
    }

    public void incResult() {
        curScore++;

        uiScore.text = curScore.ToString();
    }

    public bool isBall(GameObject gameObject) {
        if (ballInstance == null) {
            return false;
        }

        return ballInstance.gameObject == gameObject;
    }

    // Start is called before the first frame update
    private void Awake() {
        state = GameState.WAIT_FOR_START;

        Vector3 zero = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 one = Camera.main.ViewportToWorldPoint(Vector3.one);

        gameRect = new Rect(zero.x, zero.y, one.x - zero.x, one.y - zero.y);

        //walls
        GameObject wallLeft = Instantiate(wallAsset);
        wallLeft.transform.position = new Vector3(-gameRect.width / 2, 0, 0);
        wallLeft.transform.localRotation = Quaternion.Euler(0, 0, -90);

        GameObject wallRight = Instantiate(wallAsset);
        wallRight.transform.position = new Vector3(gameRect.width / 2, 0, 0);
        wallRight.transform.localRotation = Quaternion.Euler(0, 0, 90);


        //outzones
        GameObject outZoneTop = Instantiate(outZone);
        outZoneTop.transform.position = new Vector3(0, gameRect.height*0.6f, 0);

        GameObject outZoneBottom = Instantiate(outZone);
        outZoneBottom.transform.position = new Vector3(0, -gameRect.height * 0.6f, 0);

        //platforms
        platformInstances = new Platform[2];

        for (int i = 0; i <= 1; i++) {
            platformInstances[i] = Instantiate(platformAsset);
            platformInstances[i].setPlayerID(i);
        }

        bestScore = GameUtils.getBestScore();

        resetScore();
    }

    public void resetBall() {
        if (ballInstance == null) {
            ballInstance = Instantiate(ballAssets[Random.Range(0, ballAssets.Count)]);
            Renderer rend = ballInstance.GetComponent<Renderer>();

            rend.material.color = getBallColor();
        }

        ballInstance.transform.position = Vector3.zero;

        
    }

    public void startGame() {
        resetBall();

        state = GameState.PLAY;

        //ControlManager.getInstance().resetPrevTouchPos();

        ballInstance.addForce();
    }

    public void restartGame() {
        if (curScore > bestScore) {
            bestScore = curScore;
            GameUtils.saveBestScore(bestScore);

            uiBestScore.text = bestScore.ToString();
        }

        resetScore();

        state = GameState.WAIT_FOR_START;

        InterfaceManager.getInstance().showOnRestart();

        if (ballInstance != null) {
            Destroy(ballInstance);
            ballInstance = null;
        }

       
        for (int i = 0; i <= 1; i++) {
            platformInstances[i].returnToBase();
        }
    }

    public Ball getBallInstance() {
        return ballInstance;
    }

    public Rect getGameRect() {
        return gameRect;
    }

    public GameState getGameState() {
        return state;
    }

    public void pauseGame() {
        prevState = state;
        state = GameState.PAUSE;

        ballInstance.pause();
    }

    public void movePlatform(float newX) {
        for (int i = 0; i <= 1; i++) {
            platformInstances[i].move(newX);
        }
    }

    public void unpause() {
        state = prevState;

        //ControlManager.getInstance().resetPrevTouchPos();

        ballInstance.unpause();
    }

}

public enum GameState {
    WAIT_FOR_START,
    PAUSE,
    PLAY
}
