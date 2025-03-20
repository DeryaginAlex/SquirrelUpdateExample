using System;
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
        private async void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (var updateManager = await UpdateManager.GitHubUpdateManager(RepoUrl))
                {
                    var updateInfo = await updateManager.CheckForUpdate();
                
                    if (updateInfo.ReleasesToApply.Count > 0)
                    {
                        MessageBox.Show("Обновление найдено! Устанавливаем...", "Обновление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await updateManager.UpdateApp();
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
            VersionValueLabel.Text = version.ToString();
        }

    }
}
