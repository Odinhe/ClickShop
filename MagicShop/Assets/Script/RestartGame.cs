using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameRestart { 
    public class RestartGame
    {   // Start is called before the first frame update
        //restart the game when this function was called
        public void RestartScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}
