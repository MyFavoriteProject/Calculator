using System;
using Calculator.BL;
using System.Windows.Forms;

namespace Calculator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CalculatorForm form = new CalculatorForm();
            MessageService service = new MessageService();
            OperationManager manager = new OperationManager();

            MainPresenter presenter = new MainPresenter(form, service, manager);

            Application.Run(form);
        }
    }
}
