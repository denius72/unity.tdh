using UnityEngine;
using TMPro; // Necessário para usar TextMeshPro

public class GetHealthText : MonoBehaviour
{
    public TextMeshProUGUI healthText; // Referência ao componente TextMeshPro
    private gamelogic gamelogic;       // Referência ao script gamelogic
    private GameObject lCanvas;

    private int playerLife;           // Vida atual do jogador
    private int maxLife;              // Vida máxima do jogador

    void Start()
    {
        // Localiza o Canvas_Loading e obtém o script gamelogic
        lCanvas = GameObject.Find("Canvas_Loading");
        gamelogic = lCanvas.GetComponent<gamelogic>();

        // Verifica se o script gamelogic foi atribuído corretamente
        if (gamelogic == null)
        {
            Debug.LogError("O objeto 'gamelogic' não foi encontrado no Canvas_Loading.");
            enabled = false;
            return;
        }

        // Inicializa os valores de playerLife e maxLife
        FetchHealthValues();
        UpdateHealthText();
    }

    void Update()
    {
        // Atualiza os valores de playerLife e maxLife em cada frame
        FetchHealthValues();
        UpdateHealthText();
    }

    private void FetchHealthValues()
    {
        // Obtém os valores de player_life e maxlife do script gamelogic
        playerLife = (int)gamelogic.GetType().GetField("player_life").GetValue(gamelogic);
        maxLife = (int)gamelogic.GetType().GetField("maxlife").GetValue(gamelogic);

        // Garante que playerLife não exceda maxLife
        playerLife = Mathf.Clamp(playerLife, 0, maxLife);
    }

    private void UpdateHealthText()
    {
        // Atualiza o texto com o formato "player_life / maxlife"
        healthText.text = $"{playerLife} / {maxLife}";
    }
}
