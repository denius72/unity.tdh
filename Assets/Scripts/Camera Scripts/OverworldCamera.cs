using UnityEngine;

public class OverworldCamera : MonoBehaviour
{
    // Objeto que a câmera deve seguir
    public GameObject targetObject;

    // Posição inicial da câmera (usada como offset)
    private Vector3 initialOffset;

    void Start()
    {
        // Armazena a posição inicial da câmera como offset
        if (targetObject != null)
        {
            initialOffset = transform.position - targetObject.transform.position;
        }
    }

    void Update()
    {
        // Certifique-se de que há um alvo definido
        if (targetObject != null)
        {
            // Atualiza a posição da câmera com base na posição do alvo e no offset inicial
            transform.position = targetObject.transform.position + initialOffset;
        }
    }

    // Método público para definir o alvo em tempo de execução
    public void SetTarget(GameObject newTarget)
    {
        targetObject = newTarget;

        // Atualiza o offset inicial com base no novo alvo
        if (targetObject != null)
        {
            initialOffset = transform.position - targetObject.transform.position;
        }
    }
}
