using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace App.Web.Helper
{
    public static class StatusValidator
    {
        private static readonly Regex SpaceRegex = new Regex(@"\s+", RegexOptions.Compiled);

        // Map categories to their valid synonyms (all mapped to canonical forms)
        private static readonly Dictionary<string, HashSet<string>> CategorySynonyms = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase)
        {
            {
                "Success", new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    "OK", "SUCCESS", "DELIVERED", "POSTED", "COMPLETED", "PROCESSED", "DONE", 
                    "ACCEPTED", "SENT", "PAID", "TRUE", "1", "YES", "VALIDATED"
                }
            },
            {
                "Active", new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    "ACTIVE", "ENABLED", "LIVE", "OPEN", "VALID", "A", "Y", "YES", "RUNNING"
                }
            },
            {
                "Failure", new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    "FAILED", "ERROR", "REJECTED", "CANCELLED", "DECLINED", "DENIED", "RETURNED", 
                    "FALSE", "0", "NO", "INVALID"
                }
            }
        };

        /// <summary>
        /// Validates if a status string belongs to a specific category (e.g., "Success", "Active").
        /// </summary>
        public static bool IsInCategory(string status, string category)
        {
            if (string.IsNullOrWhiteSpace(status))
                return false;

            if (!CategorySynonyms.TryGetValue(category, out var synonyms))
                return false;

            string normalized = Normalize(status);
            
            // 1. Direct match in synonym list
            if (synonyms.Contains(normalized))
                return true;

            // 2. Whole word check for partial matches
            // split by space and check if any word matches a synonym
            string[] words = normalized.Split(' ');
            return words.Any(word => synonyms.Contains(word));
        }

        public static bool IsValidSuccess(string status) => IsInCategory(status, "Success");
        public static bool IsActive(string status) => IsInCategory(status, "Active");

        /// <summary>
        /// Normalizes the input string: trims, removes extra spaces, and converts to upper case.
        /// </summary>
        public static string Normalize(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // Trim and unify internal spacing
            string normalized = SpaceRegex.Replace(input.Trim(), " ");
            
            return normalized.ToUpperInvariant();
        }

        /// <summary>
        /// Fuzzy matching for minor spelling variations.
        /// </summary>
        public static bool IsFuzzyMatch(string status, string category, int maxDistance = 1)
        {
            if (IsInCategory(status, category)) return true;

            if (!CategorySynonyms.TryGetValue(category, out var synonyms))
                return false;

            string normalized = Normalize(status);
            
            return synonyms.Any(syn => ComputeLevenshteinDistance(normalized, syn) <= maxDistance);
        }

        private static int ComputeLevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            if (n == 0) return m;
            if (m == 0) return n;

            int[,] d = new int[n + 1, m + 1];
            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 0; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }
    }
}
