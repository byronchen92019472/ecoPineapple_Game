using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

public class BuildButtonScript : MonoBehaviour {

    public Button launchButton;
    public Button addTouristsButton;
    public Button removeTouristsButton;

    public Button fuelTabButton;
    public Button touristTabButton;

    public GameObject fuelDisplayPanel;
    public GameObject touristDisplayPanel;

    public Button fuelPart1;
    public Button fuelPart2;
    public Button fuelPart3;
    public Button fuelPart4;
    public Button fuelPart5;
    public Button fuelPart6;
    public Button fuelPart7;
    public Button fuelPart8;
    public Button fuelPart9;

    public Text fuelPurchaseText;
    public Button fuelPurchaseButton;

    public Button thrustPart1;
    public Button thrustPart2;
    public Button thrustPart3;
    public Button thrustPart4;

    public Text fuelDisplayText;
    public Text thrustDisplayText;
    public Text fuelEffiencyText;
    public Text moneyDisplayText;
    public Text touristDisplayText;

    public GameObject displayPanel;
    public Button closeDisplayButton;

    private Button selectedFuelButton;
    private float selectedFuelValue;
    private float selectedFuelPrice;

    public Button exitButton;


    public GameController gameController;

    void Start()
    {
        fuelDisplayText.text = "Max Fuel: " + gameController.ship.maxFuel.ToString();
        thrustDisplayText.text = "Max Thrust: " + gameController.ship.maxThrust.ToString();
        fuelEffiencyText.text = "Fuel Effiency: " + gameController.ship.fuelEfficiencyMultiplier.ToString() + "%";
        touristDisplayText.text = "Tourists: " + gameController.ship.tourists.ToString();
        fuelPurchaseButton.enabled = false;
        fuelTabButtonClick();
    }
	// Use this for initialization
	void OnEnable () {     
        launchButton.onClick.AddListener(() => launchButtonClick());
        addTouristsButton.onClick.AddListener(() => addTouristsClick());
        removeTouristsButton.onClick.AddListener(() => removeTouristsClick());

        closeDisplayButton.onClick.AddListener(() => closeDisplayClick());

        
        touristTabButton.onClick.AddListener(() => touristTabButtonClick());

        fuelTabButton.onClick.AddListener(() => fuelTabButtonClick());
        fuelPurchaseButton.onClick.AddListener(() => fuelPurchaseButtonClick());
        fuelPart1.onClick.AddListener(() => fuelButtonClick(fuelPart1));
        fuelPart2.onClick.AddListener(() => fuelButtonClick(fuelPart2));
        fuelPart3.onClick.AddListener(() => fuelButtonClick(fuelPart3));
        fuelPart4.onClick.AddListener(() => fuelButtonClick(fuelPart4));
        fuelPart5.onClick.AddListener(() => fuelButtonClick(fuelPart5));
        fuelPart6.onClick.AddListener(() => fuelButtonClick(fuelPart6));
        fuelPart7.onClick.AddListener(() => fuelButtonClick(fuelPart7));
        fuelPart8.onClick.AddListener(() => fuelButtonClick(fuelPart8));
        fuelPart9.onClick.AddListener(() => fuelButtonClick(fuelPart9));

        exitButton.onClick.AddListener(() => exitButtonClick());
        //LoadButton();
        Debug.Log("Load Button");
	}

    private Save CreateSaveGameObject(){
        Save save = new Save();
        save.fuelPart1 = !fuelPart1.interactable;
        save.fuelPart2 = !fuelPart2.interactable;
        save.fuelPart3 = !fuelPart3.interactable;
        save.fuelPart4 = !fuelPart4.interactable;
        save.fuelPart5 = !fuelPart5.interactable;
        save.fuelPart6 = !fuelPart6.interactable;
        save.fuelPart7 = !fuelPart7.interactable;
        save.fuelPart8 = !fuelPart8.interactable;
        save.fuelPart9 = !fuelPart9.interactable;
        Debug.Log(save.fuelPart1);
        Debug.Log(fuelPart1.interactable);
        return save;
    }
    public void SaveButton(){
        Save save = CreateSaveGameObject();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();
    }
    public void LoadButton(){
        if(File.Exists(Application.persistentDataPath + "/gamesave.save")){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();
            fuelPart1.interactable = !save.fuelPart1;
            fuelPart2.interactable = !save.fuelPart2;
            fuelPart3.interactable = !save.fuelPart3;
            fuelPart4.interactable = !save.fuelPart4;
            fuelPart5.interactable = !save.fuelPart5;
            fuelPart6.interactable = !save.fuelPart6;
            fuelPart7.interactable = !save.fuelPart7;
            fuelPart8.interactable = !save.fuelPart8;
            fuelPart9.interactable = !save.fuelPart9;
        }else{
            Debug.Log("No Save File");
        }
    }
    void exitButtonClick()
    {
        SceneManager.LoadSceneAsync("StartScreen");
    }
    
    void fuelTabButtonClick()
    {
        fuelDisplayPanel.SetActive(true);
        touristDisplayPanel.SetActive(false);
    }

    void touristTabButtonClick()
    {
        fuelDisplayPanel.SetActive(false);
        touristDisplayPanel.SetActive(true);
    }

    void closeDisplayClick()
    {
        displayPanel.SetActive(false);
    }

    void addTouristsClick()
    {
        
        gameController.ship.tourists += 1;
        updateText();
    }

    void removeTouristsClick()
    {
        if (gameController.ship.tourists > 0)
        {
            gameController.ship.tourists -= 1;
        }
        updateText();
    }

    void setFuelPurchase(Button button, string name, float price, float fuelvalue)
    {
        fuelPurchaseText.text = "+" + fuelvalue + " Fuel - $" + price.ToString();
        selectedFuelButton = button;
        selectedFuelValue = fuelvalue;
        selectedFuelPrice = price;
    }

    void fuelPurchaseButtonClick()
    {
        if (gameController.player.money >= selectedFuelPrice && selectedFuelButton.interactable)
        {
            gameController.ship.maxFuel += selectedFuelValue;
            gameController.player.money -= selectedFuelPrice;
            selectedFuelButton.interactable = false;
        }
        updateText();
        //SaveButton();
        
    }

    void fuelButtonClick(Button button)
    {
        if (button == fuelPart1)
        {
            setFuelPurchase(button, "Fuel 1", 5, 100);
        }
        if (button == fuelPart2)
        {
            setFuelPurchase(button, "Fuel 2", 20, 200);
        }
        if (button == fuelPart3)
        {
            setFuelPurchase(button, "Fuel 3", 50, 300);
        }
        if (button == fuelPart4)
        {
            setFuelPurchase(button, "Fuel 4", 100, 400);
        }
        if (button == fuelPart5)
        {
            setFuelPurchase(button, "Fuel 5", 200, 500);
        }
        if (button == fuelPart6)
        {
            setFuelPurchase(button, "Fuel 6", 400, 750);
        }
        if (button == fuelPart7)
        {
            setFuelPurchase(button, "Fuel 7", 600, 1000);
        }
        if (button == fuelPart8)
        {
            setFuelPurchase(button, "Fuel 8", 800, 1500);
        }
        if (button == fuelPart9)
        {
            setFuelPurchase(button, "Fuel 9", 1000, 2000);
        }
        fuelPurchaseButton.enabled = true;
        updateText();
        
    }

    void updateText()
    {
        fuelDisplayText.text = "Max Fuel: " + gameController.ship.maxFuel.ToString();
        thrustDisplayText.text = "Max Thrust: " + gameController.ship.maxThrust.ToString();
        fuelEffiencyText.text = "Fuel Effiency: " + gameController.ship.fuelEfficiencyMultiplier.ToString() + "%";
        moneyDisplayText.text = "x " + gameController.player.money.ToString();
        touristDisplayText.text = "Tourists: " + gameController.ship.tourists.ToString();
    }

    void launchButtonClick()
    {
        gameController.initLaunchPhase();
    }
}
