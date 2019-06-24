using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public AudioSource collect;
    public Canvas dialogue;
    public Canvas stats;

    public GameObject player;
    public GameObject child;
    public GameObject adult;
    public GameObject old;

    public GameObject explosion;

    private Dictionary<string, NPC> dictionary;
    public PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        //Don't show dialogue canvas
        DisableCanvas(dialogue);

        // Build up the NPC dictionary
        buildNPCDictionary();

        // Create the player stats
        playerStats = new PlayerStats();
        updateStatBar();

        //Do not show explosion sprites and animation
        explosion.SetActive(false);
        explosion.GetComponentInChildren<Animator>().enabled = false;

        Debug.Log(SceneManager.GetActiveScene().name);

        if (SceneManager.GetActiveScene().name == "Game")
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void Update()
    {

        if (SceneManager.GetActiveScene().name != "Game" && Input.GetKeyUp(KeyCode.Escape))
        {
            quitGame();
        }
      
    }
    // Obtains a NPC object by its given tag
    public NPC getNPCByTag(string tag)
    {
        return dictionary[tag];
    }

    public void updateStatBar()
    {
        List<string> keyList = new List<string>(playerStats.stats.Keys);
        foreach (string key in keyList)
        {
            // Update the slider
            int statVal = playerStats.getStat(key);
            stats.transform.Find(key).GetComponent<Slider>().value = statVal;
        }
    }

    public void EnableCanvas(Canvas canvasName)
    {
        canvasName.GetComponent<Canvas>().enabled = true;
    }
    public void DisableCanvas(Canvas canvasName)
    {
        canvasName.GetComponent<Canvas>().enabled = false;
    }

    public void ChangeCharacter(GameObject characterToHide, GameObject characterToShow, String replaceWith)
    {
        //Show explosion
        StartCoroutine(Explode());
        
        //We want to wait with the sprites a short while so we cant see the sudden change 
        StartCoroutine(ChangeCharacterRoutine(characterToHide, characterToShow));
        
        Animator animator = player.GetComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load(replaceWith) as RuntimeAnimatorController;
    }
    IEnumerator Explode()
    {
        explosion.SetActive(true);
        explosion.GetComponentInChildren<Animator>().enabled = true;
        yield return new WaitForSeconds(2);
        explosion.SetActive(false);
        explosion.GetComponentInChildren<Animator>().enabled = false;
    }
    IEnumerator ChangeCharacterRoutine(GameObject charHide, GameObject CharShow)
    {
        yield return new WaitForSeconds(0.1f);
        charHide.SetActive(false);
        CharShow.SetActive(true);
    }
    public void loadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void loadThisScene(string sceneName)
    {
        //Load blabla
        SceneManager.LoadScene(sceneName);
    }
    public void quitGame()
    {
        Application.Quit();
    }

    public void buildNPCDictionary()
    {
        // Create the options and what stat they add or remove points from if chosen
        Tuple<string, string, int> dadOption1 = Tuple.Create("1. Don’t read the book and go play with Don", "knowledge", -1);
        Tuple<string, string, int> dadOption2 = Tuple.Create("2. Read the book dad gave you", "knowledge", 2);
        Tuple<string, string, int> dadOption3 = Tuple.Create("3. Tell dad that you don’t like the book", "independence", 2);

        Tuple<string, string, int> kennethOption1 = Tuple.Create("1. Ignore Kenneth and mind your own business", "friendship", -1);
        Tuple<string, string, int> kennethOption2 = Tuple.Create("2. Stand up to Kenneth and help Molly", "friendship", 2);
        Tuple<string, string, int> kennethOption3 = Tuple.Create("3. Not wanting to become Kenneth’s victim, laugh at Molly", "friendship", -2);

        Tuple<string, string, int> DonOption1 = Tuple.Create("1. Tell Don you don't really care", "ambition", -1);
        Tuple<string, string, int> DonOption2 = Tuple.Create("2. Read the book together with Don", "knowledge", 2);
        Tuple<string, string, int> DonOption3 = Tuple.Create("3. Go play with Don", "friendship", 1);

        Tuple<string, string, int> TeacherOption1 = Tuple.Create("1. Thank the teacher for her teaching", "friendship", 1);
        Tuple<string, string, int> TeacherOption2 = Tuple.Create("2. Tell the teacher you will study a lot", "knowledge", 1);
        Tuple<string, string, int> TeacherOption3 = Tuple.Create("3. Tell her you will become the next Elon Musk", "ambition", 1);

        Tuple<string, string, int> roseOption1 = Tuple.Create("1. Tell her no, you have an important exam tomorrow", "knowledge", 1);
        Tuple<string, string, int> roseOption2 = Tuple.Create("2. Go hang out with Rose", "friendship", 1);
        Tuple<string, string, int> roseOption3 = Tuple.Create("3. Tell Rose you have no time for such nonsense", "independence", 1);

        Tuple<string, string, int> momOption1 = Tuple.Create("1. Tell mom everything is fine, you can handle it yourself", "independence", 2);
        Tuple<string, string, int> momOption2 = Tuple.Create("2. Ask her to help you study", "knowledge", 1);
        Tuple<string, string, int> momOption3 = Tuple.Create("3. Tell her about the new project you are working on", "ambition", 1);

        Tuple<string, string, int> bossOption1 = Tuple.Create("1. Quit your job to work on your own project", "independence", 1);
        Tuple<string, string, int> bossOption2 = Tuple.Create("2. Finish working on the report", "knowledge", 2);
        Tuple<string, string, int> bossOption3 = Tuple.Create("3. Tell your boss you will make your project successful", "ambition", 1);

        Tuple<string, string, int> siraOption1 = Tuple.Create("1. Tell Sira that your project will be launched shortly", "independence", 1);
        Tuple<string, string, int> siraOption2 = Tuple.Create("2. Drop your project", "ambition", -3);
        Tuple<string, string, int> siraOption3 = Tuple.Create("3. Tell her not to worry, everything will be fine", "friendship", -2);

        Tuple<string, string, int> johnOption1 = Tuple.Create("1. Tell John that he should always follow his dreams", "ambition", 1);
        Tuple<string, string, int> johnOption2 = Tuple.Create("2. Tell him that other people's joy is the most important", "friendship", 1);
        Tuple<string, string, int> johnOption3 = Tuple.Create("3. Ask John what he thinks himself", "independence", 1);

        Tuple<string, string, int> deathOption1 = Tuple.Create("1. Tell him that you have no regrets ", "independence", 1);
        Tuple<string, string, int> deathOption2 = Tuple.Create("2. Tell him that you wanted to learn more", "knowledge", 1);
        Tuple<string, string, int> deathOption3 = Tuple.Create("3. Tell him you didn't spend enough time with others", "friendship", 1);
        // Create the NPCS
        dictionary = new Dictionary<string, NPC>()
        {
            {"Dad", new NPC("DAD", "Jared, don’t forget to read the astronomy book that I gave you today.", dadOption1, dadOption2, dadOption3)},
            {"Kenneth", new NPC("KENNETH", "Look at Molly crying because she ripped her dress, she is such a loser.", kennethOption1, kennethOption2, kennethOption3)},
            {"Don", new NPC("DON", "Hey Jared, look at this awesome book I loaned from the library. Did you know that there are lot of galaxies? ", DonOption1, DonOption2, DonOption3)},
            {"Teacher", new NPC("TEACHER", "Jared, I wish you all the best on your new school. You all grow up so fast! May you become a successful person.", TeacherOption1, TeacherOption2, TeacherOption3)},
            {"Rose", new NPC("ROSE", "Do you want to hang out together?", roseOption1, roseOption2, roseOption3)},
            {"Mom", new NPC("MOM", "Is everything alright dear? Do you need help with something?", momOption1, momOption2, momOption3)},
            {"Boss", new NPC("BOSS", "Jared, I really need the report by the end of the day. You are getting to sidetracked with your own project, it will never be successful. Stop dreaming, start working.", bossOption1, bossOption2, bossOption3)},
            {"Sira", new NPC("SIRA", "Honey your personal project is still not taking off and you work to much. I am getting worried.", siraOption1, siraOption2, siraOption3)},
            {"John", new NPC("JOHN", "Dad, we need to do a project for school about what is most important in life. What do you think is the most important?", johnOption1, johnOption2, johnOption3)},
            {"Death", new NPC("GRIM REAPER", "Jared, your time has come. Do you have any regrets in life?", deathOption1, deathOption2, deathOption3)}
        };
    }
}

