using UnityEngine;

public class TrailFollowMouse : MonoBehaviour
{
    private Camera cam;
    private GameManager gm;
  

    void Start()
    {
        // Se asigna la cámara principal
        cam = Camera.main;  
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        
        if(gm.gameState != GameManager.GameState.pause){
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
}