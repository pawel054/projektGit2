using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace projektGit
{
    public partial class MainPage : ContentPage
    {
        private int currentQusetionIndex = 0;
        private int currentScore = 0;
        private List<int> questions = new List<int>();
        private Stopwatch stopwatch = new Stopwatch();
        private List<double> times = new List<double>();
        public MainPage()
        {
            InitializeComponent();
            GenerateQuestions();
        }

        private void GenerateQuestions()
        {
            Random random = new Random();
            questions = Enumerable.Range(0, 5).Select(_=> random.Next(1,1001)).ToList();
        }

        private void StartQuiz(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(userNameEntry.Text))
            {
                DisplayAlert("Alert", "pole nie może być puste!", "OK");
            }
            else
            {
                currentScore = 0;
                currentQusetionIndex = 0;
                times.Clear();
                startGameView.IsVisible = false;
                finishGameView.IsVisible = false;
                gameView.IsVisible = true;
                ShowNextQuestion();
            }
        }

        private void PlayAgainButton(object sender, EventArgs e)
        {
            finishGameView.isVisible = false;
            startGameView = true;
            GenerateQuestions();
        }

        private void ShowNextQuestion()
        {
            submitAnswerButton.IsEnabled = true;
            if(currentQuestinonIndex < questions.Count)
            {
                questionLable.IsVisible = true;
                question.Lable.Text = $"Podwojona wartość {questions[currentQusetionIndex]} to:";
                answerEntry.IsVisible = true;
                submitAnswerButton.IsVisible = true;
                feedbackLabel.IsVisible = false;
                feedbackFrame.IsVisible = false;
                stopwatch.Restart();
            }
            else
            {
                FinishQuiz();
            }
        }

        private async void SubmitAnswer(object sender, EventArgs e)
        {
            submitAnswerButton.IsEnabled = false;
            stopwatch.Stop();
            int correctAnswer = questions[currentQusetionIndex] * 2;
            if(int.TryParse(answerEntry.Text, out correctAnswer) && userAnswer == correctAnswer)
            {
                currentScore++;
                feedbackLabel.Text = "Poprawna odpowiedź";
                feedbackFrame.BorderColor = Color.Green;
                feedbackLabel.TextColor = Color.Green;
            }
            else
            {
                feedbackLabel.Text = $"Niestety, to jest zła odpowiedź.Poprawna odpowiedź to: {correctAnswer}";
                feedbackFrame.BackgroundColor = Color.Red;
                feedbackLabel.TextColor = Color.Red;
            }
            feedbackLabel.IsVisible = true;
            feedbackLabel.IsVisible = true;

            times.Add(stopwatch.Elapsed.TotalSeconds);
            currentQusetionIndex++;
            answerEntry.Text = string.Empty;
            await Task.Delay(2500);
            ShowNextQuestion();
        }

        private void FinishQuiz()
        {
            double totalTime = times.Sum();
            SaveResult(userNameEntry.Text, totalTime, currentScore);
            DisplayFinalResults(totalTime);
        }
    }
}
