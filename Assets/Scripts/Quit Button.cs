using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(QuitGame);
    }
    
    void QuitGame()
    {
        Application.Quit(); // Quits app if button is pressed.
    }

    void Update()
    {
        
    }
}
