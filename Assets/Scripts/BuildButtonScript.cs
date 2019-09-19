using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildButtonScript : MonoBehaviour {

    public Button launchButton;

    public Button fuelPart1;
    public Button fuelPart2;
    public Button fuelPart3;
    public Button fuelPart4;

    public Text fuelDisplayText;
    public Text thrustDisplayText;
    public Text fuelEffiencyText;
    public Text moneyDisplayText;


    public GameController gameController;

    void Start()
    {
        fuelDisplayText.text = "Max Fuel: " + gameController.ship.maxFuel.ToString();
        thrustDisplayText.text = "Max Thrust: " + gameController.ship.maxThrust.ToString();
        fuelEffiencyText.text = "Fuel Effiency: " + gameController.ship.fuelEfficiencyMultiplier.ToString() + "%";
    }
	// Use this for initialization
	void OnEnable () {     
        launchButton.onClick.AddListener(() => launchButtonClick());
        fuelPart1.onClick.AddListener(() => fuelButtonClick(fuelPart1));
        fuelPart2.onClick.AddListener(() => fuelButtonClick(fuelPart2));
        fuelPart3.onClick.AddListener(() => fuelButtonClick(fuelPart3));
        fuelPart4.onClick.AddListener(() => fuelButtonClick(fuelPart4));
	}

    void fuelButtonClick(Button button)
    {
        if (button == fuelPart1)
        {
            if (gameController.player.money >= 1000)
            {
                gameController.ship.maxFuel = 250;
                gameController.player.money -= 1000;

            }
        }
        if (button == fuelPart2)
        {
            if (gameController.player.money >= 50000)
            {
                gameController.ship.maxFuel = 500;
                gameController.player.money -= 50000;
            }
        }
        if (button == fuelPart3)
        {
            if (gameController.player.money >= 250000)
            {
                gameController.ship.maxFuel = 1000;
                gameController.player.money -= 250000;
            }
        }
        if (button == fuelPart4)
        {
            if (gameController.player.money >= 4000000)
            {
                gameController.ship.maxFuel = 2000;
                gameController.player.money -= 4000000;
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
    }

    void launchButtonClick()
    {
        gameController.initLaunchPhase();
    }
}
