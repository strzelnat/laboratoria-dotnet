namespace Lab01Ak.Models;

public enum Operators
{
    Unknown, Add, Mul, Sub, Div,
}

public class Calculator
{
    public Operators? op { get; set; }
    public double? a { get; set; }
    public double? b { get; set; }

    public string ErrorMessage;

    public String Op1
    {
        get
        {
            switch (op)
            {
                case Operators.Add:
                    return "+";
                case Operators.Sub:   // Corrected this case
                    return "-";
                case Operators.Mul:
                    return "*";
                case Operators.Div:
                    return "/"; // This is now correctly mapped
                default:
                    return "";
            }
        }
    }

    public bool IsValid()
    { 
        if(a is null)
        {
            ErrorMessage = "Podaj parametr a.";
            return false;
        }

        if (b is null)
        {
            ErrorMessage = "Podaj parametr b.";
            return false;
        }

        if (op is null)
        {
            ErrorMessage = "Podaj parametr operacji.";
            return false;
        }

        return true;
    }


    public double Calculate()
    {
        switch (op)
        {
            case Operators.Add:
                return (double)(a + b);
            case Operators.Sub:
                return (double)(a - b);
            case Operators.Mul:
                return (double)(a * b);
            case Operators.Div:
                if(b!=0)
                    return (double)(a / b);
                else
                {
                    return double.NaN;
                }
                    
            default: return double.NaN;
        }
    }

}
    
    
