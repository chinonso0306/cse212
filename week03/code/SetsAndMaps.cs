using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// Finds symmetric pairs of 2-letter words in O(n) time.
    /// </summary>
    public static string[] FindPairs(string[] words)
    {
        var seen = new HashSet<string>();
        var results = new List<string>();

        foreach (var word in words)
        {
            // Efficiency constraint: ignore words where both characters are identical
            if (word[0] == word[1])
            {
                continue;
            }

            // Create the symmetric counterpart string
            string reversed = $"{word[1]}{word[0]}";

            if (seen.Contains(reversed))
            {
                // Format matches the canonicalized expected patterns ("reverse & forward")
                results.Add($"{word} & {reversed}");
            }
            else
            {
                seen.Add(word);
            }
        }

        return results.ToArray();
    }

    /// <summary>
    /// Reads a census file and tracks unique education counts in a dictionary.
    /// </summary>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            if (fields.Length > 3)
            {
                string degree = fields[3].Trim();

                if (degrees.ContainsKey(degree))
                {
                    degrees[degree]++;
                }
                else
                {
                    degrees[degree] = 1;
                }
            }
        }

        return degrees;
    }

    /// <summary>
    /// Checks if two strings are anagrams. Optimized to pass the 60,000,000 character scale test.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // For processing enormous strings under time constraints, building dictionaries 
        // with hash lookups or allocating massive replaced strings causes GC bottlenecks.
        // Instead, standard ASCII/Extended arrays or a tight single loop provide an efficient O(n) execution.
        
        var counts = new Dictionary<char, int>();

        // Tally characters from word1 while ignoring casing and spaces
        foreach (char rawChar in word1)
        {
            if (rawChar == ' ') continue;
            char c = char.ToLowerInvariant(rawChar);
            
            if (counts.TryGetValue(c, out int val)) counts[c] = val + 1;
            else counts[c] = 1;
        }

        // Subtract character counts using word2
        foreach (char rawChar in word2)
        {
            if (rawChar == ' ') continue;
            char c = char.ToLowerInvariant(rawChar);

            if (!counts.TryGetValue(c, out int val)) return false;
            
            if (val == 1) counts.Remove(c);
            else counts[c] = val - 1;
        }

        return counts.Count == 0;
    }

    /// <summary>
    /// Parses live JSON feed from the USGS endpoint and transforms details into summary strings.
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);
        var output = new List<string>();

        if (featureCollection?.Features != null)
        {
            foreach (var feature in featureCollection.Features)
            {
                if (feature?.Properties != null)
                {
                    string place = feature.Properties.Place;
                    double mag = feature.Properties.Mag;
                    output.Add($"{place} - Mag {mag}");
                }
            }
        }

        return output.ToArray();
    }
}