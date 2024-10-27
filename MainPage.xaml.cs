using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;

namespace HesapMakinesi;

public partial class MainPage : ContentPage
{
    int currentState = 1;
    string operatorMath;
    double firstNum, secondNum;

    public MainPage()
    {
        InitializeComponent();
        OnClear(this, null);
    }

    void OnClear(object sender, EventArgs e)
    {
        firstNum = 0;
        secondNum = 0;
        currentState = 1;
        this.result.Text = "0";
    }

    void OnPowered(object sender, EventArgs e)
    {
        if (firstNum == 0)
            return;
        firstNum = Math.Pow(firstNum, 2);
        this.result.Text = firstNum.ToString();
    }

    void OnSquareRoot(object sender, EventArgs e)
    {
        if (firstNum < 0)
        {
            this.result.Text = "Hata";
            return;
        }

        firstNum = Math.Sqrt(firstNum);
        this.result.Text = firstNum.ToString();
    }

    void OnNumberSelection(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string btnPressed = button.Text;

        if (this.result.Text == "0" || currentState < 0)
        {
            this.result.Text = string.Empty;
            if (currentState < 0)
                currentState *= -1;
        }

        this.result.Text += btnPressed;

        if (double.TryParse(this.result.Text, out double number))
        {
            if (currentState == 1)
                firstNum = number;
            else
                secondNum = number;
        }
    }

    void onOperatorSelection(object sender, EventArgs e)
    {
        currentState = -2;
        Button button = (Button)sender;
        operatorMath = button.Text;
        result.Text = $"{firstNum} {operatorMath}";
    }

    void onCalculate(object sender, EventArgs e)
    {
        if (currentState == 2)
        {
            var result = Calculate.DoCalculation(firstNum, secondNum, operatorMath);

            this.result.Text = result.ToString();
            firstNum = result;
            currentState = -1;
        }
    }
}
