using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField] GameObject panelMenu;

	void Awake ()
	{
		panelMenu.SetActive (false); //hide menu in the start
	}

	public void ShowMenu ()
	{
		panelMenu.SetActive (true);
	}

	public void HideMenu ()
	{
		panelMenu.SetActive (false);
	}
}

