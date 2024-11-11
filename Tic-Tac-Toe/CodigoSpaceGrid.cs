using System;
using TMPro;
using UnityEngine;

public class GridSpace : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI buttonText;

    private GameController gameController;

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }

    public void SetSpace()
    {
        if (gameController.playerMove)
        {
            buttonText.text = gameController.GetPlayerSide();
            button.interactable = false;
            gameController.EndTurn();

            // Enviar jugada al servidor
            int index = Array.IndexOf(gameController.buttonList, buttonText);
            gameController.SendMessageToServer(index.ToString());
        }
    }
}
