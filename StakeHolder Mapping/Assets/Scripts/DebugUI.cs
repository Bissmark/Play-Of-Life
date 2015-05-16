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
    private GameObject _saveImageEntryPrefab = null;

    [SerializeField]
    private RawImage _imagePreview = null;

    protected override void Awake()
    {
        base.Awake();

        // Error checking
        DebugUtils.Assert( _uIContainer != null, "Check that UIElementsContainer is hooked up" );
        DebugUtils.Assert( _imageEntriesContainer != null, "Check that ImageEntriesContainer is hooked up" );
        DebugUtils.Assert( _saveImageEntryPrefab != null, "Check that SaveImageEntry is hooked up" );
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
                DestroyImageEntries();
                _imagePreview.gameObject.SetActive(false);
            }
        }
    }

    // Button click functions
    #region ButtonClicks
    public void OnSaveScreenshotButtonClick()
    {
        SaveLoadManager.Instance.TakeScreenShot();
    }

    public void OnLoadScreenshotButton()
    {
        GenerateSaveImageEntries( SaveLoadManager.Instance.GetScreenShotNames() );
    }

    public void OnSaveImageEntryClick( string imagePath )
    {
        LoadImage( imagePath );
    }

    public void OnSaveSceneButtonClick()
    {
        SaveLoadManager.Instance.SaveScene();
    }
    #endregion



    private void GenerateSaveImageEntries( string[] imageEntries )
    {
        DestroyImageEntries();

        // instantiate prefabs
        for ( int i = 0; i < imageEntries.Length; i++ )
        {
            GameObject go = Instantiate( _saveImageEntryPrefab ) as GameObject;
            go.transform.SetParent( _imageEntriesContainer.transform );
            go.GetComponentInChildren<Text>().text = imageEntries[ i ];
            go.transform.position = go.transform.position + new Vector3( 0, ( -20 * ( i + 1 ) ) );
        }
    }

    private void LoadImage( string imagePath )
    {
        Texture2D texture = SaveLoadManager.Instance.GetSavedImage( imagePath );
        _imagePreview.texture = texture;
        _imagePreview.gameObject.SetActive( true );
        _imagePreview.rectTransform.sizeDelta = new Vector2( texture.width , texture.height );
    }

    // Clean up functions
    
    //Destroy already instantiated prefabs inside image container
    private void DestroyImageEntries()
    {
        foreach ( Transform child in _imageEntriesContainer.transform )
        {
            if ( child != _imageEntriesContainer.transform )
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
