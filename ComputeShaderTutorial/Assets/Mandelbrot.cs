using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mandelbrot : MonoBehaviour
{
    float width;
    float heigth;
    float rStart;
    float iStart;
    int maxIterations;
    int increment;
    float zoom;

    //shader stuff
    public ComputeShader shader;
    ComputeBuffer buffer;
    RenderTexture texture;
    public RawImage image;

    //Data for the compute buffer
    public struct Datastruct
    {
        public float w;
        public float h;
        public float r;
        public float i;
        public int screenWith;
        public int screenHeight;
    }

    Datastruct[] data;


    // Start is called before the first frame update
    void Start()
    {
        width = 4.5f;
        heigth = width * Screen.height / Screen.width;
        rStart = -2f;
        iStart = -1.2f;
        maxIterations = 200;
        increment = 3;
        zoom = 0.5f;
        data = new Datastruct[1];
        data[0] = new Datastruct
        {
            w = width,
            h = heigth,
            r = rStart,
            i = iStart,
            screenWith = Screen.width,
            screenHeight = Screen.height
        };
        buffer = new ComputeBuffer(data.Length, 24);
        texture = new RenderTexture(Screen.width, Screen.height, 0);
        texture.enableRandomWrite = true;
        texture.Create();
        RunMandelbrot();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ZoomIn();
        }
        if (Input.GetMouseButton(1))
        {
            ZoomOut();
        }
        if (Input.GetMouseButtonDown(2))
        {
            CenterScreen();
        }
    }

    void RunMandelbrot()
    {
        int kernelHandle = shader.FindKernel("CSMain");

        buffer.SetData(data);
        shader.SetBuffer(kernelHandle, "buffer", buffer);

        shader.SetInt("maxIterations", maxIterations);
        shader.SetTexture(kernelHandle, "Result", texture);

        shader.Dispatch(kernelHandle, Screen.width / 24, Screen.height / 24, 1);

        RenderTexture.active = texture;
        image.material.mainTexture = texture;
    }

    private void OnDestroy()
    {
        buffer.Dispose();
    }

    void CenterScreen()
    {
        rStart = (Input.mousePosition.x - (Screen.width / 2.0f)) / Screen.width * width;
        iStart = (Input.mousePosition.y - (Screen.height / 2.0f)) / Screen.height * heigth;

        data[0].r = rStart;
        data[0].i = iStart;

        RunMandelbrot();
    }

    void ZoomIn()
    {
        maxIterations = Mathf.Max(100, maxIterations + increment);
        float wFactor = width * zoom * Time.deltaTime;
        float hFactor = heigth * zoom * Time.deltaTime;
        width -= wFactor;
        heigth -= hFactor;
        rStart += wFactor / 2.0f;
        iStart += hFactor / 2.0f;

        data[0].w = width;
        data[0].h = heigth;
        data[0].r = rStart;
        data[0].i = iStart;
        RunMandelbrot();
    }
    void ZoomOut()
    {
        maxIterations = Mathf.Max(100, maxIterations - increment);
        float wFactor = width * zoom * Time.deltaTime;
        float hFactor = heigth * zoom * Time.deltaTime;
        width += wFactor;
        heigth += hFactor;
        rStart -= wFactor / 2.0f;
        iStart -= hFactor / 2.0f;

        data[0].w = width;
        data[0].h = heigth;
        data[0].r = rStart;
        data[0].i = iStart;
        RunMandelbrot();
    }

}


