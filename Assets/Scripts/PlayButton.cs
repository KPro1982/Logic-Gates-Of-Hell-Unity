using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public Button button;
    public void Play()
    {
        button.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("MainScene");
        
    }
    
    
}
