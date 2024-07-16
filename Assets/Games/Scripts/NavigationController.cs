using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System.Linq;

public class NavigationController : MonoBehaviour {
    public static NavigationController instance;

    [SerializeField]
    private Transform arCamera;

    [SerializeField] private Transform target;
    public LineRenderer lineRender;
    [SerializeField] private NavMeshPath path;
    [SerializeField] private Transform[] cabins;

    [Header("UI")]
    [SerializeField] private TMP_Dropdown target_dropdown;
    [SerializeField] private CompassBehaviour compassBehaviour;

    void Start() {
        path = new NavMeshPath();
        SetupDropdown();
        Clear();
    }

    private void SetupDropdown() {
        List<TMP_Dropdown.OptionData> templist = new List<TMP_Dropdown.OptionData>();
        TMP_Dropdown.OptionData default_option = new TMP_Dropdown.OptionData();
        default_option.text = "None";
        templist.Add(default_option);

        for (int i = 0; i < cabins.Length; i++) {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = cabins[i].name;
            templist.Add(option);
        }
        target_dropdown.options = templist;

        target_dropdown.onValueChanged.AddListener(delegate {
            DropDownValueChanged(target_dropdown);
        });
        target_dropdown.value = 0;
        target_dropdown.RefreshShownValue();
    }

    private void DropDownValueChanged(TMP_Dropdown change) {
        string selected_option = change.options[change.value].text;
        Debug.Log("DD changed to: " + selected_option);

        target = cabins.ToList().Find(c => c.name.Contains(selected_option));
        Clear();
        if (target != null) {
            SetAllCabins(false);
            target.gameObject.SetActive(true);
            compassBehaviour.InitializeCompass();            
        }
    }

    private void SetAllCabins(bool isEnable) {
        cabins.ToList().ForEach(c => { c.gameObject.SetActive(isEnable); });
    }

    private void Clear() {
        if (lineRender != null) {
            path.ClearCorners();
            lineRender.positionCount = 0;
            lineRender.enabled = false;
        }
        SetAllCabins(false);
    }

    private void Update() {
        if (target == null) {
            return;
        }

        NavMesh.CalculatePath(arCamera.position, target.position, NavMesh.AllAreas, path);

        if (path.status == NavMeshPathStatus.PathComplete) {
            lineRender.positionCount = path.corners.Length;
            lineRender.SetPositions(path.corners);
            lineRender.enabled = true;
        } else {
            lineRender.enabled = false;
        }
    }
}
