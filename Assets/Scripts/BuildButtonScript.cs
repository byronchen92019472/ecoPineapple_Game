using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        thrustPart1.onClick.AddListener(() => thrustButtonClick(thrustPart1));
        thrustPart2.onClick.AddListener(() => thrustButtonClick(thrustPart2));
        thrustPart3.onClick.AddListener(() => thrustButtonClick(thrustPart3));
        thrustPart4.onClick.AddListener(() => thrustButtonClick(thrustPart4));

        exitButton.onClick.AddListener(() => exitButtonClick());
	}
    void exitButtonClick()
    {
        SceneManager.LoadScene("Start Screen");
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
        fuelPurchaseText.text = name + " - $" + price.ToString();
        selectedFuelButton = button;
        selectedFuelValue = fuelvalue;
        selectedFuelPrice = price;
    }

    void fuelPurchaseButtonClick()
    {
        if (gameController.player.money >= selectedFuelPrice)
        {
            gameController.ship.maxFuel += selectedFuelValue;
            gameController.player.money -= selectedFuelPrice;
            selectedFuelButton.interactable = false;
        }
        updateText();
        
    }

    void thrustButtonClick(Button button)
    {
        if (button == thrustPart1)
        {
            if (gameController.player.money >= 1000)
            {
                gameController.ship.maxThrust = 30;
                gameController.player.money -= 1000;
                thrustPart1.interactable = false;

            }
        }
        if (button == thrustPart2)
        {
            if (gameController.player.money >= 50000)
            {
                gameController.ship.maxThrust = 45;
                gameController.player.money -= 50000;
                thrustPart2.interactable = false;

            }
        }
        if (button == thrustPart3)
        {
            if (gameController.player.money >= 250000)
            {
                gameController.ship.maxThrust = 60;
                gameController.player.money -= 250000;
                thrustPart3.interactable = false;

            }
        }
        if (button == thrustPart4)
        {
            if (gameController.player.money >= 4000000)
            {
                gameController.ship.maxThrust = 75;
                gameController.player.money -= 4000000;
                thrustPart4.interactable = false;

            }
        }
        updateText();
    }

    void fuelButtonClick(Button button)
    {
        if (button == fuelPart1)
        {
            setFuelPurchase(button, "Fuel 1", 1, 250);
        }
        if (button == fuelPart2)
        {
            setFuelPurchase(button, "Fuel 2", 3, 300);
        }
        if (button == fuelPart3)
        {
            setFuelPurchase(button, "Fuel 3", 7, 400);
        }
        if (button == fuelPart4)
        {
            setFuelPurchase(button, "Fuel 4", 15, 800);
        }
        if (button == fuelPart5)
        {
            setFuelPurchase(button, "Fuel 5", 30, 2000);
        }
        if (button == fuelPart6)
        {
            setFuelPurchase(button, "Fuel 6", 75, 5000);
        }
        if (button == fuelPart7)
        {
            setFuelPurchase(button, "Fuel 7", 200, 15000);
        }
        if (button == fuelPart8)
        {
            setFuelPurchase(button, "Fuel 8", 1000, 30000);
        }
        if (button == fuelPart9)
        {
            setFuelPurchase(button, "Fuel 9", 10000, 100000);
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
