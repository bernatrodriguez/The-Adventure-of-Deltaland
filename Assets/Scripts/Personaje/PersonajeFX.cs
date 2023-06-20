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
    [SerializeField] private GameObject canvasTextoAnimacionPrefab; // Animación del texto
    [SerializeField] private Transform canvasTextoPosicion; // Posición del texto

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
        texto.EstablecerTexto(cantidad, color); // Pasamos la cantidad de daño y color al texto
        nuevoTextoGO.transform.SetParent(canvasTextoPosicion);
        nuevoTextoGO.transform.position = canvasTextoPosicion.position; // Pasamos la posición del texto
        nuevoTextoGO.SetActive(true); // Activamos el objeto

        yield return new WaitForSeconds(1f); // Esperamos 1 segundo para regresar el texto
        nuevoTextoGO.SetActive(false); // Volvemos a desactivar el texto
        nuevoTextoGO.transform.SetParent(pooler.ListaContenedor.transform); // Lo regresamos a la lista contenedor del pooler
    }

    private void RespuestaDañoRecibidoHaciaPlayer(float daño)
    {
        if (tipoPersonaje == TipoPersonaje.Player) // Si el objeto que tiene esta clase es el player
        {
            StartCoroutine(IEMostrarTexto(daño, Color.black)); // Mostramos daño en color negro
        }
    }

    private void RespuestaDañoHaciaEnemigo(float daño, EnemigoVida enemigoVida)
    {
        if (tipoPersonaje == TipoPersonaje.IA && _enemigoVida == enemigoVida) // Si el objeto que tiene esta clase es un personaje
        {
            StartCoroutine(IEMostrarTexto(daño, Color.red)); // Mostramos daño en color rojo
        }
    }

    private void OnEnable()
    {
        IAController.EventoDañoRealizado += RespuestaDañoRecibidoHaciaPlayer;
        PersonajeAtaque.EventoEnemigoDañado += RespuestaDañoHaciaEnemigo;
    }

    private void OnDisable()
    {
        IAController.EventoDañoRealizado -= RespuestaDañoRecibidoHaciaPlayer;
        PersonajeAtaque.EventoEnemigoDañado -= RespuestaDañoHaciaEnemigo;
    }
}

