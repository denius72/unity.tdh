using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconHeadsUp : MonoBehaviour
{
    public GameObject rotatingObject; // Objeto que irá girar
    private float height = 6.0f;       // Altura acima da cabeça do personagem
    private float rotationSpeed = 50f; // Velocidade de rotação em graus por segundo

    private float scaleOscillationSpeed = 2f; // Velocidade de oscilação do tamanho
    private float scaleMin = 30.8f;            // Escala mínima do objeto
    private float scaleMax = 35.2f;   

    private Vector3 offset;

    private void Start()
    {
        if (rotatingObject == null)
        {
            Debug.LogError("Nenhum objeto foi atribuído para girar.");
            return;
        }

        offset = new Vector3(0, height, 0);

        rotatingObject.transform.position = transform.position + offset;
    }

    private void Update()
    {
        if (rotatingObject != null)
        {
            // Mantém o objeto acima da cabeça do personagem
            rotatingObject.transform.position = transform.position + offset;

            // Faz o objeto girar ao redor de seu próprio eixo Y
            rotatingObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

            // Faz o tamanho do objeto oscilar com base em uma função senoidal
            float scale = Mathf.Lerp(scaleMin, scaleMax, (Mathf.Sin(Time.time * scaleOscillationSpeed) + 1) / 2);
            rotatingObject.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o jogador entrou no Collider
        if (other.CompareTag("Player"))
        {
            rotatingObject.SetActive(true); // Torna o objeto visível
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verifica se o jogador saiu do Collider
        if (other.CompareTag("Player"))
        {
            rotatingObject.SetActive(false); // Torna o objeto invisível
        }
    }
}
