using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    #region Variables

    private             Question[]          _questions              = null;
    public              Question[]          Questions               { get { return _questions; } }

    [SerializeField]    GameEvents          events                  = null;

    [SerializeField]    Animator            timerAnimtor            = null;
    [SerializeField]    TextMeshProUGUI     timerText               = null;
    [SerializeField]    Color               timerHalfWayOutColor    = Color.yellow;
    [SerializeField]    Color               timerAlmostOutColor     = Color.red;
    private             Color               timerDefaultColor       = Color.white;

    private             List<AnswerData>    PickedAnswers           = new List<AnswerData>();
    private             List<int>           FinishedQuestions       = new List<int>();
    private             int                 currentQuestion         = 0;

    private             int                 timerStateParaHash      = 0;

    private             IEnumerator         IE_WaitTillNextRound    = null;
    private             IEnumerator         IE_StartTimer           = null;

    public static int answerChoice = 0;

    public GameObject nextButton;

    private             bool                IsFinished
    {
        get
        {
            return (FinishedQuestions.Count < Questions.Length) ? false : true;
        }
    }

    public static int mentalDemeand;
    public static int physicalDemand;
    public static int temporalDemand;
    public static int Effort;
    public static int Performance;
    public static int Frustration;
    public static float mental_value;
    public static float physical_value;
    public static float temporal_value;
    public static float effort_value;
    public static float frustration_value;
    public static float performance_value;
    public static float nasa_tlx;


    #endregion

    #region Default Unity methods

    /// <summary>
    /// Function that is called when the object becomes enabled and active
    /// </summary>
    void OnEnable()
    {
        events.UpdateQuestionAnswer += UpdateAnswers;
    }
    /// <summary>
    /// Function that is called when the behaviour becomes disabled
    /// </summary>
    void OnDisable()
    {
        events.UpdateQuestionAnswer -= UpdateAnswers;
    }

    /// <summary>
    /// Function that is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    /// </summary>
    void Awake()
    {
        events.CurrentFinalScore = 0;
    }
    /// <summary>
    /// Function that is called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        nextButton.SetActive(false);

        events.StartupHighscore = PlayerPrefs.GetInt(GameUtility.SavePrefKey);

        timerDefaultColor = timerText.color;
        LoadQuestions();

        timerStateParaHash = Animator.StringToHash("TimerState");

        var seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        UnityEngine.Random.InitState(seed);

        Display();

        mentalDemeand = 0;
        physicalDemand = 0;
        temporalDemand = 0;
        Performance = 0;
        Effort = 0;
        Frustration = 0;

        mental_value = 0;
        physical_value = 0;
        temporal_value = 0;
        performance_value = 0;
        effort_value = 0;
        frustration_value = 0;

        nasa_tlx = 0.0f;
    }

    #endregion





    /// <summary>
    /// Function that is called to update new selected answer.
    /// </summary>
    public void UpdateAnswers(AnswerData newAnswer)
    {

        if (Questions[currentQuestion].GetAnswerType == Question.AnswerType.Single)
        {
            nextButton.SetActive(true);
            foreach (var answer in PickedAnswers)
            {
                
                if (answer != newAnswer)
                {
                    answer.Reset();

                }
            }
            PickedAnswers.Clear();
            PickedAnswers.Add(newAnswer);
            answerChoice = newAnswer.AnswerIndex;
            //Debug.Log(Questions[currentQuestion].name.ToString() + " Answerchoice: " + newAnswer.AnswerIndex);

            //Debug.Log("Total count: " + Questions.Length);

            //add active next button here.

        }
        else
        {
            bool alreadyPicked = PickedAnswers.Exists(x => x == newAnswer);
            if (alreadyPicked)
            {
                PickedAnswers.Remove(newAnswer);
            }
            else
            {
                PickedAnswers.Add(newAnswer);
            }
        }
    }

    /// <summary>
    /// Function that is called to clear PickedAnswers list.
    /// </summary>
    public void EraseAnswers()
    {
        PickedAnswers = new List<AnswerData>();
    }

    /// <summary>
    /// Function that is called to display new question.
    /// </summary>
    void Display()
    {
        EraseAnswers();
        var question = GetRandomQuestion();

        if (events.UpdateQuestionUI != null)
        {
            events.UpdateQuestionUI(question);


        }
        else
        {
            Debug.LogWarning("Ups! Something went wrong while trying to display new Question UI Data. GameEvents.UpdateQuestionUI is null. Issue occured in GameManager.Display() method.");
        }

        if (question.UseTimer)
        {
            UpdateTimer(question.UseTimer);
        }
    }

    /// <summary>
    /// Function that is called to accept picked answers and check/display the result.
    /// </summary>
    public void Accept()
    {
        UpdateTimer(false);
        bool isCorrect = CheckAnswers();
        FinishedQuestions.Add(currentQuestion);

        //conduct tally for each response here.
        if (Questions[currentQuestion].name.ToString() == "Ment.vs.Eff.")
        {
            if (answerChoice == 0)
            {
                mentalDemeand = mentalDemeand + 1;
            }
            else { Effort = Effort + 1; }
        }
        else if (Questions[currentQuestion].name.ToString() == "Phy.vs.Perf.")
        {
            if (answerChoice == 0)
            {
                physicalDemand = physicalDemand + 1;
            }
            else { Performance = Performance + 1; }
        }
        else if (Questions[currentQuestion].name.ToString() == "Phy.vs.Temp")
        {
            if (answerChoice == 0)
            {
                physicalDemand = physicalDemand + 1;
            }
            else { temporalDemand = temporalDemand + 1; }
        }
        else if (Questions[currentQuestion].name.ToString() == "Ment.vs.Phy.")
        {
            if (answerChoice == 0)
            {
                mentalDemeand = mentalDemeand + 1;
            }
            else { physicalDemand = physicalDemand + 1; }
        }
        else if (Questions[currentQuestion].name.ToString() == "Frust.vs.Eff.")
        {
            if (answerChoice == 0)
            {
                Frustration = Frustration + 1;
            }
            else { Effort = Effort + 1; }
        }
        else if (Questions[currentQuestion].name.ToString() == "Temp.vs.Eff.")
        {
            if (answerChoice == 0)
            {
                temporalDemand = temporalDemand + 1;
            }
            else { Effort = Effort + 1; }
        }
        else if (Questions[currentQuestion].name.ToString() == "Perf.vs.Frust")
        {
            if (answerChoice == 0)
            {
                Performance = Performance + 1;
            }
            else { Frustration = Frustration + 1; }
        }
        else if (Questions[currentQuestion].name.ToString() == "Temp.vs.Ment.")
        {
            if (answerChoice == 0)
            {
                temporalDemand = temporalDemand + 1;
            }
            else { mentalDemeand = mentalDemeand + 1; }
        }
        else if (Questions[currentQuestion].name.ToString() == "Eff.vs.Phy.")
        {
            if (answerChoice == 0)
            {
                Effort = Effort + 1;
            }
            else { physicalDemand = physicalDemand + 1; }
        }
        else if (Questions[currentQuestion].name.ToString() == "Perf.vs.Temp.")
        {
            if (answerChoice == 0)
            {
                Performance = Performance + 1;
            }
            else { temporalDemand = temporalDemand + 1; }
        }
        else if (Questions[currentQuestion].name.ToString() == "Eff.vs.Perf.")
        {
            if (answerChoice == 0)
            {
                Effort = Effort + 1;
            }
            else { Performance = Performance + 1; }
        }
        else if (Questions[currentQuestion].name.ToString() == "Phy.vs.Frust.")
        {
            if (answerChoice == 0)
            {
                physicalDemand = physicalDemand + 1;
            }
            else { Frustration = Frustration + 1; }
        }
        else if (Questions[currentQuestion].name.ToString() == "Perf.vs.Ment.")
        {
            if (answerChoice == 0)
            {
                Performance = Performance + 1;
            }
            else { mentalDemeand = mentalDemeand + 1; }
        }
        else if (Questions[currentQuestion].name.ToString() == "Perf.vs.Temp.")
        {
            if (answerChoice == 0)
            {
                Performance = Performance + 1;
            }
            else { temporalDemand = temporalDemand + 1; }
        }
        else if (Questions[currentQuestion].name.ToString() == "Temp.vs.Frust.")
        {
            if (answerChoice == 0)
            {
                temporalDemand = temporalDemand + 1;
            }
            else { Frustration = Frustration + 1; }
        }
        else if (Questions[currentQuestion].name.ToString() == "Frust.vs.Ment")
        {
            if (answerChoice == 0)
            {
                Frustration = Frustration + 1; 
            }
            else { mentalDemeand = mentalDemeand + 1; }
        }
        else if (Questions[currentQuestion].name.ToString() == "Effort")
        {
            effort_value = answerChoice * 10;
        }
        else if (Questions[currentQuestion].name.ToString() == "Frustration")
        {
            frustration_value = answerChoice * 10;
        }
        else if (Questions[currentQuestion].name.ToString() == "Mental Demand")
        {
            mental_value = answerChoice * 10;
        }
        else if (Questions[currentQuestion].name.ToString() == "Performance")
        {
            performance_value = answerChoice * 10;
        }
        else if (Questions[currentQuestion].name.ToString() == "Physical Demand")
        {
            physical_value = answerChoice * 10;
        }
        else if (Questions[currentQuestion].name.ToString() == "Temporal Demand")
        {
            temporal_value = answerChoice * 10;
        }
        //Debug.Log(Questions[currentQuestion].name.ToString() + " Answerchoice: " + answerChoice);


        UpdateScore((isCorrect) ? Questions[currentQuestion].AddScore : -Questions[currentQuestion].AddScore);

        if (IsFinished)
        {
            SetHighscore();
            //Debug.Log("total: " + mental_value + " , " + physical_value + " , " + temporal_value + " , " + effort_value + " , " + performance_value + " , " + frustration_value);

            //calculate the NASA TLX 
            nasa_tlx = (mental_value * (mentalDemeand / 15.0f) + physical_value * (physicalDemand / 15.0f) + temporal_value * (temporalDemand / 15.0f) + effort_value * (Effort / 15.0f) + frustration_value * (Frustration / 15.0f) + performance_value * (Performance / 15.0f));
            Debug.Log("NASA TLX: " + nasa_tlx);
        }

        var type 
            = (IsFinished) 
            ? UIManager.ResolutionScreenType.Finish 
            : (isCorrect) ? UIManager.ResolutionScreenType.Correct 
            : UIManager.ResolutionScreenType.Incorrect;

        if (events.DisplayResolutionScreen != null)
        {
            events.DisplayResolutionScreen(type, Questions[currentQuestion].AddScore);
        }

        //AudioManager.Instance.PlaySound((isCorrect) ? "CorrectSFX" : "IncorrectSFX");

        if (type != UIManager.ResolutionScreenType.Finish)
        {
            if (IE_WaitTillNextRound != null)
            {
                StopCoroutine(IE_WaitTillNextRound);
            }
            IE_WaitTillNextRound = WaitTillNextRound();
            StartCoroutine(IE_WaitTillNextRound);
        }
    }

    #region Timer Methods

    void UpdateTimer(bool state)
    {
        switch (state)
        {
            case true:
                IE_StartTimer = StartTimer();
                StartCoroutine(IE_StartTimer);

                timerAnimtor.SetInteger(timerStateParaHash, 2);
                break;
            case false:
                if (IE_StartTimer != null)
                {
                    StopCoroutine(IE_StartTimer);
                }

                //timerAnimtor.SetInteger(timerStateParaHash, 1);
                break;
        }
    }
    IEnumerator StartTimer()
    {
        var totalTime = Questions[currentQuestion].Timer;
        var timeLeft = totalTime;

        timerText.color = timerDefaultColor;
        while (timeLeft > 0)
        {
            timeLeft--;

            //AudioManager.Instance.PlaySound("CountdownSFX");

            if (timeLeft < totalTime / 2 && timeLeft > totalTime / 4)
            {
                timerText.color = timerHalfWayOutColor;
            }
            if (timeLeft < totalTime / 4)
            {
                timerText.color = timerAlmostOutColor;
            }

            timerText.text = timeLeft.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        Accept();
    }
    IEnumerator WaitTillNextRound()
    {
        yield return new WaitForSeconds(GameUtility.ResolutionDelayTime);
        Display();
    }

    #endregion

    /// <summary>
    /// Function that is called to check currently picked answers and return the result.
    /// </summary>
    bool CheckAnswers()
    {
        if (!CompareAnswers())
        {
            return false;
        }
        return true;
    }
    /// <summary>
    /// Function that is called to compare picked answers with question correct answers.
    /// </summary>
    bool CompareAnswers()
    {
        if (PickedAnswers.Count > 0)
        {
            List<int> c = Questions[currentQuestion].GetCorrectAnswers();
            List<int> p = PickedAnswers.Select(x => x.AnswerIndex).ToList();

            var f = c.Except(p).ToList();
            var s = p.Except(c).ToList();

            return !f.Any() && !s.Any();
        }
        return false;
    }

    /// <summary>
    /// Function that is called to load all questions from the Resource folder.
    /// </summary>
    void LoadQuestions()
    {
        Object[] objs = Resources.LoadAll("Questions", typeof(Question));
        Object[] objs2 = Resources.LoadAll("Questions2", typeof(Question));

        //Add to the list of questions
        _questions = new Question[objs.Length + objs2.Length];
        for (int i = 0; i < objs.Length; i++)
        {
            _questions[i] = (Question)objs[i];
            //Debug.Log("Question: " +  objs[i].name.ToString());
        }

        for (int i = 0; i < objs2.Length; i++)
        {
            _questions[i+ objs.Length] = (Question)objs2[i];
            //Debug.Log("Question: " +  objs[i].name.ToString());
        }
    }

    /// <summary>
    /// Function that is called restart the game.
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /// <summary>
    /// Function that is called to quit the application.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Function that is called to set new highscore if game score is higher.
    /// </summary>
    private void SetHighscore()
    {
        var highscore = PlayerPrefs.GetInt(GameUtility.SavePrefKey);
        if (highscore < events.CurrentFinalScore)
        {
            PlayerPrefs.SetInt(GameUtility.SavePrefKey, events.CurrentFinalScore);
        }
    }
    /// <summary>
    /// Function that is called update the score and update the UI.
    /// </summary>
    private void UpdateScore(int add)
    {
        events.CurrentFinalScore += add;

        if (events.ScoreUpdated != null)
        {
            events.ScoreUpdated();
        }
    }

    #region Getters

    Question GetRandomQuestion()
    {
        var randomIndex = GetRandomQuestionIndex();
        currentQuestion = randomIndex;
        nextButton.SetActive(false);
        return Questions[currentQuestion];
    }
    int GetRandomQuestionIndex()
    {
        var random = 0;
        if (FinishedQuestions.Count < Questions.Length)
        {
            do
            {
                random = UnityEngine.Random.Range(0, Questions.Length);
            } while (FinishedQuestions.Contains(random) || random == currentQuestion);
        }
        return random;
    }

    #endregion
}