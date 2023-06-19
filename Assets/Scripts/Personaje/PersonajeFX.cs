using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeFX : MonoBehaviour
{
    [SerializeField] private GameObject canvasTextoAnimacionPrefab; // Animaci�n del texto
    [SerializeField] private Transform canvasTextoPosicion; // Posici�n del texto

    private ObjectPooler pooler;

    private void Awake()
    {
        pooler = GetComponent<ObjectPooler>(); // Obtenemos la referencia al pooler
    }

    private void Start()
    {
        pooler.CrearPooler(canvasTextoAnimacionPrefab); // Creamos el pooler
    }

    private IEnumerator IEMostrarTexto(float cantidad)
    {
        GameObject nuevoTextoGO = pooler.ObtenerInstancia(); // Obtenemos una instancia para un nuevo texto
        TextoAnimacion texto = nuevoTextoGO.GetComponent<TextoAnimacion>(); // Referencia a la clase TextoAnimacion
        texto.EstablecerTexto(cantidad); // Pasamos la cantidad de da�o al texto
        nuevoTextoGO.transform.SetParent(canvasTextoPosicion);
        nuevoTextoGO.transform.position = canvasTextoPosicion.position; // Pasamos la posici�n del texto
        nuevoTextoGO.SetActive(true); // Activamos el objeto

        yield return new WaitForSeconds(1f); // Esperamos 1 segundo para regresar el texto
        nuevoTextoGO.SetActive(false); // Volvemos a desactivar el texto
        nuevoTextoGO.transform.SetParent(pooler.ListaContenedor.transform); // Lo regresamos a la lista contenedor del pooler
    }

    private void RespuestaDa�oRecibido(float da�o)
    {
        StartCoroutine(IEMostrarTexto(da�o)); // Llamamos a mostrar texto pasandole el da�o que se realiza al personaje
    }

    private void OnEnable()
    {
        IAController.EventoDa�oRealizado += RespuestaDa�oRecibido; // Nos suscribimos al event0
    }

    private void OnDisable()
    {
        IAController.EventoDa�oRealizado += RespuestaDa�oRecibido; // Nos desuscribimos del evento
    }
}

