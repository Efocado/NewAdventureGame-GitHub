using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Soal : MonoBehaviour
{

    public TextAsset questionAsset;

    private string[] Question;

    private string[,] questionBag;


    int questionIndex;
    int maxQuestion;
    bool takeQuestion;
    char keyJ;

    public Text txtQuestion, txtOptionA, txtOptionB, txtOptionC, txtOptionD;

    bool isResult;
    private float duration;
    public float evaluationDuration;

    int correctAnswer, wrongAnswer;
    float score;

    public GameObject panel;
    public GameObject imgEvaluation, imgResult;
    public Text txtResult;

    // Start is called before the first frame update
    void Start()
    {
        duration = evaluationDuration;

        Question = questionAsset.ToString().Split('#');

        questionBag = new string[Question.Length, 6];
        maxQuestion = Question.Length;
        ProcessQuestion();

        takeQuestion = true;
        ShowQuestion();

        print(questionBag[1, 3]);

    }

    private void ProcessQuestion()
    {
        for (int i = 0; i < Question.Length; i++)
        {
            string[] tempQuestion = Question[i].Split('+');
            for (int j = 0; j < tempQuestion.Length; j++)
            {
                questionBag[i, j] = tempQuestion[j];
                continue;
            }
            continue;
        }
    }

    private void ShowQuestion()
    {
        if (questionIndex < maxQuestion)
        {
            if (takeQuestion)
            {
                txtQuestion.text = questionBag[questionIndex, 0];
                txtOptionA.text = questionBag[questionIndex, 1];
                txtOptionB.text = questionBag[questionIndex, 2];
                txtOptionC.text = questionBag[questionIndex, 3];
                txtOptionD.text = questionBag[questionIndex, 4];
                keyJ = questionBag[questionIndex, 5][0];

                takeQuestion = false;
            }
        }
    }


    public void Option(string letterOption)
    {
        CheckJawaban(letterOption[0]);

        if (questionIndex == maxQuestion - 1)
        {
            isResult = true;
        }
        else
        {
            questionIndex++;
            takeQuestion = true;
        }

        panel.SetActive(true);
    }

    private float CountScore()
    {
        return score = (float)correctAnswer / maxQuestion * 100;
    }

    public GameObject correctObj;
    public GameObject wrongObj;
    private void CheckJawaban(char letter)
    {
        if (letter.Equals(keyJ))
        {
            correctObj.SetActive(true);
            wrongObj.SetActive(false);
            correctAnswer++;
        }
        else
        {
            wrongObj.SetActive(true);
            correctObj.SetActive(false);
            wrongAnswer++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (panel.activeSelf)
        {
            evaluationDuration -= Time.deltaTime;

            if (isResult)
            {
                imgEvaluation.SetActive(true);
                imgResult.SetActive(false);

                if (evaluationDuration <= 0)
                {
                    txtResult.text = "Correct : " + correctAnswer + "\nWrong : " + wrongAnswer + "\n\nScore : " + CountScore();

                    imgEvaluation.SetActive(false);
                    imgResult.SetActive(true);

                    evaluationDuration = 0;

                }
            }
            else
            {
                imgEvaluation.SetActive(true);
                imgResult.SetActive(false);

                if (evaluationDuration <= 0)
                {
                    panel.SetActive(false);
                    evaluationDuration = duration;

                    ShowQuestion();
                }
            }
        }


    }
}
