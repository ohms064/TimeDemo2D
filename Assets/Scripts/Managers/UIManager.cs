using Assets.Scripts.Scene_Managament;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers {
    public class UIManager : MonoBehaviour {
        public Canvas mainMenu;
        public Canvas storeMenu;
        public Text coinsText;
        public LoadScene loadScene;

        private string defaultString;

        void Start() {
            loadScene.sceneToLoad = FileManager.instance.currentGameStatusJson.lastLevel;
            storeMenu.enabled = false;
            defaultString = coinsText.text;
            UpdateCoins();
        }

        public void ShowMainMenu() {
            mainMenu.enabled = true;
            storeMenu.enabled = false;
        }

        public void ShowStoreMenu() {
            mainMenu.enabled = false;
            storeMenu.enabled = true;
        }

        public void BuyFreezeUpgrade() {
            int coins = FileManager.instance.currentGameStatusJson.coins;
            if ( coins <= 0 ) return;
            FileManager.instance.currentFreezeJson.freezeTime += 5;
            FileManager.instance.currentGameStatusJson.coins -= 2;
            FileManager.instance.SaveAll();
            UpdateCoins();
        }

        public void UpdateCoins() {
            string txt = defaultString.Replace( "<number>", FileManager.instance.currentGameStatusJson.coins.ToString() );
            coinsText.text = txt;
        }

    }
}
