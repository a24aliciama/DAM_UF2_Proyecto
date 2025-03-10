using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody rb;

    public float minForce = 12f , maxForce = 16f, minTorque = -10f, maxTorque = -10f;
    private float xRange = 4f, yRange = -5f;

    private GameManager gm;

    public int scoreValue;

    public ParticleSystem boom; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();

        rb.AddForce(RandomForce(), ForceMode.Impulse);
        rb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomPosition();
    }

    void OnMouseOver()
    {

        if(gm.gameState != GameManager.GameState.pause){
            // Si se mantiene pulsado el botón izquierdo del ratón
            if (Input.GetMouseButton(0))
            {
                Destroy(gameObject);

                Instantiate(boom, transform.position, transform.rotation);

                if(gameObject.tag == "Good"){
                    gm.UpdateScore(scoreValue);
                }else{
                    gm.UpdateScore(-scoreValue);
                    gm.UpdateVidas(1);
                    gm.destruccion();
                }

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

            if(gameObject.tag == "Good"){
                gm.UpdateScore(-scoreValue);
                gm.UpdateVidas(1);
            }else{
                gm.UpdateScore(scoreValue);
            }
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
