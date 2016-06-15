using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AndroidEnglishQuiz.QuizManagment.Model;

namespace AndroidEnglishQuiz.QuizManagment
{
  public class QuizManager
  {
    private const int RepetitionNumber = 1;
    public Dictionary<string, Word> _wordsAngPol;
    private Dictionary<string, Word> _wordsPolAng;

    public QuizManager()
    {
      List<string> words = ReadWords();

      _wordsAngPol = new Dictionary<string, Word>();
      _wordsPolAng = new Dictionary<string, Word>();

      AddWordsToDictionariesWithFiltering(words);
    }

    private static List<string> ReadWords()
    {
      string text = string.Empty;

      string path = Path.Combine(
        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
        @"Resources\Quizzes\AllUnitsQuiz.txt");

      using (var reader = new StreamReader(path))
      {
        text = reader.ReadToEnd();
      }

      return text.Split(
        new[] { Environment.NewLine, ":" }, 
        StringSplitOptions.RemoveEmptyEntries)
        .ToList();
    }

    private void AddWordsToDictionariesWithFiltering(List<string> words)
    {
      for (int i = 0; i < words.Count - 1; i += 2)
      {
        if (DictionariesNotContains(words, i))
        {
          _wordsAngPol.Add(
                words[i].Trim(),
                new Word
                {
                  WordName = words[i + 1].Trim(),
                  RepetitionNumber = RepetitionNumber
                });

          _wordsPolAng.Add(
            words[i + 1].Trim(),
            new Word
            {
              WordName = words[i].Trim(),
              RepetitionNumber = RepetitionNumber
            });
        }
      }
    }

    private bool DictionariesNotContains(List<string> words, int i)
    {
      return !_wordsAngPol.ContainsKey(words[i].Trim())
                && !_wordsPolAng.ContainsKey(words[i + 1].Trim());
    }
  }
}