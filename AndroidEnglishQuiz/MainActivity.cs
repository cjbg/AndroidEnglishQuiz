using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AndroidEnglishQuiz.QuizManagment;

namespace AndroidEnglishQuiz
{
  [Activity(Label = "EnglishQuiz", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity
  {
    int count = 1;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.Main);

      QuizManager manager = new QuizManager();

      EditText questionText = FindViewById<EditText>(Resource.Id.QuestionText);
      questionText.Text = manager.CurrentAskedWord;

      EditText answerText = FindViewById<EditText>(Resource.Id.AnswerText);
      answerText.Text = manager.CurrentAnswerWord;
    }
  }
}

