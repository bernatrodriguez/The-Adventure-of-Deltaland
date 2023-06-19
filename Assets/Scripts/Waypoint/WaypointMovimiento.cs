using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DireccionMovimiento
{
    Horizontal,
    Vertical
}

public class WaypointMovimiento : MonoBehaviour
{
    [SerializeField] private float velocidad; // Velocidad del personaje

    public Vector3 PuntoPorMoverse => _waypoint.ObtenerPosicionMovimiento(puntoActualIndex); // Posición del personaje

    protected Waypoint _waypoint;
    protected Animator _animator;
    protected int puntoActualIndex;
    protected Vector3 ultimaPosicion;
    
    // Start is called before the first frame update
    void Start()
    {
        puntoActualIndex = 0; // Primer punto de la ruta
        _animator = GetComponent<Animator>(); // Obtenemos el animator
        _waypoint = GetComponent<Waypoint>(); // Obtenemos el waypoint
    }

    // Update is called once per frame
    void Update()
    {
        MoverPersonaje();
        RotarPersonaje();
        RotarVertical();
        if (ComprobarPuntoActualAlcanzado()) // Si hemos alcanzado el punto actual
        {
            ActualizarIndexMovimiento(); // Pasamos al siguiente punto
        }
    }

    private void MoverPersonaje()
    {
        transform.position = Vector3.MoveTowards(transform.position, PuntoPorMoverse, velocidad * Time.deltaTime); // Transformamos la posición del personaje para generar el movimiento
    }

    private bool ComprobarPuntoActualAlcanzado() // Si hemos llegado al punto actual, los devuelve verdadero
    {
        float distanciaHaciaPuntoActual = (transform.position - PuntoPorMoverse).magnitude; // Obtenemos la distancia al punto
        if (distanciaHaciaPuntoActual < 0.1f) // Si estamos muy cerca del punto
        {
            ultimaPosicion = transform.position; // Guardamos la última posición en una variable
            return true; // Regresamos verdadero, lo que quiere decir que hemos alcanzado el punto actual
        }

        // Si no estamos tan cerca
        return false; // Regresamos falso, no lo hemos alcanzado aun
    }

    private void ActualizarIndexMovimiento()
    {
        if (puntoActualIndex == _waypoint.Puntos.Length - 1) // Si nuestro punto actual llega al último punto de la ruta
        {
            puntoActualIndex = 0; // Reestablecemos el punto al inicial, es decir, mandamos al personaje hacia le primer punto para que se mueva en bucle
        }
        else // Si aun no hemos llegado al último punto
        {
            if (puntoActualIndex < _waypoint.Puntos.Length - 1) // Si nuestro punto actual es menor al último punto de la ruta
            {
                puntoActualIndex++; // Pasamos al siguiente punto
            }
        }
    }

    protected virtual void RotarPersonaje() // Método que rota el personaje mediante los valores de su escala
    {

    }

    protected virtual void RotarVertical()
    {

    }
}
