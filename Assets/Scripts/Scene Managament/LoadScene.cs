using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Scene_Managament {
    public class LoadScene : MonoBehaviour {

        public Scenes sceneToLoad;
        public float waitTime;
        public Scenes sceneToSave;
        public bool saveOnLoad;

        [HideInInspector]
        public static Scenes currentScene = Scenes.MAIN_MENU;

        void OnTriggerEnter2D( Collider2D col ) {
            InvokeScene();
        }

        public void SceneLoad() {
            SceneManager.LoadScene( (int)sceneToLoad );
            currentScene = sceneToLoad;
            if ( saveOnLoad ) {
                FileManager files = FileManager.instance;
                files.currentGameStatusJson.lastLevel = sceneToSave;
                files.SaveGameStatusJson();
            }
        }

        public void InvokeScene() {
            Invoke( "SceneLoad", waitTime );
        }

        public static void ReloadScene() {
            SceneManager.LoadScene( (int)currentScene );
        }

        public static void Load(Scenes scene ) {
            SceneManager.LoadScene( (int)scene );
        }

    }
}
