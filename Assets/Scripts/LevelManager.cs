
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    //[SerializeField] private GameObject _loader;
   // [SerializeField] private Image _image;
    private float target;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void  CargarEscena(string scenename) {
        SceneManager.LoadScene(scenename);    
    }

    /*
    public async void LoadScene(string scenename) {
        target = 0;
        _image.fillAmount = 0;
        var scene = SceneManager.LoadSceneAsync(scenename);
        scene.allowSceneActivation = false;
        _loader.SetActive(true);

        do
        {
            await Task.Delay(100);
            target = scene.progress;

        }
        while (scene.progress < 0.9f);

        await Task.Delay(1000);
        scene.allowSceneActivation = true;
        _loader.SetActive(false);
    }
    private void Update()
    {
        _image.fillAmount = Mathf.MoveTowards(_image.fillAmount, target, 3*Time.deltaTime);
    }
    */
}
