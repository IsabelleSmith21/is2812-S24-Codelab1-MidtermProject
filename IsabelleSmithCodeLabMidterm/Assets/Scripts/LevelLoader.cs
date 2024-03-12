using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    int currentLevel = 0; // Variable to keep track of the current level

    GameObject level;  // Reference to the GameObject containing the loaded level objects

    public int CurrentLevel // Property to access and set the current level
    {
        get
        {
            return currentLevel;
        }
        
        set
        {
            currentLevel = value;
            // Load the level when the current level changes
            LoadLevel();
        }
    }

    string FILE_PATH;  // File path to load levels from
    
    // Singleton instance of the LevelLoader
    public static LevelLoader instance;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this; // Set the singleton instance to this LevelLoader
        
        FILE_PATH = Application.dataPath + "/Levels/LevelNum.txt";        // Construct the file path for loading levels
        
        LoadLevel(); // Load the initial level
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadLevel()   // Method to load a level from a text file
    {
        Destroy(level); // Destroy any previously loaded level
        
        level = new GameObject("Level Objects");    // Create a new GameObject to contain the level objects
        
        //Get the lines from the file
        string[] lines = File.ReadAllLines(
            FILE_PATH.Replace("Num", currentLevel + ""));

        for (int yLevelPos = 0; yLevelPos < lines.Length; yLevelPos++)
        {

            //Get a single line
            string line = lines[yLevelPos].ToUpper();

            //Turn line into a char array
            char[] characters = line.ToCharArray();

            for (int xLevelPos = 0; xLevelPos < characters.Length; xLevelPos++)  // Iterate through each character in the line
            {

                //get the first character
                char c = characters[xLevelPos];
                

                GameObject newObject = null;  // Variable to hold the instantiated GameObject

                switch (c)  // Instantiate a GameObject based on the character
                {
                    case 'W': //if the character is a 'W'
                        newObject = //Add a wall to our scene
                            Instantiate(Resources.Load<GameObject>("Prefabs/WallHorizontal"));
                        break;
                    case 'P': 
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/Heart"));
                        break;
                    case 'S':
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/WallVertical"));
                        break;
                    case 'R':
                        newObject =
                            Instantiate(Resources.Load<GameObject>("Prefabs/Wrinkles"));
                        break;
                    case 'A': 
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/A"));
                        break;
                    case 'B': 
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/And"));
                        break;     
                    case 'C':
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/Are"));
                        break;
                    case 'D':
                        newObject =
                            Instantiate(Resources.Load<GameObject>("Prefabs/Earlobes"));
                        break;
                    case 'E': 
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/Exuberant"));
                        break;
                    case 'F': 
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/Eyes"));
                        break;     
                    case 'G': 
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/Feet"));
                        break;
                    case 'H': 
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/Fresh"));
                        break;
                    case 'I':
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/Knees"));
                        break;
                    case 'J':
                        newObject =
                            Instantiate(Resources.Load<GameObject>("Prefabs/Kooky"));
                        break;
                    case 'K': 
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/Lovely"));
                        break;
                    case 'L': 
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/Lush"));
                        break;     
                    case 'M':
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/Made"));
                        break;
                    case 'N':
                        newObject =
                            Instantiate(Resources.Load<GameObject>("Prefabs/Most"));
                        break;
                    case 'O': 
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/Of"));
                        break;
                    case 'Q': 
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/Stench"));
                        break;            
                     case 'T': 
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/Scent"));
                        break;
                    case 'U': 
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/Smile"));
                        break;
                    case 'V':
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/Superb"));
                        break;
                    case 'X': 
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/Sweet"));
                        break;
                    case 'Y': 
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/The"));
                        break;     
                    case 'Z':
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/Thrilling"));
                        break;
                    case '1':
                        newObject =
                            Instantiate(Resources.Load<GameObject>("Prefabs/Veins"));
                        break;
                    case '2': 
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/What"));
                        break;
                    case '3': 
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/Wow"));
                        break;     
                    case '4': 
                        newObject = 
                            Instantiate(Resources.Load<GameObject>("Prefabs/You"));
                        break;
                    case '5': 
                        newObject = 
                              Instantiate(Resources.Load<GameObject>("Prefabs/Glowing"));
                             break;  
                    case '6': 
                           newObject = 
                                  Instantiate(Resources.Load<GameObject>("Prefabs/Colossal"));
                             break;    
                    case '7': 
                            newObject = 
                                Instantiate(Resources.Load<GameObject>("Prefabs/Mole"));
                             break;     
                    case '8': 
                            newObject = 
                                Instantiate(Resources.Load<GameObject>("Prefabs/Nostrils"));
                            break;    
                    case '9': 
                            newObject = 
                              Instantiate(Resources.Load<GameObject>("Prefabs/Sleek"));
                        break;    
                    default:
                        break;
                }

                if (newObject != null)   // If a GameObject was instantiated, set its parent and position
                {
                    newObject.transform.parent = level.transform;
                    //Give it a position based on where it was in the ASCII file
                    newObject.transform.position = new Vector2(xLevelPos, -yLevelPos);
                }
            }
        }
    }
}