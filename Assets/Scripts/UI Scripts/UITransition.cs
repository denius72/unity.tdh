using UnityEngine;
using System.Collections;

public class UITransition : MonoBehaviour
{
    public RectTransform uiElement; // Elemento da UI a ser animado
    public float transitionDuration = 1.0f; // Duração da transição
    public Vector2 startPosition; // Posição inicial (fora da tela)
    public Vector2 endPosition; // Posição final (dentro da tela)

    private void Start()
    {
        // Garante que o elemento comece na posição inicial
        if (uiElement != null)
        {
            uiElement.anchoredPosition = startPosition;
        }
    }

    // Coroutine para realizar a transição com delay
    public IEnumerator TransitionUI(bool entering, float delay = 0f)
    {
        // Aguarda o delay antes de começar a transição
        if (delay > 0f)
        {
            yield return new WaitForSeconds(delay);
        }

        float elapsedTime = 0f;

        // Define os pontos inicial e final com base na direção
        Vector2 from = entering ? startPosition : endPosition;
        Vector2 to = entering ? endPosition : startPosition;

        // Loop até completar a duração da transição
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;

            // Cálculo do progresso com uma curva Ease Out para "freada" suave
            float t = elapsedTime / transitionDuration;
            float progress = 1f - Mathf.Pow(1f - t, 3); // Curva cúbica de desaceleração (Ease Out)
            
            uiElement.anchoredPosition = Vector2.Lerp(from, to, progress);

            yield return null; // Espera até o próximo frame
        }

        // Garante que a posição final esteja precisa ao final da transição
        uiElement.anchoredPosition = to;
    }
}
