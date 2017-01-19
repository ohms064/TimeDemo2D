using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ProgressBars.Scripts {
    [ExecuteInEditMode]
    public class ProgressBar : MonoBehaviour {

        public float dissappearTime = 5.0f;

        [SerializeField] private Image _progressBar;
        private Material _progressMaterial { get { return _progressBar.material; } }
        private float innerSize;
        private float currentPercentage;
        private float decreasingSpeed;


        [SerializeField] private Color _progressColor;
        [Range( 0.0f, 1.0f )] public float initialPercentage = 0.5f;
        [Range( 0.0f, 1.0f )] public float initialInnerSize = 0.5f;
#if UNITY_EDITOR
        void Update () {
            //Called only when chaged.
            if (_progressBar == null || _progressMaterial == null || Application.isPlaying)
                return;
            _progressMaterial.SetFloat( "_TransVal", initialInnerSize );
            _progressMaterial.SetColor( "_Color2", _progressColor );
            SetPercentage( initialPercentage );
        }
#endif

        void Start() {
            innerSize = initialInnerSize;
            _progressMaterial.SetFloat("_TransVal", initialInnerSize);
            _progressMaterial.SetColor("_Color2", _progressColor);
            SetPercentage(initialPercentage);
        }

        public void SetPercentage(float percentage) {
            StopCoroutine("Dissappear");
            _progressMaterial.SetFloat("_TransVal", initialInnerSize);
            percentage = Mathf.Clamp( percentage, 0.0f, 1.0f );
            _progressMaterial.SetFloat( "_ProgressionVal", percentage );
            currentPercentage = percentage;
            Invoke("Start2Dissappear", dissappearTime);
        }

        public void SetPercentage(float targetPercentage, float decreasingSpeed) {
            StopCoroutine("Dissappear");
            _progressMaterial.SetFloat("_TransVal", initialInnerSize);
            this.decreasingSpeed = decreasingSpeed;
            StartCoroutine("SetPercentageSlowly", targetPercentage);
            Invoke("Start2Dissappear", decreasingSpeed);
        }

        IEnumerator SetPercentageSlowly(float targetPercentage) {
            float initTime = Time.time;
            float startPercentage = currentPercentage;
            if (startPercentage > targetPercentage) {
                while (currentPercentage > targetPercentage) {
                    currentPercentage = Mathf.Lerp(startPercentage, targetPercentage, (Time.time - initTime) / decreasingSpeed);
                    _progressMaterial.SetFloat("_ProgressionVal", currentPercentage);
                    yield return new WaitForEndOfFrame();
                }
            }
            else if (startPercentage < targetPercentage) {
                while (currentPercentage < targetPercentage) {
                    innerSize = Mathf.Lerp(targetPercentage, startPercentage, (Time.time - initTime) / decreasingSpeed);
                    _progressMaterial.SetFloat("_TransVal", innerSize);
                    yield return new WaitForEndOfFrame();
                }
            }

        }

        IEnumerator Dissappear() {
            float initTime = Time.time;
            innerSize = initialInnerSize;
            while (innerSize > 0.0f) {
                innerSize = Mathf.Lerp(initialInnerSize, 0.0f, (Time.time - initTime)/dissappearTime);
                _progressMaterial.SetFloat("_TransVal", innerSize);
                yield return new WaitForEndOfFrame();
            }
        }

        void Start2Dissappear() {
            StartCoroutine("Dissappear");
        }
    }
}
