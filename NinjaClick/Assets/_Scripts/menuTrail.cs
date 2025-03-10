using UnityEngine;

public class menuTrail : MonoBehaviour
{
    private Camera cam;
    private GameObject gm;
  

    void Start()
    {
        // Se asigna la cámara principal
        cam = Camera.main;  
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
                Vector3 mousePos = Input.mousePosition;
                // Ajusta este valor según la distancia de la cámara y tu escena
                mousePos.z = 10f; 
                Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
                // Solo se actualiza la posición si se mantiene pulsado el click izquierdo
                transform.position = worldPos;
        }
        
    }
}