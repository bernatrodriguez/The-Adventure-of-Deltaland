using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeFX : MonoBehaviour
{
    [SerializeField] private GameObject canvasTextoAnimacionPrefab; // Animación del texto
    [SerializeField] private Transform canvasTextoPosicion; // Posición del texto

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
        texto.EstablecerTexto(cantidad); // Pasamos la cantidad de daño al texto
        nuevoTextoGO.transform.SetParent(canvasTextoPosicion);
        nuevoTextoGO.transform.position = canvasTextoPosicion.position; // Pasamos la posición del texto
        nuevoTextoGO.SetActive(true); // Activamos el objeto

        yield return new WaitForSeconds(1f); // Esperamos 1 segundo para regresar el texto
        nuevoTextoGO.SetActive(false); // Volvemos a desactivar el texto
        nuevoTextoGO.transform.SetParent(pooler.ListaContenedor.transform); // Lo regresamos a la lista contenedor del pooler
    }

    private void RespuestaDañoRecibido(float daño)
    {
        StartCoroutine(IEMostrarTexto(daño)); // Llamamos a mostrar texto pasandole el daño que se realiza al personaje
    }

    private void OnEnable()
    {
        IAController.EventoDañoRealizado += RespuestaDañoRecibido; // Nos suscribimos al event0
    }

    private void OnDisable()
    {
        IAController.EventoDañoRealizado += RespuestaDañoRecibido; // Nos desuscribimos del evento
    }
}

