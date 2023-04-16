using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeMovimiento : MonoBehaviour
{
    [SerializeField] private float velocidad;
    // Utilizamos el atributo SerielizeField para poder definir la variable en el inspector (no usamos variables publicas para evitar posibles errores con accesos desde otras clases)
    // public float Velocidad => velocidad; // Podemos crear esta propiedad para regresar el valor y poder trabajar con otras clases como si fuese pública

    public Vector2 DireccionMovimiento => _direccionMovimiento; // Retornamos el valor de la dirección de movimiento para poder trabajarlo en otras clases, por ejemplo, en aplicar animaciones para cada dirección
    public bool EnMovimiento => _direccionMovimiento.magnitude > 0f; // Detectamos si el personaje se mueve comprobando si su dirección tiene una magnitud

    private Rigidbody2D _rigidbody2D;
    private Vector2 _direccionMovimiento;
    private Vector2 _input;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // Con esto asignamos las teclas A y D para el movimiento horizontal y las teclas S y W para el vertical, que son las que tenemos establecidas en el proyecto de Unity

        // X
        if (_input.x > 0.1f) // Si la dirección del movimiento en X es mayor a un valor muy pequeño, significa que nos movemos hacia la derecha
        {
            _direccionMovimiento.x = 1f; // El movimiento es positivo
        }
        else if (_input.x < 0f) // Si es menor a 0, nos movemos a la izquierda
        {
            _direccionMovimiento.x = -1f; // El movimiento es negativo
        }
        else // Si ninguno de los dos anteriores es el caso, no nos estamos moviendo horizontalmente
        {
            _direccionMovimiento.x = 0;
        }

        // Y
        if (_input.y > 0.1f) // Si la dirección del movimiento en Y es mayor a un valor muy pequeño, significa que nos movemos hacia arriba
        {
            _direccionMovimiento.y = 1f; // El movimiento es positivo
        }
        else if (_input.y < 0f) // Si es menor a 0, nos movemos hacia abajo
        {
            _direccionMovimiento.y = -1f; // El movimiento es negativo
        }
        else // Si ninguno de los dos anteriores es el caso, no nos estamos moviendo verticalmente
        {
            _direccionMovimiento.y = 0;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _direccionMovimiento * velocidad * Time.fixedDeltaTime); // Aplicamos el movimiento a nuestro personaje
    }
}
