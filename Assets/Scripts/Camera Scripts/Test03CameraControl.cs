using UnityEngine;

public class Test03CameraControl : MonoBehaviour
{
    // Objeto que a câmera deve observar
    public GameObject targetObject;

    // Offset vertical para ajustar o ponto de foco acima do alvo
    public float verticalOffset = 5.0f;

    void Update()
    {
        // Certifique-se de que há um alvo definido
        if (targetObject != null)
        {
            // Calcula a posição ajustada acima do alvo
            Vector3 targetPosition = targetObject.transform.position + Vector3.up * verticalOffset;

            // Faz a câmera olhar para a posição ajustada
            transform.LookAt(targetPosition);
        }
    }

    // Método público para definir o alvo em tempo de execução
    public void SetTarget(GameObject newTarget)
    {
        targetObject = newTarget;
    }

    // Método público para ajustar o offset vertical em tempo de execução
    public void SetVerticalOffset(float offset)
    {
        verticalOffset = offset;
    }
}