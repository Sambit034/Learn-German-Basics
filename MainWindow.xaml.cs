using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Learn_German_Basics
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer? _countdownTimer;
        private int _secondsRemaining;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            TestSection.Visibility = Visibility.Collapsed;
            LevelOverlay.Visibility = Visibility.Visible;
        }

        private void CloseLevelOverlay_Click(object sender, RoutedEventArgs e)
        {
            LevelOverlay.Visibility = Visibility.Collapsed;
        }

        private void A1_Click(object sender, RoutedEventArgs e)
        {
            LevelOverlay.Visibility = Visibility.Collapsed;

            MessageBox.Show(
                "A1 course selected!\n\nThe Germany journey map will be added next.",
                "Learn German Basics",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void A2_Click(object sender, RoutedEventArgs e)
        {
            A2Message.Text = "Take test to enter";

            var transform = new TranslateTransform();
            A2Button.RenderTransform = transform;

            var animation = new DoubleAnimationUsingKeyFrames
            {
                Duration = TimeSpan.FromMilliseconds(350)
            };

            animation.KeyFrames.Add(
                new EasingDoubleKeyFrame(-8, KeyTime.FromPercent(0.15)));
            animation.KeyFrames.Add(
                new EasingDoubleKeyFrame(8, KeyTime.FromPercent(0.35)));
            animation.KeyFrames.Add(
                new EasingDoubleKeyFrame(-5, KeyTime.FromPercent(0.55)));
            animation.KeyFrames.Add(
                new EasingDoubleKeyFrame(5, KeyTime.FromPercent(0.75)));
            animation.KeyFrames.Add(
                new EasingDoubleKeyFrame(0, KeyTime.FromPercent(1.0)));

            transform.BeginAnimation(TranslateTransform.XProperty, animation);
        }

        private void A2TestButton_Click(object sender, RoutedEventArgs e)
        {
            A2ConfirmOverlay.Visibility = Visibility.Visible;
        }

        private void A2ConfirmNo_Click(object sender, RoutedEventArgs e)
        {
            A2ConfirmOverlay.Visibility = Visibility.Collapsed;
        }

        private void A2ConfirmYes_Click(object sender, RoutedEventArgs e)
        {
            A2ConfirmOverlay.Visibility = Visibility.Collapsed;
            LevelOverlay.Visibility = Visibility.Collapsed;

            // Keep the existing sidebar visible and switch the main content to Test.
            TestSection.Visibility = Visibility.Visible;

            // Then open the assessment-start prompt.
            AssessmentStartOverlay.Visibility = Visibility.Visible;
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            LevelOverlay.Visibility = Visibility.Collapsed;
            TestSection.Visibility = Visibility.Visible;
            AssessmentStartOverlay.Visibility = Visibility.Collapsed;
            AssessmentPrepOverlay.Visibility = Visibility.Collapsed;
        }

        private void OpenAssessmentStart_Click(object sender, RoutedEventArgs e)
        {
            AssessmentStartOverlay.Visibility = Visibility.Visible;
        }

        private void AssessmentStartNo_Click(object sender, RoutedEventArgs e)
        {
            AssessmentStartOverlay.Visibility = Visibility.Collapsed;
            TestSection.Visibility = Visibility.Visible;
        }

        private void AssessmentStartYes_Click(object sender, RoutedEventArgs e)
        {
            AssessmentStartOverlay.Visibility = Visibility.Collapsed;
            StartPreparationCountdown();
        }

        private void StartPreparationCountdown()
        {
            _countdownTimer?.Stop();

            _secondsRemaining = 30;
            CountdownText.Text = "00:30";
            CountdownProgress.Value = 30;
            AssessmentPrepOverlay.Visibility = Visibility.Visible;

            _countdownTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            _countdownTimer.Tick += CountdownTimer_Tick;
            _countdownTimer.Start();
        }

        private void CountdownTimer_Tick(object? sender, EventArgs e)
        {
            _secondsRemaining--;

            if (_secondsRemaining <= 0)
            {
                _countdownTimer?.Stop();
                _countdownTimer = null;

                CountdownText.Text = "00:00";
                CountdownProgress.Value = 0;
                AssessmentPrepOverlay.Visibility = Visibility.Collapsed;

                MessageBox.Show(
                    "Assessment ready. The actual question screen will be implemented next.",
                    "A2 Assessment",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                TestSection.Visibility = Visibility.Visible;
                return;
            }

            CountdownText.Text = $"00:{_secondsRemaining:00}";
            CountdownProgress.Value = _secondsRemaining;
        }

        private void LeaveAssessment_Click(object sender, RoutedEventArgs e)
        {
            _countdownTimer?.Stop();
            _countdownTimer = null;

            AssessmentPrepOverlay.Visibility = Visibility.Collapsed;
            AssessmentStartOverlay.Visibility = Visibility.Collapsed;
            A2ConfirmOverlay.Visibility = Visibility.Collapsed;
            LevelOverlay.Visibility = Visibility.Collapsed;
            TestSection.Visibility = Visibility.Collapsed;

            // Return to the main welcome screen, with the sidebar still visible.
        }

        private void BackToWelcome_Click(object sender, RoutedEventArgs e)
        {
            TestSection.Visibility = Visibility.Collapsed;
            AssessmentStartOverlay.Visibility = Visibility.Collapsed;
            AssessmentPrepOverlay.Visibility = Visibility.Collapsed;
            A2ConfirmOverlay.Visibility = Visibility.Collapsed;
            LevelOverlay.Visibility = Visibility.Collapsed;
        }

        private void Progress_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Your Germany journey map will be implemented in a later stage.",
                "Progress",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            _countdownTimer?.Stop();
            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            _countdownTimer?.Stop();
            base.OnClosed(e);
        }
    }
}
