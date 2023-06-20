using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemigoBarraVida : MonoBehaviour
{
    [SerializeField] private Image barraVida; // referencia de la barra de vida

    private float saludActual;
    private float saludMax;

    private void Update()
    {
        barraVida.fillAmount = Mathf.Lerp(barraVida.fillAmount, saludActual / saludMax, 10f * Time.deltaTime); // Actualizamos el llenado de la barra de vida del enemigo
    }

    public void ModificarSalud(float pSaludActual, float pSaludMax) // Método para modificar la salud del enemigo
    {
        saludActual = pSaludActual; // Definimos la salud actual
        saludMax = pSaludMax; // Definimos la salud máxima
    }
}
