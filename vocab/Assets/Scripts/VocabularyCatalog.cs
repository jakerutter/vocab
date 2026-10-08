// using System;
// using System.Collections.Generic;
// using System.Linq;

// public static class VocabularyCatalog
// {
//     private static readonly Dictionary<int, VocabularyGrade> Grades = new()
//     {
//         { 3, Grade3.Instance },
//         { 4, Grade4.Instance },
//         { 5, Grade5.Instance },
//         { 6, Grade6.Instance }
//     };

//     public static List<KeyValuePair<string, string>> CreateWordPool(IEnumerable<int> selectedGrades)
//     {
//         var pool = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

//         foreach (int grade in selectedGrades.Distinct().OrderBy(g => g))
//         {
//             if (!Grades.TryGetValue(grade, out var vocabulary))
//                 continue;

//             foreach (var entry in vocabulary.Entries)
//             {
//                 pool[entry.Key] = entry.Value;
//             }
//         }

//         return pool.ToList();
//     }
// }