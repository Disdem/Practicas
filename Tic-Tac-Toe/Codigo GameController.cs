using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI[] buttonList;
    public GameObject gameOveralPanel;
    public TextMeshProUGUI gameOverText;
    public GameObject restartButton;
    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;
    public GameObject startInfo;

    private string playerSide;
    private string computerSide;
    public bool playerMove;
    public float delay;
    private int moveCount;

    // Agregar referencia a TCPClient
    private TCPClient tcpClient;

    void Awake()
    {
        SetGameControllerReferenceOnButtons();
        gameOveralPanel.SetActive(false);
        moveCount = 0;
        restartButton.SetActive(false);
        playerMove = true;

        // Inicializar el cliente TCP
        tcpClient = gameObject.AddComponent<TCPClient>();
        tcpClient.OnMessageReceived += OnMessageReceivedFromServer;
    }

    void Update()
    {
        if (!playerMove)
        {
            delay += Time.deltaTime;
            if (delay >= 1.0f) // Reducir el tiempo de espera por razones prácticas
            {
                // Generar jugada para el "ordenador"
                int randomIndex = Random.Range(0, 8);
                if (buttonList[randomIndex].GetComponentInParent<Button>().interactable == true)
                {
                    buttonList[randomIndex].text = computerSide;
                    buttonList[randomIndex].GetComponentInParent<Button>().interactable = false;
                    EndTurn();
                    tcpClient.SendMessageToServer(randomIndex.ToString()); // Enviar jugada al servidor
                }
            }
        }
    }

    private void OnMessageReceivedFromServer(string message)
    {
        // Aplicar jugada recibida del servidor (otro jugador)
        if (int.TryParse(message, out int index))
        {
            if (buttonList[index].GetComponentInParent<Button>().interactable == true)
            {
                buttonList[index].text = computerSide;
                buttonList[index].GetComponentInParent<Button>().interactable = false;
                EndTurn();
            }
        }
    }

    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    public void SetStartingSide(string startingSide)
    {
        playerSide = startingSide;
        computerSide = (playerSide == "X") ? "O" : "X";
        SetPlayerColors(playerX, playerO);
        StartGame();
    }

    void StartGame()
    {
        SetBoardInteractable(true);
        SetPlayerButtons(false);
        startInfo.SetActive(false);
    }

    public void EndTurn()
    {
        moveCount++;

        // Condiciones para determinar el ganador o empate
        if (CheckWinCondition(playerSide))
        {
            GameOver(playerSide);
        }
        else if (CheckWinCondition(computerSide))
        {
            GameOver(computerSide);
        }
        else if (moveCount >= 9)
        {
            GameOver("draw");
        }
        else
        {
            ChangeSides();
            delay = 0;
        }
    }

    private bool CheckWinCondition(string side)
    {
        // Comprobaciones de victoria simplificadas (mismo concepto que antes)
        return (buttonList[0].text == side && buttonList[1].text == side && buttonList[2].text == side) ||
               (buttonList[3].text == side && buttonList[4].text == side && buttonList[5].text == side) ||
               (buttonList[6].text == side && buttonList[7].text == side && buttonList[8].text == side) ||
               (buttonList[0].text == side && buttonList[3].text == side && buttonList[6].text == side) ||
               (buttonList[1].text == side && buttonList[4].text == side && buttonList[7].text == side) ||
               (buttonList[2].text == side && buttonList[5].text == side && buttonList[8].text == side) ||
               (buttonList[0].text == side && buttonList[4].text == side && buttonList[8].text == side) ||
               (buttonList[2].text == side && buttonList[4].text == side && buttonList[6].text == side);
    }

    void ChangeSides()
    {
        playerMove = !playerMove;
        SetPlayerColors(playerMove ? playerX : playerO, playerMove ? playerO : playerX);
    }

    void GameOver(string winner)
    {
        SetBoardInteractable(false);
        gameOverText.text = (winner == "draw") ? "Empate!" : winner + " gana!";
        restartButton.SetActive(true);
    }

    void SetBoardInteractable(bool toggle)
    {
        foreach (var button in buttonList)
        {
            button.GetComponentInParent<Button>().interactable = toggle;
        }
    }
}
