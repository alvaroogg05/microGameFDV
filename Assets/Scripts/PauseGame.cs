using Unity.VisualScripting;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject menuPause;
    public GameObject btnPause; 
    public bool gamePaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            
        }

    }

    public void Resume()
    {
        menuPause.SetActive(false);
        Time.timeScale = 1;
        gamePaused = false;
        btnPause.SetActive(true); // vuelve a mostrar el boton de pause
    }
    public void Pause()
    {
        menuPause.SetActive(true);
        Time.timeScale = 0;
        gamePaused = true;
        btnPause.SetActive(false); // oculta el boton de pause
        
    }
}
