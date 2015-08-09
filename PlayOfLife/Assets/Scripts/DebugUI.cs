/** 
 *	\brief Manager class which is responsible for Debug UI
 *	\author Timur Anoshechkin
 *	\copyright PenguinWolf
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DebugUI : Manager<DebugUI>
{
    // exposed variables
    [SerializeField]
    private GameObject _uIContainer = null;

    [SerializeField]
    private GameObject _imageEntriesContainer = null;

    [SerializeField]
    private GameObject _saveSceneEntriesContainer = null;

    [SerializeField]
    private GameObject _saveEntryTextUIEntryPrefab = null;

    [SerializeField]
    private RawImage _imagePreview = null;

    protected override void Awake()
    {
        base.Awake();

        // Error checking
        DebugUtils.Assert( _uIContainer != null, "Check that UIElementsContainer is hooked up" );
        DebugUtils.Assert( _imageEntriesContainer != null, "Check that ImageEntriesContainer is hooked up" );
        DebugUtils.Assert( _saveSceneEntriesContainer != null, "Check that SaveScreenEntriesContainer is hooked up" );
        DebugUtils.Assert( _saveEntryTextUIEntryPrefab != null, "Check that SaveImageEntry is hooked up" );
        DebugUtils.Assert( _imagePreview != null, "Check that ImagePreview is hooked up" );

        // Disable UIContainer on awake
        _uIContainer.SetActive( false );
        _imagePreview.gameObject.SetActive( false );
    }

    private void Update()
    {
        // Use "Tilde to enable/disbale debugUI"
        {
            if ( Input.GetKeyDown( KeyCode.BackQuote ) &&
                 _uIContainer.activeSelf == false )
            {
                _uIContainer.SetActive( true );
            }
            else if ( Input.GetKeyDown( KeyCode.BackQuote ) &&
                 _uIContainer.activeSelf == true )
            {
                _uIContainer.SetActive( false );
                DestroyEntries();
                _imagePreview.gameObject.SetActive( false );
            }
        }
    }

    // Button click functions
    public void OnSaveScreenshotButtonClick()
    {
        SaveLoadManager.Instance.SaveScreenshot();
    }
    public void OnLoadScreenshotButton()
    {
        GenerateEntries( SaveLoadManager.Instance.GetScreenshotNames(), _imageEntriesContainer );
    }
    public void OnSaveTextEntryClick( string imagePath )
    {
        LoadImage( imagePath );
    }
    public void OnSaveSceneButtonClick()
    {
        SaveLoadManager.Instance.SaveScene( string.Empty );
    }
    public void OnLoadSceneButtonClick()
    {
        List<SaveEntry> saveEntries = SaveLoadManager.Instance.GetSaveEntries();
        List<string> saveSceneImagePaths = new List<string>();
        foreach ( SaveEntry se in saveEntries )
        {
            saveSceneImagePaths.Add( se._screenshot_filename );
        }

        GenerateEntries( saveSceneImagePaths.ToArray(), _saveSceneEntriesContainer );
    }

    private void GenerateEntries( string[] entries, GameObject parent )
    {
        DestroyEntries();

        // instantiate prefabs
        for ( int i = 0; i < entries.Length; i++ )
        {
            GameObject go = Instantiate( _saveEntryTextUIEntryPrefab ) as GameObject;
            go.transform.SetParent( parent.transform );
            go.GetComponentInChildren<Text>().text = entries[ i ];
            go.transform.localPosition = new Vector3( 0f, ( -30 * ( i + 1 ) ), 0f );
        }
    }

    private void LoadImage( string imagePath )
    {
        Texture2D texture = SaveLoadManager.Instance.GetSavedImage( imagePath );
        _imagePreview.texture = texture;
        _imagePreview.gameObject.SetActive( true );
        _imagePreview.rectTransform.sizeDelta = new Vector2( texture.width, texture.height );
    }

    // Clean up functions

    //Destroy already instantiated prefabs inside image container
    private void DestroyEntries()
    {
        // destroy image entries
        foreach ( Transform child in _imageEntriesContainer.transform )
        {
            if ( child != _imageEntriesContainer.transform )
            {
                Destroy( child.gameObject );
            }
        }

        // destroy saveGame entries
        foreach ( Transform child in _saveSceneEntriesContainer.transform )
        {
            if ( child != _saveSceneEntriesContainer.transform )
            {
                Destroy( child.gameObject );
            }
        }
    }


    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
