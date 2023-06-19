using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedasManager : Singleton<MonedasManager>
{
    [SerializeField] private int monedasTest; // Testing
    
    public int MonedasTotales { get; set; }

    private string KEY_MONEDAS = "MYJUEGO_MONEDAS"; // Contrase�a para guardar monedas

    private void Start()
    {
        PlayerPrefs.DeleteKey(KEY_MONEDAS); // Testing
        CargarMonedas();
    }

    private void CargarMonedas()
    {
        MonedasTotales = PlayerPrefs.GetInt(KEY_MONEDAS, monedasTest); // Creamos una variable con las monedas de las que disponemos
    }

    public void A�adirMonedas(int cantidad) // M�todo para a�adir una cantidad de monedas
    {
        MonedasTotales += cantidad; // Sumamos la cantidad a las monedas totales de las que ya disponemos
        PlayerPrefs.SetInt(KEY_MONEDAS, MonedasTotales); // Aplicamos la contrase�a
        PlayerPrefs.Save(); // Guardamos el valor de monedas totales
    }

    public void EliminarMonedas(int cantidad)
    {
        if (cantidad > MonedasTotales) // Si la cantidad que eliminamos es mayor que las monedas que tenemos, es decir, si no tenemos suficientes monedas
        {
            return; // Detenemos la ejecuci�n, no queremos tener valores negativos en monedas
        }

        // Si tenemos suficientes monedas
        MonedasTotales -= cantidad;
        PlayerPrefs.SetInt(KEY_MONEDAS, MonedasTotales); // Aplicamos la contrase�a
        PlayerPrefs.Save(); // Guardamos el valor de monedas totales
    }
}
