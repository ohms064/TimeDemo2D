using Assets.Scripts.Scene_Managament;
using UnityEngine;

namespace Assets.Scripts.Managers {
    public class PauseManager : MonoBehaviour {

        public Canvas menuPause;

        void Start() {
            menuPause.enabled = false;
        }

        // Update is called once per frame
        void Update() {
            if ( Input.GetKeyDown( KeyCode.P ) ) {
                menuPause.enabled = !menuPause.enabled;
                if ( menuPause.enabled ) {
                    Time.timeScale = 0;
                    PlayerManager.time.enabled = false;
                }else {
                    Time.timeScale = 1;
                    PlayerManager.time.enabled = true;
                }
            }
        }

        public void Resume() {
            menuPause.enabled = false;
            PlayerManager.time.enabled = true;
            Time.timeScale = 1;
        }

        public void Restart() {
            Time.timeScale = 1;
            LoadScene.ReloadScene();
        }

        public void Exit() {
            Time.timeScale = 1;
            LoadScene.Load( Scenes.MAIN_MENU );
        }
    }
}
