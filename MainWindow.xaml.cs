using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace MoneyOrganyzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string moneyx = "0"; // деньги
        private string moneyNew = "0";
        private string moneyOld = "0";
            
        public MainWindow()
        {
            InitializeComponent();
        }

        async private void ReturnSave(object sender, RoutedEventArgs e)
        {
            using (FileStream stream = new FileStream("save.txt", FileMode.OpenOrCreate))
            {   
                byte[] buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer, 0, buffer.Length);

                if (Encoding.Default.GetString(buffer).Replace(" ", "") != "")
                {
                    moneyx = Encoding.Default.GetString(buffer);
                    CurrentMoney.Text = $"Ваш счет: {moneyx} рублей.";
                }
                else
                    MessageBox.Show("Ошибка! Файл сохранения испорчен.");
            }

            using (FileStream stream = new FileStream("WastedSave.txt", FileMode.OpenOrCreate))
            {
                byte[] buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer, 0, buffer.Length);

                if (Encoding.Default.GetString(buffer) != "")
                {
                    moneyOld = Encoding.Default.GetString(buffer);
                    wastedMoney.Text = $"Потрачено: {moneyOld} рублей.";
                }
                else
                    MessageBox.Show("Ошибка! Файл сохранения испорчен.");
            }

            using (FileStream stream = new FileStream("RewardSave.txt", FileMode.OpenOrCreate))
            {
                byte[] buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer, 0, buffer.Length);

                if (Encoding.Default.GetString(buffer) != "")
                {
                    moneyNew = Encoding.Default.GetString(buffer);
                    rewardMoney.Text = $"Заработано: {moneyNew} рублей.";
                }
                else
                    MessageBox.Show("Ошибка! Файл сохранения испорчен.");
            }
        }

        async private void Save(object sender, RoutedEventArgs e)
        {
            using (FileStream stream = new FileStream("save.txt", FileMode.OpenOrCreate))
            {
                byte[] buffer = Encoding.Default.GetBytes(moneyx);

                if (moneyx != "")
                    await stream.WriteAsync(buffer, 0, buffer.Length);
                else
                    await stream.WriteAsync(Encoding.Default.GetBytes("0"), 0, Encoding.Default.GetBytes("0").Length);
                
            }

            using (FileStream stream = new FileStream("RewardSave.txt", FileMode.OpenOrCreate))
            {
                byte[] buffer = Encoding.Default.GetBytes(moneyNew);

                if (moneyx != "")
                    await stream.WriteAsync(buffer, 0, buffer.Length);
                else
                    await stream.WriteAsync(Encoding.Default.GetBytes("0"), 0, Encoding.Default.GetBytes("0").Length);

            }

            using (FileStream stream = new FileStream("WastedSave.txt", FileMode.OpenOrCreate))
            {
                byte[] buffer = Encoding.Default.GetBytes(moneyOld);

                if (moneyx != "")
                    await stream.WriteAsync(buffer, 0, buffer.Length);
                else
                    await stream.WriteAsync(Encoding.Default.GetBytes("0"), 0, Encoding.Default.GetBytes("0").Length);

                MessageBox.Show("Успех!");
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            moneyx = (Int32.Parse(moneyx) + Int32.Parse(newMoney.Text)).ToString();
            CurrentMoney.Text = $"Ваш счет: {moneyx} рублей.";
            moneyNew = (Int32.Parse(moneyNew) + Int32.Parse(newMoney.Text)).ToString();
            rewardMoney.Text = $"Заработано: {moneyNew} рублей.";
            newMoney.Text = "";
        }

        private void Del(object sender, RoutedEventArgs e)
        {
            moneyx = (Int32.Parse(moneyx) - Int32.Parse(delMoney.Text)).ToString();
            CurrentMoney.Text = $"Ваш счет: {moneyx} рублей.";
            moneyOld = (Int32.Parse(moneyOld) + Int32.Parse(delMoney.Text)).ToString();
            wastedMoney.Text = $"Потрачено: {moneyOld} рублей.";
            delMoney.Text = "";
        }

        private void TutorialShow(object sender, RoutedEventArgs e)
        {
            Setting setWin  = new Setting();
            setWin.Show();
        }
    }
}
