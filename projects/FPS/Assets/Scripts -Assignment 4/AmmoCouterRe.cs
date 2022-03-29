using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCouterRe : MonoBehaviour
{
     [Tooltip("CanvasGroup to fade the ammo UI")]
    public CanvasGroup canvasGroup;
    [Tooltip("Image for the weapon icon")]
    public Image weaponImage;
    [Tooltip("Image component for the background")]
    public Image ammoBackgroundImage;
    [Tooltip("Image component to display fill ratio")]
    public Image ammoFillImage;
    [Tooltip("Text for image index")]
    public TMPro.TextMeshProUGUI weaponIndexText;

    [Header("Selection")]
    [Range(0, 1)]
    [Tooltip("Opacity when weapon not selected")]
    public float unselectedOpacity = 0.5f;
    [Tooltip("Scale when weapon not selected")]
    public Vector3 unselectedScale = Vector3.one * 0.8f;
    [Tooltip("Root for the control keys")]
    public GameObject controlKeysRoot;

    [Header("Feedback")]
    [Tooltip("Component to animate the color when empty or full")]
    public FillBarColorChange FillBarColorChange;
    [Tooltip("Sharpness for the fill ratio movements")]
    public float ammoFillMovementSharpness = 20f;

    public float currentFillRatio;
    public int weaponCounterIndex { get; set; }

    PlayerWeaponsManager m_PlayerWeaponsManager;
    WeaponController m_Weapon;

    public void Initialize(WeaponController weapon, int weaponIndex)
    {
        m_Weapon = weapon;
        weaponCounterIndex = weaponIndex;
        weaponImage.sprite = weapon.weaponIcon;

        m_PlayerWeaponsManager = FindObjectOfType<PlayerWeaponsManager>();
        DebugUtility.HandleErrorIfNullFindObject<PlayerWeaponsManager, AmmoCounter>(m_PlayerWeaponsManager, this);

        weaponIndexText.text = (weaponCounterIndex + 1).ToString();

        FillBarColorChange.Initialize(1f, m_Weapon.GetAmmoNeededToShoot());
    }

    void Update()
    {
        float currenFillRatio = currentFillRatio;
        ammoFillImage.fillAmount = Mathf.Lerp(ammoFillImage.fillAmount, currenFillRatio, Time.deltaTime * ammoFillMovementSharpness);

        FillBarColorChange.UpdateVisual(currenFillRatio);
    }
}
