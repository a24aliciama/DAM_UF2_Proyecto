using UnityEngine;

public class TargetZen : MonoBehaviour
{
    private Rigidbody rb;

    public ZenManager gm;

    public float minForce = 12f , maxForce = 16f, minTorque = -10f, maxTorque = -10f;
    private float xRange = 4f, yRange = -5f;

    public ParticleSystem boom; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject managerObject = GameObject.FindWithTag("manager");
        gm =  managerObject.GetComponent<ZenManager>();

        rb.AddForce(RandomForce(), ForceMode.Impulse);
        rb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomPosition();
    }

    void OnMouseOver()
    {
            // Si se mantiene pulsado el botón izquierdo del ratón
            if (Input.GetMouseButton(0))
            {
                 if(gm.gameState != ZenManager.GameState.pause){
                    Destroy(gameObject);
                    Instantiate(boom, transform.position, transform.rotation);

                     if (gameObject.CompareTag("Good"))
                    {
                        Effects.instance.PlaySoundEffect("Good");
                    }
                    else if (gameObject.CompareTag("Bad"))
                    {
                        Effects.instance.PlaySoundEffect("Bad");
                    }
                 }

                
            }
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("killZone"))
        {
            Destroy(gameObject);
        }
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }

    private float RandomTorque(){
        return Random.Range(minTorque, maxTorque);
    }

     private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-xRange,xRange), yRange);
    }

    
}
