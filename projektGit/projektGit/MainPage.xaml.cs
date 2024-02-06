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
    }
}
