using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class KeyBindingsUI : MonoBehaviour
{
    [System.Serializable]
    public class KeyBinding
    {
        public string actionName; // �������� ��������
        public KeyCode defaultKey; // ������� �� ���������
        public Button button; // ������ � UI
        public TMP_Text buttonText; // ����� �� ������
    }

    public List<KeyBinding> keyBindings; // ������ ���� ��������
    private string waitingForKey = null; // ��� ������� ����� �������

    void Start()
    {
        foreach (var binding in keyBindings)
        {
            // ��������� ���������� ��� ����������� �������
            binding.buttonText.text = KeyBindings.GetKey(binding.actionName).ToString();
            binding.button.onClick.AddListener(() => StartRebind(binding));
        }
    }

    private void StartRebind(KeyBinding binding)
    {
        waitingForKey = binding.actionName;
        binding.buttonText.text = "Set...";
    }

    void Update()
    {
        if (waitingForKey != null)
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    KeyBindings.SetKey(waitingForKey, key);
                    UpdateUI();
                    waitingForKey = null;
                    break;
                }
            }
        }
    }

    private void UpdateUI()
    {
        foreach (var binding in keyBindings)
        {
            binding.buttonText.text = KeyBindings.GetKey(binding.actionName).ToString();
        }
    }
}
