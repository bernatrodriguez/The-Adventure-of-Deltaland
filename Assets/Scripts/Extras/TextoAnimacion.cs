using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextoAnimacion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI da�oTexto; // Da�o que mostraremos en la animaci�n de da�o

    public void EstablecerTexto(float cantidad)
    {
        da�oTexto.text = cantidad.ToString(); // Convertimos el da�o en una cadena de caracteres para poder mostrarse en la UI
    }
}
