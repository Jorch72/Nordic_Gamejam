using UnityEngine;
using System.Collections;

public class CameraDistorter : MonoBehaviour {
    
    public Material distortMaterial;
    public float frequency = 1;

    void Start() {
        StartCoroutine(DistortRoutine());
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest) {
        Graphics.Blit(src, dest, distortMaterial);
    }

    private IEnumerator DistortRoutine() {
        while (true) {
            if(Random.Range(0, 3) == 1) {
                yield return new WaitForSeconds(Random.Range(0.5f, 1f) / frequency);
            } else {
                yield return new WaitForSeconds(Random.Range(5, 12) / frequency);
            }

            distortMaterial.SetFloat("_DistortionRange", Random.Range(0.025f, 0.1f));

            if (Random.Range(0, 4) == 0) {
                float scrollTime = Random.Range(0.1f, 0.4f);
                StartCoroutine(DistortScroll(scrollTime, Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f)));
                yield return new WaitForSeconds(scrollTime);
            } else {
                distortMaterial.SetFloat("_DistortionPosition", Random.Range(0.1f, 0.9f));
                yield return new WaitForSeconds(0.1f);
            }
            distortMaterial.SetFloat("_DistortionPosition", -1);
        }
        yield return null;
    }

    private IEnumerator DistortScroll(float time, float from, float to) {
        float startTime = Time.time;
        float endTime = Time.time + time;
        while (Time.time < endTime) {
            float timeValue = (Time.time - startTime) / time;
            float value = Mathf.Lerp(from, to, (endTime - Time.time) * time);
            distortMaterial.SetFloat("_DistortionPosition", value);
            yield return null;
        }
        yield return null;
    }
}
