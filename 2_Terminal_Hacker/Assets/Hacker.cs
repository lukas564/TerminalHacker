using UnityEngine;

public class Hacker : MonoBehaviour {

    // Game configuration data
    const string menuHint = "You may type menu at any time.";
    string[] level1Password = {"books", "shelf", "password", "building", "borrow", "font"};
    string[] level2Password = {"prisoner", "handcuffs", "holster", "uniform", "arrest"};
    string[] level3Password = {"spaceship", "telescope", "astronaut", "univers", "exploration", "satellite" };

    // Game state
    int level;
    enum Screen { MainMenu, Password, Win};
    Screen currentScreen = Screen.MainMenu;
    string password;

    // Use this for initialization
    void Start () {
       
        ShowMainMenu();
    }

    void Update()
    {
        int index = Random.Range(0, level1Password.Length);
        print(index);
    }

    void ShowMainMenu()
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("What would you like to hack into?\n");
        Terminal.WriteLine("Press 1 for the local library");
        Terminal.WriteLine("Press 2 for the police station");
        Terminal.WriteLine("Press 3 for NASA");
        Terminal.WriteLine("\nEnter your selection: ");
    }

    void OnUserInput(string input)
    {
        if (input == "menu")
        {
            ShowMainMenu();
        }
        else if (input == "quit" || input == "close" || input == "exit")
        {
            Application.Quit();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }

    }


    void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            AskForPassword();
        }        
        else if (input == "007")
        {
            Terminal.WriteLine("Please select a level Mr. Bond!");
        }
        else
        {
            Terminal.WriteLine("Please chooce valid level: ");
            Terminal.WriteLine(menuHint);
        }
    }

    void AskForPassword()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SetRandomPassword();
        Terminal.WriteLine("Enter your password, hint: " + password.Anagram());
        Terminal.WriteLine(menuHint);
    }

    void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = level1Password[Random.Range(0, level1Password.Length)];
                break;
            case 2:
                password = level2Password[Random.Range(0, level2Password.Length)];
                break;
            case 3:
                password = level3Password[Random.Range(0, level3Password.Length)];
                break;
            default:
                Debug.LogError("Invalid level number");
                break;
        }
    }

    void CheckPassword(string input)
    {
        if (input == password)
        {
            DisplayWinScreen();
        }
        else
        {
            AskForPassword();
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
        Terminal.WriteLine(menuHint);
    }

    void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Have a book...");
                Terminal.WriteLine(@"
    ________
   /       //
  /       //   
 /_______//
(_______)/
"
                );
                break;
            case 2:
                Terminal.WriteLine("You got the prison key!");
                Terminal.WriteLine(@"
 ____
/ o  \__________
\____/----=' = '
"
                );
                Terminal.WriteLine("Play again for a greater challange.");
                break;
            case 3:
                Terminal.WriteLine(@"
                 _____
|\   |    /\    /     \    /\
| \  |   /  \   \_____    /  \
|  \ |  /____\        \  /____\
|   \| /      \ \_____/ /      \
"
                );
                Terminal.WriteLine("Welcome to NASA's internal system!");
                break;
            default:
                Debug.LogError("Invalid level reached");
                break;
        }
        
    }
}
