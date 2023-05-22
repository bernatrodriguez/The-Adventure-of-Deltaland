using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeMana : MonoBehaviour
{
    // Inicializamos las variables
    [SerializeField] private float manaInicial;
    [SerializeField] private float manaMax;
    [SerializeField] private float regeneracionPorSegundo;

    public float ManaActual { get; private set; }

    private PersonajeVida _personajeVida;

    private void Awake()
    {
        _personajeVida = GetComponent<PersonajeVida>(); // Obtenemos el componente de la vida
    }

    void Start()
    {
        ManaActual = manaInicial; // Establecemos el maná en el valor inicial
        ActualizarBarraMana(); // Al iniciar el juego, actualizamos la barra de maná

        InvokeRepeating(nameof(RegenerarMana), 1, 1); // Llamaremos a la función de regenerar maná una vez por cada segundo (1, 1)
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) // Cuando pulsamos la letra G del teclado
        {
            UsarMana(10f); // Usaremos 10 de maná
        }
    }

    public void UsarMana(float cantidad) // Debemos especificar la cantidad de maná que queramos gastar según el uso que le demos
    {
        if (ManaActual >= cantidad) // Si tenemos suficiente maná, es decir, si el actual es mayor o igual que el requerido
        {
            ManaActual -= cantidad; // Restamos el requerido al actual
            ActualizarBarraMana(); // Llamamos al método para actualizar la barra de maná
        }
    }

    private void RegenerarMana() // Método para regenerar el maná por segundo
    {
        if (_personajeVida.Salud > 0f && ManaActual < manaMax) // Si estamos vivos y el maná no está al máximo
        {
            ManaActual += regeneracionPorSegundo; // Sumamos el valor de maná por segundo
            ActualizarBarraMana(); // Actualizamos
        }
    }

    public void RestablecerMana () // Restaura la totalidad del maná
    {
        ManaActual = manaInicial; // Establecemos el valor del maná actual con el inicial
        ActualizarBarraMana(); // Actualizamos la barra
    }

    private void ActualizarBarraMana() // Método que envía nuestros valores par actualizar la barra de maná
    {
        UIManager.Instance.ActualizarManaPersonaje(ManaActual, manaMax);
    }
}
