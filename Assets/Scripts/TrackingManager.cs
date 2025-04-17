using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.IO;
using System;
using UnityEditor.Overlays;

public class TrackingManager : MonoBehaviour
{
    public List<TrajectoryTrackerVR> trackingList;
    public bool saveData;

    private Dictionary<string, StreamWriter> _objectWriters = new Dictionary<string, StreamWriter>();
 

    private void Awake()
    {
        this.HomogeneizeAcrossCulturalSettings();
    }

    void Start()
    {
        if (!saveData)
        {
            return;
        }

        string trajectory_file = string.Concat(UIManager.subjectCode, "_", UIManager.subjectAge, "_", UIManager.subjectSex, "_TAFC_trajectory_");

        this.CreateIfInexistent(Application.dataPath + "/Data" + "/Trajectories");


        foreach (var tracker in trackingList)
        {
            string objectName = tracker.transform.name;
            if (!_objectWriters.ContainsKey(objectName))
            {
                string objectFile = string.Concat(objectName, "_", trajectory_file, GetFormattedTimestamp());
                StreamWriter writer = System.IO.File.CreateText(Application.dataPath + "//Data" + "//Trajectories//" + objectFile + ".csv");
                _objectWriters[objectName] = writer;
            }
        }
        this.WriteHeaders();

    }
    private void HomogeneizeAcrossCulturalSettings()
    {
        CultureInfo customCulture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
        customCulture.NumberFormat.NumberDecimalSeparator = ".";
        customCulture.NumberFormat.NumberGroupSeparator = ",";
        CultureInfo.DefaultThreadCurrentCulture = customCulture;
        CultureInfo.DefaultThreadCurrentUICulture = customCulture;
    }

    private void CreateIfInexistent(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    private void WriteHeaders()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Time,");
        sb.Append("PosX,");
        sb.Append("PosY,");
        sb.Append("PosZ,");
        sb.Append("RotX,");
        sb.Append("RotY,");
        sb.Append("RotZ,");
        sb.Append("RotW,");


        string headerLine = sb.ToString();

        foreach (var writer in _objectWriters.Values)
        {
            writer.WriteLine(headerLine);
        }
    }

    void FixedUpdate()
    {
        if (!saveData)
        {
            return;
        }
        foreach (var tracker in trackingList)
        {
            string name = tracker.transform.name;
            Vector3 position = tracker.transform.position;
            Quaternion rotation = tracker.transform.rotation;
            StringBuilder sb = new StringBuilder();
            sb.Append(Time.timeSinceLevelLoad);
            sb.Append(",");
            sb.Append(position.x);
            sb.Append(",");
            sb.Append(position.y);
            sb.Append(",");
            sb.Append(position.z);
            sb.Append(",");
            sb.Append(rotation.x);
            sb.Append(",");
            sb.Append(rotation.y);
            sb.Append(",");
            sb.Append(rotation.z);
            sb.Append(",");
            sb.Append(rotation.w);
            _objectWriters[name].WriteLine(sb.ToString());
        }
    }

    private string GetFormattedTimestamp()
    {
        return DateTime.Now.ToString("yyyyMMdd_HHmmss");
    }

    void OnDisable()
    {
        if (!saveData)
        {
            return;
        }
        foreach (var writer in _objectWriters.Values)
        {
            writer.Flush();
            writer.Close();
        }
    }
}

