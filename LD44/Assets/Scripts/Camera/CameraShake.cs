using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    
    private float _shakeDuration = 0.1f;
	private float _shakeAmount = 0.075f;

    public IEnumerator ShakeCamera() {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < _shakeDuration) {

            transform.localPosition = originalPos + Random.insideUnitSphere * _shakeAmount;

            elapsed += Time.deltaTime;

            yield return null;
        }
        
        transform.localPosition = originalPos;
    }
}
