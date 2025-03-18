using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MathOperators;

public partial class MainPage : ContentPage
{
    // List to remember the expressions calculation by the user
    private ObservableCollection<string> _expressionList;

    public MainPage()
    {
        InitializeComponent();

        _expressionList = new ObservableCollection<string>();
        _listHistory.ItemsSource = _expressionList;
    }

    private async void OnCalculate(object sender, EventArgs e)
    {
        try
        {
            // Get the inputs for operands
            double leftOperand = double.Parse(_txtLeftOp.Text);
            double rightOperand = double.Parse(_txtRightOp.Text);

            // Get the character that represents the operation
            string opEntry = (string)_pckOperand.SelectedItem;
            char operation = opEntry[0];

            // Calculate and store the result of the operation
            double result = PerformArithmeticOperation(operation, leftOperand, rightOperand);

            // Display the arithmetic calculation to the user, showing the expression, adding the expression to the expression list
            string expression = $"{leftOperand} {operation} {rightOperand} = {result}";
            _expressionList.Add(expression);
            _txtMathExp.Text = expression;
        }
        catch (ArgumentNullException ex)
        {
            // User did not provide a value
            await DisplayAlert("Error", "Please provide the required input!", "Okay");
        }
        catch (FormatException ex)
        {
            // User has provided invalid input
            await DisplayAlert("Error", "Please provide the correct input!", "Okay");
        }
        catch(DivideByZeroException ex)
        {
            // User has attempted to divide by zero
            await DisplayAlert("Error", "Please don't divide by zero!", "Okay");
        }
    }

    private double PerformArithmeticOperation(char operation, double leftOperand, double rightOperand)
    {
        // Perform the arithmetic operation and get the result
        switch (operation)
        {
            case '+':
                return PerformAddition(leftOperand, rightOperand);
            case '-':
                return PerformSubtraction(leftOperand, rightOperand);
            case '*':
                return PerformMultiplication(leftOperand, rightOperand);
            case '/':
                return PerformDivision(leftOperand, rightOperand);
            case '%':
                return PerformModulus(leftOperand, rightOperand);
            default:
                Debug.Assert(false, "Unknown operator. Cannot perform the operation");
                return 0;
        }

    }

    private double PerformModulus(double leftOperand, double rightOperand)
    {
        return leftOperand % rightOperand;
    }

    private double PerformDivision(double leftOperand, double rightOperand)
    {
        string divOp = _pckOperand.SelectedItem as string;

        // Integer Division
        if (divOp.Contains("Int", StringComparison.OrdinalIgnoreCase))
        {
            return (int)leftOperand / (int)rightOperand;
        }
        // Real Division
        else
        {
            return leftOperand / rightOperand;
        }

    }

    private double PerformMultiplication(double leftOperand, double rightOperand)
    {
        return leftOperand * rightOperand;
    }

    private double PerformSubtraction(double leftOperand, double rightOperand)
    {
        return leftOperand - rightOperand;
    }

    private double PerformAddition(double leftOperand, double rightOperand)
    {
        return leftOperand + rightOperand;
    }
}