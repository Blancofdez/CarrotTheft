using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class NewBehaviourScript : MonoBehaviour
{
    string frase1;
    string frase2;
    string frase3;
    string frase4;
    string fraseHS;
    string fraseGM;

    [SerializeField] private GameObject fade;
    Animator anim;
    public Text texto;
    public Text texto1;
    public Text texto2;
    public Text texto3;
    public Text texto4;
    public Text textoHS;
    public Text textoGM;

    GameData game;

    [SerializeField] Image mesPanelIntro;
    [SerializeField] Image mesPanel1;
    [SerializeField] Image mesPanel2;
    [SerializeField] Image mesPanel3;
    [SerializeField] Image mesPanel4;
    [SerializeField] Image mesPanelGM;
    [SerializeField] Image mesPanelHS;

    const string DATA_FILE = "data.json";


    void Start()
    {
        game = LoadData();

        anim = fade.GetComponent<Animator>();

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            StartCoroutine(Reloj0());
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {

            StartCoroutine(Reloj1());
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {

            StartCoroutine(Reloj2());
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {

            StartCoroutine(Reloj3());
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {

            StartCoroutine(Reloj4());

        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            Cursor.visible = false;
            anim.SetTrigger("isGameIn");
            StartCoroutine(Reloj5());
        }
    }


    GameData LoadData()
    {
        if (File.Exists(DATA_FILE))
        {
            string fileText = File.ReadAllText(DATA_FILE);
            return JsonUtility.FromJson<GameData>(fileText);
        }
        else
            return new GameData();
    }

    IEnumerator Reloj0()
    {
        Cursor.visible = false;

        frase1 = "Una catástrofre ha ocurrido,\nlos secuaces del rey ardilla\nhan robado todas las reservas\nde comida de Rabbittown.";
        frase2 = "También han robado las 12 gemas\nde la corona real.";
        frase3 = "El rey conejo, ha enviado\na su mayor heroina, Anny\na recuperar las provisiones\ny los tesoros de su pueblo.";
        frase4 = "Que comience la aventura.";



        yield return new WaitForSeconds(1f);

        foreach (char caracter in frase1)
        {
            texto.text = texto.text + caracter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(2f);

        texto.text = "";

        foreach (char caracter in frase2)
        {
            texto.text = texto.text + caracter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(2f);

        texto.text = "";

        foreach (char caracter in frase3)
        {
            texto.text = texto.text + caracter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(2f);

        texto.text = "";

        foreach (char caracter in frase4)
        {
            texto.text = texto.text + caracter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(2f);

        anim.SetTrigger("isGameOut");

        StartCoroutine(LoadNextScene(1));



    }

    IEnumerator Reloj1()
    {
        frase1 = "Consigue recuperar todas las zanahorias robadas\nasí como las gemas reales.";
        frase2 = "Acaba con los enemigos saltando sobres ellos.";
        frase3 = "Las lianas son ecurridizas, aguanta o caerás.";
        frase4 = "Buena suerte.";


        yield return new WaitForSeconds(1.5f);

        mesPanelIntro.gameObject.SetActive(!mesPanelIntro.gameObject.activeSelf);

        foreach (char caracter in frase1)
        {
            texto.text = texto.text + caracter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);

        texto.text = "";

        foreach (char caracter in frase2)
        {
            texto.text = texto.text + caracter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);

        texto.text = "";

        foreach (char caracter in frase3)
        {
            texto.text = texto.text + caracter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);

        texto.text = "";

        foreach (char caracter in frase4)
        {
            texto.text = texto.text + caracter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);


        mesPanelIntro.gameObject.SetActive(!mesPanelIntro.gameObject.activeSelf);
        texto.text = "";

    }

    IEnumerator Reloj2()
    {

        frase1 = "Investiga a fondo los túneles, tienen secretos.";
        frase2 = "Buena suerte.";


        yield return new WaitForSeconds(1.5f);

        mesPanelIntro.gameObject.SetActive(!mesPanelIntro.gameObject.activeSelf);

        foreach (char caracter in frase1)
        {
            texto.text = texto.text + caracter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);

        texto.text = "";

        foreach (char caracter in frase2)
        {
            texto.text = texto.text + caracter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);

        mesPanelIntro.gameObject.SetActive(!mesPanelIntro.gameObject.activeSelf);
        texto.text = "";

    }

    IEnumerator Reloj3()
    {

        frase1 = "Recuerda, las lianas son ecurridizas.";
        frase2 = "Buena suerte.";


        yield return new WaitForSeconds(1.5f);

        mesPanelIntro.gameObject.SetActive(!mesPanelIntro.gameObject.activeSelf);

        foreach (char caracter in frase1)
        {
            texto.text = texto.text + caracter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);

        texto.text = "";

        foreach (char caracter in frase2)
        {
            texto.text = texto.text + caracter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);

        mesPanelIntro.gameObject.SetActive(!mesPanelIntro.gameObject.activeSelf);
        texto.text = "";

    }

    IEnumerator Reloj4()
    {

        frase1 = "Las ardillas son intocables cuando ruedan.";
        frase2 = "Cuidado con las trampas\nBuena suerte.";


        yield return new WaitForSeconds(1.5f);

        mesPanelIntro.gameObject.SetActive(!mesPanelIntro.gameObject.activeSelf);

        foreach (char caracter in frase1)
        {
            texto.text = texto.text + caracter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);

        texto.text = "";

        foreach (char caracter in frase2)
        {
            texto.text = texto.text + caracter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);

        mesPanelIntro.gameObject.SetActive(!mesPanelIntro.gameObject.activeSelf);
        texto.text = "";

    }


    IEnumerator Reloj5()
    {
        frase1 = "NIVEL 1 - SCORE " + game.hscore1 + "/300 - GEM " + game.gem1 + "/3";

        frase2 ="NIVEL 2 - SCORE " + game.hscore2 + "/300 - GEM " + game.gem2 + "/3";

        frase3 = "NIVEL 3 - SCORE " + game.hscore3 + "/300 - GEM " + game.gem3 + "/3";

        frase4 = "NIVEL 4 - SCORE " + game.hscore4 + "/300 - GEM " + game.gem4 + "/3";

        fraseHS = "TOTAL SCORE " + game.hscore;

        fraseGM = "TOTAL GEM " + (game.gem1 + game.gem2 + game.gem3 + game.gem4);

        yield return new WaitForSeconds(1.5f);

        mesPanel1.gameObject.SetActive(!mesPanel1.gameObject.activeSelf);

        foreach (char caracter in frase1)
        {
            texto1.text = texto1.text + caracter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);

        mesPanel2.gameObject.SetActive(!mesPanel2.gameObject.activeSelf);

        foreach (char caracter in frase2)
        {
            texto2.text = texto2.text + caracter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);

        mesPanel3.gameObject.SetActive(!mesPanel3.gameObject.activeSelf);

        foreach (char caracter in frase3)
        {
            texto3.text = texto3.text + caracter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);

        mesPanel4.gameObject.SetActive(!mesPanel4.gameObject.activeSelf);

        foreach (char caracter in frase4)
        {
            texto4.text = texto4.text + caracter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);

        mesPanelHS.gameObject.SetActive(!mesPanelHS.gameObject.activeSelf);

        foreach (char caracter in fraseHS)
        {
            textoHS.text = textoHS.text + caracter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);

        mesPanelGM.gameObject.SetActive(!mesPanelGM.gameObject.activeSelf);

        foreach (char caracter in fraseGM)
        {
            textoGM.text = textoGM.text + caracter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(5f);

        anim.SetTrigger("isGameOut");

        StartCoroutine(LoadNextScene(0));

    }

    public IEnumerator LoadNextScene(int id)
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(id);

    }
}