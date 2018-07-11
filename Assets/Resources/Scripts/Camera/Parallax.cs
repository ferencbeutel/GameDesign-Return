using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform backgroundGroup;
    public float smoothing = 1f;

    List<Tuple<Transform, float>> backgroundParallaxList = new List<Tuple<Transform, float>>();
    float[] parallaxScales;
    Transform mainCam;
    Vector3 previousCameraPosition;

    // Use this for initialization
    void Awake()
    {
        mainCam = Camera.main.transform;
    }

    void Start()
    {
        previousCameraPosition = mainCam.position;
        foreach (Transform backgroundTransform in backgroundGroup)
        {
            backgroundParallaxList.Add(new Tuple<Transform, float>(backgroundTransform, backgroundTransform.position.z * -1));
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Tuple<Transform, float> backgroundParallaxTuple in backgroundParallaxList)
        {
            float parallaxX = (previousCameraPosition.x - mainCam.position.x) * backgroundParallaxTuple.GetRight();
            float parallaxY = (previousCameraPosition.y - mainCam.position.y) * backgroundParallaxTuple.GetRight();

            Vector3 backgroundPosition = backgroundParallaxTuple.GetLeft().position;
            backgroundParallaxTuple.GetLeft().position = Vector3.Lerp(backgroundPosition, new Vector3(backgroundPosition.x + parallaxX, backgroundPosition.y + parallaxY, backgroundPosition.z), smoothing * Time.deltaTime);
        }

        previousCameraPosition = mainCam.position;
    }
}
