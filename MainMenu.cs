using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
     public Animator Menu;
    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene("Juego");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
      
        

    }
    
    private void Start()
    {
        
    }
}
