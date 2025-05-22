using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.IO;
using System;
using UnityEditor.Overlays;

public class TrackingManager :Singleton<TrackingManager>
{
    public List<TrajectoryTrackerVR> trackingList;
    public bool saveData;

    private Dictionary<string, StreamWriter> _objectWriters = new Dictionary<string, StreamWriter>();
 


    void Start()
    {

        this.HomogeneizeAcrossCulturalSettings();

        if (!saveData)
        {
            return;
        }

        string trajectory_file = string.Concat(UIManager.subjectCode, "_", UIManager.subjectAge, "_", UIManager.subjectSex, "_trajectory_");

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
        sb.Append("Event"); 


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

    public void RecordEvent(string eventName) 
    {
        if (!saveData) return;

        foreach (var writer in _objectWriters.Values)
        {
            // Formato: Time,a,a,a,a,a,a,a,EventName
            writer.WriteLine($"{Time.timeSinceLevelLoad},a,a,a,a,a,a,a,{eventName}");
        }
    }

    public void LogCollision(int layer1, int layer2)
    {
        if (!saveData) return;

        string layerName1 = LayerMask.LayerToName(layer1);
        string layerName2 = LayerMask.LayerToName(layer2);
        string eventText = $"Collision_{layerName1}_{layerName2}";

        // Log to all active trackers (or filter by involved objects)
        foreach (var writer in _objectWriters.Values)
        {
            writer.WriteLine($"{Time.timeSinceLevelLoad},a,a,a,a,a,a,a,{eventText}");
        }
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

