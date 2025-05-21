using UnityEngine;
using UnityEngine.UI;

public class MovimientoClientes : MonoBehaviour
{
    [SerializeField] private Text panA, tomate, lechuga, cebolla, queso, champi, bacon, carne, panB;
    private int numeroAleatorio;
    private GameFlow gf;
    private UnityEngine.AI.NavMeshAgent pathfinder;
    private Transform target;

    void Start()
    {
        // Inicialización de variables
        numeroAleatorio = 5;
        pathfinder = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Cola").transform;
        gf = GameObject.Find("GM").GetComponent<GameFlow>();

        // Ajusto manualmente los textos
        GameObject canvas = GameObject.Find("Canvas");
        panA = canvas.transform.Find("PanAText").GetComponent<Text>();
        tomate = canvas.transform.Find("TomateText").GetComponent<Text>();
        lechuga = canvas.transform.Find("LechugaText").GetComponent<Text>();
        cebolla = canvas.transform.Find("CebollaText").GetComponent<Text>();
        queso = canvas.transform.Find("QuesoText").GetComponent<Text>();
        champi = canvas.transform.Find("ChampiText").GetComponent<Text>();
        bacon = canvas.transform.Find("BaconText").GetComponent<Text>();
        carne = canvas.transform.Find("CarneText").GetComponent<Text>();
        panB = canvas.transform.Find("PanBText").GetComponent<Text>();
    }

    void Update()
    {
        // Movimiento del cliente
        if (target != null)
        {
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            Vector3 targetPosition = target.position - dirToTarget;
            pathfinder.SetDestination(targetPosition);
        }
    }

    private void OnMouseDown()
    {
        if (numeroAleatorio == 5)   // Control para sólo coger el pedido una vez
        {
            numeroAleatorio = UnityEngine.Random.Range(0, 5);
            GameFlow.orderValue = GameFlow.menu[numeroAleatorio];
            string orderStr = GameFlow.orderValue.ToString("D9");

            //Debug.Log(orderStr);

            // Asignar cada dígito a los textos (empezando desde la derecha)
            if (panA != null) panA.text = "X " + orderStr[8].ToString();    // 1ª cifra
            if (tomate != null) tomate.text = "X " + orderStr[4].ToString();  // 5ª cifra
            if (lechuga != null) lechuga.text = "X " + orderStr[5].ToString(); // 4ª cifra
            if (cebolla != null) cebolla.text = "X " + orderStr[3].ToString(); // 6ª cifra
            if (queso != null) queso.text = "X " + orderStr[6].ToString();  // 3ª cifra
            if (champi != null) champi.text = "X " + orderStr[1].ToString();  // 8ª cifra
            if (bacon != null) bacon.text = "X " + orderStr[2].ToString();  // 7ª cifra
            if (carne != null) carne.text = "X " + orderStr[0].ToString();  // 9ª cifra
            if (panB != null) panB.text = "X " + orderStr[7].ToString();   // 2ª cifra

            gf.IniciarCuentaAtras();
        }
    }
}