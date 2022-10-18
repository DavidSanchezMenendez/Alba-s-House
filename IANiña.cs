using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IANiña : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public Transform objetivo;
    public NavMeshAgent agente;
    public Rigidbody rb;
    float distancia, distanciaY;
    public Transform[] posiciones;
    public int random;
    public float ComenzaraPerseguir; //Esta es la distancia a la que te empezará a perseguir
    public bool MismoPiso, pisoArriba, pisoAbajo, pisoAbajoPlayer, pisoArribaPlayer, Caza = false, unavez = true;
    float EsperaCaza = 2f, TiempoZero = 0f;
    public AudioSource AudioController, AudioController2;

    public bool sonidocaza;
    public bool kill = false;
    public Animator anim;
    public bool StartCaza=false;
   
    void Start()
    {
    
        sonidocaza = true;
        //AudioController = GetComponent<AudioSource>();
        agente = GetComponent<NavMeshAgent>();
        random = Random.Range(0, 9);
        anim = GetComponent<Animator>();
        agente.speed = 8f;


    }

    // Update is called once per frame
    void Update()
    {

        distancia = Vector3.Distance(objetivo.position, transform.position);
       // if (StartCaza)
        //{
            
            Perseguir();
            PosicionPiso();
            ModoCaza();
            MirarPlayer();
           
        //}
       
        
        

    }
    public void Perseguir()
    {

        if (distancia < 10 && MismoPiso && !Caza)
        {
            agente.destination = objetivo.position;
            Debug.Log("persiguiendo");

        }
        else if (!Caza)
            agente.destination = posiciones[random].position;
        Debug.Log("NavegandoPorlacasa");

    }
    IEnumerator Espera()
    {
        
        yield return new WaitForSeconds(0f);
        int repetido = random;
        random = Random.Range(0, 9);
        while (repetido == random)

        {

            random = Random.Range(0, 9);
        }
    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.transform == posiciones[random])
        {

            
            StartCoroutine(Espera());
            

          





        }
        if (collision.gameObject.transform.tag == "DejarDeCorrer")//le damos speed porque la señora no puede subir la escalera
        {

           
            agente.speed = 8f;




        }
        if (collision.gameObject.transform.tag == "CorrerEscalera")
        {

           
            agente.speed = 20f;




        }


    }
    void PosicionPiso()
    {
        if (transform.position.y > 43)//detectamos en que piso se encuentran por la posicion y del transform
        {
            pisoArriba = true;
            pisoAbajo = false;
        }
        else if (transform.position.y < 30)
        {
            pisoAbajo = true;
            pisoArriba = false;
        }

        if (objetivo.position.y > 43)
        {
            pisoArribaPlayer = true;
            pisoAbajoPlayer = false;
        }
        else if (objetivo.position.y < 30)
        {
            pisoAbajoPlayer = true;
            pisoArribaPlayer = false;
        }

        if (pisoAbajo == pisoAbajoPlayer || pisoArriba == pisoArribaPlayer)//si ambos estan en el mismo piso a la vez el valor de mismo piso es true, en caso contrario(cada uno en un piso diferente) es false. Esto para que no lo siga aunque el rango sea menor al inidcado porque puede que lo esté detectando arriba suya, entonces con esto solo puede seguiro estando al mismo piso
        {
            
            StartCoroutine(SubiroBajar());//damos unos segundos de espera
            if (unavez)
            {
                AudioController.Play();
                unavez = false;
            }
            

        }
        else
        {
            unavez = true;//para que el sonido no se bugue y empieze a sonar todo el rato
            MismoPiso = false;
            AudioController.Stop();
        }









    }

    IEnumerator SubiroBajar()
    {

        yield return new WaitForSeconds(3f);
        MismoPiso = true;
        Debug.Log("EsperaPiso");




    }
    IEnumerator CazaTiempo()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("Caza OF");
        anim.SetBool("ModoCaza", false);
        agente.speed = 8;
        Caza = false;
        sonidocaza = true;

        AudioController2.Stop();
        //Vector3 ultimaposicion = objetivo.transform.position;
    }
    void ModoCaza()
    {
        RaycastHit hitInfo;




        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, 200f))//esta linia hace que si desde la camara hacia delante hay algun objeto es true y entra en el if
        {




            if (hitInfo.transform.tag == "Player")
            {


                agente.speed = 18;
                
                    Caza = true;
                
                
                if (sonidocaza)
                {
                    AudioController2.Play();
                    sonidocaza = false;

                }
                
                    anim.SetBool("ModoCaza", true);
                
                





              


            }
            if (Caza)
            {

                if (hitInfo.transform.tag == "Untagged")
                {

                    if (Time.time > TiempoZero)
                    {
                        TiempoZero = Time.time + EsperaCaza;
                        StartCoroutine(CazaTiempo());//le damos un tiempo de caza mas despures de que pase un tiempo que ha perdido al jugador y para que el sonido y el modo caza no se buge mucho
                    }

                }
                agente.destination = objetivo.position;
            }


        }
        
    }
    void MirarPlayer()//Basicamente con esto hacermos que una camara que tenemos parentada en la niña mire en la posicion del player siempre
    {
        cam.transform.LookAt(objetivo);
        
    }
}
   

