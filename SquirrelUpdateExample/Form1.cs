using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Squirrel;

namespace SquirrelUpdateExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static string RepoUrl => "https://github.com/DeryaginAlex/SquirrelUpdateExample";

        /// <summary>
        /// Проверяем есть ли обновление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var updateManager = await UpdateManager.GitHubUpdateManager(RepoUrl))
                {
                    // Проверка наличия обновлений
                    var updateInfo = await updateManager.CheckForUpdate();
                
                    if (updateInfo.ReleasesToApply.Count > 0)
                    {
                        // Если есть обновления, скачиваем и устанавливаем их
                        MessageBox.Show("Обновление найдено! Устанавливаем...", "Обновление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await updateManager.UpdateApp();

                        // Перезапуск приложения после обновления
                        MessageBox.Show("Приложение будет перезапущено для применения обновления.", "Обновление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UpdateManager.RestartApp();
                    }
                    else
                    {
                        MessageBox.Show("У вас установлена последняя версия приложения.", "Обновление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Version version = assembly.GetName().Version;
            label3.Text = version.ToString();
        }
    }
}
