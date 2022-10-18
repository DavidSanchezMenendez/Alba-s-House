using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MovimientoPersonaje : MonoBehaviour
{

    public Button Interactuar,escndersecama,agacharselevantarse,acuchillar;
    public Joystick joystiackMove, joystickRotate;

    public Slider _slider;
    public float sensibilidad = 2f;
    public Text text;
    int targetFrameRate = 60;
    int contador = 0, contador1 = 0, contador2 = 0, contador3 = 0, contador4 = 0, contadorLuz = 0, contadorLuz1 = 0, contadorLuz2 = 0, contadorLuz3 = 0, contadorLuz4 = 0, contadorLuz5 = 0, contadorLuz6 = 0, contadorLuz7 = 0,contadoragachado=0,exitenter=0;
    //MOVIMIENTO
    public CharacterController controller;
    public float speed = 15f;
    public Transform player, peluche;
    public float JumpSpeed;
    public float CharacterVelocityY;
    public Transform agacharse, Levantarse, linterna, posicionLinterna, posicionpeluche, posicioncuchillo, LuzMuerte;
    public Transform PoscicionAsesinato;
    Vector3 move, posicionInical;
    public Camera cam;
    bool TieneLinterna = false, SeMuere = false;
    public GameObject[] luz;

    public float gravedad;
    public float SonidoPasos, x, z;

    //CAMERA 
    public float mouseSeniblidiad = 1000f;
    public Transform PlayerBody,entrarcama,entrarcama1;
    float xRotation = 0f;
    public GameObject Luz;
    Vector3 inical;
    IANiña NiñaMuerta;
    private Rigidbody rbNiña;
    private Animator anim;
    private Animator casilla;
    private Animator casilla2;
    private Animator cofre;
    private Animator PuertaNiña;
    private Animator Puerta;
    //public GameObject animador;
    public AudioClip[] audioClip;
    private AudioSource sonidoAmbiente;
    private AudioSource SonidoPuerta;
    private AudioSource SonidoTaquilla;
    private AudioSource SonidoLinterna;
    private AudioSource SonidoLuces;
    private AudioSource SonidoCuchillo;
    public AudioSource audiocontroller, SonidoCaja, cuchillazo, muerteNiña, RisaSonido, LuzRotaSonido, SonidosObjetos;
    public AudioSource Assesinar;
    public GameObject MUERTE, VIDA;
    //public AudioSource SonidoMuerte;
    private bool puerta = false, puertaNiña = false, tengopeluche = false, Estoyvivo = true, lucesOF = false, encendida = false,agachado=false;
    GameObject llave, cuchillo, llave1, llave2, menumuerte;


    public bool Moverse;
    //Cosas de android
    private Vector3 firstpoint; //change type on Vector3
    private Vector3 secondpoint;
    private float xAngle = 0.0f; //angle for axes x for rotation
    private float yAngle = 0.0f;
    private float xAngTemp = 0.0f; //temp variable for angle
    private float yAngTempt = 0.0f;
    private float halfScreenWidth;
    private Ads ads;
    float yAngTemp;


    public bool caminando, cursor = true, cajita = false, puertafinal = false, cojercuchillo = false, niñamuerta = false, sonidoMuerteNiña = true, haempezadolacaza = false, dejardecuchillar = false,movebed=false;


    private void Start()
    {
        ads = GameObject.Find("Ads").GetComponent<Ads>();

        escndersecama.gameObject.SetActive(false);
        SonidoPasos = 30f;
        //Cosas de android
        xAngle = 90.0f;
        yAngle = 0.0f;
        this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
        halfScreenWidth = Screen.width / 2;

        Interactuar.gameObject.SetActive(false);

        //////////////////////
        SonidosObjetos = GameObject.Find("Objetos").GetComponent<AudioSource>();
        menumuerte = GameObject.Find("Muerte");
        LuzRotaSonido = GameObject.Find("LuzRota").GetComponent<AudioSource>();
        RisaSonido = GameObject.Find("SonidoRisa").GetComponent<AudioSource>();
        muerteNiña = GameObject.Find("Cuerpo2").GetComponent<AudioSource>();
        rbNiña = GameObject.Find("NiñaMuertablend").GetComponent<Rigidbody>();
        cuchillazo = GameObject.Find("PosicionCuchillo").GetComponent<AudioSource>();
        cuchillo = GameObject.Find("cuchillo");
        SonidoCaja = GameObject.Find("Cajita").GetComponent<AudioSource>();
        llave = GameObject.Find("Llave");
        llave1 = GameObject.Find("Llave1");
        llave2 = GameObject.Find("Llave2");
        SonidoLuces = GameObject.Find("EnchufeLuz (1)").GetComponent<AudioSource>();
        //SonidoMuerte = GameObject.Find("CameraMain").GetComponent<AudioSource>();
        SonidoLinterna = GameObject.Find("Linterna").GetComponent<AudioSource>();
        Moverse = true;
        SonidoTaquilla = GameObject.Find("Taquilla").GetComponent<AudioSource>();
        SonidoPuerta = GameObject.Find("Puerta").GetComponent<AudioSource>();
        sonidoAmbiente = GameObject.Find("envoirment").GetComponent<AudioSource>();
        Puerta = GameObject.Find("Puerta").GetComponent<Animator>();
        PuertaNiña = GameObject.Find("PuertaNiña").GetComponent<Animator>();
        casilla = GameObject.Find("Esconidte").GetComponent<Animator>();
        casilla2 = GameObject.Find("Esconidte2").GetComponent<Animator>();
        cofre = GameObject.Find("cofre").GetComponent<Animator>();
        //animador = anim = GameObject.Find("CameraMain").GetComponent<GameObject>();
        anim = GameObject.Find("CameraMain").GetComponent<Animator>();
        audiocontroller = GetComponent<AudioSource>();
        Assesinar = GameObject.Find("Cuerpo").GetComponent<AudioSource>();
        SonidoCuchillo = GameObject.Find("SonidoCuchillo").GetComponent<AudioSource>();
        NiñaMuerta = GameObject.Find("NiñaMuertablend").GetComponent<IANiña>();
        acuchillar.gameObject.SetActive(false);
        QualitySettings.vSyncCount = 0;
        



        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;

        // Cursor.visible = false;

    }
    void Update()
    {

        
        if (cursor)
        {
            //Cursor.lockState = CursorLockMode.Locked;
            // Cursor.visible = false;
        }


        if (Moverse)
        {
            MovimientoDelPersonaje();
            //MovimientoCamera();
            if (!movebed)
            {
                PortaMovil();
            }
            
            MoveTouch();
        }
        CogerObjeto();
        EncenderLuces();
        AnimacionesPuetras();
        CMuere();
        //Acuchillar();
        Cursor.visible = true;




        NiñaMuerta.ComenzaraPerseguir = SonidoPasos;//Le pasamos lo que será el rango de sonido el cual tendrá la niña para persegir a nuestro player

    }
    public void MoveTouch()
    {

        for (int i = 0; i < Input.touchCount; i++)
        {


            Touch t = Input.GetTouch(i);

            
                //Touch began, save position
                if (t.phase == TouchPhase.Began && t.position.x > halfScreenWidth)
                {
                    firstpoint = t.position;
                    xAngTemp = xAngle;
                    yAngTemp = yAngle;
                }
                //Move finger by screen
                if (t.phase == TouchPhase.Moved && t.position.x > halfScreenWidth)
                {
                    secondpoint = t.position;
                    //Mainly, about rotate camera. For example, for Screen.width rotate on 180 degree
                    xAngle = xAngTemp + (secondpoint.x - firstpoint.x) * 180.0f / Screen.width * sensibilidad ;
                ;
                yAngle = yAngTemp - (secondpoint.y - firstpoint.y) * 90.0f / Screen.height * sensibilidad ;
                ;
                //Rotate camera
                float yRotacion = Mathf.Clamp(yAngle, -90, 90); //Para bloquear que no pueda mirar hacia arriba o hacia abajo infinitamente
                if (movebed)
                {
                    yRotacion = 0f;
                }
                cam.transform.rotation = Quaternion.Euler(yRotacion, xAngle, 0.0f);



                

            }

        }
    }




    
    public void PortaMovil()
    {


        x = joystiackMove.Horizontal + Input.GetAxis("Horizontal");
        z = joystiackMove.Vertical + Input.GetAxis("Vertical");
        move = cam.transform.right * x + cam.transform.forward * z;
        if (!movebed)
        {
            controller.Move(move * speed * Time.deltaTime);
        }
        


        if (x == 0f && z ==0f)
        {
            SonidoPasos = 0;
            audiocontroller.Stop();
            caminando = true;
            
        }
        else if (agachado)
        {
            SonidoPasos = 5f;
            audiocontroller.pitch = 1f;
            audiocontroller.volume = 0.1f;
            

        }
        else 
        {
            
            SonidoPasos = 30f;
            audiocontroller.pitch = 1.2f;
            audiocontroller.volume = 0.5f;

        }
        if (x != 0.0f && z != 0.0f   && caminando )
        {
            audiocontroller.Play();
            caminando = false;
           
        }
      



        /* float rotateH = joystickRotate.Horizontal * 3;
         float rotateV = joystickRotate.Vertical * 3;

         xRotation -= rotateV * sensibilidad;
         xRotation = Mathf.Clamp(xRotation, -90, 90); //Para bloquear que no pueda mirar hacia arriba o hacia abajo infinitamente

         cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
          PlayerBody.Rotate(Vector3.up * xAngle * sensibilidad);//para mirar a la derecha y izquierda y rotar el cuerpo
        */





    }


    public void AgachareseMovil()
    {
        // audiocontroller.pitch = 0.8f;
        //audiocontroller.volume = 0.2f;
        if (!movebed)
        {
            anim.enabled = false;



            contadoragachado++;
            if (contadoragachado % 2 == 1)
            {
                cam.transform.position = Vector3.MoveTowards(cam.transform.position, agacharse.position, 10f);
                speed = 7;
                agacharselevantarse.gameObject.SetActive(false);
                agachado = true;

            }
            else
            {
                cam.transform.position = Vector3.MoveTowards(cam.transform.position, Levantarse.position, 10f);
                speed = 15;

                agacharselevantarse.gameObject.SetActive(true);
                agachado = false;

            }

        }

    }



    public void Acuchillar()
    {
        if (Input.GetMouseButtonDown(0) && tengopeluche && Estoyvivo && !dejardecuchillar)
        {
            //inical = posicioncuchillo.transform.position;
            posicioncuchillo.transform.position = Vector3.MoveTowards(posicioncuchillo.transform.position, posicionpeluche.position, 1f);
            
            cuchillazo.Play();
            niñamuerta = true;
            NiñaMuerta.anim.SetBool("CMuere", true);
            NiñaMuerta.anim.SetBool("ModoCaza", false);
            Destroy(rbNiña);
            NiñaMuerta.enabled = false;
            NiñaMuerta.AudioController.Stop();
            if (sonidoMuerteNiña)
            {
                muerteNiña.Play();
                sonidoMuerteNiña = false;
            }
            StartCoroutine(dejarObjetos());
            if (!dejardecuchillar)
            {
                text.text = "Back to Start";
            }


        }
        if (Input.GetMouseButtonUp(0) && tengopeluche)
        {
            posicioncuchillo.transform.position = Vector3.MoveTowards(posicioncuchillo.transform.position, posicionLinterna.transform.position, 1f);
        }
    }
    public void AcuchillarMovil()
    {
        //inical = posicioncuchillo.transform.position;
        posicioncuchillo.transform.position = Vector3.MoveTowards(posicioncuchillo.transform.position, posicionpeluche.position, 1f);
        
        cuchillazo.Play();
        niñamuerta = true;
        NiñaMuerta.anim.SetBool("CMuere", true);
        NiñaMuerta.anim.SetBool("ModoCaza", false);
        Destroy(rbNiña);
        NiñaMuerta.enabled = false;
        NiñaMuerta.AudioController.Stop();
        NiñaMuerta.agente.speed=0;
        Interactuar.gameObject.SetActive(false);
        acuchillar.gameObject.SetActive(false);
        if (sonidoMuerteNiña)
        {
            muerteNiña.Play();
            sonidoMuerteNiña = false;
        }
        StartCoroutine(dejarObjetos());
        if (!dejardecuchillar)
        {
            text.text = "Back to start";
           
        }
    }
    IEnumerator dejarObjetos()
    {

        
        yield return new WaitForSeconds(4f);
        acuchillar.gameObject.SetActive(false);
        linterna.gameObject.SetActive(true);
        peluche.gameObject.SetActive(false);
        cuchillo.gameObject.SetActive(false);
        dejardecuchillar = true;
        text.text = "";


    }
    public void MovimientoDelPersonaje()
    {
        /* x = Input.GetAxis("Horizontal");//A D
         z = Input.GetAxis("Vertical");//W S

         if (Input.GetKey(KeyCode.LeftShift))
         {

             audiocontroller.volume = 1f;
             audiocontroller.pitch = 1.5f;

             speed = 18f;
             SonidoPasos = 50f;
             anim.enabled = true;
             anim.speed = 1.35f;


         }
         else if (Input.GetKey(KeyCode.LeftControl))
         {
             audiocontroller.pitch = 0.8f;
             audiocontroller.volume = 0.2f;
             anim.enabled = false;

             speed = 5;
             SonidoPasos = 5;
             cam.transform.position = Vector3.MoveTowards(cam.transform.position, agacharse.position, 0.2f);


         }
         else
         {


             audiocontroller.pitch = 1.2f;
             audiocontroller.volume = 0.5f;

             anim.speed = 1;
             cam.transform.position = Vector3.MoveTowards(cam.transform.position, Levantarse.position, 0.2f);
             speed = 10f;
             SonidoPasos = 35;


         }
         if (cam.transform.position == Levantarse.position)
         {
             anim.speed = 1;
             anim.enabled = true;
             anim.SetBool("Caminar", true);
         }
         if (z > 0 && caminando)//Si nuestro player no tiene velocidad no hace ruido.
         {
             audiocontroller.Play();

             caminando = false;
         }

         if (x.Equals(0) && z.Equals(0))//Si nuestro player no tiene velocidad no hace ruido.
         {
             audiocontroller.Stop();
             SonidoPasos = 5f;
             anim.speed = 0f;
             caminando = true;
         }





         move = transform.right * x * speed + transform.forward * z * speed;//para que cambie la direcion con la camara*/


        if (controller.isGrounded)
        {
            CharacterVelocityY = 0;
            if (PulsarSpace())
            {
                JumpSpeed = 15f;
                CharacterVelocityY = JumpSpeed;

            }


        }




        gravedad = -60;
        CharacterVelocityY += gravedad * Time.deltaTime;


        move.y = CharacterVelocityY;




        //Finalizamos con el movimiento y dandole las variables
        controller.Move(move * Time.deltaTime);






    }

    public void MovimientoCamera()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSeniblidiad * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSeniblidiad * Time.deltaTime;

        xRotation -= mouseY * sensibilidad;

        xRotation = Mathf.Clamp(xRotation, -90, 70); //Para bloquear que no pueda mirar hacia arriba o hacia abajo infinitamente



        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);//Para mirar arriba y abajo



        PlayerBody.Rotate(Vector3.up * mouseX * sensibilidad);//para mirar a la derecha y izquierda y rotar el cuerpo
    }

    private void ResetGravedad()
    {
        CharacterVelocityY = 0;
    }

    private bool PulsarSpace()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    void CogerObjeto()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, 10f))//esta linia hace que si desde la camara hacia delante hay algun objeto es true y entra en el if
        {

            if (hitInfo.transform.tag == "Objeto")
            {
                Interactuar.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SonidosObjetos.Play();
                    TieneLinterna = true;
                    linterna.position = posicionLinterna.position;
                    linterna.rotation = posicionLinterna.rotation;
                    linterna.parent = posicionLinterna;
                }

            }
            else
            {
                Interactuar.gameObject.SetActive(false);
            }
            if (hitInfo.transform.tag == "llave")
            {
                Interactuar.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {

                    Destroy(llave);
                    puerta = true;
                    SonidosObjetos.Play();

                }

            }

            if (hitInfo.transform.tag == "llave1")
            {
                Interactuar.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {

                    Destroy(llave1);
                    cajita = true;
                    SonidosObjetos.Play();

                }


            }
            if (hitInfo.transform.tag == "llave2")
            {
                Interactuar.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {

                    Destroy(llave2);
                    puertaNiña = true;
                    SonidosObjetos.Play();


                }

            }
            if (hitInfo.transform.tag == "Cofre")
            {
                Interactuar.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && cajita)
                {

                    cofre.SetBool("cajita", true);
                    puertafinal = true;
                    SonidoCaja.Play();



                }
                else if (Input.GetKeyDown(KeyCode.E) && !cajita)
                {
                    text.text = "You need a key";
                }


            }
            if (hitInfo.transform.tag == "cuchillo")
            {
                Interactuar.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    SonidoCuchillo.Play();
                    cojercuchillo = true;
                    cuchillo.SetActive(false);
                    cuchillo.transform.position = posicioncuchillo.transform.position;
                    cuchillo.transform.rotation = posicioncuchillo.transform.rotation;
                    cuchillo.transform.SetParent(posicioncuchillo);
                    text.text = "";

                    if (!haempezadolacaza)
                    {
                        StartCoroutine(CerrarLuces());
                    }





                    //sonidocuchillo

                }
               
              
            }
            if (hitInfo.transform.tag == "peluche")
            {
                Interactuar.gameObject.SetActive(true);
                Debug.Log("peluche");
                if (Input.GetKeyDown(KeyCode.E) && cojercuchillo)
                {
                    peluche.transform.position = posicionpeluche.transform.position;
                    peluche.transform.rotation = posicionpeluche.transform.rotation;
                    peluche.SetParent(posicionpeluche);
                    linterna.gameObject.SetActive(false);
                    cuchillo.SetActive(true);
                    tengopeluche = true;

                }


            }
        }
        /* if (TieneLinterna)//Encendemos y apagamos linterna con click derecho
         {
             if (Input.GetMouseButtonDown(1))
             {
                 contador++;
                 if (contador % 2 == 0)
                 {
                     SonidoLinterna.Play();
                     Luz.SetActive(false);
                     encendida = false;
                 }
                 else
                 {
                     encendida = true;
                     SonidoLinterna.Play();
                     Luz.SetActive(true);


                 }
             }
         }
         if (encendida)
         {
             SonidoPasos = 35f;
         }*/



    }
    IEnumerator CerrarLuces()
    {
        yield return new WaitForSeconds(2f);

        LuzRotaSonido.Play();
        lucesOF = true;


        yield return new WaitForSeconds(2f);
        RisaSonido.Play();
        NiñaMuerta.StartCaza = true;



    }
    void EncenderLuces()//CopyPaste ZZZ
    {
        if (lucesOF)
        {
            luz[0].SetActive(false);
            luz[1].SetActive(false);
            luz[2].SetActive(false);
            luz[3].SetActive(false);
            luz[4].SetActive(false);
            luz[5].SetActive(false);
            luz[6].SetActive(false);
            luz[7].SetActive(false);
            luz[8].SetActive(false);
            luz[9].SetActive(false);
            luz[10].SetActive(false);
            luz[11].SetActive(false);
            luz[12].SetActive(false);
            luz[13].SetActive(false);
            luz[14].SetActive(false);
            luz[15].SetActive(true);
            luz[16].SetActive(false);
            luz[17].SetActive(false);

        }
        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, 8f))//esta linia hace que si desde la camara hacia delante hay algun objeto es true y entra en el if
        {
            if (hitInfo.transform.tag == "Luz")
            {

                //luz[0] = GameObject.Find("Luz1");
                // luz[1] = GameObject.Find("Luz2");

                if (Input.GetKeyDown(KeyCode.E) && !lucesOF)
                {

                    contadorLuz++;
                    if (contadorLuz % 2 == 0)
                    {
                        luz[0].SetActive(false);
                        luz[1].SetActive(false);
                        SonidoLuces.Play();
                    }
                    else
                    {


                        luz[0].SetActive(true);
                        luz[1].SetActive(true);
                        SonidoLuces.Play();

                    }



                }
            }
        }
        RaycastHit hitInfo1;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo1, 8f))//esta linia hace que si desde la camara hacia delante hay algun objeto es true y entra en el if
        {
            if (hitInfo1.transform.tag == "Luz2" && !lucesOF)
            {



                if (Input.GetKeyDown(KeyCode.E))
                {
                    contadorLuz1++;
                    if (contadorLuz1 % 2 == 0)
                    {
                        luz[2].SetActive(false);
                        luz[3].SetActive(false);
                        SonidoLuces.Play();
                    }
                    else
                    {


                        luz[2].SetActive(true);
                        luz[3].SetActive(true);
                        SonidoLuces.Play();

                    }
                }
            }





        }
        RaycastHit hitInfo3;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo3, 8f))//esta linia hace que si desde la camara hacia delante hay algun objeto es true y entra en el if
        {
            if (hitInfo1.transform.tag == "Luz3" && !lucesOF)
            {



                if (Input.GetKeyDown(KeyCode.E))
                {
                    contadorLuz2++;
                    if (contadorLuz2 % 2 == 0)
                    {
                        luz[4].SetActive(false);
                        luz[5].SetActive(false);
                        luz[6].SetActive(false);
                        SonidoLuces.Play();
                    }
                    else
                    {


                        luz[4].SetActive(true);
                        luz[5].SetActive(true);
                        luz[6].SetActive(true);
                        SonidoLuces.Play();

                    }
                }
            }





        }
        RaycastHit hitInfo4;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo4, 8f))//esta linia hace que si desde la camara hacia delante hay algun objeto es true y entra en el if
        {
            if (hitInfo4.transform.tag == "Luz4" && !lucesOF)
            {



                if (Input.GetKeyDown(KeyCode.E))
                {
                    contadorLuz3++;
                    if (contadorLuz3 % 2 == 0)
                    {
                        luz[7].SetActive(false);
                        luz[8].SetActive(false);
                        luz[9].SetActive(false);
                        luz[10].SetActive(false);
                        luz[11].SetActive(false);
                        SonidoLuces.Play();

                    }
                    else
                    {


                        luz[7].SetActive(true);
                        luz[8].SetActive(true);
                        luz[9].SetActive(true);
                        luz[10].SetActive(true);
                        luz[11].SetActive(true);
                        SonidoLuces.Play();

                    }
                }
            }





        }
        RaycastHit hitInfo5;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo5, 8f))//esta linia hace que si desde la camara hacia delante hay algun objeto es true y entra en el if
        {
            if (hitInfo5.transform.tag == "Luz5" && !lucesOF)
            {



                if (Input.GetKeyDown(KeyCode.E))
                {
                    contadorLuz4++;
                    if (contadorLuz4 % 2 == 0)
                    {
                        luz[12].SetActive(false);
                        luz[13].SetActive(false);
                        SonidoLuces.Play();

                    }
                    else
                    {



                        luz[12].SetActive(true);
                        luz[13].SetActive(true);
                        SonidoLuces.Play();
                    }
                }
            }





        }
        RaycastHit hitInfo6;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo6, 8f))//esta linia hace que si desde la camara hacia delante hay algun objeto es true y entra en el if
        {
            if (hitInfo6.transform.tag == "Luz6" && !lucesOF)
            {



                if (Input.GetKeyDown(KeyCode.E))
                {
                    contadorLuz5++;
                    if (contadorLuz5 % 2 == 0)
                    {
                        luz[14].SetActive(false);

                        SonidoLuces.Play();

                    }
                    else
                    {



                        luz[14].SetActive(true);

                        SonidoLuces.Play();
                    }
                }
            }





        }
        RaycastHit hitInfo7;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo7, 8f))//esta linia hace que si desde la camara hacia delante hay algun objeto es true y entra en el if
        {
            if (hitInfo7.transform.tag == "Luz7")
            {



                if (Input.GetKeyDown(KeyCode.E))
                {
                    contadorLuz6++;
                    if (contadorLuz6 % 2 == 0)
                    {
                        luz[15].SetActive(false);

                        SonidoLuces.Play();

                    }
                    else
                    {



                        luz[15].SetActive(true);
                        SonidoLuces.Play();

                    }
                }
            }





        }
        RaycastHit hitInfo8;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo8, 8f))//esta linia hace que si desde la camara hacia delante hay algun objeto es true y entra en el if
        {
            if (hitInfo8.transform.tag == "Luz8" && !lucesOF)
            {



                if (Input.GetKeyDown(KeyCode.E))
                {
                    contadorLuz7++;
                    if (contadorLuz7 % 2 == 0)
                    {
                        luz[16].SetActive(false);
                        luz[17].SetActive(false);

                        SonidoLuces.Play();

                    }
                    else
                    {




                        luz[16].SetActive(true);
                        luz[17].SetActive(true);
                        SonidoLuces.Play();

                    }
                }
            }






        }


    }
    public void EntrarCama()
    {
        exitenter++;
        if (exitenter % 2 == 0)
        {
            controller.gameObject.SetActive(false);
            cam.transform.position = Levantarse.transform.position;
            PlayerBody.transform.position = new Vector3(-22.53f, 45.57f, 66.98f);
            controller.gameObject.SetActive(true);
            movebed = false;
            escndersecama.gameObject.SetActive(false);
            

        }
        else
        {


            controller.gameObject.SetActive(false);//para poder teleportar a nustro personaje

            PlayerBody.transform.position = new Vector3(0f, 51.08f, 13f);
            cam.transform.position = entrarcama.transform.position;


            movebed = true;

            controller.gameObject.SetActive(true);
        }
    }
  
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.transform.tag == "cama")
        {
            escndersecama.gameObject.SetActive(true);
           



        }
        
        if (other.gameObject.transform.tag == "salir cama")
        {
            escndersecama.gameObject.SetActive(false);




        }
       
        if (other.gameObject.transform.tag == "puerta")
        {

            audiocontroller.clip = audioClip[0];
            audiocontroller.Play();
            sonidoAmbiente.Play();
        }
       

        if (other.gameObject.transform.tag == "PuertaEntrada")
        {
            // Puerta.ResetTrigger("Puerta");
            Debug.Log("HIII");

            sonidoAmbiente.Play();
        }
        if (other.gameObject.transform.tag == "PuertaEntradaFuera")
        {
            // Puerta.ResetTrigger("Puerta");
            Debug.Log("HIII");
            audiocontroller.clip = audioClip[1];
            audiocontroller.Play();

            sonidoAmbiente.Stop();
        }
        if (other.gameObject.transform.tag == "puertaentrada2")
        {
            // Puerta.ResetTrigger("Puerta");
            Debug.Log("HIII");
            audiocontroller.clip = audioClip[0];
            audiocontroller.Play();
            sonidoAmbiente.Play();
        }
        if (other.gameObject.transform.tag == "puertasalida")
        {
            // Puerta.ResetTrigger("Puerta");
            Debug.Log("HIII");
            audiocontroller.clip = audioClip[1];
            audiocontroller.Play();

            sonidoAmbiente.Stop();
        }
        if (other.gameObject.transform.tag == "Niña" && !niñamuerta)
        {

            SeMuere = true;
            StartCoroutine(Esperamuerte());
            Assesinar.Play();
            NiñaMuerta.AudioController.Stop();

        }
        if (other.gameObject.transform.tag == "StartCaza" && !cojercuchillo)
        {
            haempezadolacaza = true;
            //StartCoroutine(CerrarLuces());
            //Destroy(other);

        }
        if (other.gameObject.transform.tag == "Win" && niñamuerta)
        {
            VIDA.SetActive(true);
            Moverse = false;
            cursor = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0;
            audiocontroller.Stop();

        }


    }
    IEnumerator Esperamuerte()
    {
        yield return new WaitForSeconds(2f);
        //SonidoMuerte.Play();
        ads.GameOver();
        yield return new WaitForSeconds(1f);

        MUERTE.SetActive(true);


        cursor = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;





    }



    void AnimacionesPuetras()
    {
        RaycastHit hitInfo10;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo10, 10f))//esta linia hace que si desde la camara hacia delante hay algun objeto es true y entra en el if
        {

            if (hitInfo10.transform.tag == "taquilla")
            {
                Interactuar.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    contador1++;
                    if (contador1 % 2 == 0)
                    {
                        SonidoTaquilla.Play();
                        casilla.SetBool("Casilla", false);
                    }
                    else
                    {
                        SonidoTaquilla.Play();
                        casilla.SetBool("Casilla", true);
                    }


                }

            }


            if (hitInfo10.transform.tag == "a")
            {
                Interactuar.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {

                    contador2++;
                    if (contador2 % 2 == 0)
                    {
                        SonidoTaquilla.Play();
                        casilla2.SetBool("taquilla1", false);
                    }
                    else
                    {
                        SonidoTaquilla.Play();
                        casilla2.SetBool("taquilla1", true);
                    }


                }
            }



                if (hitInfo10.transform.tag == "PuertaEntrada" || hitInfo10.transform.tag == "PuertaEntradaFuera" || hitInfo10.transform.tag == "puerta")
                {
                    Interactuar.gameObject.SetActive(true);
                    if (contador3 % 2 == 0 && puerta)
                    {
                       
                    }
                    else if (contador3 % 2 == 1 && puerta)
                    {
                        
                    }

                    if (!puerta)
                    {
                        //sonido cerrado
                        text.text = "You need a key";
                    }
                    if (Input.GetKeyDown(KeyCode.E) && puerta)
                    {
                        //Puerta.SetTrigger("Puerta");


                        contador3++;
                        if (contador3 % 2 == 0)
                        {

                            SonidoPuerta.Play();
                            Puerta.SetBool("PuertaEntrada", false);
                        }
                        else
                        {
                            SonidoPuerta.Play();
                            Puerta.SetBool("PuertaEntrada", true);
                        }

                    }


                }








                if (hitInfo10.transform.tag == "PuertaNiña")
                {
                    Interactuar.gameObject.SetActive(true);
                    if (contador4 % 2 == 0 && puerta)
                    {
                        //text.text = "[E] Para Abir";
                    }
                    else if (contador4 % 2 == 1 && puerta)
                    {
                        // text.text = "[E] Para Cerrar";
                    }
                    if (!puertaNiña)
                    {
                        //sonido cerrado
                        text.text = "you need a Key";
                    }
                    if (Input.GetKeyDown(KeyCode.E) && puertaNiña)
                    {
                        //Puerta.SetTrigger("Puerta");


                        contador4++;
                        if (contador4 % 2 == 0)
                        {

                            SonidoPuerta.Play();
                            PuertaNiña.SetBool("Puerta1", false);
                        }
                        else
                        {
                            SonidoPuerta.Play();
                            PuertaNiña.SetBool("Puerta1", true);
                        }

                    }

                }
                RaycastHit hitInfo11;
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo11, 2000f))//esta linia hace que si desde la camara hacia delante hay algun objeto es true y entra en el if
                {
                    if (hitInfo11.transform.tag == "Untagged" || hitInfo11.transform.tag == "puerta")
                    {
                        text.text = "";
                    }
                }
                if (hitInfo11.transform.tag == "Untagged")
                {
                    
                }

            }
        }
    
    void CMuere()
    {
        if (SeMuere && !niñamuerta)
        {
            NiñaMuerta.cam.enabled = false;
            NiñaMuerta.anim.StopPlayback();
            NiñaMuerta.AudioController2.Stop();
            Estoyvivo = false;


            NiñaMuerta.anim.SetBool("Muerte", true);
            audiocontroller.Stop();
            Moverse = false;
            LuzMuerte.gameObject.SetActive(true);
            NiñaMuerta.transform.position = PoscicionAsesinato.transform.position;
            NiñaMuerta.transform.rotation = PoscicionAsesinato.transform.rotation;

            anim.enabled = false;
            StartCoroutine(Morir());
            





        }

    }
    IEnumerator Morir()
    {
        yield return new WaitForSeconds(2);

    }
    public void changeSensitivity()
    {
        sensibilidad = _slider.value;
    }

    public void CojerLinterna()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, 15f))//esta linia hace que si desde la camara hacia delante hay algun objeto es true y entra en el if
        {

            if (hitInfo.transform.tag == "Objeto")
            {


                SonidosObjetos.Play();
                TieneLinterna = true;
                linterna.position = posicionLinterna.position;
                linterna.rotation = posicionLinterna.rotation;
                linterna.parent = posicionLinterna;


            }

            if (hitInfo.transform.tag == "llave")
            {



                Destroy(llave);
                puerta = true;
                SonidosObjetos.Play();



            }
            if (hitInfo.transform.tag == "llave1")
            {



                Destroy(llave1);
                cajita = true;
                SonidosObjetos.Play();




            }
            if (hitInfo.transform.tag == "llave2")
            {



                Destroy(llave2);
                puertaNiña = true;
                SonidosObjetos.Play();




            }
            if (hitInfo.transform.tag == "Cofre")
            {

                if (cajita)
                {

                    cofre.SetBool("cajita", true);
                    puertafinal = true;
                    SonidoCaja.Play();



                }
                else if (!cajita)
                {
                    text.text = "You need a key";
                }


            }
            if (hitInfo.transform.tag == "cuchillo")
            {



                SonidoCuchillo.Play();
                cojercuchillo = true;
                cuchillo.SetActive(false);
                cuchillo.transform.position = posicioncuchillo.transform.position;
                cuchillo.transform.rotation = posicioncuchillo.transform.rotation;
                cuchillo.transform.SetParent(posicioncuchillo);
                text.text = "";

                if (!haempezadolacaza)
                {
                    //StartCoroutine(CerrarLuces());
                }





                //sonidocuchillo




            }
            if (hitInfo.transform.tag == "peluche")
            {
                Debug.Log("peluche");
                if (cojercuchillo)

                {
                    cuchillo.transform.tag = "Untagged";
                    Interactuar.gameObject.SetActive(false);
                    peluche.transform.position = posicionpeluche.transform.position;
                    peluche.transform.rotation = posicionpeluche.transform.rotation;
                    peluche.SetParent(posicionpeluche);
                    linterna.gameObject.SetActive(false);
                    cuchillo.SetActive(true);
                    tengopeluche = true;
                    acuchillar.gameObject.SetActive(true);

                }
                else
                {
                    text.text = "You Need a Knife";
                    
                }



            }
            if (hitInfo.transform.tag == "PuertaEntrada" || hitInfo.transform.tag == "PuertaEntradaFuera" || hitInfo.transform.tag == "puerta")
            {
                if (contador3 % 2 == 0 && puerta)
                {
                   
                }
                else if (contador3 % 2 == 1 && puerta)
                {
                  
                }

                if (!puerta)
                {
                    //sonido cerrado
                    text.text = "You need a key";
                }
                if (puerta)
                {
                    //Puerta.SetTrigger("Puerta");


                    contador3++;
                    if (contador3 % 2 == 0)
                    {

                        SonidoPuerta.Play();
                        Puerta.SetBool("PuertaEntrada", false);
                    }
                    else
                    {
                        SonidoPuerta.Play();
                        Puerta.SetBool("PuertaEntrada", true);
                    }

                }


            }
            Debug.Log(hitInfo.transform.tag);


            




            if (hitInfo.transform.tag == "PuertaNiña")
            {
                if (contador4 % 2 == 0 && puerta)
                {
                   
                }
                else if (contador4 % 2 == 1 && puerta)
                {
                    
                }
                if (!puertaNiña)
                {
                    //sonido cerrado
                    text.text = "You need a key";
                }
                if (puertaNiña)
                {
                    //Puerta.SetTrigger("Puerta");


                    contador4++;
                    if (contador4 % 2 == 0)
                    {

                        SonidoPuerta.Play();
                        PuertaNiña.SetBool("Puerta1", false);
                    }
                    else
                    {
                        SonidoPuerta.Play();
                        PuertaNiña.SetBool("Puerta1", true);
                    }

                }

            }
            if (hitInfo.transform.tag == "a")
            {


                contador1++;
                if (contador1 % 2 == 0)
                {
                    SonidoTaquilla.Play();
                    casilla.SetBool("Casilla", false);
                }
                else
                {
                    SonidoTaquilla.Play();
                    casilla.SetBool("Casilla", true);
                }




            }


            if (hitInfo.transform.tag == "taquilla")
            {


                contador2++;
                if (contador2 % 2 == 0)
                {
                    SonidoTaquilla.Play();
                    casilla2.SetBool("taquilla1", false);
                }
                else
                {
                    SonidoTaquilla.Play();
                    casilla2.SetBool("taquilla1", true);
                }




            }
            if (hitInfo.transform.tag == "Untagged")
            {

            }
        }
        RaycastHit hitInfo1;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo1, 200f))//esta linia hace que si desde la camara hacia delante hay algun objeto es true y entra en el if
        {
            if (hitInfo1.transform.tag == null || hitInfo1.transform.tag == "Untagged")
            {
                Interactuar.gameObject.SetActive(false);
            }
        }
    }
    public void EncenderApagar()
    {
        if (TieneLinterna)//Encendemos y apagamos linterna con click derecho
        {

            contador++;
            if (contador % 2 == 0)
            {
                SonidoLinterna.Play();
                Luz.SetActive(false);
                encendida = false;
            }
            else
            {
                encendida = true;
                SonidoLinterna.Play();
                Luz.SetActive(true);


            }
        }

        if (encendida)
        {
            //SonidoPasos = 35f;
        }





    }
}