using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerStatisticsManager
{
    readonly static public string SAVE_FOLDER = "Saves";
    readonly static public string SAVE_FILE = "statistics.xml";

    private PlayerStatistics stats;

    private PlayerStatisticsManager()
    {
        try
        {
            LoadData();
        }
        catch (IOException execption)
        {
            //We haven't sucessfully loaded our statistics
        }

        if (stats == null)
        {
            stats = new PlayerStatistics();
        }
    }

    /// <summary>
    /// A standard singleton private instance variable
    /// </summary>
    static private PlayerStatisticsManager _Instance;
    /// <summary>
    /// A standard singleton public instance variable
    /// </summary>
    static public PlayerStatisticsManager Instance
    {
        get {
            if (_Instance == null)
            {
                _Instance = new PlayerStatisticsManager();
            }
            return _Instance;
        }
    }

    /// <summary>
    /// Saves the statistics as a XML text file. I chose XML because the unity json serializer has bugs around
    /// serializing the DateTime object.
    /// </summary>
    public void SaveData()
    {
        if (!Directory.Exists(SAVE_FOLDER))
            Directory.CreateDirectory(SAVE_FOLDER);

        XmlSerializer xsSubmit = new XmlSerializer(typeof(PlayerStatistics));
        StringWriter sww = new StringWriter();
        using (XmlWriter writer = XmlWriter.Create(sww))
        {
            xsSubmit.Serialize(writer, stats);
            var xml = sww.ToString(); // Your XML
            File.WriteAllText(Path.Combine(SAVE_FOLDER, SAVE_FILE), xml);
        }
    }

    /// <summary>
    /// Loads the XML data from the "Saves/statistics.xml" file
    /// </summary>
    public void LoadData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerStatistics));

        using (StreamReader reader = new StreamReader(Path.Combine(SAVE_FOLDER, SAVE_FILE)))
        {
            stats = (PlayerStatistics)serializer.Deserialize(reader);
        }
    }

    /// <summary>
    /// This is a function which adds a peak flow result to the statistics object.
    /// </summary>
    /// <param name="result">A float representing the peak flow results that is to be saved</param>
    public void AddPeakFlowResult(float result)
    {
        DateTime now = DateTime.Now;
        stats.peakFlowData.Add(new KeyValuePair<DateTime, float>(now, result));
        stats.lastPeakFlowPerformed = now;
        recalcuatePeakFlowBenchmark();
        SaveData();
    }

    /// <summary>
    /// This is a stub function to calculate the peak flow benchmark we're going to use
    /// </summary>
    private void recalcuatePeakFlowBenchmark()
    {

    }
}

[Serializable]
public class PlayerStatistics {
    /// <summary>
    /// The date that the last peak flow test was performed
    /// </summary>
    public DateTime lastPeakFlowPerformed;

    /// <summary>
    /// A List of every peakflow result stored with the time that it was performed. I've had to store
    /// a list of KeyValuePairs because you can't serialised a Dictionary which was the original way
    /// I had wanted to store the date.
    /// 
    /// This might need revisting as I'm not convinced this is the best way to store the data,
    /// plus we might need more than the just the peak flow result.
    /// </summary>
	public List<KeyValuePair<DateTime, float>> peakFlowData;

    /// <summary>
    /// This is the an averaged peak flow of all the previous results,
    /// it is going to be used as benchmark for the flow meter.
    /// 
    /// It's not clear at this point if this is going to be a mean average,
    /// or median or some other more nuanced way of analysing the data.
    /// </summary>
    public float peakFlowBenchmark;

    public PlayerStatistics()
    {
        peakFlowData = new List<KeyValuePair<DateTime, float>>();
    }
}
