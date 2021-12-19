using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputeShaderTest : MonoBehaviour
{

    public ComputeShader computeShader;
    public RenderTexture renderTexture;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (renderTexture == null)
        {
            createTexture();
        }
        computeShader.SetTexture(0, "Result", renderTexture);
        computeShader.SetFloat("resolution", renderTexture.width);
        computeShader.Dispatch(0, renderTexture.width / 8, renderTexture.height / 8, 1);
        Graphics.Blit(renderTexture, destination);
    }

    private void createTexture()
    {
        renderTexture = new RenderTexture(256, 256, 24);
        renderTexture.enableRandomWrite = true;
        renderTexture.Create();
    }
}
