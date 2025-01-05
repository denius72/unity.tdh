using UnityEngine;
using TMPro; // Necessário para usar TextMeshPro

public class GetMPText : MonoBehaviour
{
    public TextMeshProUGUI mpText; // Referência ao componente TextMeshPro
    private gamelogic gamelogic;   // Referência ao script gamelogic
    private GameObject lCanvas;

    private int playerMP;          // MP atual do jogador
    private int maxMP;             // MP máximo do jogador

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

        // Inicializa os valores de playerMP e maxMP
        FetchMPValues();
        UpdateMPText();
    }

    void Update()
    {
        // Atualiza os valores de playerMP e maxMP em cada frame
        FetchMPValues();
        UpdateMPText();
    }

    private void FetchMPValues()
    {
        // Obtém os valores de player_mp e maxmp do script gamelogic
        playerMP = (int)gamelogic.GetType().GetField("player_mp").GetValue(gamelogic);
        maxMP = (int)gamelogic.GetType().GetField("maxmp").GetValue(gamelogic);

        // Garante que playerMP não exceda maxMP
        playerMP = Mathf.Clamp(playerMP, 0, maxMP);
    }

    private void UpdateMPText()
    {
        // Atualiza o texto com o formato "player_mp / maxmp"
        mpText.text = $"{playerMP} / {maxMP}";
    }
}
