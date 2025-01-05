using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GetMPSlider : MonoBehaviour
{
    public Slider healthSlider; // O slider da barra de HP
    private gamelogic gamelogic; // Referência ao objeto gamelogic
    private GameObject lCanvas;

    private float playermp; // Saúde atual do jogador
    private float maxmp;    // Saúde máxima do jogador

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

        // Inicializa playermp e maxmp a partir do gamelogic
        FetchmpValues();
        UpdateHealthBar();
    }

    void Update()
    {
        // Atualiza playermp e maxmp em cada frame
        FetchmpValues();
        UpdateHealthBar();
    }

    private void FetchmpValues()
    {
        // Tenta obter os valores de player_mp e maxmp do gamelogic
        playermp = (float)(int)gamelogic.GetType().GetField("player_mp").GetValue(gamelogic);
        maxmp = (float)(int)gamelogic.GetType().GetField("maxmp").GetValue(gamelogic);
    }

    private void UpdateHealthBar()
    {
        if (maxmp <= 0)
        {
            Debug.LogWarning("maxmp deve ser maior que zero!");
            healthSlider.value = 0;
            return;
        }

        // Calcula o valor normalizado (0 a 1) para o slider
        float normalizedHealth = Mathf.Clamp01(playermp / maxmp);

        // Atualiza o slider
        healthSlider.value = normalizedHealth;
    }
}
