using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPartida : MonoBehaviour
{
    private Ads ads;
    MovimientoPersonaje Player;
    public GameObject MenuJuego;
    public bool MenuOpciones=false;
    // Start is called before the first frame update
    
    public void Renudar()
    {
        Time.timeScale = 1;
        MenuJuego.SetActive(false);
        Player.cursor = true;
        Player.audiocontroller.Play();
        Player.Moverse = true;
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("Menu");
        
    }
    private void Update()
    {
        
        
        if (Input.GetKey(KeyCode.Escape)&& !MenuOpciones)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Debug.Log("Hola");
            MenuJuego.SetActive(true);
            Time.timeScale = 0;
            Player.audiocontroller.Stop();
            Player.cursor = false;
            
        }
    }
    private void Start()
    {
        ads = GameObject.Find("Ads").GetComponent<Ads>();

        Player = GameObject.Find("Player").GetComponent<MovimientoPersonaje>();
        Cursor.visible = false;
        Time.timeScale = 1;
        MenuJuego.SetActive(false);
    }
    public void EstaEnOpciones()
    {
        MenuOpciones = true;
    }
    public void NoEstaEnOpciones()
    {
        MenuOpciones = false;
    }
    public void reintentar()
    {
        
        SceneManager.LoadScene("Juego");
       
    }
    public void AbrirMenu()
    {
        MenuJuego.SetActive(true);
        Time.timeScale = 0;
        Player.audiocontroller.Stop();
        Player.Moverse = false;
       

    }
}
