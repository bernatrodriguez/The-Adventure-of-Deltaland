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
        ManaActual = manaInicial; // Establecemos el man� en el valor inicial
        ActualizarBarraMana(); // Al iniciar el juego, actualizamos la barra de man�

        InvokeRepeating(nameof(RegenerarMana), 1, 1); // Llamaremos a la funci�n de regenerar man� una vez por cada segundo (1, 1)
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) // Cuando pulsamos la letra G del teclado
        {
            UsarMana(10f); // Usaremos 10 de man�
        }
    }

    public void UsarMana(float cantidad) // Debemos especificar la cantidad de man� que queramos gastar seg�n el uso que le demos
    {
        if (ManaActual >= cantidad) // Si tenemos suficiente man�, es decir, si el actual es mayor o igual que el requerido
        {
            ManaActual -= cantidad; // Restamos el requerido al actual
            ActualizarBarraMana(); // Llamamos al m�todo para actualizar la barra de man�
        }
    }

    private void RegenerarMana() // M�todo para regenerar el man� por segundo
    {
        if (_personajeVida.Salud > 0f && ManaActual < manaMax) // Si estamos vivos y el man� no est� al m�ximo
        {
            ManaActual += regeneracionPorSegundo; // Sumamos el valor de man� por segundo
            ActualizarBarraMana(); // Actualizamos
        }
    }

    public void RestablecerMana () // Restaura la totalidad del man�
    {
        ManaActual = manaInicial; // Establecemos el valor del man� actual con el inicial
        ActualizarBarraMana(); // Actualizamos la barra
    }

    private void ActualizarBarraMana() // M�todo que env�a nuestros valores par actualizar la barra de man�
    {
        UIManager.Instance.ActualizarManaPersonaje(ManaActual, manaMax);
    }
}
