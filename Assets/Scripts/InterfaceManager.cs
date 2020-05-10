using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour {
    [SerializeField]
    private GameObject settingsPannel;

    [SerializeField]
    private Slider sliderRed;
    [SerializeField]
    private Slider sliderGreen;
    [SerializeField]
    private Slider sliderBlue;

    [SerializeField]
    private Image imageColor;

    [SerializeField]
    private Button buttonPlay;

    private static InterfaceManager instance;

    public static InterfaceManager getInstance() {
        if (instance == null) {

            instance = FindObjectOfType<InterfaceManager>();

        }

        return instance;
    }

    public void Awake() {
        Color color = Game.getInstance().getBallColor();

        sliderRed.value = color.r;
        sliderGreen.value = color.g;
        sliderBlue.value = color.b;

        imageColor.color = color;

        sliderRed.onValueChanged.RemoveAllListeners();
        sliderRed.onValueChanged.AddListener(delegate { redrawColor(); });

        sliderGreen.onValueChanged.RemoveAllListeners();
        sliderGreen.onValueChanged.AddListener(delegate { redrawColor(); });

        sliderBlue.onValueChanged.RemoveAllListeners();
        sliderBlue.onValueChanged.AddListener(delegate { redrawColor(); });

        buttonPlay.onClick.RemoveAllListeners();
        buttonPlay.onClick.AddListener(delegate {
            settingsPannel.SetActive(false);
            Game.getInstance().setBallColor(imageColor.color);
            Game.getInstance().startGame();
        });
    }

    public void showOnRestart() {
        settingsPannel.SetActive(true);
    }

    public void redrawColor() {
        imageColor.color = new Color(sliderRed.value, sliderGreen.value, sliderBlue.value);
    }



}
