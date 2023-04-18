using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeAnimaciones : MonoBehaviour
{
    [SerializeField] private string layerIdle;
    [SerializeField] private string layerCaminar;

    private Animator _animator;
    private PersonajeMovimiento _personajeMovimiento;

    private readonly int direccionX = Animator.StringToHash("X");
    private readonly int direccionY = Animator.StringToHash("Y");
    private readonly int derrotado = Animator.StringToHash("Derrotado");
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _personajeMovimiento = GetComponent<PersonajeMovimiento>(); // Acceso a la clase PersonajeMovimiento
    }

    // Update is called once per frame
    void Update()
    {
        ActualizarLayers(); // Llamamos a la funci�n que nos activa el layer de animaci�n deseado

        if (_personajeMovimiento.EnMovimiento == false) // Si nuestro personaje no se est� moviendo
        {
            return; // No seguimos ejecutando el c�digo siguiente (esto nos permite que al parar de mover nuestro personaje este se quede con la �ltima animaci�n)
        }
        _animator.SetFloat(direccionX, _personajeMovimiento.DireccionMovimiento.x);
        _animator.SetFloat(direccionY, _personajeMovimiento.DireccionMovimiento.y);
    }

    private void ActivarLayer(string nombreLayer) // Activaremos el layer de animaci�n que necesitemos en cada momento
    {
        for (int i = 0; i < _animator.layerCount; i++) // Por todos los layer que tenemos en nuestro animator
        {
            _animator.SetLayerWeight(i, 0); // Los desactivamos
        }

        _animator.SetLayerWeight(_animator.GetLayerIndex(nombreLayer), 1); // Activamos este layer en concreto, que ser� en base al movimiento que tenga en el momento
    }

    private void ActualizarLayers()
    {
        if (_personajeMovimiento.EnMovimiento) // Si el personaje est� en movimiento
        {
            ActivarLayer(layerCaminar); // Activamos el layer de caminar
        }
        else // En caso contrario
        {
            ActivarLayer(layerIdle); // Activamos el layer de idle
        }
    }

    private void PersonajeDerrotadoRespuesta()
    {
        if (_animator.GetLayerWeight(_animator.GetLayerIndex(layerIdle)) == 1) // Si estamos en el layer idle
        {
            _animator.SetBool(derrotado, true); // Modificamos el par�metro de derrotado a true
        }
    }
    
    // Para que esta clase escuche eventos, debemos a�adir los siguientes m�todos
    private void OnEnable() // Cuando la clase es activada
    {
        PersonajeVida.EventoPersonajeDerrotado += PersonajeDerrotadoRespuesta; // Nos suscribimos al evento
    }

    private void OnDisable() // Cuando la clase es desactivada
    {
        PersonajeVida.EventoPersonajeDerrotado -= PersonajeDerrotadoRespuesta; // Nos desuscribimos del evento
    }
}
