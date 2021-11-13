using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    //[SerializeField] GameObject characterPanelGameObject;
    [SerializeField] GameObject equipmentPanelGameObject;
    //[SerializeField] KeyCode[] toggleCharacterPanelKeys;
    [SerializeField] KeyCode[] toggleInventoryKeys;
    [SerializeField] bool showAndHideMouse = true;

    void Start()
    {
        //ToggleCharacterPanel();
        //ToggleEquipmentPanel();
        //ToggleInventory();
    }

    //private void ToggleCharacterPanel()
    //{
    //	for (int i = 0; i < toggleCharacterPanelKeys.Length; i++)
    //	{
    //		if (Input.GetKeyDown(toggleCharacterPanelKeys[i]))
    //		{
    //			characterPanelGameObject.SetActive(!characterPanelGameObject.activeSelf);

    //			if (characterPanelGameObject.activeSelf)
    //			{
    //				equipmentPanelGameObject.SetActive(true);
    //				ShowMouseCursor();
    //			}
    //			else
    //			{
    //				HideMouseCursor();
    //			}

    //			break;
    //		}
    //	}
    //}

    private void ToggleInventory()
    {
        for (int i = 0; i < toggleInventoryKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleInventoryKeys[i]))
            {
                ToggleEquipmentPanel();
                break;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleEquipmentPanel();
        }
    }

    public void ShowMouseCursor()
    {
        if (showAndHideMouse)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void HideMouseCursor()
    {
        if (showAndHideMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ToggleEquipmentPanel()
    {
        equipmentPanelGameObject.SetActive(!equipmentPanelGameObject.activeSelf);
    }
}
