using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public interface ICalculatorForm
    {
        string Content { get; set; }

        event EventHandler EqualClick;
    }

    public partial class CalculatorForm : Form, ICalculatorForm
    {
        private string _valuesAndOperations = "";

        private bool _bracketOpen = true;

        public string Content
        {
            get {return fldContent.Text; }
            set {fldContent.Text = value; }
        }

        public event EventHandler EqualClick;

        public CalculatorForm()
        {
            InitializeComponent();

            #region Numbers
            butNumberZero.Click += ButNumberZero_Click;
            butNumberOne.Click += ButNumberOne_Click;
            butNumberTwo.Click += ButNumberTwo_Click;
            butNumberThree.Click += ButNumberThree_Click;
            butNumberFour.Click += ButNumberFour_Click;
            butNumberFive.Click += ButNumberFive_Click;
            butNumberSix.Click += ButNumberSix_Click;
            butNumberSeven.Click += ButNumberSeven_Click;
            butNumberEight.Click += ButNumberEight_Click;
            butNumberNine.Click += ButNumberNine_Click;
            butDot.Click += ButDot_Click;
            #endregion

            #region Operation
            butResidueFromTheDivision.Click += ButResidueFromTheDivision_Click;
            butDivide.Click += ButDivide_Click;
            butMultiply.Click += ButMultiply_Click;
            butMinus.Click += ButMinus_Click;
            butPlus.Click += ButPlus_Click;
            butHooks.Click += ButHooks_Click;
            butEqual.Click += ButEqual_Click;
            butDeleteLast.Click += ButDeleteLast_Click;   
            #endregion

            butClean.Click += ButClean_Click;
        }

        private void ButDeleteLast_Click(object sender, EventArgs e)
        {
            if (_valuesAndOperations == "")
                return;
            if (_valuesAndOperations.Length - 1 == ')')
                _bracketOpen = false;
            if (_valuesAndOperations.Length - 1 == '(')
                _bracketOpen = true;

            int lastSymbIndx = _valuesAndOperations.Length - 1;
            _valuesAndOperations = _valuesAndOperations.Remove(lastSymbIndx);

            SetValuesAndOperationInContent();
        }

        private void ButClean_Click(object sender, EventArgs e)
        {
            _valuesAndOperations = "";
            _bracketOpen = true;
            SetValuesAndOperationInContent();
        }

        #region Operation
        private void ButEqual_Click(object sender, EventArgs e)
        {
            if (_bracketOpen == false)
            {
                if (_valuesAndOperations[_valuesAndOperations.Length - 1] == '(')
                {
                    int lastSymbIndx = _valuesAndOperations.Length - 1;
                    _valuesAndOperations = _valuesAndOperations.Remove(lastSymbIndx);

                    SetValuesAndOperationInContent();
                }

                else
                    _valuesAndOperations += ")";
            }
                
            _bracketOpen = true;
            if (Char.IsDigit(_valuesAndOperations[_valuesAndOperations.Length - 1]) != true && _valuesAndOperations[_valuesAndOperations.Length - 1] != ')')
                _valuesAndOperations += "0";

            Content = _valuesAndOperations;

            if (EqualClick != null) EqualClick(this, EventArgs.Empty);

            _valuesAndOperations = Content;
        }

        private void ButHooks_Click(object sender, EventArgs e)
        {
            if (_bracketOpen == true)
            {
                if (_valuesAndOperations == "")
                {
                    _valuesAndOperations += "(";
                    _bracketOpen = false;
                }
                else if(Char.IsDigit(_valuesAndOperations[_valuesAndOperations.Length - 1]) == false)
                {
                    _valuesAndOperations += "(";
                    _bracketOpen = false;
                }
            }
            else if(_bracketOpen == false && Char.IsDigit(_valuesAndOperations[_valuesAndOperations.Length - 1]) ==true)
            {
                _valuesAndOperations += ")";
                _bracketOpen = true;
            }
            SetValuesAndOperationInContent();
        }

        private void ButPlus_Click(object sender, EventArgs e)
        {
            if (_valuesAndOperations == "") { }

            else if (Char.IsDigit(_valuesAndOperations[_valuesAndOperations.Length - 1]) == true)
                _valuesAndOperations += "+";

            else if (_valuesAndOperations[_valuesAndOperations.Length - 1] != '(' && _valuesAndOperations[_valuesAndOperations.Length - 1] == ')')
                _valuesAndOperations += "+";
            else 
            {
                _valuesAndOperations = GetChangeSign(_valuesAndOperations, '+');
            }
            SetValuesAndOperationInContent();
        }

        private void ButMinus_Click(object sender, EventArgs e)
        {
            if (Char.IsDigit(_valuesAndOperations[_valuesAndOperations.Length - 1]) == true)
                _valuesAndOperations += "-";

            else
            {
                _valuesAndOperations = GetChangeSign(_valuesAndOperations, '-');
            }
            SetValuesAndOperationInContent();
        }

        private void ButMultiply_Click(object sender, EventArgs e)
        {
            if (_valuesAndOperations == "")
                return;
            else if (Char.IsDigit(_valuesAndOperations[_valuesAndOperations.Length - 1]) == true)
                _valuesAndOperations += "*";

            else
            {
                _valuesAndOperations = GetChangeSign(_valuesAndOperations, '*');
            }
            SetValuesAndOperationInContent();
        }

        private void ButDivide_Click(object sender, EventArgs e)
        {
            if (_valuesAndOperations == "")
                return;
            else if (Char.IsDigit(_valuesAndOperations[_valuesAndOperations.Length - 1]) == true)
                _valuesAndOperations += "/";
            else
            {
                _valuesAndOperations = GetChangeSign(_valuesAndOperations, '/');
            }
            SetValuesAndOperationInContent();
        }

        private void ButResidueFromTheDivision_Click(object sender, EventArgs e)
        {
            if (_valuesAndOperations == "")
                return;
            else if (Char.IsDigit(_valuesAndOperations[_valuesAndOperations.Length - 1]) == true)
                _valuesAndOperations += "%";
            else
            {
                _valuesAndOperations = GetChangeSign(_valuesAndOperations, '%');
            }
            SetValuesAndOperationInContent();
        }
        #endregion

        #region Numbers Method
        private void ButDot_Click(object sender, EventArgs e)
        {
            if (_valuesAndOperations == "")
                _valuesAndOperations += "0.";
            else if(Char.IsDigit(_valuesAndOperations[_valuesAndOperations.Length - 1]) == true)
                _valuesAndOperations += ",";

            SetValuesAndOperationInContent();
        }
        private void ButNumberNine_Click(object sender, EventArgs e)
        {
            _valuesAndOperations += "9";
            SetValuesAndOperationInContent();
        }

        private void ButNumberEight_Click(object sender, EventArgs e)
        {
            _valuesAndOperations += "8";
            SetValuesAndOperationInContent();
        }

        private void ButNumberSeven_Click(object sender, EventArgs e)
        {
            _valuesAndOperations += "7";
            SetValuesAndOperationInContent();
        }

        private void ButNumberSix_Click(object sender, EventArgs e)
        {
            _valuesAndOperations += "6";
            SetValuesAndOperationInContent();
        }

        private void ButNumberFive_Click(object sender, EventArgs e)
        {
            _valuesAndOperations += "5";
            SetValuesAndOperationInContent();
        }

        private void ButNumberFour_Click(object sender, EventArgs e)
        {
            _valuesAndOperations += "4";
            SetValuesAndOperationInContent();
        }

        private void ButNumberThree_Click(object sender, EventArgs e)
        {
            _valuesAndOperations += "3";
            SetValuesAndOperationInContent();
        }

        private void ButNumberTwo_Click(object sender, EventArgs e)
        {
            _valuesAndOperations += "2";
            SetValuesAndOperationInContent();
        }

        private void ButNumberOne_Click(object sender, EventArgs e)
        {
            _valuesAndOperations += "1";
            SetValuesAndOperationInContent();
        }

        private void ButNumberZero_Click(object sender, EventArgs e)
        {
            _valuesAndOperations += "0";
            SetValuesAndOperationInContent();
        }
        #endregion

        #region AddLogic
        private void SetValuesAndOperationInContent()
        {
            Content = _valuesAndOperations;
        }

        private string GetChangeSign(string valuesAndSymbol, char sign)
        {
            char[] ch = valuesAndSymbol.ToCharArray();
            ch[ch.Length - 1] = sign;
            valuesAndSymbol = new string(ch);
            return valuesAndSymbol;
        }

        #endregion
    }
}
