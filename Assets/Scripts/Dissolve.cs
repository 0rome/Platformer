using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

public class Dissolve : MonoBehaviour
{
    public float dissolveSpeed = 1.0f;
    public float noiseScale = 10f;

    [ColorUsage(true, true)]
    public Color color;

    private Material material;
    private float dissolveAmount = 0f;

    
    void Start()
    {
        // ������� ����� ��������� (����� �� ������ ������������)
        material = Instantiate(GetComponent<SpriteRenderer>().material);
        GetComponent<SpriteRenderer>().material = material;

        dissolveAmount = material.GetFloat("_DissolveAmount");

        material.SetColor("_OutlineColor", color);
        material.SetFloat("_NoiseScale", noiseScale);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Disappear();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Appear();
        }
    }

    public void Appear()
    {
        StartCoroutine(DissolveCoroutine(0.0f)); // ��������������
    }
    public void Disappear()
    {
        StartCoroutine(DissolveCoroutine(1.0f)); // �����������
    }
    public void VerticalAppear()
    {
        StartCoroutine(VerticalDissolveCoroutine(0.0f)); // ������������ ��������������
    }
    public void VerticalDisappear()
    {
        StartCoroutine(VerticalDissolveCoroutine(1.1f)); // ������������ �����������
    }

    IEnumerator DissolveCoroutine(float target)
    {
        while (!Mathf.Approximately(dissolveAmount, target))
        {
            dissolveAmount = Mathf.MoveTowards(dissolveAmount, target, dissolveSpeed * Time.deltaTime);

            // ���������, ������������ �� �������� ���� ��������
            if (material.HasProperty("_DissolveAmount"))
            {
                material.SetFloat("_DissolveAmount", dissolveAmount);
            }

            yield return null;
        }
    }
    IEnumerator VerticalDissolveCoroutine(float target)
    {
        while (!Mathf.Approximately(dissolveAmount, target))
        {
            dissolveAmount = Mathf.MoveTowards(dissolveAmount, target, dissolveSpeed * Time.deltaTime);

            // ���������, ������������ �� �������� ���� ��������
            if (material.HasProperty("_VerticalDissolve"))
            {
                material.SetFloat("_VerticalDissolve", dissolveAmount);
            }

            yield return null;
        }
    }
    void OnDestroy()
    {
        // ����������� ��������, ����� �� ���� ������ ������
        Destroy(material);
    }
}
