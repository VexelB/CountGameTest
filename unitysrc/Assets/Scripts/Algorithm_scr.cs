using System.Collections;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine.UI;
using UnityEngine;

public class Algorithm_scr : MonoBehaviour
{
    [SerializeField] public Text input;
    [SerializeField] public Text previnput;
    [SerializeField] public Text output;
    [SerializeField] public Text btnNumb;
    [SerializeField] public Text Task;
    [SerializeField] public GameObject Restart;
    [SerializeField] public GameObject Success;
    [SerializeField] public Text Score;
    [SerializeField] public Text Timetxt;
    [SerializeField] public Text HStxt;
    [SerializeField] public GameObject HSBoard;
    private float time;    

    public string Alg(int arg){
		string str = "";
		if (arg%3 == 0)
		{
			str += "Cheeki";
		}
		if (arg%5 == 0)
		{
			str += "Breeki";
		}
		if ((arg%3 != 0)&&(arg%5 != 0))
		{
			str += arg.ToString();
		}
		return str;
	}

    public void GetHS() {
        var webClient = new WebClient();
        string response = webClient.DownloadString("https://localhost:5001/api/result");
        HStxt.text = response.Replace("},{", "\n").Replace("[{", "").Replace("}]", "").Replace("\"", "");
        HSBoard.active = true;
    }

    public void clsbtn() {
        HSBoard.active = false;
    }

    public void Send() {
        var request = WebRequest.Create("https://localhost:5001/api/result");
        request.ContentType = "application/json";
        request.Headers["Authorization"] = "Basic";
        request.Method = "POST";
        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        {
            string json = "{\"time\":\""+time.ToString().Remove(2, 5)+"\",\"score\":\""+(int.Parse(input.text)-1).ToString()+"\"}";
            streamWriter.Write(json);
        }
        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
    }

    public void Fail() {
        Task.text = "Oh! You're wrong :( Try better next time!";
        Score.text = "Your score: " + (int.Parse(input.text)-1).ToString();
        Timetxt.text = "Your time: " + time.ToString().Remove(2, 5);
        Restart.active = true;
        Send();   
    }

    public void Win() {
        Score.text = "Your score: " + (int.Parse(input.text)-1).ToString();
        Timetxt.text = "Your time: " + time.ToString();
        Success.active = true;
        Send();
    }

    public void Main () {
        Task.text = "Gues the algorithms output!";
        output.text = "???";
        input.text = "1";
        btnNumb.text = input.text;
        Restart.active = false;
        Success.active = false;
        HSBoard.active = false;
        time = 0f;
    }

  	public void Guess (string arg) {
        output.text = Alg(int.Parse(input.text));
        if (output.text == arg) {
            if (input.text == "100") {
                Win();
            }
            Task.text = "Right! And the next one?";
            previnput.text = input.text;
            input.text = (int.Parse(input.text)+1).ToString();
            btnNumb.text = input.text;
            
        }
        else {
            Fail();
        }
  	}

    public void GuessCheeki () {
        Guess("Cheeki");
    }

    public void GuessBreeki () {
        Guess("Breeki");
    }

    public void GuessChBr () {
        Guess("CheekiBreeki");
    }

    public void GuessNumber() {
        Guess(btnNumb.text);
    }

    void Update() {
        time += Time.deltaTime;
    }
}
