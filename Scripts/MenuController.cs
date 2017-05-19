using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public Canvas loadMenu;
    public Canvas quitMenu;
    public Canvas starMenu;

    public Button startText;
    public Button exitText;
    public Button settingsText;

    public Slider loadBar;

    public Text loadText;

    public static MenuController menuController;
    // Use this for initialization

    private void Awake() {
        QualitySettings.SetQualityLevel(5, true);
    }
    void Start() {
        quitMenu = quitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        settingsText = settingsText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        quitMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

    }

    public void ExitPress() {
        quitMenu.gameObject.SetActive(true);
        starMenu.gameObject.SetActive(false);
    }

    public void NoPress() {
        quitMenu.gameObject.SetActive(false);
        starMenu.gameObject.SetActive(true);
    }

    public void YesPress() {
        Debug.Log("Close game");
        Application.Quit();
    }

    public void startGame() {
        StartCoroutine(LoadNewScene());
    }

    IEnumerator LoadNewScene() {
        starMenu.gameObject.SetActive(false);
        loadMenu.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        AsyncOperation async = SceneManager.LoadSceneAsync(1);
        Debug.Log("dsada");
        while (!async.isDone) {
            loadBar.value = async.progress;
            Debug.Log("Carregando:"+async.progress);
            if(async.progress == 0.9f) {
                loadText.text = "Press Any Key to Continue";
                if (Input.anyKeyDown) {
                    async.allowSceneActivation = true;
                }
            }
            yield return null;
        }


    }
}
