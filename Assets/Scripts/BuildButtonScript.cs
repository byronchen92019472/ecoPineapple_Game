using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildButtonScript : MonoBehaviour {

    public Button launchButton;
    public Button addTouristsButton;
    public Button removeTouristsButton;

    public Button fuelPart1;
    public Button fuelPart2;
    public Button fuelPart3;
    public Button fuelPart4;

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


    public GameController gameController;

    void Start()
    {
        fuelDisplayText.text = "Max Fuel: " + gameController.ship.maxFuel.ToString();
        thrustDisplayText.text = "Max Thrust: " + gameController.ship.maxThrust.ToString();
        fuelEffiencyText.text = "Fuel Effiency: " + gameController.ship.fuelEfficiencyMultiplier.ToString() + "%";
        touristDisplayText.text = "Tourists: " + gameController.ship.tourists.ToString();
    }
	// Use this for initialization
	void OnEnable () {     
        launchButton.onClick.AddListener(() => launchButtonClick());
        addTouristsButton.onClick.AddListener(() => addTouristsClick());
        removeTouristsButton.onClick.AddListener(() => removeTouristsClick());

        closeDisplayButton.onClick.AddListener(() => closeDisplayClick());


        fuelPart1.onClick.AddListener(() => fuelButtonClick(fuelPart1));
        fuelPart2.onClick.AddListener(() => fuelButtonClick(fuelPart2));
        fuelPart3.onClick.AddListener(() => fuelButtonClick(fuelPart3));
        fuelPart4.onClick.AddListener(() => fuelButtonClick(fuelPart4));

        thrustPart1.onClick.AddListener(() => thrustButtonClick(thrustPart1));
        thrustPart2.onClick.AddListener(() => thrustButtonClick(thrustPart2));
        thrustPart3.onClick.AddListener(() => thrustButtonClick(thrustPart3));
        thrustPart4.onClick.AddListener(() => thrustButtonClick(thrustPart4));
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
            if (gameController.player.money >= 1000)
            {
                gameController.ship.maxFuel = 250;
                gameController.player.money -= 1000;
                fuelPart1.interactable = false;
                gameController.ship.shipParts.shipFuel1.SetActive(true);

            }
        }
        if (button == fuelPart2)
        {
            if (gameController.player.money >= 50000)
            {
                gameController.ship.maxFuel = 500;
                gameController.player.money -= 50000;
                fuelPart2.interactable = false;
                gameController.ship.shipParts.shipFuel2.SetActive(true);
            }
        }
        if (button == fuelPart3)
        {
            if (gameController.player.money >= 250000)
            {
                gameController.ship.maxFuel = 1000;
                gameController.player.money -= 250000;
                fuelPart3.interactable = false;
            }
        }
        if (button == fuelPart4)
        {
            if (gameController.player.money >= 4000000)
            {
                gameController.ship.maxFuel = 2000;
                gameController.player.money -= 4000000;
                fuelPart4.interactable = false;
            }
        }
        updateText();
        
    }

    void updateText()
    {
        fuelDisplayText.text = "Max Fuel: " + gameController.ship.maxFuel.ToString();
        thrustDisplayText.text = "Max Thrust: " + gameController.ship.maxThrust.ToString();
        fuelEffiencyText.text = "Fuel Effiency: " + gameController.ship.fuelEfficiencyMultiplier.ToString() + "%";
        moneyDisplayText.text = "Money: " + gameController.player.money.ToString();
        touristDisplayText.text = "Tourists: " + gameController.ship.tourists.ToString();
    }

    void launchButtonClick()
    {
        gameController.initLaunchPhase();
    }
}
