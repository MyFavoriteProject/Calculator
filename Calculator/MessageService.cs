using System.Windows.Forms;

namespace Calculator
{
    public interface IMessageService
    {
        void ShowMessage(string message);
        void ShowExlamation(string exlamation);
        void ShowError(string error);
    }
    class MessageService:IMessageService
    {
        public void ShowError(string error)
        {
            MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowExlamation(string exlamation)
        {
            MessageBox.Show(exlamation, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
