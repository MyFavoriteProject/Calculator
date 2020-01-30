using System;
using Calculator.BL;
namespace Calculator
{
    class MainPresenter
    {
        private readonly ICalculatorForm _view;
        private readonly IOperationManager _manager;
        private readonly IMessageService _messageService;

        public MainPresenter(ICalculatorForm view,  IMessageService messageService, IOperationManager manager)
        {
            _view = view;
            _messageService = messageService;
            _manager = manager;

            _view.EqualClick += _view_EqualClick;
        }

        private void _view_EqualClick(object sender, EventArgs e)
        {
            string[] arrValuesAndSigns=null;
            string number = "";
            int startIndexMove = 0;
            int openBrec = 0;   
            int closeBrec = 0;
            bool canEnterToMultiply = false;
            bool canEnterToPlus = false;

            for (int i = 0; i<_view.Content.Length; i++)
            {
                if (Char.IsDigit(_view.Content[i]) || _view.Content[i] == ',')
                {
                    number += _view.Content[i];
                }
                else
                {
                    if (number != "")
                    {
                        arrValuesAndSigns = AddOneArrCell(arrValuesAndSigns);
                        arrValuesAndSigns[arrValuesAndSigns.Length - 1] = number;
                        number = "";
                    }
                    arrValuesAndSigns = AddOneArrCell(arrValuesAndSigns);
                    arrValuesAndSigns[arrValuesAndSigns.Length - 1] = _view.Content[i].ToString();
                }
                if(i == _view.Content.Length - 1&& number != "")
                {
                    arrValuesAndSigns = AddOneArrCell(arrValuesAndSigns);
                    arrValuesAndSigns[arrValuesAndSigns.Length - 1] = number;
                    number = "";
                }
            }
            for (int i = 0; i < arrValuesAndSigns.Length; i++)
            {
                if (arrValuesAndSigns[i] == "(")
                {
                    openBrec = i;
                    //startIndexMove = i;
                }
                else if (arrValuesAndSigns[i] == ")")
                {
                    closeBrec = i;
                    //startIndexMove = 0;
                    arrValuesAndSigns = MultiplyAndPlusOperation(arrValuesAndSigns, openBrec + 1, closeBrec - 1);
                    arrValuesAndSigns = RemovePartOfArr(arrValuesAndSigns, arrValuesAndSigns[openBrec+1], openBrec, arrValuesAndSigns.Length-1);
                    i = i - 1;
                }
            }

            arrValuesAndSigns = MultiplyAndPlusOperation(arrValuesAndSigns, 0, arrValuesAndSigns.Length - 1);

            _view.Content = arrValuesAndSigns[arrValuesAndSigns.Length - 1];
        }

        private string[] MultiplyAndPlusOperation(string[] valuesAndOperation, int startInx, int finishInx)
        {
            bool canComeInToPluse = false;
            double number = 0;
            for(int i = startInx; i < finishInx; i++)
            {
                if (i >= finishInx)
                    break;
                if (valuesAndOperation[i] == "*")
                {
                    number = _manager.Multiply(Convert.ToDouble(valuesAndOperation[i - 1]), Convert.ToDouble(valuesAndOperation[i + 1]));
                    valuesAndOperation = RemovePartOfArr(valuesAndOperation, number.ToString(), i - 1, i + 1);
                    i = i - 1;
                    finishInx = valuesAndOperation.Length;
                }
                if (valuesAndOperation[i] == "/")
                {
                    try
                    {
                        number = _manager.Divide(Convert.ToDouble(valuesAndOperation[i - 1]), Convert.ToDouble(valuesAndOperation[i + 1]));
                        valuesAndOperation = RemovePartOfArr(valuesAndOperation, number.ToString(), i - 1, i + 1);
                        i = i - 1;
                        finishInx = valuesAndOperation.Length;
                    }
                    catch(Exception ex)
                    {
                        _messageService.ShowError(ex.Message);
                    }
                }
                if(valuesAndOperation[i] == "%")
                {
                    number = _manager.RemainderOfTheDivision(Convert.ToDouble(valuesAndOperation[i - 1]), Convert.ToDouble(valuesAndOperation[i + 1]));
                    valuesAndOperation = RemovePartOfArr(valuesAndOperation, number.ToString(), i - 1, i + 1);
                    i = i - 1;
                    finishInx = valuesAndOperation.Length;
                }

                if(canComeInToPluse == true)
                {
                    if(valuesAndOperation[i] == "+")
                    {
                        number = _manager.Pluse(Convert.ToDouble(valuesAndOperation[i - 1]), Convert.ToDouble(valuesAndOperation[i + 1]));
                        valuesAndOperation = RemovePartOfArr(valuesAndOperation, number.ToString(), i - 1, i + 1);
                        i = i - 1;
                        finishInx = valuesAndOperation.Length;
                    }
                    if (valuesAndOperation[i] == "-")
                    {
                        number = _manager.Minus(Convert.ToDouble(valuesAndOperation[i - 1]), Convert.ToDouble(valuesAndOperation[i + 1]));
                        valuesAndOperation = RemovePartOfArr(valuesAndOperation, number.ToString(), i - 1, i + 1);
                        i = i - 1;
                        finishInx = valuesAndOperation.Length;
                    }
                }
                if (i == finishInx - 1 && canComeInToPluse == false)
                {
                    canComeInToPluse = true;
                    i = startInx;
                }
            }
            return valuesAndOperation;
        }

        private string[] RemovePartOfArr(string[] valuesAndOperation, string value, int startInx, int finishInx)
        {
            int valueRemove = finishInx - startInx;
            string[] arr = new string[valuesAndOperation.Length - valueRemove];

            for(int i = 0; i < arr.Length; i++)
            {
                if (i == startInx)
                    arr[i] = value;
                else if (i > startInx)
                    arr[i] = valuesAndOperation[i + valueRemove];
                else
                    arr[i] = valuesAndOperation[i];
            }
            return arr;
        }

        private string[] AddOneArrCell(string[] valuesAndOperation)
        {
            if(valuesAndOperation == null)
            {
                return new string[1];
            }

            string[] arr = new string[valuesAndOperation.Length + 1];

            for(int i = 0; i < valuesAndOperation.Length; i++)
            {
                arr[i] = valuesAndOperation[i];
            }

            return arr;
        }

        private void _view_ContentChanded(object sender, EventArgs e)
        {
            string content = _view.Content;
        }
    }
}
