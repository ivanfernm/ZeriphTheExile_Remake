using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MouseReact : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] bool TextMeshPro;
    [SerializeField] Text _meshText;
    [SerializeField] TMP_Text _textMeshPro;
     float _fontSize;
     Color _color;
    [SerializeField] float _textMultiplier;
    private bool mouseOver;
    
    private void Awake()
    {
        if (!TextMeshPro)
        {
            _meshText = gameObject.GetComponentInChildren<Text>();
        }
        else
        {
            _textMeshPro = gameObject.GetComponentInChildren<TMPro.TMP_Text>();
        }
       
    }
    private void Start()
    {
        if (!TextMeshPro)
        {
            _fontSize = _meshText.fontSize;
            _color = _meshText.color;

        }
        else
        {
            _fontSize = _textMeshPro.fontSize;
            _color = _textMeshPro.color;

        }
    }

    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!TextMeshPro)
        {
            _meshText.color = Color.white;
            _meshText.fontSize = ((int)(_meshText.fontSize * _textMultiplier));
        }
        else
        {
            _textMeshPro.color = Color.white;
            _textMeshPro.fontSize = ((int)(_textMeshPro.fontSize * _textMultiplier));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      

        if (!TextMeshPro)
        {
            _meshText.color = _color;
            _meshText.fontSize = ((int)_fontSize);
        }
        else
        {
            _textMeshPro.color = _color;
            _textMeshPro.fontSize = ((int)_fontSize);
        }
    }
}
