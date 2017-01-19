using Assets.Scripts.Scene_Managament;

namespace Assets.Scripts.JSON {
    [System.Serializable]
    public class GameStatusJSON {

        public int coins;
        public Scenes lastLevel;
        public bool firstLevelCoin;
        public bool secondLevelCoin;

        public GameStatusJSON() {
            coins = 0;
            lastLevel = Scenes.FIRST_LEVEL;
            firstLevelCoin = false;
            secondLevelCoin = false;
        }

        public GameStatusJSON(int coins, Scenes lastLevel, bool firstLevelCoin, bool secondLevelCoin) {
            this.coins = coins;
            this.lastLevel = lastLevel;
            this.firstLevelCoin = firstLevelCoin;
            this.secondLevelCoin = secondLevelCoin;
        }
    }
}
