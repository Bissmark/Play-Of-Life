/** 
 *	\brief Manager class which is responsible for Debug UI
 *	\author Timur Anoshechkin
 *	\copyright PenguinWolf
 */

using UnityEngine;
using System.Collections;

public class DebugUI : Manager<DebugUI>
{
    [SerializeField]
    private GameObject _uIContainer = null;
    private void Awake()
    {
        // Error checking
        DebugUtils.Assert( _uIContainer != null, "Check that UIElementsContainer is hooked up" );

        // Disable UIContainer on awake
        _uIContainer.SetActive( false );
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
            }
        }
    }
}
