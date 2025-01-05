using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GetHealthSlider : MonoBehaviour
{
    public Slider healthSlider; // O slider da barra de HP
    private gamelogic gamelogic; // Referência ao objeto gamelogic
    private GameObject lCanvas;

    private float playerLife; // Saúde atual do jogador
    private float maxLife;    // Saúde máxima do jogador

    void Start()
    {
        lCanvas = GameObject.Find("Canvas_Loading");;
        gamelogic = lCanvas.GetComponent<gamelogic>();
        // Certifique-se de que o objeto gamelogic não está vazio
        if (gamelogic == null)
        {
            Debug.LogError("O objeto 'gamelogic' não foi atribuído ao script HealthBar.");
            enabled = false;
            return;
        }

        // Inicializa playerLife e maxLife a partir do gamelogic
        FetchLifeValues();
        UpdateHealthBar();
    }

    void Update()
    {
        // Atualiza playerLife e maxLife em cada frame
        FetchLifeValues();
        UpdateHealthBar();
    }

    private void FetchLifeValues()
    {
        // Tenta obter os valores de player_life e maxlife do gamelogic
        playerLife = (float)(int)gamelogic.GetType().GetField("player_life").GetValue(gamelogic);
        maxLife = (float)(int)gamelogic.GetType().GetField("maxlife").GetValue(gamelogic);
    }

    private void UpdateHealthBar()
    {
        if (maxLife <= 0)
        {
            Debug.LogWarning("maxLife deve ser maior que zero!");
            healthSlider.value = 0;
            return;
        }

        // Calcula o valor normalizado (0 a 1) para o slider
        float normalizedHealth = Mathf.Clamp01(playerLife / maxLife);

        // Atualiza o slider
        healthSlider.value = normalizedHealth;
    }
}
