using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamWorldDoremyIntro : MonoBehaviour
{
	
	public GameObject doremy;
	public GameObject sphere;
	public GameObject particle;
	
    public float scaleUpSpeed = 2.0f; // Speed of scaling up
    public float deflateSpeed = 50.0f; // Speed of deflating
    public float maxScale = 12.0f;  // Maximum scale value
    public float interpolationDuration = 1.0f; // Duration of interpolation between inflating and deflating
    public float delayBeforeInflation = 5.0f;
	public float countBeforeInflation = 0.0f;
	public float timeVery = 0.0f;

    private Vector3 initialScale;
    private bool isDeflating = false;
	private GameObject player;
	public GameObject stgBox;
	private GameObject mainCamera;
	private GameObject scripts;
	
    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.Find("Player");
		mainCamera = GameObject.Find("Main Camera");
		scripts = GameObject.Find("Scripts");
    }

    void Update()
    {
		countBeforeInflation += Time.deltaTime;
		
        if (sphere != null && countBeforeInflation >= delayBeforeInflation)
        {
			timeVery += Time.deltaTime;
            if (!isDeflating)
            {
                // Calculate the interpolation value using a sine wave for inflating
                float elapsedTime = Mathf.Min(timeVery * scaleUpSpeed / maxScale, 1.0f);
                float interpolationValue = Mathf.Sin(elapsedTime * Mathf.PI * 0.5f);

                // Scale up the sphere
                float scaleValue = Mathf.Lerp(0, maxScale, interpolationValue);
                sphere.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);

                // Check if the sphere has reached the maximum scale
                if (scaleValue >= maxScale)
                {
                    StartCoroutine(StartDeflating());
                }
            }
            else
            {
                // Deflate the sphere
                float scaleValue = Mathf.Max(0, sphere.transform.localScale.x - deflateSpeed * Time.deltaTime);
                sphere.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);

                // Check if the sphere has deflated to 0
                if (scaleValue <= 0)
                {
                    sphere.SetActive(false);
                }
            }
        }
    }

    IEnumerator StartDeflating()
    {

		particle.SetActive(true);
		doremy.SetActive(true);
		
        float elapsedTime = 0f;

        while (elapsedTime < interpolationDuration)
        {
			
            float t = elapsedTime / interpolationDuration;
            float interpolationValue = Mathf.Sin(t * Mathf.PI * 0.5f); // Sine wave interpolation
            float currentSpeed = Mathf.Lerp(scaleUpSpeed, deflateSpeed, interpolationValue);

            // Deflate during the interpolation phase
            float scaleValue = Mathf.Max(0, sphere.transform.localScale.x - currentSpeed * Time.deltaTime);
            sphere.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isDeflating = true;
		StartCoroutine(StartSTG());
    }
	
	IEnumerator StartSTG()
	{
		doremy.SetActive(false);
		player.SetActive(false);
		mainCamera.SetActive(false);
		stgBox.SetActive(true);
		scripts.GetComponent<SampleBulletTestSTG>().enabled = true;
		yield return null;
	}
}
