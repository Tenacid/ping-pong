using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Config {
    [SerializeField]
    private Color ballColor = Color.white;

    public Color BallColor {
        get => ballColor; set {
            if (ballColor != value) {
                ballColor = value;
                GameUtils.saveConfig(this);
            }
        }
    }
}
