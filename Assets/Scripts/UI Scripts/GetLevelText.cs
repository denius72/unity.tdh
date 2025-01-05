using UnityEngine;
using TMPro; // Necessário para usar TextMeshPro

public class GetLevelText : MonoBehaviour
{
    public TextMeshProUGUI levelText; // Referência ao componente TextMeshPro
    private gamelogic gamelogic;      // Referência ao script gamelogic
    private GameObject lCanvas;

    private int playerLevel;          // Nível atual do jogador
    private const int maxLevel = 99;  // Nível máximo é fixo

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

        // Inicializa o valor de playerLevel
        FetchLevelValue();
        UpdateLevelText();
    }

    void Update()
    {
        // Atualiza o valor de playerLevel em cada frame
        FetchLevelValue();
        UpdateLevelText();
    }

    private void FetchLevelValue()
    {
        // Obtém o valor de player_level do script gamelogic
        playerLevel = (int)gamelogic.GetType().GetField("player_level").GetValue(gamelogic);

        // Garante que playerLevel não exceda maxLevel
        playerLevel = Mathf.Clamp(playerLevel, 0, maxLevel);
    }

    private void UpdateLevelText()
    {
        // Atualiza o texto com o prefixo "Lv." e o nível atual do jogador
        levelText.text = $"Lv. {playerLevel}";
    }
}
