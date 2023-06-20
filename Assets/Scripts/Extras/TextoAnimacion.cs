using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextoAnimacion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dañoTexto; // Daño que mostraremos en la animación de daño

    public void EstablecerTexto(float cantidad, Color color)
    {
        dañoTexto.text = cantidad.ToString(); // Convertimos el daño en una cadena de caracteres para poder mostrarse en la UI
        dañoTexto.color = color; // APlicamos el color
    }
}
