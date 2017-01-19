using System.IO;
using Assets.Scripts.JSON;
using Assets.Scripts.Scene_Managament;
using UnityEngine;

namespace Assets.Scripts.Managers {
    public class FileManager : MonoBehaviour {

        [HideInInspector] public static FileManager instance;
        [SerializeField] private string frozenJsonFileName;
        [SerializeField] private string gameStatusJsonFileName;


        [SerializeField] private FreezeJson _frozenDefaultJson;
        [SerializeField] private GameStatusJSON _gameStatusDefaultJson;

        [HideInInspector] public FreezeJson currentFreezeJson;
        [HideInInspector] public GameStatusJSON currentGameStatusJson;

        // Use this for initialization
        void Awake () {
            instance = this;
            DontDestroyOnLoad( this.gameObject );
#if UNITY_EDITOR
            frozenJsonFileName = "GameData/" + frozenJsonFileName;
            gameStatusJsonFileName = "GameData/" + gameStatusJsonFileName;
#elif UNITY_STANDALONE
        frozenJsonFileName = "WhenHellFreezesOver_Data/Resources/" + frozenJsonFileName;
        gameStatusJsonFileName = "WhenHellFreezesOver_Data/Resources/" + gameStatusJsonFileName;
#endif
            LoadAll();
        }

        public void LoadAll() {
            LoadGameStatusJson();
            LoadFrozenJson();
        }

        public void LoadFrozenJson() {
            if ( !File.Exists( frozenJsonFileName ) ) {
                currentFreezeJson = _frozenDefaultJson;
                SaveFrozenJson();
            }
            StreamReader sr = new StreamReader( frozenJsonFileName );
            string fileString = sr.ReadToEnd();

            currentFreezeJson = JsonUtility.FromJson<FreezeJson>( fileString );
            sr.Close();        
        }

        public void SaveFrozenJson() {
            StreamWriter sw = new StreamWriter( frozenJsonFileName );
            sw.Write( JsonUtility.ToJson( currentFreezeJson ) );
            sw.Close();
        }

        public void LoadGameStatusJson() {
            if ( !File.Exists( gameStatusJsonFileName ) ) {
                currentGameStatusJson = _gameStatusDefaultJson;
                SaveGameStatusJson();
            }
            StreamReader sr = new StreamReader( gameStatusJsonFileName );
            string fileString = sr.ReadToEnd();
            currentGameStatusJson = JsonUtility.FromJson<GameStatusJSON>( fileString );
            sr.Close();
        }

        public void SaveGameStatusJson() {
            StreamWriter sw = new StreamWriter( gameStatusJsonFileName );
            sw.Write( JsonUtility.ToJson( currentGameStatusJson ) );
            sw.Close();
        }

        public void SaveAll() {
            SaveFrozenJson();
            SaveGameStatusJson();
        }

        public void ResetAll() {
            File.Delete( gameStatusJsonFileName );
            File.Delete( frozenJsonFileName );
            LoadAll();
        }

        public void ResetLevels() {
            currentGameStatusJson.lastLevel = _gameStatusDefaultJson.lastLevel;
            SaveGameStatusJson();
            FindObjectOfType<LoadScene>().sceneToLoad = currentGameStatusJson.lastLevel;
        }
    }
}
