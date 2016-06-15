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
    public string CurrentAskedWord;
    public string CurrentAnswerWord;
    private int _currentRepetitionNumber;
    private readonly Random _random;

    private const int RepetitionNumber = 1;
    private Dictionary<string, Word> _wordsAngPol;
    //private Dictionary<string, Word> _wordsPolAng;

    public QuizManager()
    {
      _random = new Random();
      List<string> words = ReadWords();

      _wordsAngPol = new Dictionary<string, Word>();
      //_wordsPolAng = new Dictionary<string, Word>();

      AddWordsToDictionariesWithFiltering(words);
      SetRandomWord();
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

          //_wordsPolAng.Add(
          //  words[i + 1].Trim(),
          //  new Word
          //  {
          //    WordName = words[i].Trim(),
          //    RepetitionNumber = RepetitionNumber
          //  });
        }
      }
    }

    private bool DictionariesNotContains(List<string> words, int i)
    {
      return !_wordsAngPol.ContainsKey(words[i].Trim())
                //&& !_wordsPolAng.ContainsKey(words[i + 1].Trim())
                ;
    }

    private void SetRandomWord()
    {
      int randomWordNumber;

      do
      {
        randomWordNumber = _random.Next(0, _wordsAngPol.Count);
      }
      while (_wordsAngPol.Values.ElementAt(randomWordNumber).RepetitionNumber == 0);

      CurrentAskedWord = _wordsAngPol.Keys.ElementAt(randomWordNumber);
      CurrentAnswerWord = _wordsAngPol[CurrentAskedWord].WordName;
      _currentRepetitionNumber = _wordsAngPol[CurrentAskedWord].RepetitionNumber;
    }
  }
}