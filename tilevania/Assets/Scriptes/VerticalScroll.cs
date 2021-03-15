using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    [Tooltip("Game units per Sceond")]
    [SerializeField] float ScrollRate = 0.2f;

    // Update is called once per frame
    void Update()
    {
        float Ymove = ScrollRate * Time.deltaTime;
        transform.Translate(new Vector2(0f, Ymove));
    }
}
