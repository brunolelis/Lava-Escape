using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreNameInputWindow : MonoBehaviour {

    private static HighscoreNameInputWindow instance;

    private Action<string> onNameSubmitted;
    private Text nameText;
    private Text scoreText;
    private InputField nameInputField;

    private void Awake() {
        instance = this;
        nameText = transform.Find("nameText").GetComponent<Text>();
        scoreText = transform.Find("scoreText").GetComponent<Text>();
        nameInputField = transform.Find("nameText").GetComponent<InputField>();
        nameInputField.onValidateInput = (string text, int charIndex, char addedChar) => {
            if (addedChar == ' ') return '\0'; 
            return addedChar.ToString().ToUpper()[0];
        };
        gameObject.SetActive(false);
    }

    private void Update() {
        if (nameText.text.Length >= 3) {
            onNameSubmitted(nameText.text);
            gameObject.SetActive(false);
        }
    }

    public static void Show(int score, Action<string> onNameSubmitted) {
        instance.gameObject.SetActive(true);
        instance.scoreText.text = "SCORE: " + score;
        instance.onNameSubmitted = onNameSubmitted;
        instance.nameInputField.text = "";
        instance.nameInputField.Select();
        instance.nameInputField.ActivateInputField();
    }

}
