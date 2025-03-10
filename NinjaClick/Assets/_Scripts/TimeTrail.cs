using UnityEngine;

public class TimeTrail : MonoBehaviour
{
    private Camera cam;
    private TimeManager gm;
  

    void Start()
    {
        // Se asigna la cámara principal
        cam = Camera.main;  
        gm = GetComponent<TimeManager>();
    }

    void Update()
    {
        if(gm.gameState != TimeManager.GameState.pause){
            if (Input.GetMouseButton(0))
                    {
                            Vector3 mousePos = Input.mousePosition;
                            //distancia de la cámara y escena
                            mousePos.z = 10f; 
                            Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
                            // Solo se actualiza la posición si se mantiene pulsado el click izquierdo
                            transform.position = worldPos;
                    }
                    
        }
        
    }
}