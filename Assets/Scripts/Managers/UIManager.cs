using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [Header("Stats")]
    [SerializeField] private PersonajeStats stats; // Es del tipo PersonajeStats (ScriptableObject que hemos creado)

    [Header("Paneles")]
    [SerializeField] private GameObject panelStats;

    [Header("Barra")]
    [SerializeField] private Image vidaPlayer;
    [SerializeField] private Image manaPlayer;
    [SerializeField] private Image expPlayer;

    [Header("Texto")]
    [SerializeField] private TextMeshProUGUI vidaTMP;
    [SerializeField] private TextMeshProUGUI manaTMP;
    [SerializeField] private TextMeshProUGUI expTMP;
    [SerializeField] private TextMeshProUGUI nivelTMP;

    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI statDañoTMP;
    [SerializeField] private TextMeshProUGUI statDefensaTMP;
    [SerializeField] private TextMeshProUGUI statCriticoTMP;
    [SerializeField] private TextMeshProUGUI statBloqueoTMP;
    [SerializeField] private TextMeshProUGUI statVelocidadTMP;
    [SerializeField] private TextMeshProUGUI statNivelTMP;
    [SerializeField] private TextMeshProUGUI statExpTMP;
    [SerializeField] private TextMeshProUGUI statExpRequeridaTMP;
    [SerializeField] private TextMeshProUGUI atributoFuerzaTMP;
    [SerializeField] private TextMeshProUGUI atributoInteligenciaTMP;
    [SerializeField] private TextMeshProUGUI atributoDestrezaTMP;
    [SerializeField] private TextMeshProUGUI atributosDisponiblesTMP;

    // Vida
    private float vidaActual;
    private float vidaMax;
    
    // Maná
    private float manaActual;
    private float manaMax;

    // Experiencia
    private float expActual;
    private float expRequeridaNuevoNivel;

    // Update is called once per frame
    void Update()
    {
        ActualizarUIPersonaje();
        ActualizarPanelStats();
    }

    private void ActualizarUIPersonaje()
    {
        vidaPlayer.fillAmount = Mathf.Lerp(vidaPlayer.fillAmount, vidaActual / vidaMax, 10f * Time.deltaTime); // Actualizamos la barra de vida
        manaPlayer.fillAmount = Mathf.Lerp(manaPlayer.fillAmount, manaActual / manaMax, 10f * Time.deltaTime); // Actualizamos la barra de maná
        expPlayer.fillAmount = Mathf.Lerp(expPlayer.fillAmount, expActual / expRequeridaNuevoNivel, 10f * Time.deltaTime); // Actualizamos la barra de experiencia

        vidaTMP.text = $"{vidaActual}/{vidaMax}"; // Actualizamos el texto de vida
        manaTMP.text = $"{manaActual}/{manaMax}"; // Actualizamos el texto de maná
        expTMP.text = $"{expActual} XP"; // Actualizamos el texto de experiencia (puntos XP)
        // expTMP.text = $"{((expActual/expRequeridaNuevoNivel) * 100):F2}%"; // Actualizamos el texto de experiencia (porcentaje)

        nivelTMP.text = stats.Nivel.ToString(); // Actualizamos el nivel del personaje mostrado convirtiendo el stat en texto
    }

    private void ActualizarPanelStats()
    {
        if (panelStats.activeSelf == false) // Si el panel de stats no está activo, es decir, no se muestra en pantalla
        {
            return; // No actualizamos, paramos aquí
        }

        // Convertimos las variables a texto de TextMeshPro
        statDañoTMP.text = stats.Daño.ToString();
        statDefensaTMP.text = stats.Defensa.ToString();
        statCriticoTMP.text = $"{stats.PorcentajeCritico}%"; // Porcentaje, usamos string interpolation
        statBloqueoTMP.text = $"{stats.PorcentajeBloqueo}%"; // Porcentaje, usamos string interpolation
        statVelocidadTMP.text = stats.Velocidad.ToString();
        statNivelTMP.text = stats.Nivel.ToString();
        statExpTMP.text = stats.ExpActual.ToString();
        statExpRequeridaTMP.text = stats.ExpRequeridaSiguienteNivel.ToString();

        atributoFuerzaTMP.text = stats.Fuerza.ToString();
        atributoInteligenciaTMP.text = stats.Inteligencia.ToString();
        atributoDestrezaTMP.text = stats.Destreza.ToString();
        atributosDisponiblesTMP.text = $"Puntos: {stats.PuntosDisponibles}"; // Porcentaje, usamos string interpolation
    }
    
    public void ActualizarVidaPersonaje(float pVidaActual, float pVidaMax) // Método para obtener los valores de vida
    {
        vidaActual = pVidaActual;
        vidaMax = pVidaMax;
    }

    public void ActualizarManaPersonaje(float pManaActual, float pManaMax) // Método para obtener los valores de maná
    {
        manaActual = pManaActual;
        manaMax = pManaMax;
    }

    public void ActualizarExpPersonaje(float pExpActual, float pExpRequerida) // Método para obtener los valores de maná
    {
        expActual = pExpActual;
        expRequeridaNuevoNivel = pExpRequerida;
    }
}
