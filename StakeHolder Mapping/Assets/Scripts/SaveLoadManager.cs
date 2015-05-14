/** 
 *	\brief Manager class which is responsible for Save/Load system implementation
 *	\author Timur Anoshechkin
 *	\copyright PenguinWolf
 */

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class SaveLoadManager : Manager<SaveLoadManager>
{

    // exposed variables
    [SerializeField]
    private string _screenShotFileName = string.Empty;

    //  hidden varibales
    private string folder;

    // the extensions we only want to load
    private List<string> _imageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };


    // Must call as per manager class
    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        // Create path for the screenshot
        {
            if ( Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.OSXWebPlayer )
            {
                folder = Application.dataPath + "/StreamingAssets/";//"/../Screenshots"; // this will do nothing StreamingAssets don't work on the web
            }
            else if ( Application.platform == RuntimePlatform.WindowsPlayer )
            {
                folder = Application.persistentDataPath + "/Screenshots/";
            }
            else
            {
                folder = Application.dataPath + "/Screenshots/";
            }

            System.IO.Directory.CreateDirectory( folder );
        }
       
    }

    public void TakeScreenShot()
    {
        TakeScreenShot( this.folder, this._screenShotFileName );
    }

    public string TakeScreenShot( string folder, string fileName )
    {
        return SaveScreenshot( folder, fileName );
    }

    private string SaveScreenshot( string folder, string fileName )
    {
        string currentDT = DateTime.Now.ToString( "yyyy-MM-dd HH.mm.ss" );
        string filePath = folder + fileName + currentDT + ".png";

        Application.CaptureScreenshot( filePath );
        // TODO: this method is not going to work for the web.
        // Task 1: Capture screenshot using RenderTexture(web only)
        // Task 2: Upload it to the server(web only)

        return filePath;
    }

    public string[] GetScreenShotNames()
	{
		if (Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.OSXWebPlayer) 
		{
			return Directory.GetFiles (Application.dataPath + "/StreamingAssets/"); // this never is going to happen
		}
        else if ( Application.platform == RuntimePlatform.WindowsPlayer )
        {
            return Directory.GetFiles( Application.persistentDataPath + "/Screenshots" );
        }
		else 
		{
			return Directory.GetFiles (Application.dataPath + "/Screenshots");
		}
	}

    public List<Texture2D> GetSavedImages()
	{
		string[] filenames = GetScreenShotNames();

		if (filenames.Length == 0)
			return null;

        List<Texture2D> savedImages = new List<Texture2D>();

		foreach (string s in filenames)
		{
            Texture2D texture = GetSavedImage( s );
            if ( texture != null )
            {
                savedImages.Add( texture );
            }
		}

        return savedImages;
	}
    
    public Texture2D GetSavedImage(string fileName)
    {
        // skipping on non image files
        if ( !_imageExtensions.Contains( Path.GetExtension( fileName ).ToUpperInvariant() ) )
        {
            return null;
        }

        FileStream fs = new FileStream( fileName, FileMode.Open, FileAccess.Read );
        byte[] imageData = new byte[ fs.Length ];
        fs.Read( imageData, 0, ( int )fs.Length );

        Texture2D texture = new Texture2D( 4, 4 );
        texture.LoadImage( imageData );
        fs.Close();
        return texture;
    }



    // Must call as per manager class
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
