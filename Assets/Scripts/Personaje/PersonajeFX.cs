using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoPersonaje // Tipo de personaje
{
    Player,
    IA
}

public class PersonajeFX : MonoBehaviour
{
    [Header("Pooler")]
    [SerializeField] private ObjectPooler pooler;

    [Header("Config")]
    [SerializeField] private GameObject canvasTextoAnimacionPrefab; // Animaci�n del texto
    [SerializeField] private Transform canvasTextoPosicion; // Posici�n del texto

    [Header("Tipo")]
    [SerializeField] private TipoPersonaje tipoPersonaje; // Tipo de personaje

    private EnemigoVida _enemigoVida;

    private void Awake()
    {
        _enemigoVida = GetComponent<EnemigoVida>();
    }

    private void Start()
    {
        pooler.CrearPooler(canvasTextoAnimacionPrefab); // Creamos el pooler
    }

    private IEnumerator IEMostrarTexto(float cantidad, Color color)
    {
        GameObject nuevoTextoGO = pooler.ObtenerInstancia(); // Obtenemos una instancia para un nuevo texto
        TextoAnimacion texto = nuevoTextoGO.GetComponent<TextoAnimacion>(); // Referencia a la clase TextoAnimacion
        texto.EstablecerTexto(cantidad, color); // Pasamos la cantidad de da�o y color al texto
        nuevoTextoGO.transform.SetParent(canvasTextoPosicion);
        nuevoTextoGO.transform.position = canvasTextoPosicion.position; // Pasamos la posici�n del texto
        nuevoTextoGO.SetActive(true); // Activamos el objeto

        yield return new WaitForSeconds(1f); // Esperamos 1 segundo para regresar el texto
        nuevoTextoGO.SetActive(false); // Volvemos a desactivar el texto
        nuevoTextoGO.transform.SetParent(pooler.ListaContenedor.transform); // Lo regresamos a la lista contenedor del pooler
    }

    private void RespuestaDa�oRecibidoHaciaPlayer(float da�o)
    {
        if (tipoPersonaje == TipoPersonaje.Player) // Si el objeto que tiene esta clase es el player
        {
            StartCoroutine(IEMostrarTexto(da�o, Color.black)); // Mostramos da�o en color negro
        }
    }

    private void RespuestaDa�oHaciaEnemigo(float da�o, EnemigoVida enemigoVida)
    {
        if (tipoPersonaje == TipoPersonaje.IA && _enemigoVida == enemigoVida) // Si el objeto que tiene esta clase es un personaje
        {
            StartCoroutine(IEMostrarTexto(da�o, Color.red)); // Mostramos da�o en color rojo
        }
    }

    private void OnEnable()
    {
        IAController.EventoDa�oRealizado += RespuestaDa�oRecibidoHaciaPlayer;
        PersonajeAtaque.EventoEnemigoDa�ado += RespuestaDa�oHaciaEnemigo;
    }

    private void OnDisable()
    {
        IAController.EventoDa�oRealizado -= RespuestaDa�oRecibidoHaciaPlayer;
        PersonajeAtaque.EventoEnemigoDa�ado -= RespuestaDa�oHaciaEnemigo;
    }
}

