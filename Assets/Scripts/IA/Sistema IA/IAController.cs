using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TiposDeAtaque
{
    Melee,
    Embestida
}

public class IAController : MonoBehaviour // Clase principal del enemigo
{
    public static Action<float> EventoDañoRealizado; // Evento de daño realizado

    [Header("Stats")]
    [SerializeField] private PersonajeStats stats; // Stats del personaje, las compararemos a la hora del combate

    [Header("Estados")]
    [SerializeField] private IAEstado estadoInicial; // Estado inicial del enemigo
    [SerializeField] private IAEstado estadoDefault; // Estado inicial del enemigo

    [Header("Config")]
    [SerializeField] private float rangoDeteccion; // Variable que indica el rango de detección del enemigo
    [SerializeField] private float rangoDeAtaque; // Variable que indica el rango de ataque
    [SerializeField] private float rangoDeEmbestida; // Variable que indica el rango de la embestida
    [SerializeField] private float velocidadMovimiento; // Variable que indica la velocidad de movimiento del enemigo
    [SerializeField] private float velocidadDeEmbestida; // Variable que indica el la velocidad de la embestida
    [SerializeField] private LayerMask personajeLayerMask; // LayerMask de nuestro personaje, es lo que tratamos de detectar en le juego

    [Header("Ataque")]
    [SerializeField] private float daño; // Variable de daño
    [SerializeField] private float tiempoEntreAtaques; // Valor del tiempo entre ataque y ataque, para evitar de este modo muchos ataques en un pequeño tiempo
    [SerializeField] private TiposDeAtaque tipoAtaque; // Variable que define el tipo de ataque

    [Header("Debug")] // Para testing
    [SerializeField] private bool mostrarDeteccion; // Variable que muestra el rango de detección del enemigo (Gizmo)
    [SerializeField] private bool mostrarRangoAtaque; // Variable que muestra el rango de ataque del enemigo (Gizmo)
    [SerializeField] private bool mostrarRangoEmbestida; // Variable que muestra el rango de embestida del enemigo (Gizmo)

    private float tiempoParaSiguienteAtaque; // Variable interna para controlar el tiempo para el siguiente ataque
    private BoxCollider2D _boxCollider2D; // Referencia al collider del enemigo (usaremos esta variable para desactivarlo cuando realice una embestida)

    public Transform PersonajeReferencia { get; set; } // Referencia de la posición del personaje
    public EnemigoMovimiento EnemigoMovimiento { get; set; } // Referencia al movimiento del enemigo
    public float RangoDeteccion => rangoDeteccion; // Propiedad para regresar el rango de detección del enemigo
    public float RangoDeAtaqueDeterminado => tipoAtaque == TiposDeAtaque.Embestida ? rangoDeEmbestida : rangoDeAtaque; // Propiedad que nos regresa lo siguiente:
    //Si el tipo de ataque es embestida, nos regresa que el rango determinado es el rango de embestida. Si no, nos regresa que el rango determinado es el rango de ataque
    public float VelocidadMovimiento => velocidadMovimiento; // Propiedad para regresar la valocidad de movimiento del enemigo
    public float Daño => daño; // Pro piedad que nos regresa el valor del daño
    public TiposDeAtaque TipoAtaque => tipoAtaque; // Propiedad que nos regresa el tipo de ataque
    public LayerMask PersonajeLayerMask => personajeLayerMask; // Propiedad para regresar personajeLayerMask
    public IAEstado EstadoActual { get; set; } // Propiedad que nos guarda el valor del estado actual

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>(); // Obtenemos el collider del enemigo
        EstadoActual = estadoInicial; // Inicializamos el estado actual definiéndolo como el inicial
        EnemigoMovimiento = GetComponent<EnemigoMovimiento>(); // Obtenemos el componente del movimiento del enemigo
    }

    private void Update()
    {
        EstadoActual.EjecutarEstado(this); // Ejecutamos el estado con la referencia de este controller
    }

    public void CambiarEstado(IAEstado nuevoEstado) // Método para cambiar a un nuevo estado
    {
        if (nuevoEstado != estadoDefault) // Si el estado al que queremos cambiar es diferente al predeterminado
        {
            EstadoActual = nuevoEstado; // Lo cambiamos
        }
    }

    public void AtaqueMelee(float cantidad) // Ataque melee o directo
    {
        if (PersonajeReferencia != null) // Si tenemos una referencia al personaje
        {
            AplicarDañoAlPersonaje(cantidad); // Llamamos al método para aplicar daño con la cantidad especificada
        }
    }
    
    public void AtaqueEmbestida(float cantidad) // Ataque de embestida
    {
        StartCoroutine(IEEmbestida(cantidad));
    }

    private IEnumerator IEEmbestida(float cantidad)
    {
        Vector3 personajePosicion = PersonajeReferencia.position; // Vector que guarda la posición del personaje
        Vector3 posicionInicial = transform.position; // Vector que guarda la posición inicial del enemigo (la usamos para posteriormente volver a ella en la embestida)
        Vector3 direccionHaciaPersonaje = (personajePosicion - posicionInicial).normalized; // Vector que indica la dirección hacia nuestro personaje
        Vector3 posicionDeAtaque = personajePosicion - direccionHaciaPersonaje * 0.5f; // Con este vector nos aseguramos de que el enemigo no llega hasta la posición del personaje al hacer la embestida, sino que guarda un poco las distancias
        _boxCollider2D.enabled = false; // Desactivamos el collider del enemigo para que no interfiera en la transición del ataque

        float transicionDeAtaque = 0;
        while (transicionDeAtaque <= 1f) // Mientras la transición de ataque sea menor o igual a 1, continuamos la ejecución
        {
            transicionDeAtaque += Time.deltaTime * velocidadMovimiento; // Actualizamos el valor de la transición con el tiempo
            float interpolacion = (-Mathf.Pow(transicionDeAtaque, 2) + transicionDeAtaque) * 4f; // Fórmula que nos permite que el enemigo vaya hacia el personaje y después regrese a su posición inicial
            transform.position = Vector3.Lerp(posicionInicial, posicionDeAtaque, interpolacion); // Actualizamos la posición del enemigo con los tres parámetros de transición
            yield return null; // Esperamos un fotograma para continuar con la ejecución del while
        }

        if (PersonajeReferencia != null) // Si tenemos una referencia del personaje
        {
            AplicarDañoAlPersonaje(cantidad); // Aplicamos el daño al personaje
        }

        _boxCollider2D.enabled = true; // Volvemos a activar el collider del enemigo
    }

    public void AplicarDañoAlPersonaje(float cantidad) // Método para que el personaje reciba daño, le pasamos el valor de ese daño
    {
        float dañoPorRealizar = 0;
        if (UnityEngine.Random.value < stats.PorcentajeBloqueo / 100) // Generamos un valor aleatorio (0-1) y si este es menor al porcentaje de bloqueo (0-1)
        {
            return; // Regresamos, no recibimos daño, bloqueamos el ataque
        }

        // Si es mayor, es decir, si no bloqueamos el ataque
        dañoPorRealizar = Mathf.Max(cantidad - stats.Defensa, 1f); // Guardamos en una variable el mayor de los valores especificados, con esto conseguimos que siempre recibamos por lo menos 1 de daño y para mayores valores entre en juego la stat de defensa
        PersonajeReferencia.GetComponent<PersonajeVida>().RecibirDaño(dañoPorRealizar); // Aplicamos el daño al personaje en la clase PersonajeVida
        EventoDañoRealizado?.Invoke(dañoPorRealizar); // Si el evento no es nulo, lo invocamos
    }

    public bool PersonajeEnRangoDeAtaque(float rango) // Método para saber si el personaje está en el rango de ataque. Recibe el valor del rango
    {
        float distanciaHaciaPersonaje = (PersonajeReferencia.position - transform.position).sqrMagnitude; // Creamos una variable con la distancia hasta el personaje restando los dos vectores. Utilizamos sqrMagnitude para obtener la magnitud al cuadrado y poder compararla de forma más efectiva
        if (distanciaHaciaPersonaje < Mathf.Pow(rango, 2)) // Si la distancia hasta el personaje es menor al rango al cuadrado, es decir, si el personaje está en rango
        {
            return true; // Regresamos verdadero
        }

        // Si no es el caso
        return false; // Regresamos falso
    }

    public bool EsTiempoDeAtacar() // Método que nos regresa si es momento de atacar o no
    {
        if (Time.time > tiempoParaSiguienteAtaque) // Si hemos superado el tiempo para el siguiente ataque
        {
            return true; // Regresamos verdadero, podemos atacar de nuevo
        }

        // Si no
        return false; // Regresamos falso, debemos esperar que se cumpla el tiempo para el siguiente ataque
    }

    public void ActualizarTiempoEntreAtaques() // Método para actualizar la variable del tiempo para el siguiente ataque
    {
        tiempoParaSiguienteAtaque = Time.time + tiempoEntreAtaques;
    }

    private void OnDrawGizmos()
    {
        if (mostrarDeteccion) // Si definimos mostrarDeteccion como verdadero (si lo activamos desde el inspector)
        {
            //Dibujamos los Gizmos
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
        }

        if (mostrarRangoAtaque) // Si definimos mostrarRangoAtaque como verdadero (si lo activamos desde el inspector)
        {
            //Dibujamos los Gizmos
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, rangoDeAtaque);
        }

        if (mostrarRangoEmbestida) // Si definimos mostrarRangoEmbestida como verdadero (si lo activamos desde el inspector)
        {
            //Dibujamos los Gizmos
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, rangoDeEmbestida);
        }
    }
}
