namespace Assets.Scripts.JSON {
    [System.Serializable]
    public class FreezeJson {
        public float freezeTime;

        public FreezeJson() {
            freezeTime = 5.0f;
        }

        public FreezeJson(float freezeTime ) {
            this.freezeTime = freezeTime;
        }
    }
}
