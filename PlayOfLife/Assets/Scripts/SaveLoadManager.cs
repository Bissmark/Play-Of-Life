﻿/** 
 *	\brief Manager class which is responsible for Save/Load system implementation
 *	\author Timur Anoshechkin
 *	\copyright PenguinWolf
 */

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


public class SaveLoadManager : Manager<SaveLoadManager>
{

    // exposed variables
    [SerializeField]
    private string _screenShotFileName = string.Empty;

    [SerializeField]
    private bool _saveGUI = false;

    // the extensions we only want to load
    private List<string> _imageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };

    // hidden variables
    private string _screenshotFolder;
    private string _screenshotSaveSceneFolder;


    // Must call as per manager class
    protected override void Awake()
    {
        base.Awake();

        // Create path for the screenshot
        {
            if ( Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.OSXWebPlayer )
            {
                // need to do server base solution
            }
            else
            {
                _screenshotFolder = Application.persistentDataPath + "/Screenshots/";
            }

            System.IO.Directory.CreateDirectory( _screenshotFolder );
        }

        // Create path for the save scene screenshot
        {
            if ( Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.OSXWebPlayer )
            {
                // need to do server base solution
            }
            else
            {
                _screenshotSaveSceneFolder = Application.persistentDataPath + "/POL_Screenshot_Save/";
            }

            System.IO.Directory.CreateDirectory( _screenshotSaveSceneFolder );
        }

    }

    // Save/Load screenshot functions
    public string SaveScreenshot()
    {
        return SaveScreenshot( this._screenshotFolder, this._screenShotFileName );
    }

    public string SaveScreenshot( string folder, string fileName )
    {
        string currentDT = DateTime.Now.ToString( "yyyy-MM-dd HH.mm.ss" );
        string filePath = folder + fileName + currentDT + ".png";

        CaptureScreenshot( filePath, _saveGUI );

        return filePath;
    }

    private void CaptureScreenshot(string filePath, bool saveGUI)
    {
        if ( saveGUI )
        {
            Application.CaptureScreenshot( filePath );
        }
        else
        {
            // this was copied from http://answers.unity3d.com/questions/22954/how-to-save-a-picture-take-screenshot-from-a-camer.html
            // in order to save image without UI
            {
                int resWidth = Screen.width;
                int resHeight = Screen.height;

                RenderTexture rt = new RenderTexture( resWidth, resHeight, 24 );
                Camera.main.targetTexture = rt;
                Texture2D screenShot = new Texture2D( resWidth, resHeight, TextureFormat.RGB24, false );
                Camera.main.Render();
                RenderTexture.active = rt;
                screenShot.ReadPixels( new Rect( 0, 0, resWidth, resHeight ), 0, 0 );
                Camera.main.targetTexture = null;
                RenderTexture.active = null; // JC: added to avoid errors
                Destroy( rt );
                byte[] bytes = screenShot.EncodeToPNG();
                System.IO.File.WriteAllBytes( filePath, bytes );
            }
        }
    }

    public string[] GetScreenshotNames()
	{
        string[] files = Directory.GetFiles( _screenshotFolder);

        // filtering out non image files
        List<string> filteredFiles = new List<string>();
        for ( int i = 0; i < files.Length; i++ )
        {
            if ( _imageExtensions.Contains( Path.GetExtension( files[i] ).ToUpperInvariant() ) )
            {
                filteredFiles.Add( files[ i ] );
            }
        }

        return filteredFiles.ToArray();
	}

    public List<Texture2D> GetSavedImages()
	{
		string[] filenames = GetScreenshotNames();

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

    public void SaveScene(string name)
    {
        DoSaveScene( string.Empty );
    }

    //Save/Load scene functions
    private void DoSaveScene( string saveName )
    {
        SaveEntry save_game = new SaveEntry();

        // save the filepath of the screenshot
        save_game._screenshot_filename = SaveScreenshot( _screenshotSaveSceneFolder, "screenshot" );
        Debug.Log( save_game._screenshot_filename );

        // saving objects in the scene
        GameObject[] objects = GameObject.FindGameObjectsWithTag( "Object" );
        foreach ( GameObject go in objects )
        {
            ObjectSaveEntry entry = new ObjectSaveEntry();

            entry._name = go.name;
            entry._position = go.transform.position;
            entry._scale = go.transform.localScale;
            entry._rotation = go.transform.rotation;
            save_game._objects.Add( entry );
        }

        // save time
        save_game._saveTime = System.DateTime.Now;

        // save name
        save_game._saveName = saveName;

        // serializing into xml
        var serializer = new XmlSerializer( typeof( SaveEntry ) );
        TextWriter WriteFileStream = new StreamWriter( Application.persistentDataPath + "/" + "Sandplay_Save" + System.DateTime.Now.ToFileTime() );
        serializer.Serialize( WriteFileStream, save_game );
    }

    // Returns all the save name files
    private string[] GetSaveGameNames()
    {
        return Directory.GetFiles( Application.persistentDataPath, "Sandplay_Save*" );
    }

    // Deserialize XML into SaveEntry
    private SaveEntry DeserializeSaveEntry( string filePath )
    {
        XmlSerializer serializer = new XmlSerializer( typeof( SaveEntry ) );

        using ( StreamReader reader = new StreamReader( filePath ) )
        {
            return ( SaveEntry )serializer.Deserialize( reader );
        }
    }

    // returns save entry specified by file path
    public SaveEntry GetSaveEntry(string filePath)
    {
        return DeserializeSaveEntry( filePath );
    }

    // returns all save entries
    public List<SaveEntry> GetSaveEntries()
    {
        string[] savedFilePaths = GetSaveGameNames();
        List<SaveEntry> saveEntries = new List<SaveEntry>();
        for ( int i = 0; i < savedFilePaths.Length; i++ )
        {
            saveEntries.Add( GetSaveEntry( savedFilePaths[ i ] ) );
        }
        return saveEntries;
    }

    void LoadScene()
    {
        /*
        foreach ( ObjectSaveEntry ose in _save_game._objects )
        {
            GameObject go = ( GameObject )Instantiate( _object_spawner.FindPanelPrefabByName( ose._name ) );
            go.name = ose._name;
            go.GetComponent<Rigidbody>().isKinematic = true;
            go.transform.position = ose._position;
            go.transform.localScale = ose._scale;
            go.transform.rotation = ose._rotation;
            go.transform.parent = _object_spawner.transform;
        }
         */
    }

    // Must call as per manager class
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}

//class for each object stored in the sandbox
public struct ObjectSaveEntry
{
    public string _name; /* name of the prefab type */

    /* transform data */
    public Vector3 _position;
    public Vector3 _scale;
    public Quaternion _rotation;
};


public class SaveEntry
{
    //the objects along with the sandbox
    public List<ObjectSaveEntry> _objects = new List<ObjectSaveEntry>();
    //take a screenshot for the load screen
    public string _screenshot_filename;
    public string _saveName;
    public System.DateTime _saveTime;
};
